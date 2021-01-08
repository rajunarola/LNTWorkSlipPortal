using System;
using System.Linq;
using LNTSlipPortal_Repository.ServiceContract;
using LNTSlipPortal_Repository.Data;
using System.Data;
using LNTSlipPortal_Repository.DataServices;

namespace LNTSlipPortal_Repository.Service
{
    public class Employee_Repository : IEmployee_Repository, IDisposable
    {
        private LNTSlipPortalEntities context;
        public Employee_Repository(LNTSlipPortalEntities _context)
        {
            context = _context;
        }
        public Employee_Repository()
        {
            context = new LNTSlipPortalEntities();
        }
        public void InsertEmployee(EmployeeMaster objEmployee)
        {
            try
            {
                context.EmployeeMasters.Add(objEmployee);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ex.SetLog("InsertEmployee,Repository");
                throw;
            }
        }

        public void UpdateEmployee(EmployeeMaster objEmployee)
        {
            try
            {
                context.Entry(objEmployee).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ex.SetLog("Update,Repository");
                throw;
            }
        }
        public void DeleteEmployee(int EmployeeId)
        {
            try
            {
                EmployeeMaster obj = GetEmployeeByID(EmployeeId);
                if (obj != null)
                {
                    obj.IsDelete = true;
                    UpdateEmployee(obj);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("DeleteEmployee,Repository");
                throw;
            }
        }

        public IQueryable<EmployeeMaster> GetAllEmployees()
        {
            try
            {
                return context.EmployeeMasters.Where(x => x.IsDelete == false).AsQueryable();
            }
            catch (Exception ex)
            {
                ex.SetLog("GetAllEmployees,Repository");
                throw;
            }

        }

        public EmployeeMaster GetEmployeeByID(int EmployeeId)
        {
            try
            {
                return (from e in context.EmployeeMasters
                        join r in context.RoleMasters on e.RoleId equals r.RoleId
                        where e.EmployeeId == EmployeeId
                        select e).AsQueryable().FirstOrDefault();
            }
            catch (Exception ex)
            {
                ex.SetLog("GetEmployeebyId,Repository");
                throw;
            }
        }

        public bool IsDuplicate(string EmployeeName, int EmployeeId)
        {
            try
            {
                var objEmployee = (from e in context.EmployeeMasters
                                   where (e.EmployeeId > 0 ? e.EmployeeId != EmployeeId : e.EmployeeId == EmployeeId) && e.EmployeeName.Trim() == EmployeeName.Trim()
                                   select e).AsQueryable();
                return objEmployee.ToList().Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                ex.SetLog("IsDuplicate,Repository");
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
