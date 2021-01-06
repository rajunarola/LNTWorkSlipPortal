using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNTSlipPortal_Repository.Data
{
    [MetadataType(typeof(EmployeeMasterMetaData))]
    public partial class EmployeeMaster
    {
        public string RoleName { get; set; }
    }
    public class EmployeeMasterMetaData
    {
        [Required(ErrorMessage = "Employee Name can't be blank.")]
        public string EmployeeName { get; set; }

        [Range(1, 9999999, ErrorMessage = "Role can not be blank.")]
        [Required(ErrorMessage = "Role can't be blank.")]
        public string RoleId { get; set; }

        [Required(ErrorMessage = "PSNumber can't be blank.")]
        public string PSNumber { get; set; }
    }
}
