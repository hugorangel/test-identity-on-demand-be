using System;
using System.Threading.Tasks;
using test_identity_on_demand_be.Model;

namespace test_identity_on_demand_be.Interfaces
{
    public interface ISampleService
    {
        Task<TokenDto> GetBearerTokenAsync();
        Task<SessionDto> GetSessionInfoAsync(SessionDc sessionData, string Authorization);
        Task<SessionInfoDto> GetUserInfoAsync(string sessionId, string Authorization);
    }
}
