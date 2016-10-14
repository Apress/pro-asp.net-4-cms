using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonLibrary.Interfaces;
using CommonLibrary.Entities;
using Content = Business.Operations.Content;

public partial class admin_masters_controls_ContentVersionRow : UserControl
{
    private Guid _contentID;
    private int _siteID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["content"] != null)
        {
            _contentID = new Guid(Request.QueryString["content"]);
            _siteID = Convert.ToInt32(Profile.LastSiteSelected);
            CreateRevisionLinks();
        }
    }

    /// <summary>
    /// Creates a LinkButton for each revision and attaches an event handler to it.
    /// </summary>
    private void CreateRevisionLinks()
    {
        var business = new Content();
        IContentEntity entity = business.GetContentEntity(_contentID);
        var revisions = business.LoadRevisions(_contentID, _siteID);

        plcRevisionLinks.Controls.Clear();

        foreach (var rev in revisions)
        {
            LinkButton lnk = new LinkButton();
            lnk.ID = rev.VersionID.ToString();
            lnk.Text = rev.TimeStamp.ToString();

            if (lnk.ID == entity.CurrentRevision.ToString()) lnk.Text = lnk.Text.Insert(0, "LIVE - ");

            lnk.Click += new EventHandler(PromoteVersion);
            plcRevisionLinks.Controls.Add(lnk);

            Literal l = new Literal();
            l.Text = "<br/>";
            plcRevisionLinks.Controls.Add(l);
        }
    }

    /// <summary>
    /// Promotes a revision to the current live content.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PromoteVersion(object sender, EventArgs e)
    {
        var link = (LinkButton)sender;
        
        Content c = new Content();
        IContentEntity entity = c.GetContentEntity(_contentID);
        entity.CurrentRevision = new Guid(link.ID);
        c.SaveEntity(entity);

        Response.Redirect(Request.RawUrl);
    }
}