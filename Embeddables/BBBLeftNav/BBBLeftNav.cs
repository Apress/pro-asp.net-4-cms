using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using CommonLibrary.Interfaces;
using CommonLibrary.Permissions;

namespace BBBLeftNav
{
    [Export(typeof(IEmbeddable))]
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:BBBLeftNav runat=server></{0}:BBBLeftNav>")]
    public class BBBLeftNav : WebControl, IEmbeddable
    {
        public Guid ContentID { get; set; }

        public EmbeddablePermissions Permissions
        {
            get
            {
                return (EmbeddablePermissions.AllowedInPrimaryNav);
            }
        }

        public string EmbeddableName
        {
            get { return "BBB Left Navigation"; }
        }

        public int EmbeddableID
        {
            get { return 6; }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            RenderContents(writer);
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div id=\"primary-nav\" class=\"left sidebar\">");
            sb.AppendLine(GetLinks());
            sb.AppendLine("</div>");
            output.Write(sb.ToString());
        }

        private string GetLinks()
        {
            string output = String.Empty;
            StringBuilder sb = new StringBuilder();

            Business.SiteTree.Mapper mapper = new Business.SiteTree.Mapper(113);
            var tree = mapper.GetTree();

            var parent = tree.FindPage(ContentID);

            sb.Append("<h2>");
            sb.Append(parent.Title);
            sb.Append("</h2>");
            sb.Append("<ul>");
            
            foreach (var p in parent.Pages)
            {
                sb.Append("<li>");
                sb.Append("<a href=\"#\">");
                sb.Append(p.Title);
                sb.Append("</a></li>");
                output += sb.ToString();
                sb.Clear();
            }
            sb.Append("</ul>");
            return output;
        }
    }
}
