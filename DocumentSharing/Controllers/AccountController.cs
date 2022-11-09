using DocumentSharing.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentSharing.Controllers
{
    public class AccountController : Controller
    {
        // Provided by Microsoft.AspNetCore.Identity
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        //To impelement return function Task<IActionResult>
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            //My model is valid or not
            if (ModelState.IsValid)
            {
                //Copy data fron RegisterViewModel to IdentityUser
                var user = new IdentityUser()
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                //Store user data in AspNetUsers database table
                var result = await userManager.CreateAsync(user, model.Password);

                //If user is successfully created, sign-in the user using
                //SignInManager and redirect to index action of ClassroomController
                if (result.Succeeded)
                {
                    HttpContext.Session.SetString("UserEmail", model.Email);
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "classrooms");
                }

                //If there are any errors, and then to the ModelState object
                //which will be displayed by the validation summary tag hhelper
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
            /*return RedirectToPage("index");*/
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        //If there is a return url then want to back the user to return url for this pass parameter
        public async Task<IActionResult> Login(LoginViewModel model ,String returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    HttpContext.Session.SetString("UserEmail", model.Email);
                    if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "classrooms");
                    }
                }


                /*public IActionResult Login(UserDetails model)
                {
                    UserDetails success = _userRepository.Login(model.UserEmail, model.password);
                    if (model.UserEmail == "admin@gmail.com" && model.password == "123")
                    {
                        return RedirectToAction(actionName: "Index", controllerName: "Vaccine");
                    }
                    if (success != null)
                    {
                        HttpContext.Session.SetString("UserEmail", model.UserEmail);
                        HttpContext.Session.SetInt32("UserId", success.Id);
                        return RedirectToAction(actionName: "Index", controllerName: "VaccMembers");
                        //return View("SignUp");
                    }
                    else
                    {
                        return View("");
                    }


                }*/

                //Error
                ModelState.AddModelError(String.Empty, "Incorrect Username or Password.");
            }

            return View(model);
        }

    }
}
