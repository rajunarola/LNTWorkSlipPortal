using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LNTSlipPortal_Repository.Data;

namespace LNTSlipPortal_Repository.ServiceContract
{
    public interface IUser_Repository:IDisposable
    {
        IQueryable<UserMaster> GetAllUsers();
        UserMaster GetUserByID(int UserId);
        UserMaster LogInUsers(int PSNumber, string Password);
    }
}
