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
    public class LoginModel : PageModel
    {
        [BindProperty]
        public OtfTracker.Website.Models.LoginModel Input { get; set; }

        private readonly OtfApi _api;

        public LoginModel(OtfApi api)
        {
            _api = api;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            LoginResponse response = await _api.LoginAsync(Input.Email, Input.Password);
            await HttpContext.SignInOtfUserAsync(response);
            return new RedirectToPageResult("home");
        }
    }
}
