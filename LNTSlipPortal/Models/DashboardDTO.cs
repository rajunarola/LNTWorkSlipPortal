using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LNTSlipPortal.Models;
using System.Data;
using System.Data.SqlClient;
using LNTSlipPortal_Repository.DataServices;

namespace LNTSlipPortal.Models
{
    public class DashboardDTO
    {
        public int EmployeeCount { get; set; }
        public int RAWSCount { get; set; }
        public int ProjectCount { get; set; }
    }
}