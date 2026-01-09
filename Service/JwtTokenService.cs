using ClinicAPI.Configurations;
using ClinicAPI.CustomExceptions;
using ClinicAPI.Enums;
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
        private readonly IPatientRepo _patientRepo;
        private readonly IRefreshTokenRepo _refreshTokenRepo;
        public JwtTokenService(IOptions<JwtSettings> jwtSettings , IPatientRepo patientRepo, IRefreshTokenRepo refreshTokenRepo)
        {
            _jwtSettings = jwtSettings;
            _patientRepo = patientRepo;
            _refreshTokenRepo = refreshTokenRepo;
        }
        public async Task<JwtTokenResponse> GenerateAccessToken(JwtTokenRequest request , string type)
        {
            var claims = new List<Claim>()
            {
                new (JwtRegisteredClaimNames.Sub , request.UserId.ToString()),
                new ("given_name" , request.FirstName),
                new ("LastName" , request.LastName),
                new (JwtRegisteredClaimNames.Email , request.Email),
            };

            var patient = await _patientRepo.GetPatientByIdAsync(request.UserId);

            if (patient is null) throw new NotFoundException("Patient Not Found");

            // Lets say we have a hash algo
            if (patient.PasswordHash != request.Password)
                throw new ValidationException("Wrong Password !");

            foreach(var role in patient.Roles)
                claims.Add(new(ClaimTypes.Role, role));

            foreach (var permission in patient.Permissions)
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

            var refreshToken = new RefreshTokenModel()
            {
                RefreshTokenHash = "RefreshToken",
                UserId = request.UserId,
                Expires = DateTime.UtcNow.AddMinutes(2)
            };
            await _refreshTokenRepo.DeleteRefreshTokenAsync(request.UserId);
            await _refreshTokenRepo.AddRefreshTokenAsync(refreshToken);

            return new JwtTokenResponse
            {
                AccessToken = tokenHandler.WriteToken(securityToken),
                RefreshToken = "RefreshToken",
                Expires = expires
            };
        }

        public async Task<JwtTokenResponse> GenerateAccessTokenFromRefreshToken(RefreshTokenRequest request, int userId)
        {
            // if valid refresh Token
            var refreshToken = await _refreshTokenRepo.GetRefreshTokenAsync(request.RefreshToken!);

            if (refreshToken is null || refreshToken.UserId != userId || refreshToken.RefreshTokenHash != request.RefreshToken)
                throw new ValidationException("Refresh is not Valid");

            if (refreshToken.Expires < DateTime.UtcNow)
                throw new ValidationException("Refresh is Expired");

            // we issue a new token response  (access + refresh)

            var user = await _patientRepo.GetPatientByIdAsync(userId);

            var jwtRequest = new JwtTokenRequest()
            {
                UserId = user.PatientId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Password = user.PasswordHash,

            };

            var token = GenerateAccessToken(jwtRequest , "patient");

            return await token;
        }
    }
}
