using System;
using Microsoft.AspNetCore.Mvc;
using XinematriX.Models.Models;
using XinematriX.WebAdmin.Providers;
using XinematriX.Api.Controllers;

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
            try
            {
                if (string.IsNullOrEmpty(usercred.password) || string.IsNullOrEmpty(usercred.username))
                {
                    ViewData["Error"] = "Username and Password cant be null";
                    return View();
                }
                bool isValid = ValidateCredentials(usercred.username, usercred.password);
                if (!isValid)
                {
                    ViewData["Error"] = "Invalid Username/Password";
                    return View();
                }
                else
                {
                    string code = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
                    code = code.Replace("-", "");
                    code = code.Substring(0, 25);
                    var token = AdminController.Instance.CreateWebAccountToken(usercred.username, code).Result + ":" + PasswordCrypt.Instance.Encrypt(usercred.username);
                    HttpContext.Response.Cookies.Append("XTkCookie", token);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception x)
            {
                ViewData["Error"] = "An error occured! Please Try Again Later!";
                return View();
            }
        }

        public IActionResult Logout()
        {
            var tkValue = HttpContext.Request.Cookies["XTkCookie"];
            AdminController.Instance.ExpireToken(tkValue);
            Response.Cookies.Delete("XTkCookie");
            return RedirectToAction("Login");
        }

            private bool ValidateCredentials(string username, string password)
        {  
            string cipherText = PasswordCrypt.Instance.Encrypt(password);
            return AdminController.Instance.VerifyAdmin(username, cipherText).Result;
        }
    }
}
