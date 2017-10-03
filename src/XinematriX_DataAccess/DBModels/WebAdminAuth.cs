using System;
using System.Collections.Generic;

namespace XinematriX_DataAccess.DBModels
{
    public partial class WebAdminAuth
    {
        public int WebAdminAuthId { get; set; }
        public string WebAdminUsername { get; set; }
        public string WebAdminPassword { get; set; }
        public string WebAdminToken { get; set; }
        public DateTime WebAdminTokenGeneratedDate { get; set; }
        public DateTime WebAdminTokenExpiryDate { get; set; }
    }
}
