using ClinicAPI.Requests;
using ClinicAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAPI.Controllers
{
    [ApiController]
    [Route("tokens")]
    public class TokensController(IJwtTokenService _jwtService) : ControllerBase
    {
        [HttpPost("generate")]
        public async Task<IActionResult> GetToken(JwtTokenRequest request)
        {
            var token = await _jwtService.GenerateAccessToken(request);
            return Ok(token);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> GetTokenFromRefreshToken([FromBody] RefreshTokenRequest request)
        {
            var token = await _jwtService.GenerateAccessTokenFromRefreshToken(request);
            return Ok(token);
        }
    }
}
