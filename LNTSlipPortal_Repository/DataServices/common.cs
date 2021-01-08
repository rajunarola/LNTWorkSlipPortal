using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNTSlipPortal_Repository.DataServices
{
    public class CommonDTO
    {
        public enum RAWSStatus
        {
            Draft = 1,
            Initiated = 2,
            Approved_by_PMG_SCG = 3,
            Acknowledged_by_QC = 4,
            Accepted_by_Shop = 5,
            Validated_by_Planner = 6,
            Checked_verified = 7,
            Closed = 8
        }
        public enum Role
        {
            Admin = 1,
            PMG1 = 2,
            PMG2 = 3,
            SCG1 = 4,
            SCG2 = 5,
            QC = 6,
            Planner = 7,
            Shop = 8
        }
    }
}
