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

namespace PrimarySearch
{
    [Export(typeof(IEmbeddable))]
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:PrimarySearch runat=server></{0}:PrimarySearch>")]
    public class PrimarySearch : WebControl, IEmbeddable
    {
        public Guid ContentID { get; set; }

        public EmbeddablePermissions Permissions
        {
            get
            {
                return (EmbeddablePermissions.AllowedInHeader);
            }
        }

        public string EmbeddableName
        {
            get { return "PrimarySearch"; }
        }

        public int EmbeddableID
        {
            get { return 2; }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            RenderContents(writer);
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div id=\"search\">");
            sb.AppendLine("<label for=\"search-box\">Search For:</label>");
            sb.AppendLine("<!-- add water mark for [enter business or charity] -->");
            sb.AppendLine("<input type=\"text\" id=\"search-box\" class=\"search-box\" />");
            sb.AppendLine("<label for=\"location\">In:</label>");
            sb.AppendLine("<!-- add watermark for [enter zip or city,state] -->");
            sb.AppendLine("<input type=\"text\" id=\"search-location\" class=\"search-box\" />");
            sb.AppendLine("<a href=\"#\" class=\"search-btn\" >Search</a>");
            sb.AppendLine("<a href=\"#\" class=\"search-advanced\">Advanced Search</a>");
            sb.AppendLine("<!--demo funcionality for advanced search-->");
            sb.AppendLine("<div id=\"advanced-search\" style=\"display:none;\">");
        	sb.AppendLine("<label for=\"search-phone\">Phone Number:</label>");
            sb.AppendLine("<input type=\"text\" id=\"search-phone\" />");
            sb.AppendLine("<a href=\"#\" class=\"search-btn\">Search</a>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            output.Write(sb.ToString());
        }
    }
}
