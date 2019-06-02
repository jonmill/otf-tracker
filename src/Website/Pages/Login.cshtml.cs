using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtfTracker.Common;
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
        private readonly SignInManager<OtfUser> _signInManager;

        [BindProperty]
        public OtfTracker.Website.Models.LoginModel Input { get; set; }

        public LoginModel(SignInManager<OtfUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.ExternalLoginSignInAsync(Input.Email, Input.Password, true, false);
            return new RedirectToPageResult("home");
        }
    }
}
