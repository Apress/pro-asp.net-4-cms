using System;
using System.Web;
using Business.Operations;

namespace GlobalModule
{
    public class Global : IHttpModule
    {
        public string ModuleName
        {
            get { return "Global"; }
        }

        #region IHttpModule Members

        public void Init(HttpApplication application)
        {
            application.BeginRequest += (Application_BeginRequest);
        }

        public void Dispose()
        {
        }

        #endregion

        private void Application_BeginRequest(Object source, EventArgs e)
        {
            var request = HttpContext.Current.Request.RawUrl.ToLower();
            var host = HttpContext.Current.Request.Url.Host;
            var business = new Rewrite();

            // all excluded types should signal an exit from the rewrite pipeline
            if (business.IsExcludedRequest(request)) return;

            // attempt to pull up a primary URL by checking aliases
            string foundPrimaryViaAlias = business.GetPrimaryUrlByAlias(request, host);

            // if we found a primary, indicating this was an alias, redirect to the primary
            if (!String.IsNullOrEmpty(foundPrimaryViaAlias))
            {
                HttpContext.Current.Response.RedirectPermanent(foundPrimaryViaAlias);
            }

            // attempt to pull up a content ID by checking the primary URLs
            Guid? contentID = business.GetIDByPrimaryUrl(request, host);

            if (contentID != null)
            {
                HttpContext.Current.RewritePath("/content.aspx?id=" + contentID, false);
            }
            else
            {
                // unknown URLs will simply default to the homepage for now
                HttpContext.Current.Response.Redirect("/");
            }
        }
    }
}