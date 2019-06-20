using System.Web;
using System.Web.Optimization;

namespace JewelryStore.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-theme.css",
                      "~/Content/site.css"));
            //bundles.Add(new ScriptBundle("~/bundles/mybundle").Include(
            //"~/plugins/bootstrap/js/bootstrap.js",
            //"~/plugins/countdown/jquery.syotimer.js",
            //"~/plugins/jquery-ui/jquery-ui.js",
            //"~/plugins/owl-carousel/owl-carousel.js",
            //"~/plugins/rs-plugib/jquery.themepunch.enablelog.js",
            //"~/plugins/rs-plugib/jquery.themepunch.revolution.js",
            //"~/plugins/rs-plugib/jquery.themepunch.revolution.min.js",
            //"~/plugins/rs-plugib/jquery.themepunch.tools.min.js",
            //"~/plugins/selectbox/jquery.selectbox-0.1.3.min.js",
            //"~/plugins/smoothscroll/Smoothscroll.min.js",
            //"~/plugins/smoothscroll/Smoothscroll.js"));

            //bundles.Add(new StyleBundle("~/css/css").Include(
            //    "~/css/style.css",
            //    "~/css/collor-option3.css"));

        }
    }
}
