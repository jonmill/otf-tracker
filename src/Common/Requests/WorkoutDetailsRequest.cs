using System;
using Newtonsoft.Json;

namespace OtfTracker.Common.Requests
{
    public class WorkoutDetailsRequest
    {
        [JsonProperty("ClassHistoryUUId")]
        public string ClassHistoryUuid { get; set; }

        [JsonProperty("MemberUUId")]
        public string MemberUuid { get; set; }
    }
}
