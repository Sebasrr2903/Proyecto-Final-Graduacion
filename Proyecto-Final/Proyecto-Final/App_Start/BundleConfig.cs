using System.Web;
using System.Web.Optimization;

namespace Proyecto_Final
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

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));     
            
            bundles.Add(new StyleBundle("~/Content/css1").Include(
                      "~/Content/css/bootstrap-Material-design.min.css",
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/jquery.mCustomScrolbar.css",
                      "~/Content/css/main.css",
                      "~/Content/css/material-design-iconic-font.min.css",
                      "~/Content/css/ripples.min.css",
                      "~/Content/css/sweetalert2.css")); 

            bundles.Add(new ScriptBundle("~/bundles/js1").Include(
                      "~/Scripts/js/bootstrap.min.js",
                      "~/Scripts/js/jquery-3.1.1.min.js",
                      "~/Scripts/js/jquery.mCustomScrolbar.concat.min.js",
                      "~/Scripts/js/main.js",
                      "~/Scripts/js/material.mi.js",
                      "~/Scripts/js/ripples.min.js",
                      "~/Scripts/js/sweetalert2.js"));

        }
    }
}
