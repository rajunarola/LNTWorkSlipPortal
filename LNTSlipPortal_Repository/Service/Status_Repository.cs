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
    public class Status_Repository : IStatus_Repository, IDisposable
    {
        private LNTSlipPortalEntities context;
        public Status_Repository(LNTSlipPortalEntities _context)
        {
            context = _context;
        }
        public Status_Repository()
        {
            context = new LNTSlipPortalEntities();
        }
        public IQueryable<Status> GetAllStatus()
        {
            try
            {
                return context.Status.AsQueryable();
            }
            catch (Exception ex)
            {
                ex.SetLog("GetAllStatus,Repository");
                throw;
            }

        }

        public Status  GetStatusByID(int StatusId)
        {
            try
            {
                var objStatus=( from e in context.Status
                                 where e.StatusId == StatusId
                                  select e).AsQueryable();
                return objStatus.FirstOrDefault();
            }
            catch (Exception ex)
            {
                ex.SetLog("GetStatusbyId,Repository");
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
