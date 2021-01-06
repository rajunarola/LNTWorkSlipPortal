using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNTSlipPortal_Repository.Data
{
    [MetadataType(typeof(UserMasterMetaData))]
    public partial class UserMaster
    {
        public string RoleName { get; set; }
    }
    public class UserMasterMetaData
    {
        [Required(ErrorMessage = "Password can't be blank.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "PSNumber can't be blank.")]
        public int PSNumber { get; set; }

    }
}
