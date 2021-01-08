using LNTSlipPortal_Repository.Data;
using LNTSlipPortal_Repository.DataServices;
using LNTSlipPortal_Repository.Service;
using LNTSlipPortal_Repository.ServiceContract;
using LNTSlipPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LNTSlipPortal.Controllers
{
    public class EmployeeController : Controller
    {
        private Employee_Repository _IEmployee_Repository;
        private Role_Repository _IRole_Repository;

        public EmployeeController()
        {
            this._IEmployee_Repository = new Employee_Repository();
            this._IRole_Repository = new Role_Repository();

        }
        // GET: Employee
        public ActionResult Index()
        {
            var SessionRole = Session["RoleId"];
            var SessionUser = Session["UserId"];
            if (SessionRole==null || SessionUser == null)
            {
                return RedirectToAction("Index", "Login");
            }else if (Convert.ToInt32(SessionRole) == (int)Common.Role.Admin)
            {
                return View();
            }
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public ActionResult ManageEmployee(LNTSlipPortal_Repository.Data.EmployeeMaster obj)
        {
            LNTSlipPortal_Repository.Data.EmployeeMaster ObjOld = new LNTSlipPortal_Repository.Data.EmployeeMaster();
            LNTSlipPortal_Repository.Data.RoleMaster ObjRole = new LNTSlipPortal_Repository.Data.RoleMaster();
            try
            {
                
                if (!ModelState.IsValid || obj.PSNumber <= 0)
                {
                    if (obj.PSNumber <= 0)
                        ModelState.AddModelError("PSNumber", "PSNumber must be greater than 0");

                    if (obj.RoleId != 0)
                    {
                        ObjRole = _IRole_Repository.GetRoleByID(obj.RoleId);
                        ObjOld.RoleName = ObjRole.RoleName;
                        ObjOld.RoleId = obj.RoleId;
                    }
                    if (obj.EmployeeId == 0)
                    {
                        ViewBag.Check = "Add";
                    }
                    else
                    {
                        ViewBag.Check = "Edit";
                    }
                    return PartialView(ObjOld);
                }

                if (obj.EmployeeId == 0)
                {
                    if (!_IEmployee_Repository.IsDuplicate(obj.EmployeeName, obj.EmployeeId))
                    {
                        obj.IsDelete = false;
                        obj.IsActive = true;
                        obj.CreatedBy = Convert.ToInt32(Session["UserId"]);
                        obj.CreatedAt = DateTime.Now;
                        _IEmployee_Repository.InsertEmployee(obj);
                        ViewBag.Check = "Add";
                    }
                    else
                    {
                        ModelState.AddModelError("EmployeeName", "This EmployeeName Is Already Exists");
                        ViewBag.Check = "Add";
                        return PartialView(obj);
                    }
                }
                else
                {
                    ViewBag.Check = "Edit";

                    if (!_IEmployee_Repository.IsDuplicate(obj.EmployeeName, obj.EmployeeId))
                    {
                        ObjOld = _IEmployee_Repository.GetEmployeeByID(obj.EmployeeId);//.Where(x => x.EmployeeId == obj.EmployeeId).FirstOrDefault();
                        ObjOld.EmployeeName = obj.EmployeeName;
                        ObjOld.RoleId = obj.RoleId;
                        ObjOld.PSNumber = obj.PSNumber;

                        if (obj.RoleId != 0)    
                        {
                            ObjRole = _IRole_Repository.GetRoleByID(obj.RoleId);
                            ObjOld.RoleName = ObjRole.RoleName;
                            ObjOld.RoleId = obj.RoleId;
                        }
                        else
                        {
                            ModelState.AddModelError("RoleId", "This Role is Required");
                            return PartialView(obj);
                        }
                        _IEmployee_Repository.UpdateEmployee(ObjOld);
                        ViewBag.Check = "Edit";
                    }
                    else
                    {
                        ModelState.AddModelError("EmployeeName", "This EmployeeName Is Already Exists");
                        return PartialView(obj);
                    }
                }
                return RedirectToAction("Index", "Employee");
            }
            catch (Exception ex)
            {
                ex.SetLog("ManageEmployee,IU");
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult ManageEmployee(int id = 0)
        {
            try
            {
                var SessionRole = Session["RoleId"];
                var SessionUser = Session["UserId"];
                if (SessionRole == null || SessionUser == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                else if (Convert.ToInt32(SessionRole) == (int)Common.Role.Admin)
                {
                    LNTSlipPortal_Repository.Data.EmployeeMaster obj = new LNTSlipPortal_Repository.Data.EmployeeMaster();
                    if (id > 0)
                    {
                        obj = _IEmployee_Repository.GetEmployeeByID(id);
                        obj.RoleName = _IRole_Repository.GetRoleByID(obj.RoleId).RoleName;
                        ViewBag.Check = "Edit";
                    }
                    else
                    {
                        ViewBag.Check = "Add";
                    }
                    return View(obj);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ex.SetLog("ManageEmployee,id=0");
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult DeleteEmployee(int Id)
        {
            try
            {
                _IEmployee_Repository.DeleteEmployee(Id);
                return Json("OK");
            }
            catch (Exception ex)
            {
                ex.SetLog("DeleteEmployee,id=" + Id);
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult ActiveEmployee(int Id)
        {
            try
            {
                var obj = _IEmployee_Repository.GetEmployeeByID(Id);
                obj.IsActive = obj.IsActive ? false : true;
                _IEmployee_Repository.UpdateEmployee(obj);
                return Json("OK");
            }
            catch (Exception ex)
            {
                ex.SetLog("DeleteEmployee,id=" + Id);
                throw ex;
            }
        }

        protected override void Dispose(bool disposing)
        {
            _IEmployee_Repository.Dispose();
            _IRole_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}