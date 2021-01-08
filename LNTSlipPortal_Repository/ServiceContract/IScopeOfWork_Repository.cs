using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LNTSlipPortal_Repository.Data;

namespace LNTSlipPortal_Repository.ServiceContract
{
    public interface IScopeOfWork_Repository:IDisposable
    {
        IQueryable<ScopeOfWork> GetAllScopeOfWorks();
        ScopeOfWork GetScopeOfWorkByID(int ScopeOfWorkId);
        ScopeOfWork InsertScopeOfWork(ScopeOfWork objScopeOfWork);
        ScopeOfWork UpdateScopeOfWork(ScopeOfWork objScopeOfWork);
    }
}
