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
    public class ScopeOfWork_Repository : IScopeOfWork_Repository, IDisposable
    {
        private LNTSlipPortalEntities context;
        public ScopeOfWork_Repository(LNTSlipPortalEntities _context)
        {
            context = _context;
        }
        public ScopeOfWork_Repository()
        {
            context = new LNTSlipPortalEntities();
        }
        public IQueryable<ScopeOfWork> GetAllScopeOfWorks()
        {
            try
            {
                return context.ScopeOfWorks.AsQueryable();
            }
            catch (Exception ex)
            {
                ex.SetLog("GetAllScopeOfWorks,Repository");
                throw;
            }

        }

        public ScopeOfWork  GetScopeOfWorkByID(int ScopeOfWorkId)
        {
            try
            {
                var objScopeOfWork=( from e in context.ScopeOfWorks
                                 where e.ScopeOfWorkId == ScopeOfWorkId
                                  select e).AsQueryable();
                return objScopeOfWork.FirstOrDefault();
            }
            catch (Exception ex)
            {
                ex.SetLog("GetScopeOfWorkbyId,Repository");
                throw;
            }
        }

        public ScopeOfWork InsertScopeOfWork(ScopeOfWork objScopeOfWork)
        {
            try
            {
                context.ScopeOfWorks.Add(objScopeOfWork);
                context.SaveChanges();

                return objScopeOfWork;
            }
            catch (Exception ex)
            {
                ex.SetLog("InsertScopeOfWork,Repository");
                throw;
            }
        }

        public ScopeOfWork UpdateScopeOfWork(ScopeOfWork objScopeOfWork)
        {
            try
            {
                context.Entry(objScopeOfWork).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return objScopeOfWork;
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
