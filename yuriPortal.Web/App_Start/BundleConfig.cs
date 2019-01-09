using System.Web.Optimization;

namespace yuriPortal.Web
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
							"~/Content/bootstrap.css",
							"~/Content/site.css"));

			//Modernizr Ver1
			bundles.Add(new ScriptBundle("~/bundles/modernizr/ver1").Include(
							"~/Scripts/modernizr/modernizr.js"));

			//Default Layout Css
			bundles.Add(new StyleBundle("~/Css/theme").Include(
							"~/Content/bootstrap/css/bootstrap.css",
							"~/Content/font-awesome/css/font-awesome.css",
							"~/Content/magnific-popup/magnific-popup.css",
							"~/Content/bootstrap-datepicker/css/datepicker3.css",
							"~/Css/theme.css",
							"~/Css/skins/default.css",
							"~/Css/theme-custom.css"));

			//Default Layout Javascript
			bundles.Add(new StyleBundle("~/Script/theme").Include(
							"~/Scripts/modernizr/modernizr.js",
							"~/Scripts/jquery/jquery.js",
							"~/Scripts/jquery-browser-mobile/jquery.browser.mobile.js",
							"~/Content/bootstrap/js/bootstrap.js",
							"~/Scripts/nanoscroller/nanoscroller.js",
							"~/Content/bootstrap-datepicker/js/bootstrap-datepicker.js",
							"~/Content/magnific-popup/magnific-popup.js",
							"~/Scripts/jquery-placeholder/jquery.placeholder.js",
							"~/javascripts/theme.js",
							"~/javascripts/theme.custom.js",
							"~/javascripts/theme.init.js"));

		}
	}
}
