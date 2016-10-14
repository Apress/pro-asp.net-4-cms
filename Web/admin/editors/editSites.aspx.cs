using System;
using System.Web.UI;
using System.Collections.Generic;
using Business.Operations;
using CommonLibrary.Interfaces;

public partial class admin_editors_editSites : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SiteOperations site = new SiteOperations();
            var list = (List<ISite>)site.GetAllSites();
            try
            {
                ISite currentSite = list.Find(i => i.siteID == Convert.ToInt32(Profile.LastSiteSelected));
                txtSiteTitleUpdate.Text = currentSite.siteName;
                txtDomainUpdate.Text = currentSite.siteHost;
            }
            catch
            {
                btnUpdateSite.Enabled = false;
            }
        }
    }

    /// <summary>
    /// Creates a new website in the CMS.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCreateSite_Click(object sender, EventArgs e)
    {
        SiteOperations site = new SiteOperations();
        if (!String.IsNullOrEmpty(txtTitle.Text) && !String.IsNullOrEmpty(txtDomain.Text))
        {
            site.CreateSite(txtTitle.Text, txtDomain.Text);
        }

        var list = (List<ISite>)site.GetAllSites();
        var s = list.Find(i => i.siteName == txtTitle.Text);
        Profile.LastSiteSelected = s.siteID.ToString();

        Response.Redirect("/admin/content.aspx");
    }

    /// <summary>
    /// Updates a website in the CMS.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdateSite_Click(object sender, EventArgs e)
    {
        SiteOperations site = new SiteOperations();
        if (!String.IsNullOrEmpty(txtSiteTitleUpdate.Text) && !String.IsNullOrEmpty(txtDomainUpdate.Text))
        {
            site.UpdateSite(Convert.ToInt32(Profile.LastSiteSelected), txtSiteTitleUpdate.Text, txtDomainUpdate.Text);
        }
    }
}