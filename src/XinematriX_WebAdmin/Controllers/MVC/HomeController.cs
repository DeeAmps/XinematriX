using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;
using XinematriX.Models.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XinematriX.WebAdmin.Controllers.MVC
{
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

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Promo()
        {
            return View();
        }

        public IActionResult Polls()
        {
            var polls = new XinematriX.Api.Controllers.PollsController();
            var returnPolls = polls.GetAllMoviePolls().GroupBy(p => p.PollQuestion).ToList();
            return View(returnPolls);
        }
    }
}
