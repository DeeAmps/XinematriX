using Microsoft.AspNetCore.Mvc;
using XinematriX.Models.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XinematriX.WebAdmin.Controllers.MVC
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserLogin usercred)
         {
            ViewData["ValidateCred"] = "Invalid Credentials!";
            return View();
        }
    }
}
