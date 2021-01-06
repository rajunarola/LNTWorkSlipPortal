using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LNTSlipPortal_Repository.Data;

namespace LNTSlipPortal_Repository.ServiceContract
{
    public interface IRAW_Repository:IDisposable
    {
        RAW InsertRAWS(RAW objRAW);
        void UpdateRAWS(RAW objRAW);
        void DeleteRAWS(int RAWId); 
        IQueryable<RAW> GetAllRAWS();
        RAWDTO GetRAWSByIDForReport(int RAWId);
        RAW GetRAWSByID(int RAWId);
        int GetAllRAWSCountbyRole(int UserRole);
    }
}
