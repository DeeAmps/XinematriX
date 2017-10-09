using System;
using System.Collections.Generic;

namespace XinematriX.DataAccess.DBModels
{
    public partial class MovieDayPromos
    {
        public int MovieDayPromosId { get; set; }
        public string PromoDay { get; set; }
        public string PromoDescription { get; set; }
    }
}
