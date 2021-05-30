using System.Web;
using System.Web.Optimization;

namespace TEST_DEV
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/global").Include(
                "~/Scripts/uikit/uikit.min.js",
                "~/Scripts/uikit/uikit-icons.min.js",
                "~/Scripts/site.js"));

            bundles.Add(new ScriptBundle("~/bundles/axios").Include(
                "~/Scripts/axios/axios.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/uikit/uikit.min.css",
                "~/Content/site.css"));
        }
    }
}
