using System;
namespace test_identity_on_demand_be.Model
{
    public class SessionInfoDto
    {
        public string id { get; set; }
        public string url { get; set; }
        public SessionInfoIdentityDto identity { get; set; }

    }
}
