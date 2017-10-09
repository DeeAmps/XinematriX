using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using XinematriX.DataAccess.DBModels;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XinematriX.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("AllowSpecificOrigin")]
    public class MoviesController : Controller
    {
        [HttpGet]
        [ActionName("GetMovieRating")]
        public List<MovieRating> GetMovieRating()
        {
            using (var db = new XinematriXContext())
            {
                return db.MovieRating.ToList();
            }
        }

        [HttpGet]
        [ActionName("GetMovieGenres")]
        public List<MovieGenre> GetMovieGenres()
        {
            using (var db = new XinematriXContext())
            {
                return db.MovieGenre.ToList();
            }
        }
    }
}
