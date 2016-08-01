using System.Web;
using System.Web.Optimization;

namespace Plus54PortfolioRedesign2014.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Admin Bundles

            #region JS Frameworks

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/areas/admin/js/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/areas/admin/js/jquery/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/areas/admin/js/jquery/jquery.unobtrusive*",
                        "~/areas/admin/js/jquery/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/areas/admin/js/vendor/bootstrap*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/areas/admin/js/vendor/modernizr-*"));

            #endregion

            #region JS Component

            bundles.Add(new ScriptBundle("~/bundles/FileUpload").Include(
                        "~/areas/admin/js/FileUpload/tmpl.js",
                        "~/areas/admin/js/FileUpload/canvas-to-blob.js",
                        "~/areas/admin/js/FileUpload/load-image.js",
                        "~/areas/admin/js/FileUpload/jquery.iframe-transport.js",
                        "~/areas/admin/js/FileUpload/jquery.fileupload.js",
                        "~/areas/admin/js/FileUpload/jquery.fileupload-ip.js",
                        "~/areas/admin/js/FileUpload/jquery.fileupload-ui.js",
                        "~/areas/admin/js/FileUpload/locale.js",
                        "~/areas/admin/js/FileUpload/fileupload.helper.js"));

            bundles.Add(new ScriptBundle("~/bundles/DataTable").Include(
                        "~/areas/admin/js/DataTable/js/jquery.dataTables.js",
                        "~/areas/admin/js/DataTable/js/jquery.dataTables.custom.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/MiscJS").Include(
                        "~/areas/admin/js/ajax-helper.js",
                        "~/areas/admin/js/main.js",
                        "~/areas/admin/js/misc-functions.js"));


            bundles.Add(new ScriptBundle("~/bundles/Chosen").Include(
                        "~/areas/admin/js/chosen/chosen.jquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/PopupJS").Include("~/areas/admin/js/popupAdmin.js"));

            bundles.Add(new ScriptBundle("~/bundles/TabsJS").Include("~/areas/admin/js/tabs.js"));

            bundles.Add(new ScriptBundle("~/bundles/ImportJS").Include("~/areas/admin/js/importfiles.js"));

            #endregion

            #region JS Pages

            bundles.Add(new ScriptBundle("~/bundles/LoginJS").Include("~/areas/admin/js/pages/login.js"));
            bundles.Add(new ScriptBundle("~/bundles/TechnologyJS").Include("~/areas/admin/js/pages/technology.js"));
            bundles.Add(new ScriptBundle("~/bundles/ProjectCategoryJS").Include("~/areas/admin/js/pages/projectcategory.js"));
            bundles.Add(new ScriptBundle("~/bundles/ScreenSupportJS").Include("~/areas/admin/js/pages/screensupport.js"));
            bundles.Add(new ScriptBundle("~/bundles/SocialMediaJS").Include("~/areas/admin/js/pages/socialmedia.js"));
            bundles.Add(new ScriptBundle("~/bundles/ProjectJS").Include("~/areas/admin/js/pages/project.js"));

            #endregion

            #region CSS Component

            bundles.Add(new StyleBundle("~/css").Include(
                "~/areas/admin/css/main.css",
                "~/areas/admin/css/demo_page.css",
                "~/areas/admin/css/demo_table.css",
                "~/areas/admin/css/demo_table_jui.css",
                "~/areas/admin/css/dataTables/jquery.dataTables.css",
                "~/areas/admin/css/dataTables/jquery.dataTables_themeroller.css",
                "~/areas/admin/css/smoothness/jquery-ui-1.8.4.custom.css",
                "~/areas/admin/css/main-custom.css"));

            bundles.Add(new StyleBundle("~/css/bootstrap").Include(
                "~/areas/admin/css/bootstrap/bootstrap.css",
                "~/areas/admin/css/bootstrap/bootstrap-select.css",
                "~/areas/admin/css/bootstrap/bootstrap-responsive.css"));

            bundles.Add(new StyleBundle("~/css/FileUpload").Include(
                "~/areas/admin/css/FileUpload/jquery.fileupload-ui.css"));

            bundles.Add(new StyleBundle("~/css/chosen").Include(
                "~/areas/admin/css/chosen/chosen.css"));

            #endregion

            #endregion


            #region Front Bundles

            bundles.Add(new ScriptBundle("~/bundles/foundationFE").Include(
                       "~/js/foundation/foundation*",
                       "~/js/foundation/jquery.offcanvas.js"));

            bundles.Add(new ScriptBundle("~/bundles/mainFE").Include("~/js/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/fastclickFE").Include("~/js/fastclick.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryFE").Include("~/js/jquery*"));

            bundles.Add(new ScriptBundle("~/bundles/placeholderFE").Include("~/js/placeholder.js"));   

            bundles.Add(new StyleBundle("~/stylesheets").Include(
                            "~/css/normalize.css",
                            "~/css/foundation.css",
                            "~/css/offcanvas.css",
                            "~/css/main.css"));

            #endregion
        }

    }
}