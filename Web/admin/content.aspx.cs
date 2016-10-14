using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.SiteTree;

public partial class admin_content : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // clear the tree
        siteTree.Nodes.Clear();

        // configure a mapper to move the n-ary tree into the TreeView
        int siteID = Convert.ToInt32(Profile.LastSiteSelected);

        var mapper = new Mapper(siteID);
        List<TreeNode> nodes = mapper.GetTreeNodes();

        // if there is no content, redirect to create one, otherwise load the tree
        if (nodes.Count > 0)
        {
            foreach (TreeNode n in nodes)
            {
                siteTree.Nodes.Add(n);
            }

            siteTree.CollapseAll();
        }
        else
        {
            Response.Redirect("/admin/editors/editContent.aspx");
        }
    }

    protected void siteTree_SelectedNodeChanged(object sender, EventArgs e)
    {
        Response.Redirect("/admin/editors/editContent.aspx?content=" + siteTree.SelectedNode.Value);
    }

    protected void lnkShowAll_Click(object sender, EventArgs e)
    {
        siteTree.ExpandAll();
    }
}