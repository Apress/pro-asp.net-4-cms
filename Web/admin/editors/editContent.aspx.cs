using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Admin;
using Business.Extensions;
using Business.SiteTree;
using CommonLibrary.Entities;
using CommonLibrary.Interfaces;
using Content = Business.Operations.Content;

/// <summary>
/// This page could stand to be cleaned somewhat; it's very busy and has a good number of moving pieces.
/// I sacrified certain design characteristics in favor of keeping everything centralized to help keep the
/// class-to-class hopping down if you decide to breakpoint through this. It would be only a few minutes
/// work to refactor this and create something similar to the Business.Operations.PageAssembler class.
/// </summary>
public partial class admin_editors_editContent : Page
{
    private int _siteID;
    private Content _content;
    private Mapper _mapper;
    private Guid? _contentID;
    private IContentEntity _contentEntity;
    private IList<IContentRevision> _contentRevisions;
    private IContentRevision _currentRevision;

    protected void Page_Load(object sender, EventArgs e)
    {
        Disabler.DisableSiteDropDown(this);
        _content = new Content();

        FillSiteID();
        FillContentID();

        LoadExistingContent();

        if (Page.IsPostBack)
        {
            ParsePostBack();
            RestoreEmbeddablesAfterPostBack();
        }
        else
        {
            // clear the temporary revision ID if it exists
            Session["newRevision"] = null;

            SetupTree();
            if (_contentEntity != null)
            {
                FillPageContent(_contentEntity);
                LoadBucketsAndEmbeddables(_contentEntity);
            }
        }
    }

    #region Editing Existing Content
    /// <summary>
    /// Loads content and revisions for editing.
    /// </summary>
    private void LoadExistingContent()
    {
        if (_contentID.HasValue)
        {
            _contentEntity = _content.GetContentEntity(_contentID.Value);

            ValidateCurrentSite();

            _contentRevisions = _content.LoadRevisions(_contentID.Value, _siteID);
            _currentRevision = ((List<IContentRevision>)_contentRevisions).Find(i => i.VersionID == _contentEntity.CurrentRevision);
        }
        else
        {
            // this is new content, so assign some IDs for use throughout the page
            _contentID = Guid.NewGuid();
        }
    }

    /// <summary>
    /// Fills the private contentID field based on the querystring value, if available.
    /// </summary>
    private void FillContentID()
    {
        try
        {
            if (Request.QueryString["content"] != null) _contentID = new Guid(Request.QueryString["content"]);
        }
        catch
        {
            return;
        }
    }

    /// <summary>
    /// Fills the private siteID field with the currently selected site.
    /// </summary>
    private void FillSiteID()
    {
        _siteID = Convert.ToInt32(Profile.LastSiteSelected);
    }

    /// <summary>
    /// Ensures that the GUID is for the current site.
    /// </summary>
    private void ValidateCurrentSite()
    {
        // If the user has attempted to enter a GUID manually for a site other than the one they are permitted in,
        // redirect them to their home page. You may wish to modify this to display an error or log the attempt, etc.
        if (_contentEntity.SiteID != _siteID) Response.Redirect("/admin/home.aspx");
    }

    /// <summary>
    /// Fills in basic page information.
    /// </summary>
    /// <param name="page">The IContentEntity object that represents the page.</param>
    private void FillPageContent(IContentEntity page)
    {
        txtTitle.Text = page.Title;
        txtAuthor.Text = page.Author;
        txtURL.Text = page.FriendlyUrl;
        txtDescription.Text = page.Description;
        txtKeywords.Text = page.Keywords;
        chkExcludeSearch.Checked = page.Visible;
        chkFollowLinks.Checked = page.FollowLinks;

        if (page.ParentID.HasValue) siteTree.FindNode(page.ParentID.ToString()).Selected = true;
    }

    /// <summary>
    /// Toggles which buckets and embeddables are enabled for this content revision by default.
    /// </summary>
    /// <param name="page">The IContentEntity object that represents the page.</param>
    private void LoadBucketsAndEmbeddables(IContentEntity page)
    {
        var business = new Content();
        var content = (List<IContentRow>)business.LoadContent(page.CurrentRevision.Value);
        string bucket = String.Empty;

        foreach (var b in content)
        {
            switch (b.bucketID)
            {
                case 1:
                    chkHeader.Checked = true;
                    bucket = "AddHeaderEmbeddableRow";
                    break;
                case 2:
                    chkPrimaryNav.Checked = true;
                    bucket = "AddPrimaryNavEmbeddableRow";
                    break;
                case 3:
                    chkContent.Checked = true;
                    bucket = "AddContentEmbeddableRow";
                    break;
                case 4:
                    chkSubNav.Checked = true;
                    bucket = "AddSubNavEmbeddableRow";
                    break;
                case 5:
                    chkFooter.Checked = true;
                    bucket = "AddFooterEmbeddableRow";
                    break;
                default:
                    break;
            }

            var uniqueID = Guid.NewGuid();

            AddEmbeddableRowToBucket(uniqueID.ToString(), bucket, b.embeddableID);
            AddEmbeddableToList(uniqueID, bucket, b.embeddableID);
        }
    }
    #endregion

    #region Handle PostBacks and Embeddable Restoration
    /// <summary>
    /// Identifies which control sent the PostBack and adds appropriate Embeddables, if any.
    /// </summary>
    private void ParsePostBack()
    {
        Control c = this.GetPostBackControl();
        if (c is LinkButton)
        {
            try
            {
                int embeddable = Convert.ToInt32(((DropDownList)c.Parent.Controls[3]).SelectedValue);
                string parent = c.Parent.ID;
                Guid uniqueID = Guid.NewGuid();

                AddEmbeddableToList(uniqueID, parent, embeddable);
            }
            catch
            {
                // no action necessary
            }
        }
        else if (c is TreeView)
        {
            ViewState["parentID"] = new Guid(siteTree.SelectedNode.Value);
        }
    }

    /// <summary>
    /// Handles ViewState and controls stored within it.
    /// </summary>
    /// <param name="uniqueID">A GUID to identify this element.</param>
    /// <param name="parent">The parent (i.e. "primaryNav") of the embeddable.</param>
    /// <param name="embeddable">The embeddable ID.</param>
    private void AddEmbeddableToList(Guid uniqueID, string parent, int embeddable)
    {
        if (ViewState["embeddables"] == null)
        {
            var embeddables = new List<string> { uniqueID.ToString() + ":" + parent + "|" + embeddable };
            ViewState["embeddables"] = embeddables;
        }
        else
        {
            var embeddables = (List<string>)ViewState["embeddables"];
            embeddables.Add(uniqueID.ToString() + ":" + parent + "|" + embeddable);
            ViewState["embeddables"] = embeddables;
        }
    }

    /// <summary>
    /// After PostBack, all custom-added controls must be restored manually.
    /// </summary>
    private void RestoreEmbeddablesAfterPostBack()
    {
        if (ViewState["embeddables"] == null) return;

        var embeddables = (List<string>)ViewState["embeddables"];
        foreach (var entry in embeddables.Select(e => e.Split('|')))
        {
            string[] chunks = entry[0].Split(':');

            AddEmbeddableRowToBucket(chunks[0], chunks[1], Convert.ToInt32(entry[1]));
        }
    }

    /// <summary>
    /// Adds an embeddable editor row to a bucket.
    /// </summary>
    /// <param name="uniqueID">The unique ID of the element.</param>
    /// <param name="bucket">The name of the bucket.</param>
    /// <param name="embeddable">The embeddable ID.</param>
    private void AddEmbeddableRowToBucket(string uniqueID, string bucket, int embeddable)
    {
        Control plcEmbeddables = Master.FindDeepControl(bucket).FindControl("plcEmbeddables");
        Control row = LoadControl("~/admin/masters/controls/EditEmbeddableRow.ascx");

        // wire up the edit button with the necessary ID values
        Button editButton = (Button)((admin_masters_controls_EditEmbeddableRow)row).FindDeepControl("btnEdit");
        if (_currentRevision != null)
        {
            editButton.Attributes["onClick"] = ("javascript:$(this).DisplayDialog('" + _currentRevision.VersionID.ToString() + "','" + embeddable.ToString() + "');");
        }
        else
        {
            var newRevision = GenerateNewRevisionID();
            editButton.Attributes["onClick"] = ("javascript:$(this).DisplayDialog('" + newRevision.ToString() + "','" + embeddable.ToString() + "');");
        }

        // wire up the remove button
        LinkButton removeButton = (LinkButton)((admin_masters_controls_EditEmbeddableRow)row).FindDeepControl("lnkDelete");
        removeButton.ID = "delete_" + uniqueID.ToString();
        removeButton.Click += new EventHandler(RemoveEmbeddableRowFromBucket);

        // assign unique identifiers to the row and add it to the bucket
        ((admin_masters_controls_EditEmbeddableRow)row).EmbeddableID = embeddable;
        ((admin_masters_controls_EditEmbeddableRow)row).ID = uniqueID;
        ((admin_masters_controls_EditEmbeddableRow)row).UniqueRowID = new Guid(uniqueID);
        plcEmbeddables.Controls.Add(row);
    }

    /// <summary>
    /// When creating totally new content, this establishes a temporary session variable for the revision ID
    /// </summary>
    /// <returns>A GUID for the revision ID</returns>
    private Guid GenerateNewRevisionID()
    {
        if (Session["newRevision"] != null)
        {
            return (Guid)Session["newRevision"];
        }
        else
        {
            Guid newRevision = Guid.NewGuid();
            Session["newRevision"] = newRevision;
            return newRevision;
        }
    }

    /// <summary>
    /// Removes an emeddable editor row from a bucket.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RemoveEmbeddableRowFromBucket(object sender, EventArgs e)
    {
        // determine the row to delete and remove it
        var ID = ((LinkButton)sender).ID.Replace("delete_","");
        var embeddables = (List<string>)ViewState["embeddables"];
        var item = embeddables.Find(i => i.Substring(0,36) == ID);
        embeddables.Remove(item);

        // physically delete the row from the editor page
        var p = ((LinkButton)sender).Parent;
        Control plcEmbeddables = Master.FindDeepControl(p.NamingContainer.ClientID).FindControl("plcEmbeddables");
        Control toRemove = plcEmbeddables.FindDeepControl(ID);
        plcEmbeddables.Controls.Remove(toRemove);
    }

    #endregion

    #region Saving Content
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveNewContent();
    }

    /// <summary>
    /// Saves an entry for this piece of content.
    /// </summary>
    private void SaveNewContent()
    {
        IContentEntity e = new ContentEntity();
        IContentRevision r = new ContentRevision();

        FillEntity(e);
        FillRevision(e, r);

        var rows = new List<ContentRow>();

        FillContentRows(r, rows);

        _content.SaveEntity(e);
        _content.SaveRevision(r);
        _content.SaveRows(r, rows);

        Response.Redirect("/admin/content.aspx");
    }

    /// <summary>
    /// Fills the properties of a content entity object.
    /// </summary>
    /// <param name="e">The IContentEntity object.</param>
    private void FillEntity(IContentEntity e)
    {
        e.Title = txtTitle.Text;
        e.Author = txtAuthor.Text;
        e.FriendlyUrl = txtURL.Text;
        e.Description = txtDescription.Text;
        e.Keywords = txtKeywords.Text;
        e.FollowLinks = chkFollowLinks.Checked;
        e.Visible = chkExcludeSearch.Checked;
        e.SiteID = _siteID;

        if (siteTree.SelectedNode != null)
        {
            if (_contentID.HasValue)
            {
                if (siteTree.SelectedNode.Value != _contentID.Value.SaferToString())
                {
                    e.ParentID = new Guid(siteTree.SelectedNode.Value);
                }
            }
        }

        if (_contentID.HasValue)
        {
            e.ContentID = _contentID.Value;
        }
        else
        {
            e.ContentID = Guid.NewGuid();
        }
    }

    /// <summary>
    /// Fills the properties of a content revision object.
    /// </summary>
    /// <param name="e">The IContentEntity object.</param>
    /// <param name="r">The IContentRevision object.</param>
    private void FillRevision(IContentEntity e, IContentRevision r)
    {
        r.ContentID = e.ContentID;
        r.IsLive = true;
        r.TimeStamp = DateTime.Now;
        r.VersionID = GenerateNewRevisionID();
        e.CurrentRevision = r.VersionID;
    }

    /// <summary>
    /// Fills the properties of a content row (bucket / embeddable) object.
    /// </summary>
    /// <param name="r">The IContentRevision object.</param>
    /// <param name="rows">The list of content rows.</param>
    private void FillContentRows(IContentRevision r, List<ContentRow> rows)
    {
        var embeddables = (List<string>)ViewState["embeddables"];

        if (embeddables == null) return;

        foreach (var entry in embeddables.Select(embeddable => embeddable.Split('|')))
        {
            string[] chunks = entry[0].Split(':');

            ContentRow row = new ContentRow();
            switch (chunks[1])
            {
                case "AddHeaderEmbeddableRow":
                    row.bucketID = 1;
                    break;
                case "AddPrimaryNavEmbeddableRow":
                    row.bucketID = 2;
                    break;
                case "AddContentEmbeddableRow":
                    row.bucketID = 3;
                    break;
                case "AddSubNavEmbeddableRow":
                    row.bucketID = 4;
                    break;
                case "AddFooterEmbeddableRow":
                    row.bucketID = 5;
                    break;
                default:
                    break;
            }

            row.embeddableID = Convert.ToInt32(entry[1]);
            row.contentID = r.VersionID;
            rows.Add(row);
        }
    }

    #endregion

    #region Tree Operations
    /// <summary>
    /// Configures the site tree if available.
    /// </summary>
    private void SetupTree()
    {
        // clear the tree
        siteTree.Nodes.Clear();

        // configure a mapper to move the n-ary tree into the TreeView
        _mapper = new Mapper(_siteID);
        List<TreeNode> nodes = _mapper.GetTreeNodes();

        // if there is no content, display a friendly message, otherwise load the tree
        if (nodes.Count > 0)
        {
            noContent.Visible = false;
            foreach (TreeNode n in nodes)
            {
                siteTree.Nodes.Add(n);
            }

            siteTree.CollapseAll();
        }
        else
        {
            content.Visible = false;
        }
    }

    /// <summary>
    /// Update ViewState with a parent ID for this page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void siteTree_SelectedNodeChanged(object sender, EventArgs e)
    {
        // nothing to perform here; will be handled by ParsePostBack()
    }
    #endregion
}