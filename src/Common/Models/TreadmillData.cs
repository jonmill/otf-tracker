namespace OtfTracker.Common.Models
{
    public class TreadmillData
    {
        public DecimalDataUnit AvgSpeed { get; set; }
        public DecimalDataUnit MaxSpeed { get; set; }
        public DecimalDataUnit AvgIncline { get; set; }
        public DecimalDataUnit MaxIncline { get; set; }
        public TimeDataUnit AvgPace { get; set; }
        public TimeDataUnit MaxPace { get; set; }
        public DecimalDataUnit TotalDistance { get; set; }
        public string MovingTime { get; set; }
        public DecimalDataUnit ElevationGained { get; set; }
    }
}
