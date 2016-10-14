using System.Web.UI;
using System.Web.UI.WebControls;

namespace Business.Admin
{
    public static class Disabler
    {
        /// <summary>
        /// Disables the site drop down control.
        /// </summary>
        /// <param name="page">The current page object.</param>
        public static void DisableSiteDropDown(Page page)
        {
            dynamic siteCtrl = page.Master.FindControl("drpSitesCtrl");
            var siteDdl = (DropDownList) siteCtrl.FindControl("drpSites");
            siteDdl.Enabled = false;
        }
    }
}