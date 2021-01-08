using System;
using System.Linq;
using LNTSlipPortal_Repository.ServiceContract;
using LNTSlipPortal_Repository.Data;
using System.Data;
using LNTSlipPortal_Repository.DataServices;
using static LNTSlipPortal_Repository.DataServices.CommonDTO;

namespace LNTSlipPortal_Repository.Service
{
    public class RAWS_Repository : IRAW_Repository, IDisposable
    {
        private LNTSlipPortalEntities context;
        public RAWS_Repository(LNTSlipPortalEntities _context)
        {
            context = _context;
        }
        public RAWS_Repository()
        {
            context = new LNTSlipPortalEntities();
        }
        public RAW InsertRAWS(RAW objRAW)
        {
            try
            {
                context.RAWS.Add(objRAW);
                context.SaveChanges();
                return objRAW;
            }
            catch (Exception ex)
            {
                ex.SetLog("InsertRAW,Repository");
                throw;
            }
        }

        public void UpdateRAWS(RAW objRAW)
        {
            try
            {
                context.Entry(objRAW).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ex.SetLog("Update,Repository");
                throw;
            }
        }
        public void DeleteRAWS(int RAWId)
        {
            try
            {
                RAW obj = GetRAWSByID(RAWId);
                if (obj != null)
                {
                    obj.IsActive = false;
                    UpdateRAWS(obj);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("DeleteRAW,Repository");
                throw;
            }
        }

        public IQueryable<RAW> GetAllRAWS()
        {
            try
            {
                return context.RAWS.Where(x => x.IsActive == true).AsQueryable();
            }
            catch (Exception ex)
            {
                ex.SetLog("GetAllRAWs,Repository");
                throw;
            }

        }

        public int GetAllRAWSCountbyRole(int UserRole)
        {
            try
            {
                var d = (from r in context.RAWS
                         join u in context.UserMasters on r.CreatedBy equals u.UserId
                         join p in context.ProductCategories on r.ProductCatId equals p.ProductCatId
                         join p1 in context.Projects on r.ProjectId equals p1.ProjectId
                         join sc in context.Scopes on r.ScopeId equals sc.ScopeId
                         join s in context.Status on r.Status equals s.StatusId
                         where r.IsActive &&
                               (UserRole == (int)Role.PMG1 ? r.Status >= (int)RAWSStatus.Draft && u.RoleId != (int)Role.SCG1 :
                               UserRole == (int)Role.SCG1 ? r.Status >= (int)RAWSStatus.Draft && u.RoleId != (int)Role.PMG1 :
                               UserRole == (int)Role.PMG2 ? r.Status >= (int)RAWSStatus.Initiated && u.RoleId != (int)Role.SCG1 :
                               UserRole == (int)Role.SCG2 ? r.Status >= (int)RAWSStatus.Initiated && u.RoleId != (int)Role.PMG1 :
                               UserRole == (int)Role.QC ? r.Status >= (int)RAWSStatus.Approved_by_PMG_SCG :
                               UserRole == (int)Role.Planner ? r.Status >= (int)RAWSStatus.Accepted_by_Shop :
                               UserRole == (int)Role.Shop ? r.Status >= (int)RAWSStatus.Acknowledged_by_QC : true)

                         select r).Count();

                return d;
            }
            catch (Exception ex)
            {
                ex.SetLog("GetAllRAWs,Repository");
                throw;
            }

        }

        public RAWDTO GetRAWSByIDForReport(int RAWId)
        {
            try
            {
                var obj = (from e in context.RAWS
                           join p in context.Projects on e.ProjectId equals p.ProjectId
                           join pc in context.ProductCategories on e.ProductCatId equals pc.ProductCatId
                           join s in context.Scopes on e.ScopeId equals s.ScopeId
                           join sw in context.ScopeOfWorks on e.ScopeOfWorkId equals sw.ScopeOfWorkId
                           join d in context.Documents on e.DocumentId equals d.DocumentId
                           where e.RAWSID == RAWId
                           select new RAWDTO
                           {
                               RAWSID = e.RAWSID,
                               ProjectId = e.ProjectId,
                               ProjectName = p.ProjectName,
                               ProductCatId = e.ProductCatId,
                               ProductDescription = e.ProductDescription,
                               ProductCatNo = pc.ProductCatNo.ToString(),
                               RAWSNO = e.RAWSNO,
                               AdditionalWork = e.AdditionalWork,
                               TotalQuantity = e.TotalQuantity,
                               ScopeId = e.ScopeId,
                               TotalManHours = e.TotalManHours,
                               ScopeOfWorkId = e.ScopeOfWorkId,
                               JigFixtures_SpecialTools = e.JigFixtures_SpecialTools,
                               JigFixtures_SpecialToolsLabel = e.JigFixtures_SpecialTools != null ? e.JigFixtures_SpecialTools == true ? "Yes" : e.JigFixtures_SpecialTools == false ? "No" : "" : "",
                               DocumentId = e.DocumentId,
                               Work = e.Work,
                               NCRNo = e.NCRNo,
                               Status = e.Status,
                               ScopeName = s.ScopeName,
                               Grinding = sw.Grinding,
                               PaintTouchUp = sw.PaintTouchUp,
                               Replacement = sw.Replacement,
                               Testing = sw.Testing,
                               Assembly = sw.Assembly,
                               Other = sw.Other,
                               BOM = d.BOM,
                               AssemblyList = d.AssemblyList,
                               GeneralAssembly = d.GeneralAssembly,
                               TestingDocument = d.TestingDocument,
                               WiringScheme = d.WiringScheme,
                               ProductData = d.ProductData,
                               BOMRevision = d.BOMRevision,
                               BOMCatNo = d.BOMCatNo,
                               AssemblyListCatNo = d.AssemblyListCatNo,
                               GeneralAssemblyCatNo = d.GeneralAssemblyCatNo,
                               TestingDocumentCatNo = d.TestingDocumentCatNo,
                               ProductDataCatNo = d.ProductDataCatNo,
                               WiringSchemeCatNo = d.WiringSchemeCatNo,
                               AssemblyListRevision = d.AssemblyListRevision,
                               GeneralAssemblyRevision = d.GeneralAssemblyRevision,
                               TestingDocumentRevision = d.TestingDocumentRevision,
                               WiringSchemeRevision = d.WiringSchemeRevision,
                               ProductDataRevision = d.ProductDataRevision,
                               GrindingLabel = sw.Grinding != null ? sw.Grinding == true ? "Yes" : sw.Grinding == false ? "No" : "" : "",
                               PaintTouchUpLabel = sw.PaintTouchUp != null ? sw.PaintTouchUp == true ? "Yes" : sw.PaintTouchUp == false ? "No" : "" : "",
                               ReplacementLabel = sw.Replacement != null ? sw.Replacement == true ? "Yes" : sw.Replacement == false ? "No" : "" : "",
                               TestingLabel = sw.Testing != null ? sw.Testing == true ? "Yes" : sw.Testing == false ? "No" : "" : "",
                               AssemblyLabel = sw.Assembly != null ? sw.Assembly == true ? "Yes" : sw.Assembly == false ? "No" : "" : "",
                               BOMLabel = d.BOM != null ? d.BOM == true ? "Yes" : d.BOM == false ? "No" : "" : "",
                               AssemblyListLabel = d.AssemblyList != null ? d.AssemblyList == true ? "Yes" : d.AssemblyList == false ? "No" : "" : "",
                               GeneralAssemblyLabel = d.GeneralAssembly != null ? d.GeneralAssembly == true ? "Yes" : d.GeneralAssembly == false ? "No" : "" : "",
                               TestingDocumentLabel = d.TestingDocument != null ? d.TestingDocument == true ? "Yes" : d.TestingDocument == false ? "No" : "" : "",
                               WiringSchemeLabel = d.WiringScheme != null ? d.WiringScheme == true ? "Yes" : d.WiringScheme == false ? "No" : "" : "",
                               ProductDataLabel = d.ProductData != null ? d.ProductData == true ? "Yes" : d.ProductData == false ? "No" : "" : "",
                               ActualManHours= e.ActualManHours,
                               JigFixtures_Detail =e.JigFixtures_Detail
                           }).FirstOrDefault();
                var query = (from s in context.StatusLogs
                             join u in context.UserMasters on s.UserId equals u.UserId
                             where s.RAWSID == RAWId
                             select new { s, u }).ToList();
                foreach (var item in query)
                {
                    switch (item.s.StatusId)
                    {
                        case (int)RAWSStatus.Initiated:
                            obj.InitiatorName = item.u.UserName;
                            obj.InitiatorPSNumber = item.u.PSNumber.ToString();
                            obj.InitiatorCreatedAt = item.s.CreatedAt;
                            break;
                        case (int)RAWSStatus.Approved_by_PMG_SCG:
                            obj.Approved_By_PMG_SCGName = item.u.UserName;
                            obj.Approved_By_PMG_SCGPSNumber = item.u.PSNumber.ToString();
                            obj.Approved_By_PMG_SCGCreatedAt = item.s.CreatedAt;
                            break;
                        case (int)RAWSStatus.Acknowledged_by_QC:
                            obj.Acknowlwdge_By_QCName = item.u.UserName;
                            obj.Acknowlwdge_By_QCPSNumber = item.u.PSNumber.ToString();
                            obj.Acknowlwdge_By_QCCreatedAt = item.s.CreatedAt;
                            break;
                        case (int)RAWSStatus.Accepted_by_Shop:
                            obj.Accepted_By_ShopName = item.u.UserName;
                            obj.Accepted_By_ShopPSNumber = item.u.PSNumber.ToString();
                            obj.Accepted_By_ShopCreatedAt = item.s.CreatedAt;
                            break;
                        case (int)RAWSStatus.Validated_by_Planner:
                            obj.Validated_By_PlannerName = item.u.UserName;
                            obj.Validated_By_PlannerPSNumber = item.u.PSNumber.ToString();
                            obj.Validated_By_PlannerCreatedAt = item.s.CreatedAt;
                            break;
                        case (int)RAWSStatus.Checked_verified:
                            obj.Checked_Verified_By_ShopName = item.u.UserName;
                            obj.Checked_Verified_By_ShopPSNumber = item.u.PSNumber.ToString();
                            obj.Checked_Verified_By_ShopCreatedAt = item.s.CreatedAt;
                            break;
                        case (int)RAWSStatus.Closed:
                            obj.Checked_By_QCName = item.u.UserName;
                            obj.Checked_By_QCPSNumber = item.u.PSNumber.ToString();
                            obj.Checked_By_QCCreatedAt = item.s.CreatedAt;
                            break;
                        default:
                            break;
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                ex.SetLog("GetRAWbyId,Repository");
                throw;
            }
        }

        public RAW GetRAWSByID(int RAWId)
        {
            try
            {
                var obj = (from e in context.RAWS
                           where e.RAWSID == RAWId
                           select e).FirstOrDefault();
                return obj;
            }
            catch (Exception ex)
            {
                ex.SetLog("GetRAWbyId,Repository");
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
