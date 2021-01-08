using LNTSlipPortal.Models;
using LNTSlipPortal_Repository.Data;
using LNTSlipPortal_Repository.Service;
using LNTSlipPortal_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LNTSlipPortal.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private IUser_Repository _IUser_Repository;
        private IRole_Repository _IRole_Repository;
        public LoginController()
        {
            this._IUser_Repository = new User_Repository(new LNTSlipPortal_Repository.Data.LNTSlipPortalEntities());
            this._IRole_Repository = new Role_Repository(new LNTSlipPortal_Repository.Data.LNTSlipPortalEntities());
        }
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            Session["UserId"] = null;
            Session["RoleId"] = null;
            return RedirectToAction("Index", "Login");
        }


        [HttpPost]
        public ActionResult Index(UserMaster obj)
        {
            if (ModelState.IsValid && (obj.PSNumber > 0 && !string.IsNullOrEmpty(obj.Password)))
            {
                Common cm = new Common();
                var objUser = _IUser_Repository.LogInUsers(obj.PSNumber, obj.Password);
                if (objUser != null)
                {
                    Session["RoleId"] = objUser.RoleId.ToString();
                    Session["UserId"] = objUser.UserId.ToString();
                    Session["LoggedUserName"] = objUser.UserName.ToString();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMsg = "Enter Valid PSNumber & Password";
                }
            }

            return View();
        }

    }
}