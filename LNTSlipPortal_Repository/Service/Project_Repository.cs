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
    public class Project_Repository : IProject_Repository, IDisposable
    {
        private LNTSlipPortalEntities context;
        public Project_Repository(LNTSlipPortalEntities _context)
        {
            context = _context;
        }
        public Project_Repository()
        {
            context = new LNTSlipPortalEntities();
        }
        public IQueryable<Project> GetAllProjects()
        {
            try
            {
                return context.Projects.AsQueryable();
            }
            catch (Exception ex)
            {
                ex.SetLog("GetAllProjects,Repository");
                throw;
            }

        }

        public Project  GetProjectByID(int ProjectId)
        {
            try
            {
                var objProject=( from e in context.Projects
                                 where e.ProjectId == ProjectId
                                  select e).AsQueryable();
                return objProject.FirstOrDefault();
            }
            catch (Exception ex)
            {
                ex.SetLog("GetProjectbyId,Repository");
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
