using Microsoft.AspNetCore.Mvc;
using System.Linq;
using XinematriX.Api.Controllers;
using XinematriX.Models.Models;
using XinematriX.WebAdmin.Providers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XinematriX.WebAdmin.Controllers.MVC
{
    [AuthorizationHelper]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddMovie()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Profile(ProfilePassword pass)
        {
            if (string.IsNullOrEmpty(pass.oldpassword) || string.IsNullOrEmpty(pass.confirmpassword) || string.IsNullOrEmpty(pass.newpassword))
            {
                ViewData["Error"] = "Please fill all fields";
                return View();
            }
            if (pass.newpassword != pass.confirmpassword)
            {
                ViewData["Error"] = "Passwords Dont Match!";
                return View();
            }
            var verifyPassword = AdminController.Instance.VerifyPassword(pass.oldpassword, HttpContext.Request.Cookies["XTkCookie"]).Result;
            if (!verifyPassword)
            {
                ViewData["Error"] = "Incorrect Password";
                return View();
            }
            else
            {
                var saveNewPassword = AdminController.Instance.ChangePassword(pass, HttpContext.Request.Cookies["XTkCookie"]);
                ViewData["Success"] = "Password Changed Successfully!";
                return View();
            }
            
        }

        public IActionResult Promo()
        {
            return View();
        }

        public IActionResult Polls()
        {
            var polls = new PollsController();
            var returnPolls = polls.GetAllMoviePolls().GroupBy(p => p.MoviePollsId).ToList();
            return View(returnPolls);
        }
    }
}
