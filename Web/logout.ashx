<%@ WebHandler Language="C#" Class="logout" %>

using System.Web;
using System.Web.Security;

public class logout : IHttpHandler
{
    #region IHttpHandler Members

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        FormsAuthentication.SignOut();
        context.Response.Redirect("~/login.aspx");
    }

    public bool IsReusable
    {
        get { return false; }
    }

    #endregion
}