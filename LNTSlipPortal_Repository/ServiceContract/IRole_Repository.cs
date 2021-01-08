using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LNTSlipPortal_Repository.Data;

namespace LNTSlipPortal_Repository.ServiceContract
{
    public interface IRole_Repository:IDisposable
    {
        IQueryable<RoleMaster> GetAllRoles();
        RoleMaster GetRoleByID(int RoleId);

    }
}
