using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtfTracker.Common;
using OtfTracker.Common.Responses;
using OtfTracker.Website.Helpers;
using OtfTracker.Website.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Website.Pages
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        public LogoutModel()
        {
        }

        public async Task<IActionResult> OnGet()
        {
            await HttpContext.SignOutOtfUserAsync();
            return new RedirectToPageResult("Index");
        }
    }
}
