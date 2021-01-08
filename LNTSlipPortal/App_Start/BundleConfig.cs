using System.Web;
using System.Web.Optimization;

namespace LNTSlipPortal
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/assets").Include(
                //"~/assets/css/custom.css",
                //"~/assets/css/responsive.css",
                 "~/assets/css/app.min.css",
                 "~/assets/css/bootstrap.min.css",
                 "~/assets/libs/metrojs/release/MetroJs.Full/MetroJs.min.css",
                 "~/assets/css/icons.min.css"));

            bundles.Add(new StyleBundle("~/datatable/css").Include(
               "~/assets/libs/datatables.net-bs4/css/dataTables.bootstrap4.min.css",
               "~/assets/libs/datatables.net-buttons-bs4/css/buttons.bootstrap4.min.css",
               "~/assets/libs/metrojs/release/MetroJs.Full/MetroJs.min.css",
               "~/assets/libs/datatables.net-responsive-bs4/css/responsive.bootstrap4.min.css"));

            bundles.Add(new ScriptBundle("~/assets/").Include(
              //  "~/assets/libs/jquery/jquery.min.js",
                "~/assets/libs/bootstrap/js/bootstrap.bundle.min.js",
                "~/assets/libs/metismenu/metisMenu.min.js",
                "~/assets/libs/simplebar/simplebar.min.js",
                "~/assets/libs/node-waves/waves.min.js",
                "~/assets/libs/morris.js/morris.min.js",
                "~/assets/libs/raphael/raphael.min.js",
                "~/assets/libs/jquery-knob/jquery.knob.min.js",
               "~/assets/libs/metrojs/release/MetroJs.Full/MetroJs.min.js",
                "~/assets/js/pages/dashboard.init.js"//,
              //  "~/assets/js/app.js"
               ));


            bundles.Add(new ScriptBundle("~/datatable/js").Include(
                "~/assets/libs/datatables.net/js/jquery.dataTables.min.js",
                "~/assets/libs/datatables.net-bs4/js/dataTables.bootstrap4.min.js",
                "~/assets/libs/datatables.net-buttons/js/dataTables.buttons.min.js",
                "~/assets/libs/datatables.net-buttons-bs4/js/buttons.bootstrap4.min.js",
                "~/assets/libs/jszip/jszip.min.js",
                "~/assets/libs/pdfmake/build/pdfmake.min.js",
                "~/assets/libs/pdfmake/build/vfs_fonts.js",
                "~/assets/libs/datatables.net-buttons/js/buttons.html5.min.js",
               "~/assets/libs/datatables.net-buttons/js/buttons.print.min.js",
                "~/assets/libs/datatables.net-buttons/js/buttons.colVis.min.js",
                "~/assets/libs/datatables.net-responsive/js/dataTables.responsive.min.js",
                "~/assets/libs/datatables.net-responsive-bs4/js/responsive.bootstrap4.min.js",
                "~/assets/js/pages/datatables.init.js"
               ));
        }
    }
}
