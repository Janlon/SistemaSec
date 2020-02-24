using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"
                       //,"~/Scripts/jcrop-v0.9.12/js/jquery.Jcrop.min.js"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/bootstrap.css",
                     "~/Content/Site.css",
                     "~/Scripts/jcrop-v0.9.12/css/jquery.Jcrop.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/Web").Include(
               "~/Scripts/vanilla-masker.min.js",
                "~/Scripts/telerik.grid.notifications.js",
                "~/Scripts/moment.js",
                "~/Scripts/Validacoes.js"
                //"~/Scripts/jcrop-v0.9.12/css/jquery.Jcrop.min.css"
                ));

           
    }
    }
}
