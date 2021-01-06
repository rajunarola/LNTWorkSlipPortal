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
    public class Scope_Repository : IScope_Repository, IDisposable
    {
        private LNTSlipPortalEntities context;
        public Scope_Repository(LNTSlipPortalEntities _context)
        {
            context = _context;
        }
        public Scope_Repository()
        {
            context = new LNTSlipPortalEntities();
        }
        public IQueryable<Scope> GetAllScopes()
        {
            try
            {
                return context.Scopes.AsQueryable();
            }
            catch (Exception ex)
            {
                ex.SetLog("GetAllScopes,Repository");
                throw;
            }

        }

        public Scope  GetScopeByID(int ScopeId)
        {
            try
            {
                var objScope=( from e in context.Scopes
                                 where e.ScopeId == ScopeId
                                  select e).AsQueryable();
                return objScope.FirstOrDefault();
            }
            catch (Exception ex)
            {
                ex.SetLog("GetScopebyId,Repository");
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
