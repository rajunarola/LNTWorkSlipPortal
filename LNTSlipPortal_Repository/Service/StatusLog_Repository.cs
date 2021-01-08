using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LNTSlipPortal_Repository.ServiceContract;
using System.Data.Entity;
using LNTSlipPortal_Repository.Data;
using System.Data;
using System.Data.SqlClient;
using LNTSlipPortal_Repository.DataServices;

namespace LNTSlipPortal_Repository.Service
{
    public class StatusLog_Repository : IStatusLog_Repository, IDisposable
    {
        private LNTSlipPortalEntities context;
        public StatusLog_Repository(LNTSlipPortalEntities _context)
        {
            context = _context;
        }
        public StatusLog_Repository()
        {
            context = new LNTSlipPortalEntities();
        }
        public IQueryable<StatusLog> GetAllStatusLogs()
        {
            try
            {
                return context.StatusLogs.AsQueryable();
            }
            catch (Exception ex)
            {
                ex.SetLog("GetAllStatusLogs,Repository");
                throw;
            }

        }
        public StatusLog GetStatusLogByID(int StatusLogId)
        {
            try
            {
                var objStatusLog = (from e in context.StatusLogs
                                    where e.StatusLogId == StatusLogId
                                    select e).AsQueryable();
                return objStatusLog.FirstOrDefault();
            }
            catch (Exception ex)
            {
                ex.SetLog("GetStatusLogbyId,Repository");
                throw;
            }
        }

        public StatusLog InsertStatusLog(StatusLog objStatusLog)
        {
            try
            {
                context.StatusLogs.Add(objStatusLog);
                context.SaveChanges();

                return objStatusLog;
            }
            catch (Exception ex)
            {
                ex.SetLog("InsertStatusLog,Repository");
                throw;
            }
        }

        public StatusLog UpdateStatusLog(StatusLog objStatusLog)
        {
            try
            {
                context.Entry(objStatusLog).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return objStatusLog;
            }
            catch (Exception ex)
            {
                ex.SetLog("Update,Repository");
                throw;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
