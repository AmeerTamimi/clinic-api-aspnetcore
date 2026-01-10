using ClinicAPI.Requests;
using ClinicAPI.Responses;

namespace ClinicAPI.Service
{
    public interface IJwtTokenService
    {
        Task<JwtTokenResponse> GenerateAccessToken(JwtTokenRequest jwtRequest);
        Task<JwtTokenResponse> GenerateAccessTokenFromRefreshToken(RefreshTokenRequest refreshTokenReuqest);
    }
}
