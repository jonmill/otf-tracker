using System;
using Newtonsoft.Json;
using OtfTracker.Common.Models;

namespace OtfTracker.Common.Requests
{
    public class LifetimeStatsRequest
    {
        [JsonProperty("MemberUUId")]
        public string MemberUuid { get; set; }

        [JsonProperty("SelectTime")]
        public DateTime AsOfDate { get; set; }
    }
}
