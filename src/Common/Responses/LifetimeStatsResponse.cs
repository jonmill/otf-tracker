using OtfTracker.Common.Models;

namespace OtfTracker.Common.Responses
{
    public class LifetimeStatsResponse
    {
        public Units Unit { get; set; }
        public PersonalStats LastYear { get; set; }
        public PersonalStats ThisYear { get; set; }
        public PersonalStats LastMonth { get; set; }
        public PersonalStats ThisMonth { get; set; }
        public PersonalStats LastWeek { get; set; }
        public PersonalStats ThisWeek { get; set; }
        public PersonalStats AllTime { get; set; }
    }
}
