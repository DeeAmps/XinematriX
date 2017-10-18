using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace XinematriX.Models.Models
{
     public class UserLogin
    {
        [Required]
        public string username { get; set; }
        [Required] 
        public string password { get; set; }
    }
}
