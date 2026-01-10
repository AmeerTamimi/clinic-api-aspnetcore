using ClinicAPI.Configurations;
using ClinicAPI.CustomExceptions;
using System.Security.Cryptography;
using ClinicAPI.Models;
using ClinicAPI.Repositories;
using ClinicAPI.Requests;
using ClinicAPI.Responses;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



// wanna make refresh token repo , that ahs the method that will get the refresh token !!!!!!
// and saves also !!!

namespace ClinicAPI.Service
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly UserRepo _userRepo;
        private readonly IRefreshTokenRepo _refreshTokenRepo;
        public JwtTokenService(IOptions<JwtSettings> jwtSettings , UserRepo userRepo, IRefreshTokenRepo refreshTokenRepo)
        {
            _jwtSettings = jwtSettings;
            _userRepo = userRepo;
            _refreshTokenRepo = refreshTokenRepo;
        }
        public async Task<JwtTokenResponse> GenerateAccessToken(JwtTokenRequest request)
        {
            var claims = new List<Claim>()
            {
                new (JwtRegisteredClaimNames.Sub , request.UserId.ToString()),
                new ("given_name" , request.FirstName),
                new ("LastName" , request.LastName),
                new (JwtRegisteredClaimNames.Email , request.Email),
            };

            var user = await _userRepo.GetUserByIdAsync(request.UserId);

            if (user is null) throw new NotFoundException("User Not Found");

            var requestPasswordHash1 = HashRefreshToken(request.Password!);
            var requestPasswordHash2 = HashRefreshToken(user.PasswordHash); // LMAO

            if (requestPasswordHash2 != requestPasswordHash1)
                throw new ValidationException("Wrong Password , Try Again");

            foreach(var role in user.Roles)
                claims.Add(new(ClaimTypes.Role, role));

            foreach (var permission in user.Permissions)
                claims.Add(new("permission", permission));

            var settings = _jwtSettings.Value;
            var issuer = settings.Issuer;
            var audience = settings.Audience;
            var key = settings.SecretKey;
            var expires = DateTime.UtcNow.AddMinutes(settings.TokenExpirationInMinutes);

            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = issuer,
                Audience = audience,
                Expires = expires,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(descriptor);

            var randomRefreshToken = GenerateRandomRefreshToken();

            var hashedRefreshToken = HashRefreshToken(randomRefreshToken);

            user.RefreshToken = randomRefreshToken;

            var refreshToken = new RefreshTokenModel()
            {
                RefreshTokenHash = hashedRefreshToken,
                UserId = request.UserId,
                Expires = DateTime.UtcNow.AddHours(12)
            };            

            await _refreshTokenRepo.DeleteRefreshTokenAsync(user.UserId);            
            await _refreshTokenRepo.AddRefreshTokenAsync(refreshToken);

            return new JwtTokenResponse
            {
                AccessToken = tokenHandler.WriteToken(securityToken),
                RefreshToken = randomRefreshToken,
                Expires = expires
            };
        }

        public async Task<JwtTokenResponse> GenerateAccessTokenFromRefreshToken(RefreshTokenRequest request)
        {
            // if valid refresh Token
            var refreshTokenHash = HashRefreshToken(request.RefreshToken);
            var refreshToken = await _refreshTokenRepo.GetRefreshTokenAsync(refreshTokenHash);

            if (refreshToken is null)
                throw new ValidationException("Refresh is not Valid");

            if (refreshToken.Expires < DateTime.UtcNow)
                throw new ValidationException("Refresh is Expired");

            // we issue a new token response  (access + refresh)

            var user = await _userRepo.GetUserByIdAsync(refreshToken.UserId);

            var jwtRequest = new JwtTokenRequest()
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Password = user.PasswordHash,

            };

            var token = GenerateAccessToken(jwtRequest);

            return await token;
        }

        public string GenerateRandomRefreshToken()
        {
            var random = RandomNumberGenerator.GetBytes(32);

            return Convert.ToBase64String(random);
        }

        public string HashRefreshToken(string randomRefreshToken)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(randomRefreshToken);
            byte[] hashBytes = SHA256.HashData(bytes);

            return Convert.ToHexString(hashBytes);
        }
    } 
}
