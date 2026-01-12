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
            
            var user = await _userRepo.GetUserByIdAsync(request.UserId);
            if (user is null) throw new NotFoundException("User Not Found");

            var claims = FillClaims(user);
            
            if(!ValidPassword(request.Password!, user))
                throw new ValidationException("Wrong Password , Try Again");

            var descriptor = BuildDescriptor(claims);

            var refreshToken = await BuildRefreshToken(user);
            
            var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.Value.TokenExpirationInMinutes);
            
            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(descriptor);

            return new JwtTokenResponse
            {
                AccessToken = tokenHandler.WriteToken(securityToken),
                RefreshToken = refreshToken,
                Expires = expires
            };
        }

        public async Task<JwtTokenResponse> GenerateAccessTokenFromRefreshToken(RefreshTokenRequest refreshTokenRequest)
        {

            var refreshTokenHash = HashString(refreshTokenRequest.RefreshToken!);

            var refreshToken = await _refreshTokenRepo.GetRefreshTokenAsync(refreshTokenHash);

            ValidateRefreshToken(refreshToken);

            // we issue a new token response  (access + refresh)
            var user = await _userRepo.GetUserByIdAsync(refreshToken.UserId);

            if (user is null)
                throw new NotFoundException($"No user With Id {refreshToken.UserId} Found");

            var jwtRequest = FillJwtRequest(user);

            var token = GenerateAccessToken(jwtRequest);

            return await token;
        }

        // Helper Methods --------------------------------------
        public string HashString(string randomRefreshToken)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(randomRefreshToken);
            byte[] hashBytes = SHA256.HashData(bytes);

            return Convert.ToHexString(hashBytes);
        }
        private string GenerateRandomRefreshToken()
        {
            var random = RandomNumberGenerator.GetBytes(32);

            return Convert.ToBase64String(random);
        }
        
        private List<Claim> FillClaims(User user)
        {
            var claims = new List<Claim>()
            {
                new (JwtRegisteredClaimNames.Sub , user.UserId.ToString()),
                new ("given_name" , user.FirstName),
                new ("LastName" , user.LastName),
                new (JwtRegisteredClaimNames.Email , user.Email)
            };

            foreach(var role in user.Roles)
                claims.Add(new(ClaimTypes.Role, role));

            foreach (var permission in user.Permissions)
                claims.Add(new("permission", permission));

            return claims;
        }
        private bool ValidPassword(string password , User user)
        {
            var requestPasswordHash1 = HashRefreshToken(password);
            var requestPasswordHash2 = HashRefreshToken(user.PasswordHash); // LMAO

            if (requestPasswordHash2 != requestPasswordHash1)
                return false;

            return true;
        }
        private SecurityTokenDescriptor BuildDescriptor(List<Claim> claims)
        {
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

            return descriptor;
        }
        private async Task<string> BuildRefreshToken(User user)
        {
            var randomRefreshToken = GenerateRandomRefreshToken();

            var hashedRefreshToken = HashRefreshToken(randomRefreshToken);

            user.RefreshToken = randomRefreshToken;

            var refreshToken = new RefreshTokenModel()
            {
                RefreshTokenHash = hashedRefreshToken,
                UserId = user.UserId,
                Expires = DateTime.UtcNow.AddHours(12)
            };

            await _refreshTokenRepo.DeleteRefreshTokenAsync(user.UserId);
            await _refreshTokenRepo.AddRefreshTokenAsync(refreshToken);

            return randomRefreshToken;
        }
        private void ValidateRefreshToken(RefreshTokenModel refreshToken)
        {
            if (refreshToken is null)
                throw new ValidationException("Refresh is not Valid");

            if (refreshToken.Expires < DateTime.UtcNow)
                throw new ValidationException("Refresh is Expired");
        }
        private JwtTokenRequest FillJwtRequest(User user)
        {
            return new JwtTokenRequest()
                {
                    UserId = user!.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.PasswordHash,

                };
        }
    } 
}
