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
    public class DetailsModel : PageModel
    {
        private readonly OtfApi _api;

        [BindProperty]
        public ClassDetails Details { get; set; }

        [BindProperty]
        public ClassSummary Summary { get; set; }

        public DetailsModel(OtfApi api)
        {
            _api = api;
        }

        public async Task<IActionResult> OnGet(string id)
        {
            OtfUser otfUser = HttpContext.GetSignedInOtfUser();
            Details = await _api.GetClassDetailsAsync(id, otfUser.MemberId, otfUser.SignInJwt);
            Summary = await _api.GetClassSummaryAsync(id, otfUser.MemberId, otfUser.SignInJwt);
            return Page();
        }
    }
}
