using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LNTSlipPortal_Repository.Data;

namespace LNTSlipPortal_Repository.ServiceContract
{
    public interface IStatusLog_Repository:IDisposable
    {
        IQueryable<StatusLog> GetAllStatusLogs();
        StatusLog GetStatusLogByID(int StatusLogId);
        StatusLog InsertStatusLog(StatusLog objStatusLog);
        StatusLog UpdateStatusLog(StatusLog objStatusLog);
    }
}
