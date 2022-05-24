using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        public IActionResult OnGetAsync(string returnUrl = "https://staging-movie-platform-sep6.herokuapp.com/signin-google")
        {
            string provider = "Google";
            // Request a redirect to the external login provider.
            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = Url.Page("./Login",
                pageHandler: "Callback",
                values: new { returnUrl }),
            };
            return new ChallengeResult(provider, authenticationProperties);
        }
        
        public async Task<IActionResult> OnGetCallbackAsync(
            string returnUrl = "https://staging-movie-platform-sep6.herokuapp.com/signin-google", string remoteError = null)
        {
            // Get the information about the user from the external login provider
            var GoogleUser = this.User.Identities.FirstOrDefault();
            if (GoogleUser.IsAuthenticated)
            {
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    
                    RedirectUri = "https://" + Request.Host.Value 
                };
               HttpContext.Response.Redirect("https://" + Request.Host.Value);
                try {
                    
                    await HttpContext.SignInAsync(
                    
                    new ClaimsPrincipal(GoogleUser),
                    authProperties);
                } catch(AggregateException e) {
                    e.InnerException.ToString();
                    e.Source.ToString();
            
                }
                
            }
            return LocalRedirect("/home");
        }


    }


}

