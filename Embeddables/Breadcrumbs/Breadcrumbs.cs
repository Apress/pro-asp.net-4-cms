using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.SiteTree;
using CommonLibrary.Interfaces;
using CommonLibrary.Permissions;

namespace Breadcrumbs
{
    [Export(typeof (IEmbeddable))]
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:Breadcrumbs runat=server></{0}:Breadcrumbs>")]
    public class Breadcrumbs : WebControl, IEmbeddable
    {
        #region IEmbeddable Members

        public Guid ContentID { get; set; }

        public EmbeddablePermissions Permissions
        {
            get { return (EmbeddablePermissions.AllowedInContent); }
        }

        public string EmbeddableName
        {
            get { return "Breadcrumb Navigation"; }
        }

        public int EmbeddableID
        {
            get { return 7; }
        }

        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write(GetLinks());
        }

        private string GetLinks()
        {
            var mapper = new Mapper(113);
            Tree tree = mapper.GetTree();
            Node parent = tree.FindPage(ContentID);

            var sb = new StringBuilder();
            sb.Append("<ul class=\"crumbs\">");
            string output = String.Empty;
            GetPreviousParent(ref output, tree, parent, true);
            sb.Append(output);
            sb.Append("</ul>");
            return sb.ToString();
        }

        private void GetPreviousParent(ref string output, Tree tree, Node node, bool isCurrentPage)
        {
            var content = new StringBuilder();

            if (isCurrentPage)
            {
                content.Append("<li class=\"active\">");
                content.Append(node.Title);
                content.Append("</li>");
            }
            else
            {
                content.Append("<li><a href=\"#\">");
                content.Append(node.Title);
                content.Append("</a></li>");
            }

            var innerLinks = new StringBuilder(output);
            innerLinks.Insert(0, content.ToString());
            output = innerLinks.ToString();

            if (node.ParentID.HasValue)
            {
                Node parent = tree.FindPage(node.ParentID.Value);
                GetPreviousParent(ref output, tree, parent, false);
            }
        }
    }
}