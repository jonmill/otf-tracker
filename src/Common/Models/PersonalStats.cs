namespace OtfTracker.Common.Models
{
    public class PersonalStats
    {
        public decimal Calories { get; set; }
        public decimal SplatPoint { get; set; }
        public decimal TreadmillDistance { get; set; }
        public decimal TreadmillElevationGained { get; set; }
        public decimal RowerDistance { get; set; }
        public decimal RowerWatt { get; set; }
        public int TotalBlackZone { get; set; }
        public int TotalGreenZone { get; set; }
        public int TotalBlueZone { get; set; }
        public int TotalOrangeZone { get; set; }
        public int TotalRedZone { get; set; }
        public int StepCount { get; set; }
        public int WorkoutDuration { get; set; }
    }
}
