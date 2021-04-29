using System;
namespace test_identity_on_demand_be.Model
{
    public class SessionInfoIdentityDto
    {
        public string providerId { get; set; }
        public string fullName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dateOfBirth { get; set; }
    }
}
