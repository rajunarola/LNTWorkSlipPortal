
using LNTSlipPortal_Repository.Data;
using LNTSlipPortal_Repository.DataServices;
using LNTSlipPortal_Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static LNTSlipPortal.Models.Common;

namespace LNTSlipPortal.Controllers
{
    public class RAWSController : Controller
    {
        private RAWS_Repository _IRAWS_Repository;
        private Project_Repository _IProject_Repository;
        private ProductCategory_Repository _IProductCategory_Repository;
        private ScopeOfWork_Repository _IScopeOfWork_Repository;
        private Scope_Repository _IScope_Repository;
        private Document_Repository _IDocument_Repository;
        private StatusLog_Repository _IStatusLog_Repository;


        public RAWSController()
        {
            this._IRAWS_Repository = new RAWS_Repository();
            this._IProject_Repository = new Project_Repository();
            this._IProductCategory_Repository = new ProductCategory_Repository();
            this._IScopeOfWork_Repository = new ScopeOfWork_Repository();
            this._IScope_Repository = new Scope_Repository();
            this._IDocument_Repository = new Document_Repository();
            this._IStatusLog_Repository = new StatusLog_Repository();

        }

        // GET: RAWS
        public ActionResult Index()
        {
            var SessionRole = Session["RoleId"];
            var SessionUser = Session["UserId"];
            if (SessionRole == null || SessionUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpGet]
        public ActionResult ManageRAWS(int id = 0)
        {
            try
            {
                var SessionRole = Session["RoleId"];
                var SessionUser = Session["UserId"];
                if (SessionRole == null || SessionUser == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    LNTSlipPortal_Repository.Data.RAW obj = new LNTSlipPortal_Repository.Data.RAW();
                    if (id > 0)
                    {
                        obj = _IRAWS_Repository.GetRAWSByID(id);
                        obj.ProjectName = obj.ProjectId != null ? _IProject_Repository.GetProjectByID((int)obj.ProjectId).ProjectName : null;

                        obj.ProductCatNo = obj.ProductCatId != null ? _IProductCategory_Repository.GetProductCategoryByID((int)obj.ProductCatId).ProductCatNo.ToString() : null;

                        obj.ScopeName = obj.ScopeId != null ? _IScope_Repository.GetScopeByID((int)obj.ScopeId).ScopeName : null;

                        var objScopeOfWork = _IScopeOfWork_Repository.GetScopeOfWorkByID((int)obj.ScopeOfWorkId);
                        if (objScopeOfWork != null)
                        {
                            obj.Grinding = objScopeOfWork.Grinding;
                            obj.PaintTouchUp = objScopeOfWork.PaintTouchUp;
                            obj.Replacement = objScopeOfWork.Replacement;
                            obj.Testing = objScopeOfWork.Testing;
                            obj.Assembly = objScopeOfWork.Assembly;
                            obj.Other = objScopeOfWork.Other;
                        }

                        var objDocument = _IDocument_Repository.GetDocumentByID((int)obj.DocumentId);
                        if (objDocument != null)
                        {
                            obj.BOM = objDocument.BOM;
                            obj.AssemblyList = objDocument.AssemblyList;
                            obj.GeneralAssembly = objDocument.GeneralAssembly;
                            obj.TestingDocument = objDocument.TestingDocument;
                            obj.WiringScheme = objDocument.WiringScheme;
                            obj.ProductData = objDocument.ProductData;
                            obj.BOMRevision = objDocument.BOMRevision;
                            obj.AssemblyListRevision = objDocument.AssemblyListRevision;
                            obj.GeneralAssemblyRevision = objDocument.GeneralAssemblyRevision;
                            obj.TestingDocumentRevision = objDocument.TestingDocumentRevision;
                            obj.WiringSchemeRevision = objDocument.WiringSchemeRevision;
                            obj.ProductDataRevision = objDocument.ProductDataRevision;
                            obj.BOMCatNo = objDocument.BOMCatNo;
                            obj.AssemblyListCatNo = objDocument.AssemblyListCatNo;
                            obj.GeneralAssemblyCatNo = objDocument.GeneralAssemblyCatNo;
                            obj.TestingDocumentCatNo = objDocument.TestingDocumentCatNo;
                            obj.WiringSchemeCatNo = objDocument.WiringSchemeCatNo;
                            obj.ProductDataCatNo = objDocument.ProductDataCatNo;
                        }
                        ViewBag.Check = "Edit";
                    }
                    else
                    {

                        Random r = new Random();
                        obj.RAWSNO = r.Next(1000000, 99999999).ToString();
                        obj.JigFixtures_SpecialTools = true;
                        ViewBag.Check = "Add";
                    }
                    return View(obj);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("ManageEmployee,id=0");
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult ManageRAWS(LNTSlipPortal_Repository.Data.RAW obj)
        {
            var ObjOld = new LNTSlipPortal_Repository.Data.RAW();
            var ObjProject = new LNTSlipPortal_Repository.Data.Project();
            var ObjProductCategory = new LNTSlipPortal_Repository.Data.ProductCategory();
            var ObjScope = new LNTSlipPortal_Repository.Data.Scope();
            try
            {
                if (!ModelState.IsValid || obj.TotalManHours <= 0 || obj.NCRNo <= 0 || obj.TotalQuantity <= 0 || (obj.Status ==(int)RAWSStatus.Validated_by_Planner && (obj.ActualManHours<=0 || obj.ActualManHours==null)))
                {
                    if (obj.NCRNo <= 0)
                        ModelState.AddModelError("NCRNo", "NCR No must be greater than 0");

                    if (obj.ActualManHours == null && obj.Status == (int)RAWSStatus.Validated_by_Planner)
                        ModelState.AddModelError("ActualManHours", "Actual Man Days can't be blank");

                    else if (obj.ActualManHours <= 0 && obj.Status == (int)RAWSStatus.Validated_by_Planner)
                        ModelState.AddModelError("ActualManHours", "Actual Man Days No must be greater than 0");

                    if (obj.TotalManHours <= 0)
                        ModelState.AddModelError("TotalManHours", "Total Man Days must be greater than 0");

                    if (obj.TotalQuantity <= 0)
                        ModelState.AddModelError("TotalQuantity", "Total Quantity must be greater than 0");

                    if (obj.ProjectId != null && obj.ProjectId != 0)
                    {
                        ObjProject = _IProject_Repository.GetProjectByID((int)obj.ProjectId);
                        obj.ProjectName = ObjProject.ProjectName;
                        obj.ProjectId = obj.ProjectId;
                    }
                    if (obj.ProductCatId != null && obj.ProductCatId != 0)
                    {
                        ObjProductCategory = _IProductCategory_Repository.GetProductCategoryByID((int)obj.ProductCatId);
                        obj.ProductCatNo = ObjProductCategory.ProductCatNo.ToString();
                        obj.ProductCatId = obj.ProductCatId;
                    }
                    if (obj.ScopeId != null && obj.ScopeId != 0)
                    {
                        ObjScope = _IScope_Repository.GetScopeByID((int)obj.ScopeId);
                        obj.ScopeName = ObjScope.ScopeName;
                        obj.ScopeId = obj.ScopeId;
                    }
                    if (obj.RAWSID == 0)
                    {
                        ViewBag.Check = "Add";
                    }
                    else
                    {
                        ViewBag.Check = "Edit";
                    }
                    return PartialView(obj);
                }

                if (obj.RAWSID == 0)
                {
                    // Document
                    var dataDocument = SaveDocument(obj);
                    obj.DocumentId = dataDocument.DocumentId;

                    // Scope Of Work
                    var dataScopeOfWork = SaveScopeOfWork(obj);
                    obj.ScopeOfWorkId = dataScopeOfWork.ScopeOfWorkId;
                    obj.IsActive = true;
                    obj.Status = (int)RAWSStatus.Draft;

                    obj.CreatedBy = Convert.ToInt32(Session["UserId"]);
                    obj.CreatedAt = DateTime.Now;
                    var objRAWSData = _IRAWS_Repository.InsertRAWS(obj);

                    var objStatusLog = new LNTSlipPortal_Repository.Data.StatusLog
                    {
                        UserId = Convert.ToInt32(Session["UserId"]),
                        RAWSID = objRAWSData.RAWSID,
                        StatusId = (int)RAWSStatus.Draft,
                        CreatedAt = DateTime.Now,

                    };
                    _IStatusLog_Repository.InsertStatusLog(objStatusLog);

                    ViewBag.Check = "Add";
                }
                else
                {
                    ViewBag.Check = "Edit";
                    ObjOld = _IRAWS_Repository.GetAllRAWS().Where(x => x.RAWSID == obj.RAWSID).FirstOrDefault();

                    ObjOld.ProjectId = obj.ProjectId;
                    ObjOld.ProductCatId = obj.ProductCatId;
                    ObjOld.ProductDescription = obj.ProductDescription;
                    ObjOld.TotalManHours = obj.TotalManHours;
                    ObjOld.TotalQuantity = obj.TotalQuantity;
                    ObjOld.JigFixtures_SpecialTools = obj.JigFixtures_SpecialTools;
                    ObjOld.AdditionalWork = obj.AdditionalWork;
                    ObjOld.Work = obj.Work;
                    ObjOld.RAWSNO = obj.RAWSNO;
                    ObjOld.NCRNo = obj.NCRNo;
                    ObjOld.JigFixtures_Detail = obj.JigFixtures_Detail;
                    ObjOld.ActualManHours = obj.ActualManHours;
                    var objScopeOfWork = SaveScopeOfWork(obj);
                    var objDocument = SaveDocument(obj);

                    _IRAWS_Repository.UpdateRAWS(ObjOld);
                    ViewBag.Check = "Edit";
                }
                return RedirectToAction("Index", "RAWS");
            }

            catch (Exception ex)
            {
                ex.SetLog("ManageRAWS,IU");
                throw ex;
            }
        }

        public Document SaveDocument(RAW obj)
        {
            var ObjDocument = new LNTSlipPortal_Repository.Data.Document
            {
                BOM = obj.BOM,
                AssemblyList = obj.AssemblyList,
                GeneralAssembly = obj.GeneralAssembly,
                TestingDocument = obj.TestingDocument,
                ProductData = obj.ProductData,
                WiringScheme = obj.WiringScheme,
                BOMRevision = obj.BOMRevision,
                AssemblyListRevision = obj.AssemblyListRevision,
                GeneralAssemblyRevision = obj.GeneralAssemblyRevision,
                TestingDocumentRevision = obj.TestingDocumentRevision,
                ProductDataRevision = obj.ProductDataRevision,
                WiringSchemeRevision = obj.WiringSchemeRevision,
                BOMCatNo = obj.BOMCatNo,
                AssemblyListCatNo = obj.AssemblyListCatNo,
                GeneralAssemblyCatNo = obj.GeneralAssemblyCatNo,
                TestingDocumentCatNo = obj.TestingDocumentCatNo,
                ProductDataCatNo = obj.ProductDataCatNo,
                WiringSchemeCatNo = obj.WiringSchemeCatNo,
            };
            if (obj.DocumentId > 0)
            {
                ObjDocument.DocumentId = (int)obj.DocumentId;
                return _IDocument_Repository.UpdateDocument(ObjDocument);
            }
            else
                return _IDocument_Repository.InsertDocument(ObjDocument);
        }

        [HttpPost]
        public ActionResult DeleteRAWS(int Id)
        {
            try
            {
                var userRole = Session["RoleId"];
                if (userRole !=null && (Convert.ToInt16(userRole) == (int)Role.PMG1 || Convert.ToInt16(userRole) == (int)Role.SCG2))
                {
                    _IRAWS_Repository.DeleteRAWS(Id);
                    return Json("OK");
                }
                else
                {
                    return Json("Error");
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("DeleteEmployee,id=" + Id);
                throw ex;
            }
        }

        public ScopeOfWork SaveScopeOfWork(RAW obj)
        {
            var ObjScopeOfWork = new LNTSlipPortal_Repository.Data.ScopeOfWork
            {
                Grinding = obj.Grinding,
                PaintTouchUp = obj.PaintTouchUp,
                Replacement = obj.Replacement,
                Assembly = obj.Assembly,
                Testing = obj.Testing,
                Other = obj.Other,
            };

            if (obj.ScopeOfWorkId > 0)
            {
                ObjScopeOfWork.ScopeOfWorkId = (int)obj.ScopeOfWorkId;

                return _IScopeOfWork_Repository.UpdateScopeOfWork(ObjScopeOfWork);
            }
            else
                return _IScopeOfWork_Repository.InsertScopeOfWork(ObjScopeOfWork);
        }

        public ActionResult RAWSReport(int id = 0)
        {
            var SessionRole = Session["RoleId"];
            var SessionUser = Session["UserId"];
            if (SessionRole == null || SessionUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var obj = _IRAWS_Repository.GetRAWSByIDForReport(id);
            return View(obj);
        }

        [HttpPost]
        public ActionResult ChangeRAWSStatus(int id, int statusid)
        {
            try
            {
                var obj = _IRAWS_Repository.GetRAWSByID(id);
                switch (statusid)
                {
                    case (int)RAWSStatus.Draft:
                        obj.Status = (int)RAWSStatus.Initiated;
                        break;
                    case (int)RAWSStatus.Initiated:
                        obj.Status = (int)RAWSStatus.Approved_by_PMG_SCG;
                        break;
                    case (int)RAWSStatus.Approved_by_PMG_SCG:
                        obj.Status = (int)RAWSStatus.Acknowledged_by_QC;
                        break;
                    case (int)RAWSStatus.Acknowledged_by_QC:
                        obj.Status = (int)RAWSStatus.Accepted_by_Shop;
                        break;
                    case (int)RAWSStatus.Accepted_by_Shop:
                        obj.Status = (int)RAWSStatus.Validated_by_Planner;
                        break;
                    case (int)RAWSStatus.Validated_by_Planner:
                        obj.Status = (int)RAWSStatus.Checked_verified;
                        break;
                    case (int)RAWSStatus.Checked_verified:
                        obj.Status = (int)RAWSStatus.Closed;
                        break;
                    default:
                        break;
                }
                _IRAWS_Repository.UpdateRAWS(obj);
                var objStatusLog = new LNTSlipPortal_Repository.Data.StatusLog
                {
                    UserId = Convert.ToInt32(Session["UserId"]),
                    RAWSID = obj.RAWSID,
                    StatusId = obj.Status,
                    CreatedAt = DateTime.Now,
                };
                _IStatusLog_Repository.InsertStatusLog(objStatusLog);

                return Json("OK");
            }
            catch (Exception ex)
            {
                ex.SetLog("ChangeStatusRAWS ,id=" + id);
                throw ex;
            }
        }

    }
}