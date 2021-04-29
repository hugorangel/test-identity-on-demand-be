using System;
namespace test_identity_on_demand_be.Model
{
    public class TokenDto
    {
        public string access_token { get; set; }
        public double expires_in { get; set; }
        public string token_type { get; set; }
        public string scope { get; set; }
    }
}
