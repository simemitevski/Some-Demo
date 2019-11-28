using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecurityDemo.Domain.Logging;
using SecurityDemo.Models;
using SecurityDemo.Services.Extensions;

namespace SecurityDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoggerManager logger;

        public HomeController(ILoggerManager logger)
        {
            this.logger = logger;
        }
        public IActionResult Index()
        {
            try
            {
                var name = this.User.Identity.Name;
                throw new Exception("Exception");
            }
            catch(Exception ex)
            {
                logger.LogError(ex);
            }
            return View();
        }

        [Authorize(Policy = "MustBeAdmin")]
        public IActionResult About()
        {
            RecordInSession("About");

            ViewData["Message"] = "Your application description page. This page is visible for admins and system admins; Logged in user id: " + this.User.GetLoggedInUserId();

            return View();
        }

        [Authorize(Policy = "MustBeSystemAdmin")]
        public IActionResult Contact()
        {
            RecordInSession("Contact");

            ViewData["Message"] = "Your contact page. This page is visible only for system admins; Logged in user id: " + this.User.GetLoggedInUserId();

            return View();
        }

        public IActionResult Forbidden()
        {
            return View();
        }

        [Authorize(Policy = "MustBeAdmin")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void RecordInSession(string action)
        {
            var paths = HttpContext.Session.GetString("actions") ?? string.Empty;
            HttpContext.Session.SetString("actions", paths + ";" + action);
        }
    }
}
