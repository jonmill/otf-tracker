using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtfTracker.Common;
using OtfTracker.Common.Models;
using OtfTracker.Website.Helpers;
using OtfTracker.Website.Identity;

namespace Website.Pages
{
    [Authorize]
    public class TrendsModel : PageModel
    {
        private readonly OtfApi _api;

        [BindProperty]
        public string ClassDates { get; set; }

        [BindProperty]
        public string SplatPoints { get; set; }

        [BindProperty]
        public string RedZonePoints { get; set; }

        [BindProperty]
        public string OrangeZonePoints { get; set; }

        [BindProperty]
        public string GreenZonePoints { get; set; }

        [BindProperty]
        public string BlueZonePoints { get; set; }

        [BindProperty]
        public string GreyZonePoints { get; set; }

        [BindProperty]
        public int ClassesPerMonth { get; set; }

        [BindProperty]
        public int ClassesTotal { get; set; }

        [BindProperty]
        public int ClassesThisYear { get; set; }

        [BindProperty]
        public string PerMonthLabels { get; set; }

        [BindProperty]
        public string LastClassesLabels { get; set; }

        [BindProperty]
        public string AverageHeartRateLastClasses { get; set; }

        [BindProperty]
        public string AverageHeartRatePerMonth { get; set; }

        [BindProperty]
        public string MaxHeartRateLastClasses { get; set; }

        [BindProperty]
        public string MaxHeartRatePerMonth { get; set; }

        public TrendsModel(OtfApi api)
        {
            _api = api;
        }

        public async Task<IActionResult> OnGet()
        {
            OtfUser otfUser = HttpContext.GetSignedInOtfUser();
            IEnumerable<ClassSummary> summaries = await _api.GetClassSummariesAsync(otfUser.MemberId, otfUser.SignInJwt);
            summaries = summaries.OrderByDescending(s => s.ClassTime);

            List<KeyValuePair<ClassSummary, ClassDetails>> details = new List<KeyValuePair<ClassSummary, ClassDetails>>();
            foreach (ClassSummary summary in summaries)
            {
                ClassDetails detail = await _api.GetClassDetailsAsync(summary.ClassHistoryUuid.ToString(), otfUser.MemberId, otfUser.SignInJwt);
                details.Add(new KeyValuePair<ClassSummary, ClassDetails>(summary, detail));
            }

            GeneratePointsTrend(details);
            GenerateClassesTrend(summaries);
            GenerateHeartRateTrend(details);

            return Page();
        }

        private void GeneratePointsTrend(List<KeyValuePair<ClassSummary, ClassDetails>> details)
        {
            List<DateTime> classDates = new List<DateTime>();
            List<int> splatPoints = new List<int>();
            List<int> redZonePoints = new List<int>();
            List<int> orangeZonePoints = new List<int>();
            List<int> greenZonePoints = new List<int>();
            List<int> blueZonePoints = new List<int>();
            List<int> greyZonePoints = new List<int>();

            foreach (KeyValuePair<ClassSummary, ClassDetails> detail in details.Take(10))
            {
                classDates.Add(detail.Key.ClassTime);
                splatPoints.Add(detail.Value.HeartRateData.SplatPoint);
                redZonePoints.Add(detail.Value.HeartRateData.RedZone);
                orangeZonePoints.Add(detail.Value.HeartRateData.OrangeZone);
                greenZonePoints.Add(detail.Value.HeartRateData.GreenZone);
                blueZonePoints.Add(detail.Value.HeartRateData.BlueZone);
                greyZonePoints.Add(detail.Value.HeartRateData.BlackZone);
            }

            ClassDates = $"'{string.Join("','", classDates.Select(d => d.ToShortDateString()))}'";
            SplatPoints = $"{string.Join(",", splatPoints.Select(s => s.ToString()))}";
            RedZonePoints = $"{string.Join(",", redZonePoints.Select(s => s.ToString()))}";
            OrangeZonePoints = $"{string.Join(",", orangeZonePoints.Select(s => s.ToString()))}";
            GreenZonePoints = $"{string.Join(",", greenZonePoints.Select(s => s.ToString()))}";
            BlueZonePoints = $"{string.Join(",", blueZonePoints.Select(s => s.ToString()))}";
            GreyZonePoints = $"{string.Join(",", greyZonePoints.Select(s => s.ToString()))}";
        }

        private void GenerateClassesTrend(IEnumerable<ClassSummary> summaries)
        {
            Dictionary<int, int> classesThisYear = new Dictionary<int, int>();
            Dictionary<int, int> monthlyClasses = new Dictionary<int, int>();

            foreach (ClassSummary summary in summaries)
            {
                int year = summary.ClassTime.Year;
                int month = summary.ClassTime.Month;
                if (classesThisYear.ContainsKey(year) == false)
                {
                    classesThisYear[year] = 0;
                }
                if (monthlyClasses.ContainsKey(month) == false)
                {
                    monthlyClasses[month] = 0;
                }

                classesThisYear[year]++;
                monthlyClasses[month]++;
                ClassesTotal++;
            }

            ClassesPerMonth = (int)monthlyClasses.Values.Average();
            ClassesThisYear = classesThisYear[classesThisYear.Keys.OrderByDescending(k => k).First()];
        }

        private void GenerateHeartRateTrend(List<KeyValuePair<ClassSummary, ClassDetails>> details)
        {
            List<int> maxes = new List<int>();
            List<int> avges = new List<int>();
            List<string> labels = new List<string>();
            Dictionary<DateTime, HeartRateTrack> yearTrack = new Dictionary<DateTime, HeartRateTrack>();
            IEnumerable<KeyValuePair<ClassSummary, ClassDetails>> orderedDetails = details.OrderByDescending(kv => kv.Key.ClassTime);
            foreach (KeyValuePair<ClassSummary, ClassDetails> d in orderedDetails)
            {
                DateTime dt = new DateTime(d.Key.ClassTime.Year, d.Key.ClassTime.Month, 1);
                if (yearTrack.ContainsKey(dt) == false)
                {
                    yearTrack[dt] = new HeartRateTrack();
                }

                HeartRateTrack hr = yearTrack[dt];
                hr.AvgHeartRate += d.Value.HeartRateData.AverageHeartRate;
                hr.MaxHeartRate += d.Value.HeartRateData.MaxHeartRate;
                hr.Count++;
                yearTrack[dt] = hr;
            }

            maxes = yearTrack.OrderByDescending(kv => kv.Key).Select(kv => (int)((decimal)kv.Value.MaxHeartRate / (decimal)kv.Value.Count)).ToList();
            avges = yearTrack.OrderByDescending(kv => kv.Key).Select(kv => (int)((decimal)kv.Value.AvgHeartRate / (decimal)kv.Value.Count)).ToList();
            labels = yearTrack.OrderByDescending(kv => kv.Key).Select(kv => $"{kv.Key.Month}/{kv.Key.Year}").ToList();

            List<int> lastMaxes = new List<int>();
            List<int> lastAvges = new List<int>();
            List<string> lastLabels = new List<string>();
            foreach (KeyValuePair<ClassSummary, ClassDetails> d in orderedDetails.Take(10))
            {
                lastMaxes.Add(d.Value.HeartRateData.MaxHeartRate);
                lastAvges.Add(d.Value.HeartRateData.AverageHeartRate);
                lastLabels.Add(d.Key.ClassTime.ToShortDateString());
            }

            PerMonthLabels = $"'{string.Join("','",labels)}'";
            LastClassesLabels = $"'{string.Join("','", lastLabels)}'";
            MaxHeartRatePerMonth = string.Join(",", maxes);
            MaxHeartRateLastClasses = string.Join(",", lastMaxes);
            AverageHeartRatePerMonth = string.Join(",", avges);
            AverageHeartRateLastClasses = string.Join(",", lastAvges);
        }
    }

    public struct HeartRateTrack
    {
        public int AvgHeartRate { get; set; }
        public int MaxHeartRate { get; set; }
        public int Count { get; set; }
    }
}
