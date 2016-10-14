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

namespace PrimaryNav
{
    [Export(typeof(IEmbeddable))]
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:PrimaryNav runat=server></{0}:PrimaryNav>")]
    public class PrimaryNav : WebControl, IEmbeddable
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
            get { return "PrimaryNav"; }
        }

        public int EmbeddableID
        {
            get { return 1; }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            RenderContents(writer);
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div id=\"header-content\">");
            sb.AppendLine("<div id=\"bbb-logo\">");
            sb.AppendLine("<a href=\"/\"><img src=\"/images/BBB-tag_7469.png\" alt=\"bbb logo\" /></a>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div id=\"masthead\">");
            sb.AppendLine("<h1><a href=\"/\">Better Business Bureau</a></h1>");
            sb.AppendLine("<h2>Metro Washington DC and Eastern Pennsylvania</h2>");
            sb.AppendLine("<!--<h4>In Metro Washington DC and Eastern Pennsylvania</h4>-->");
            sb.AppendLine("<div id=\"find-bbb\">");
            sb.AppendLine("<fieldset>");
            sb.AppendLine("<select id=\"bbb-finder\" name=\"bbb-finder\">");
            sb.AppendLine("<option value=\"#\">Change BBB Location</option>");
            sb.AppendLine("<option value=\"#\">Austin</option>");
            sb.AppendLine("<option value=\"#\">Akron</option>");
            sb.AppendLine("<option value=\"#\">Boston</option>");
            sb.AppendLine("</select>");
            sb.AppendLine("</fieldset>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div id=\"global-nav\">");
            sb.AppendLine("<ul>");
            sb.AppendLine("<li><a href=\"/consumers/\">Consumers</a></li>");
            sb.AppendLine("<li><a href=\"/business/\">Businesses</a></li>");
            sb.AppendLine("<li><a href=\"/charity/\">Charities &amp; Donors</a></li>");
            sb.AppendLine("<li><a href=\"/About-BBB/\">About Us</a></li>");
            sb.AppendLine("<li><a href=\"/bbb-news/\">News</a></li>");
            sb.AppendLine("<li><a href=\"#\">Contact Us</a></li>");
            sb.AppendLine("<li id=\"biz-login\"><a href=\"#\">Business Login</a></li>");
            sb.AppendLine("</ul>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            output.Write(sb.ToString());
        }
    }
}
