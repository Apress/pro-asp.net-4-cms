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

namespace Content
{
    [Export(typeof(IEmbeddable))]
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:Content runat=server></{0}:Content>")]
    public class Content : WebControl, IEmbeddable
    {
        public Guid ContentID { get; set; }

        public EmbeddablePermissions Permissions
        {
            get
            {
                return (EmbeddablePermissions.AllowedInContent |
                        EmbeddablePermissions.AllowedInFooter |
                        EmbeddablePermissions.AllowedInHeader |
                        EmbeddablePermissions.AllowedInPrimaryNav |
                        EmbeddablePermissions.AllowedInSubNav);
            }
        }

        public string EmbeddableName
        {
            get { return "Content"; }
        }

        public int EmbeddableID
        {
            get { return 4; }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div id=\"section\" class=\"threecol main\">");
            sb.AppendLine("<h2>This Works Now!</h2>");
            sb.AppendLine("<p>Founded in 1978, BBB AUTO LINE is the nation's oldest and most respected auto warranty dispute resolution program.&nbsp; Information about BBB AUTO LINE rules, procedures and <a href=\"/us/auto-line-lemon-laws/accredited-manufacturers/\">participating manufacturers </a>is included on this site to help you understand the process.</p>");
            sb.AppendLine("<p>BBB AUTO LINE proceedings are informal.&nbsp; In fact, most cases can be resolved through telephone discussions facilitated by BBB staff. Disputes not resolved by agreement may proceed to arbitration.</p>");
            sb.AppendLine("<p>Arbitration is an informal hearing at which the consumer and manufacturer's representative present their views of a dispute to a neutral third party, the arbitrator, who will decide how the dispute will be resolved.&nbsp; <a target=\"_blank\" href=\"https://www.auto.bbb.org/scripts/cgiip.exe/WService=DevTest/snow/viewtraincust.w\">Click here to view video clips about the arbitration process.</a></p>");
            sb.AppendLine("<p>For complete information about the program rules and procedures, click on the appropriate option below.&nbsp;If you have questions regarding information you have read on this page, please email us at <a href=\"mailto:contactdr@council.bbb.org\">contactdr@council.bbb.org</a></p><p>If you are interested in learning about becoming an arbitrator for the BBB AUTO LINE program, please <a href=\"/us/Dispute-Resolution-Services/Arbitrator-Requirements/\">click here</a>.</p>");
            sb.AppendLine("</div>");
            writer.Write(sb.ToString());
        }

        /*protected override void RenderContents(HtmlTextWriter output)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div id=\"content\">");
            sb.AppendLine("[Page content here]");
            sb.AppendLine("</div>");
            output.Write(sb.ToString());
        }*/
    }
}
