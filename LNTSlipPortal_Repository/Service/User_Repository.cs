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
    public class User_Repository : IUser_Repository, IDisposable
    {
        private LNTSlipPortalEntities context;
        public User_Repository(LNTSlipPortalEntities _context)
        {
            context = _context;
        }
        public User_Repository()
        {
            context = new LNTSlipPortalEntities();
        }
        public IQueryable<UserMaster> GetAllUsers()
        {
            try
            {
                return context.UserMasters.AsQueryable();
            }
            catch (Exception ex)
            {
                ex.SetLog("GetAllUsers,Repository");
                throw;
            }

        }

        public UserMaster  GetUserByID(int UserId)
        {
            try
            {
                var objUser=( from e in context.UserMasters
                                 where e.UserId == UserId
                                  select e).AsQueryable();
                return objUser.FirstOrDefault();
            }
            catch (Exception ex)
            {
                ex.SetLog("GetUserbyId,Repository");
                throw;
            }
        }

        public UserMaster LogInUsers(int PSNumber, string Password)
        {
            try
            {
                return context.UserMasters.Where(x => x.Password.Equals(Password) && x.PSNumber == PSNumber).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ex.SetLog("LogIn error,Repository");
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
