using System;

namespace OtfTracker.Common.Models
{
    public class HeartRateData
    {
        public DateTime ClassTime { get; set; }
        public string ClassType { get; set; }
        public int BlackZone { get; set; }
        public int BlueZone { get; set; }
        public int GreenZone { get; set; }
        public int OrangeZone { get; set; }
        public int RedZone { get; set; }
        public int Calories { get; set; }
        public int SplatPoints { get; set; }
        public int AverageHeartRate { get; set; }
        public int AverageHeartRatePercent { get; set; }
        public int MaxHeartRate { get; set; }
        public int MaxPercentHr { get; set; }
        public int StepCount { get; set; }
    }
}
