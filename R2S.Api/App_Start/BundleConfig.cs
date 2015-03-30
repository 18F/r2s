using System.Web;
using System.Web.Optimization;

namespace R2S.Api
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

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/Scripts/fastclick.js",
                        "~/Scripts/sweet-alert.min.js",
                        "~/Scripts/app.js"));
            
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/normalize.css",
                      "~/Content/sweet-alert.css",
                      "~/Content/main.css"));

            /* Example `StyleBundle`:
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            */
        }
    }
}
