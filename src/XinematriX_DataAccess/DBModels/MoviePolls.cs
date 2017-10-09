using System;
using System.Collections.Generic;

namespace XinematriX.DataAccess.DBModels
{
    public partial class MoviePolls
    {
        public MoviePolls()
        {
            MoviePollsOptions = new HashSet<MoviePollsOptions>();
        }

        public int MoviePollsId { get; set; }
        public string PollQuestion { get; set; }
        public string Active { get; set; }

        public ICollection<MoviePollsOptions> MoviePollsOptions { get; set; }
    }
}
