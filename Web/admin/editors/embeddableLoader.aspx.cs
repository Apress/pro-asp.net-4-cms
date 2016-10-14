using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Business.Operations;
using CommonLibrary.Interfaces;

public partial class admin_editors_embeddableLoader : System.Web.UI.Page
{
    private IEmbeddableAdmin _embeddable;
    private Guid _revisionID;
    private int _embeddableID;

    protected void Page_Load(object sender, EventArgs e)
    {
        GetRevisionID();
        GetEmbeddableID();
        LoadAdminEmbeddable();
    }

    /// <summary>
    /// Retrieves the revision ID from the querystring
    /// </summary>
    private void GetRevisionID()
    {
        _revisionID = new Guid(Request.QueryString["id"]);
    }

    /// <summary>
    /// Retrieves the embeddable ID from the querystring
    /// </summary>
    private void GetEmbeddableID()
    {
        _embeddableID = Convert.ToInt32(Request.QueryString["em"]);
    }

    /// <summary>
    /// Loads the IEmbeddableAdmin control into the page and displays the admin options
    /// </summary>
    private void LoadAdminEmbeddable()
    {
        var p = new Plugins();
        var list = (List<IEmbeddableAdmin>)p.GetAdminEmbeddablePlugins();
        _embeddable = list.Find(i => i.EmbeddableID == _embeddableID);

        Literal l = new Literal { Text = _embeddable.DisplayAdminOptions() };
        plcEmbeddableControls.Controls.Add(l);
    }

    /// <summary>
    /// Calls the save revision method within the control
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        _embeddable.SaveRevision(_revisionID, new dynamic[] { this.Page });
    }
}