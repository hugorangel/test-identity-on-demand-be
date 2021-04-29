using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using test_identity_on_demand_be.Interfaces;
using test_identity_on_demand_be.Model;

namespace test_identity_on_demand_be.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private ISampleService _sampleService;

        public AuthenticationController(ISampleService sampleService)
        {
            _sampleService = sampleService;
        } 
        
        [HttpGet, Route("api/v1/authentication/token")]
        [Description("Getting the token (1st step)")]
        public async Task<TokenDto> GetInitialToken()
        {
            var tokenObject = await _sampleService.GetBearerTokenAsync();
            return tokenObject;
        }

        [HttpPost, Route("api/v1/authentication/session")]
        [Description("Getting the session id (2nd step)")]
        public async Task<SessionDto> GetSessionInfo([FromBody] SessionDc sessionData, [FromHeader] string Authorization)
        {
            var sessionObject = await _sampleService.GetSessionInfoAsync(sessionData, Authorization);
            return sessionObject;
        }

        [HttpGet, Route("api/v1/authentication/session/{sessionId}")]
        [Description("Getting user info (3rd step)")]
        public async Task<SessionInfoDto> GetUserInfo(string sessionId, [FromHeader] string Authorization)
        {
            var userInfo = await _sampleService.GetUserInfoAsync(sessionId, Authorization);
            return userInfo;
        }
    }
}
