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
            var token = _jwtService.GenerateAccessToken(request , "patient");
            return Ok(token);
        }

        [HttpPost("refresh-token/{userId:int}")]
        public async Task<IActionResult> GetTokenFromRefreshToken([FromBody] RefreshTokenRequest request , int userId)
        {
            var token = _jwtService.GenerateAccessTokenFromRefreshToken(request , userId);
            return Ok(token);
        }
    }
}
