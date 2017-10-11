using System;
using System.Collections.Generic;

<<<<<<< HEAD
namespace XinematriX.DataAccess.DBModels
=======
namespace XinematriX_DataAccess.DBModels
>>>>>>> refs/remotes/origin/danny
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
