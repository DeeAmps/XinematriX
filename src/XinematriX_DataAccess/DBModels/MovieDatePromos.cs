using System;
using System.Collections.Generic;

namespace XinematriX.DataAccess.DBModels
{
    public partial class MovieDatePromos
    {
        public int MovieDatePromosId { get; set; }
        public string PromoDate { get; set; }
        public string PromoDescription { get; set; }
    }
}
