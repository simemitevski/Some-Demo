using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecurityDemo.Services.Definition;
using SecurityDemo.Services.Definition.Services;
using SecurityDemo.Services.ViewModels;
using SecurityDemo.Services.Extensions;
using System;
using SecurityDemo.Domain.DataObjects;
using System.Threading.Tasks;

namespace SecurityDemo.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly ISecurityService securityService;

        public UserController(IUserService userService,
            ISecurityService securityService)
        {
            this.userService = userService;
            this.securityService = securityService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = this.userService.GetUserByUsername(model.UserName);

            if(user == null)
            {
                return View();
            }

            var ipAddress = HttpContext.Request.UserIpAddress();

            if (securityService.VerifyHashedPassword(user.Password, model.Password))
            {
                await securityService.LoginAsync(new IdentityData(user, ipAddress));
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            if (User.IsLoggedIn())
            {
                await securityService.LogoutAsync();
            }

            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(UserViewModel model)
        {
            try
            {
                this.userService.CreateUser(model);
                return View();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}