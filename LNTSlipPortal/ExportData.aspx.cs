using LNTSlipPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LNTSlipPortal
{
    public partial class ExportData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                if (!string.IsNullOrEmpty(Request.Form["mode"]))
                {
                    try
                    {
                        var userRole = Session["RoleId"];
                        string mode = Convert.ToString(Request.Form["mode"]);
                        GridData og = new GridData(mode, true, Convert.ToInt16(userRole));
                    }
                    catch (Exception ex)
                    {
                        //ex.SetLog("For Export Data");
                    }
                    finally
                    {
                        Response.Write("<script>window.parent.document.getElementsByClassName('dataTables_processing')[0].style.display='none';</script>");
                    }
                }
            }
        }
    }
}