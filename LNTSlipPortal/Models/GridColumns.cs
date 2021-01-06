using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LNTSlipPortal.Models
{

    public class RoleMaster
    {
        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }
        public Int64 RowNumber { get; set; }
    } 
     
    public class EmployeeMaster
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int PSNumber { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        public Int64 RowNumber { get; set; }
    }

    public class RAW
    {
        public int RAWSID { get; set; }
        public string RAWSNO { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int ProductCatId { get; set; }
        public int ProductCatNo { get; set; }
        public string ProductDescription { get; set; }
        public string AdditionalWork { get; set; }
        public int TotalQuantity { get; set; }
        public int ScopeId { get; set; }
        public string ScopeName { get; set; }
        public int TotalManHours { get; set; }
        public int ScopeOfWorkId { get; set; }
        public bool JigFixtures_SpecialTools { get; set; }
        public int DocumentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }

        public Int64 RowNumber { get; set; }
        public int Status { get; set; }
        public int RoleId { get; set; }
        public int ActualManHours { get; set; }
        public string StatusName { get; set; }
        public string UserName { get; set; }
    }

}