using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace ExamPoppers.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/commoncss")
                .Include("~/Content/bootstrap.min.css")
                .Include("~/Content/bootstrap-theme.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/commonjavascript")
                .Include("~/Scripts/jquery-2.1.4.min.js")
                .Include("~/Scripts/bootstrap.min.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
