using System;
using System.Collections.Generic;
using System.Text;

namespace XinematriX.Models.Models
{
    public class VirtualMoviePolls
    {
        public int MoviePollsId { get; set; }
        public string PollQuestion { get; set; }
        public string PollOptions { get; set; }
        public int Votes { get; set; }
        public double PercentageVote { get; set; }
    }
}
