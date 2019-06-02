using System.Collections.Generic;
using Newtonsoft.Json;
using OtfTracker.Common.Models;

namespace OtfTracker.Common.Responses
{
    public class ClassSummariesResponse
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("data")]
        public IList<ClassSummary> Data { get; set; }
    }
}
