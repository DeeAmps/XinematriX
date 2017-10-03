using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XinematriX_DataAccess.DBModels;

namespace XinematriX_Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        
        [HttpGet]
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
