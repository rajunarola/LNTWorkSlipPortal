using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LNTSlipPortal_Repository.Data;

namespace LNTSlipPortal_Repository.ServiceContract
{
    public interface IStatus_Repository:IDisposable
    {
        IQueryable<Status> GetAllStatus();
        Status GetStatusByID(int StatusId);
    }
}
