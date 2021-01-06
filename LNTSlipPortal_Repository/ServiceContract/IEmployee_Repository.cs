using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LNTSlipPortal_Repository.Data;

namespace LNTSlipPortal_Repository.ServiceContract
{
    public interface IEmployee_Repository:IDisposable
    {
        void InsertEmployee(EmployeeMaster objEmployee);
        void UpdateEmployee(EmployeeMaster objEmployee);
        void DeleteEmployee(int EmployeeId); 
        IQueryable<EmployeeMaster> GetAllEmployees();
        EmployeeMaster GetEmployeeByID(int EmployeeId);
        Boolean IsDuplicate(string EmployeeName, int EmployeeId);
    }
}
