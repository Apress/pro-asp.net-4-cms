using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Operations;
using CommonLibrary.Interfaces;

public partial class admin_masters_controls_SiteDropDown : UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadSites();

            if (!String.IsNullOrEmpty(Profile.LastSiteSelected))
            {
                drpSites.SelectedIndex = drpSites.Items.IndexOf(drpSites.Items.FindByValue(Profile.LastSiteSelected));
            }
        }
    }

    /// <summary>
    /// Loads the available sites into the DropDownList.
    /// </summary>
    private void LoadSites()
    {
        drpSites.Items.Clear();

        var sites = new SiteOperations();
        IList<ISite> siteList = sites.GetAllSites();

        foreach (ISite s in siteList)
        {
            var entry = new ListItem();
            entry.Text = s.siteName;
            entry.Value = s.siteID.ToString();
            drpSites.Items.Add(entry);
        }
    }

    /// <summary>
    /// Note in the profile which site is being worked on.
    /// </summary>
    private void SaveCurrentSiteToProfile()
    {
        Profile.LastSiteSelected = drpSites.SelectedItem.Value;
        Profile.Save();
    }

    protected void drpSites_SelectedIndexChanged(object sender, EventArgs e)
    {
        SaveCurrentSiteToProfile();
        Response.Redirect(Request.Url.ToString());
    }
}