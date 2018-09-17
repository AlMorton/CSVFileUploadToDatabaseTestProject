using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CSVUploadToDataTestProject.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSVUploadToDataTestProject.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult LogIn(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogInActionAsync(LogInViewModel logIn)
        {
            if(!ModelState.IsValid)
            {
                return View("LogIn");
            }

            if(await ValidateUser(logIn))
            { 

                if(!String.IsNullOrEmpty(logIn.ReturnUrl) && Url.IsLocalUrl(logIn.ReturnUrl))
                {
                    return Redirect(logIn.ReturnUrl);
                }

                return Redirect("/Home/Index");
            }

            return View("LogIn");
        }


        private async Task<bool> ValidateUser(LogInViewModel logIn)
        {

            if((logIn.Username == "Test" && logIn.Password == "Password1"))
            {
                var claims = new List<Claim> {

                    new Claim(ClaimTypes.Name, logIn.Username),
                    new Claim(ClaimTypes.Role, "AppAccess")
                };

                var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies"));

                await HttpContext.SignInAsync("Cookies", user);
                
                return true;
            }

            return false;
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("LogIn");
        }

        public IActionResult CurrentUser()
        {
            var userName = HttpContext.User.Identity.Name;

            ViewData["username"] = userName;

            return PartialView();
        }
    }
}