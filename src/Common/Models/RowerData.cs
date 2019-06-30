namespace OtfTracker.Common.Models
{
    public class RowerData
    {
        public DecimalDataUnit AvgPower { get; set; }
        public DecimalDataUnit MaxPower { get; set; }
        public DecimalDataUnit AvgSpeed { get; set; }
        public DecimalDataUnit MaxSpeed { get; set; }
        public TimeDataUnit AvgPace { get; set; }
        public TimeDataUnit MaxPace { get; set; }
        public DecimalDataUnit AvgCadence { get; set; }
        public DecimalDataUnit MaxCadence { get; set; }
        public DecimalDataUnit TotalDistance { get; set; }
        public string MovingTime { get; set; }
    }
}
