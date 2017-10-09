using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XinematriX.DataAccess.DBModels;
using XinematriX.Models.Models;
using XinematriX.DataAccess.DBHelper;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XinematriX.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PollsController : Controller
    {
        private DBHelper _db;

        public PollsController()
        {
            _db = new DBHelper();
        }

        [HttpGet]
        [ActionName("GetAllMoviePolls")]
        public List<VirtualMoviePolls> GetAllMoviePolls()
        {
            return _db.GetMoviePolls().Result;
        }
    }
}
