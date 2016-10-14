using System;
using System.IO;
using System.Web;
using System.Web.UI;
using Business.Operations;

public partial class content : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == null) return;

        var id = new Guid(Request.QueryString["id"]);

        // it's important to set the form action - PostBacks fail if the URL differs from it
        form1.Action = HttpContext.Current.Request.RawUrl.ToLower();

        // The PageAssembler is where all the action and excitement lives
        var pa = new PageAssembler(this);
        Page = pa.GetAssembledPage(id);
    }

    /// <summary>
    /// Overrides the Render method for application of MEF plugins.
    /// </summary>
    /// <param name="writer">an HtmlTextWriter object</param>
    protected override void Render(HtmlTextWriter writer)
    {
        var stringWriter = new StringWriter();
        var htmlWriter = new HtmlTextWriter(stringWriter);

        base.Render(htmlWriter);
        var page = stringWriter.ToString();

        // modify page based on available plugins
        var business = new Plugins();
        page = business.ComposeRenderPlugins(page);

        writer.Write(page);
    }
}