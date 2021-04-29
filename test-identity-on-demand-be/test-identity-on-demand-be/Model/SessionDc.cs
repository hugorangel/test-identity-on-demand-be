using System;
using System.Collections.Generic;

namespace test_identity_on_demand_be.Model
{
    public class SessionDc
    {
        public string flow { get; set; }
        public string[] allowedProviders { get; set; }        
        public string[] include { get; set; }
        public Dictionary<string, string> redirectSettings { get; set; }
    }
}
