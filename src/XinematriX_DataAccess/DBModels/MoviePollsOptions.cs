using System;
using System.Collections.Generic;

namespace XinematriX.DataAccess.DBModels
{
    public partial class MoviePollsOptions
    {
        public int MoviePollsOptionsId { get; set; }
        public int MoviePollsId { get; set; }
        public string Options { get; set; }
        public int? Votes { get; set; }

        public MoviePolls MoviePolls { get; set; }
    }
}
