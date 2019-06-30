using System;
using Newtonsoft.Json;

namespace OtfTracker.Common.Models
{
    public class ClassSummary
    {
        [JsonProperty("classTime")]
        public DateTime ClassTime { get; set; }

        [JsonProperty("caloriesBurned")]
        public int CaloriesBurned { get; set; }

        [JsonProperty("splatPoints")]
        public int SplatPoints { get; set; }

        public Guid ClassHistoryUuid { get; set; }

        public string Coach { get; set; }

        public string ClassType { get; set; }

        public int ActiveTime { get; set; }

        public string StudioNumber { get; set; }

        public string StudioName { get; set; }
    }
}
