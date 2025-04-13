﻿using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebFrameworksGroupCA2Project.Data;
using WebFrameworksGroupCA2Project.Models;


namespace WebFrameworksGroupCA2Project.Controllers
{
    public class AccountController : Controller
    {

        private readonly WebFrameworksGroupCA2ProjectContext _context;

        private readonly SignInManager<AppUser> signInManager;

        private readonly UserManager<AppUser> userManager;

        public AccountController(WebFrameworksGroupCA2ProjectContext context, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _context = context;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {

            if (ModelState.IsValid)
            {
                //login
                var result = await signInManager.PasswordSignInAsync(model.Username!, model.Password!, model.RememberMe!, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");

                }

                ModelState.AddModelError("", "Invalid login attempt");

                return View(model);

            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {

            if (ModelState.IsValid)
            {

                AppUser user = new()
                {
                    Name = model.Name,
                    UserName = model.Name,
                    Email = model.Email,
                    Address = model.Address,
                };

                var result = await userManager.CreateAsync(user, model.Password!);

                var User = new[] { "User" };


                await userManager.AddToRolesAsync(user, User);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);

                    


                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }


        public async Task<IActionResult> Logout()
        {

            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        
        // External login (Google)
        public IActionResult ExternalLogin(string provider)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account");
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        // Callback method for Google authentication
        public async Task<IActionResult> ExternalLoginCallback()
        {
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {
                // User is logged in with external provider (Google)
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // If the user does not exist, allow them to register
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var fullName = info.Principal.FindFirstValue(ClaimTypes.Name); 

                // Replace any non-alphanumeric characters (e.g., spaces, punctuation) and convert to a valid username
                var validUsername = new string(fullName.Where(c => Char.IsLetterOrDigit(c)).ToArray());
                
                var user = new AppUser { Name = fullName, UserName = validUsername, Email = email };

                var createResult = await userManager.CreateAsync(user);
                if (createResult.Succeeded)
                {


                    var User = new[] { "User" };


                    await userManager.AddToRolesAsync(user, User);


                    await userManager.AddLoginAsync(user, info);
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in createResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("Login");
        }
    }
}
