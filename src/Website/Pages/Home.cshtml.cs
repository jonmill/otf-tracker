using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using OtfTracker.Common;
using OtfTracker.Common.Models;
using OtfTracker.Website.Helpers;
using OtfTracker.Website.Identity;
using OtfTracker.Website.Models;

namespace Website.Pages
{
    [Authorize]
    public class HomeModel : PageModel
    {
        private readonly OtfApi _api;

        [BindProperty]
        public IEnumerable<ClassSummary> Summaries { get; set; }

        public HomeModel(OtfApi api)
        {
            _api = api;
        }

        public async Task<IActionResult> OnGet()
        {
            OtfUser otfUser = HttpContext.GetSignedInOtfUser();
            Summaries = await _api.GetClassSummariesAsync(otfUser.MemberId, otfUser.SignInJwt);
            Summaries = Summaries.OrderByDescending(s => s.ClassTime);
            return Page();
        }
    }
}
