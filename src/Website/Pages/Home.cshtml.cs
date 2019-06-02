using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtfTracker.Common;
using OtfTracker.Common.Models;
using OtfTracker.Website.Identity;
using OtfTracker.Website.Models;

namespace Website.Pages
{
    [Authorize]
    public class HomeModel : PageModel
    {
        private readonly OtfApi _api;
        private readonly UserManager<OtfUser> _userManager;

        [BindProperty]
        public IEnumerable<SummaryViewModel> Summaries { get; set; }

        public HomeModel(
            OtfApi api,
            UserManager<OtfUser> userManager)
        {
            _api = api;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {
            OtfUser user = _userManager.Users.Single();
            IEnumerable<ClassSummary> summaries = await _api.GetClassSummariesAsync(user.Id, user.SignInJwt);
            Summaries = summaries.OrderByDescending(s => s.ClassTime).Select(s => new SummaryViewModel()
            {
                ActiveTime = s.ActiveTime,
                CaloriesBurned = s.CaloriesBurned,
                ClassHistoryUuid = s.ClassHistoryUuid.ToString(),
                ClassTime = s.ClassTime,
                ClassType = s.ClassType,
                Coach = s.Coach,
                SplatPoints = s.SplatPoints,
                StudioName = s.StudioName,
                StudioNumber = s.StudioNumber
            });
            return Page();
        }
    }
}
