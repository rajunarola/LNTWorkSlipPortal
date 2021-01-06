using LNTSlipPortal.Models;
using LNTSlipPortal_Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LNTSlipPortal.Controllers
{
    public class HomeController : Controller
    {
        private Employee_Repository _IEmployee_Repository;
        private RAWS_Repository _IRAWS_Repository;
        private Project_Repository _IProject_Repository;

        public HomeController()
        {
            this._IEmployee_Repository = new Employee_Repository();
            this._IRAWS_Repository = new RAWS_Repository();
            this._IProject_Repository = new Project_Repository();
        }
        public ActionResult Index()
        {
            var SessionRole = Session["RoleId"];
            var SessionUser = Session["UserId"];
            if (SessionRole == null || SessionUser == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var obj = new DashboardDTO();
            obj.EmployeeCount = _IEmployee_Repository.GetAllEmployees().Count();
            obj.RAWSCount = _IRAWS_Repository.GetAllRAWSCountbyRole(Convert.ToInt16(SessionRole));
            obj.ProjectCount = _IProject_Repository.GetAllProjects().Count();
            return View(obj);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpPost]
        public string GetGridData()
        {
            string mode = Convert.ToString(Request.Form["mode"]);
            var userRole = Session["RoleId"];
            GridData og = new GridData(mode,false,Convert.ToInt16(userRole));
            return og.JsonData;
        }

        [HttpGet]
        public JsonResult AutoCompleteData(string q, string mode, string relatedTo)
        {
            Models.AutoCompleteData obj = new Models.AutoCompleteData(mode, q, relatedTo);

            //SamplePOC_Repository.DataServices.dalc odal = new SamplePOC_Repository.DataServices.dalc();
            //System.Data.DataTable dt = odal.selectbyquerydt("SELECT SupplierGroupId,SupplierGroup FROM suppliergroupmaster WHERE SupplierGroup LIKE '%" + q + "%'");
            //List<AutoComplete> lst = new List<AutoComplete>();
            //foreach (System.Data.DataRow dr in dt.Rows)
            //{
            //    lst.Add(new AutoComplete(Convert.ToString(dr[1]), Convert.ToString(dr[0])));
            //}
            return Json(obj.GetAutocompleteData(), JsonRequestBehavior.AllowGet);
        }
    }
    public class AutoComplete
    {
        public string text { get; set; }
        public string id { get; set; }
        public AutoComplete(string t, string v)
        {
            this.text = t;
            this.id = v;
        }
    }
}