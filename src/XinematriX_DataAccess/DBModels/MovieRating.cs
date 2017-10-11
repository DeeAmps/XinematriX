using System;
using System.Collections.Generic;

namespace XinematriX.DataAccess.DBModels
{
    public partial class MovieRating
    {
        public MovieRating()
        {
            Movie = new HashSet<Movie>();
        }

        public int MovieRatingId { get; set; }
        public string MovieRatingType { get; set; }

        public ICollection<Movie> Movie { get; set; }
    }
}
