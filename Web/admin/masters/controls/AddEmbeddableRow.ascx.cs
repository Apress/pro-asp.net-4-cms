using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Operations;
using CommonLibrary.Interfaces;
using CommonLibrary.Permissions;

public partial class admin_masters_controls_AddEmbeddableRow : UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadEmbeddables();
        }
    }

    /// <summary>
    /// Loads the appropriate embeddables for this bucket.
    /// </summary>
    private void LoadEmbeddables()
    {
        var manager = new Plugins();
        foreach (IEmbeddable e in manager.GetEmbeddablePlugins())
        {
            bool validForThisSlot = false;

            switch (Parent.ID)
            {
                case "pnlHeaderEmbeddables":
                    if (e.Permissions.HasFlag(EmbeddablePermissions.AllowedInHeader)) validForThisSlot = true;
                    break;
                case "pnlPrimaryNavEmbeddables":
                    if (e.Permissions.HasFlag(EmbeddablePermissions.AllowedInPrimaryNav)) validForThisSlot = true;
                    break;
                case "pnlContentEmbeddables":
                    if (e.Permissions.HasFlag(EmbeddablePermissions.AllowedInContent)) validForThisSlot = true;
                    break;
                case "pnlSubNavEmbeddables":
                    if (e.Permissions.HasFlag(EmbeddablePermissions.AllowedInSubNav)) validForThisSlot = true;
                    break;
                case "pnlFooterEmbeddables":
                    if (e.Permissions.HasFlag(EmbeddablePermissions.AllowedInFooter)) validForThisSlot = true;
                    break;
                default:
                    continue;
            }

            if (validForThisSlot)
                drpEmbeddables.Items.Add(new ListItem {Text = e.EmbeddableName, Value = e.EmbeddableID.ToString()});
        }
    }

    protected void lnkAddEmbeddable_Click(object sender, EventArgs e)
    {
        // no action necessary; will be handled by calling page  
    }
}