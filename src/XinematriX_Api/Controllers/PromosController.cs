using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XinematriX.Models.Models;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XinematriX.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("AllowSpecificOrigin")]
    public class PromosController : Controller
    {
        [HttpGet]
        [ActionName("GetDaysOfWeek")]
        public List<DaysOfWeek> GetDaysOfWeek()
        {
            var results = new List<DaysOfWeek>
            {
                new DaysOfWeek { Day = "Monday" },
                new DaysOfWeek { Day = "Tuesday" },
                new DaysOfWeek { Day = "Wednesday" },
                new DaysOfWeek { Day = "Thursday" },
                new DaysOfWeek { Day = "Friday" },
                new DaysOfWeek { Day = "Saturday" },
                new DaysOfWeek { Day = "Sunday" }
            };

            return results;
        }
    }
}
