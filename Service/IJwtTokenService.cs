using ClinicAPI.Requests;
using ClinicAPI.Responses;

namespace ClinicAPI.Service
{
    public interface IJwtTokenService
    {
        Task<JwtTokenResponse> GenerateAccessToken(JwtTokenRequest jwtRequest , string type);
        Task<JwtTokenResponse> GenerateAccessTokenFromRefreshToken(RefreshTokenRequest jwtRequest , int userId);

    }
}
