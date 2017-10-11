using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XinematriX.DataAccess.DBModels;

namespace XinematriX_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ValuesController : Controller
    {
        
        [HttpGet]
        [ActionName("GetGenre")]
        public List<MovieGenre> GetGenre()
        {
            using (var con = new XinematriXContext())
            {
                var all = con.MovieGenre.ToList();
                return all;
            }
        }
    }
}
