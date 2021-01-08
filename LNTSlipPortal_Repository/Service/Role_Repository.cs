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
    public class Role_Repository : IRole_Repository, IDisposable
    {
        private LNTSlipPortalEntities context;
        public Role_Repository(LNTSlipPortalEntities _context)
        {
            context = _context;
        }
        public Role_Repository()
        {
            context = new LNTSlipPortalEntities();
        }
        public IQueryable<RoleMaster> GetAllRoles()
        {
            try
            {
                return context.RoleMasters.AsQueryable();
            }
            catch (Exception ex)
            {
                ex.SetLog("GetAllRoles,Repository");
                throw;
            }

        }

        public RoleMaster  GetRoleByID(int RoleId)
        {
            try
            {
                var objRole=( from e in context.RoleMasters
                                 where e.RoleId == RoleId
                                  select e).AsQueryable();
                return objRole.FirstOrDefault();
            }
            catch (Exception ex)
            {
                ex.SetLog("GetRolebyId,Repository");
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
