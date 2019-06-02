using System;

namespace OtfTracker.Website.Models
{
    public class SummaryViewModel
    {
        public DateTime ClassTime { get; set; }

        public int CaloriesBurned { get; set; }

        public int SplatPoints { get; set; }

        public string ClassHistoryUuid { get; set; }

        public string Coach { get; set; }

        public string ClassType { get; set; }

        public int ActiveTime { get; set; }

        public string StudioNumber { get; set; }

        public string StudioName { get; set; }
    }
}
