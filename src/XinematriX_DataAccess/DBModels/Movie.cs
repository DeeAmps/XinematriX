using System;
using System.Collections.Generic;

namespace XinematriX.DataAccess.DBModels
{
    public partial class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string PosterPath { get; set; }
        public string TrailerPath { get; set; }
        public string Synopsis { get; set; }
        public TimeSpan RunningTime { get; set; }
        public int MovieRatingId { get; set; }
        public string Casts { get; set; }
        public string Genres { get; set; }
        public string Showing { get; set; }
        public string ComingSoon { get; set; }
        public string Premiering { get; set; }
        public string Active { get; set; }

        public MovieRating MovieRating { get; set; }
    }
}
