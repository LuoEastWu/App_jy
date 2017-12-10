using System;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Models
{
    public partial class Version
    {
        [JsonProperty("EJEAndroidPDA")]
        public List<EJEAndroidPDA> EJEAndroidPDA { get; set; }

        [JsonProperty("State")]
        public bool State { get; set; }
    }
    public partial class EJEAndroidPDA
    {
        [JsonProperty("geturl")]
        public string Geturl { get; set; }

        [JsonProperty("Rem")]
        public string Rem { get; set; }

        [JsonProperty("vosoin")]
        public string Vosoin { get; set; }
    }
}
