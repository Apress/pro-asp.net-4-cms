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

namespace Footer
{
    [Export(typeof(IEmbeddable))]
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:Footer runat=server></{0}:Footer>")]
    public class Footer : WebControl, IEmbeddable
    {
        public Guid ContentID { get; set; }

        public EmbeddablePermissions Permissions
        {
            get
            {
                return (EmbeddablePermissions.AllowedInFooter);
            }
        }

        public string EmbeddableName
        {
            get { return "Footer"; }
        }

        public int EmbeddableID
        {
            get { return 3; }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            RenderContents(writer);
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<a href=\"#\">Privacy Policy</a> | ");
            sb.AppendLine("<a href=\"#\">Terms of Use</a> | ");
            sb.AppendLine("<a href=\"#\">Trademarks</a> | ");
            sb.AppendLine("<a href=\"#\">Site Map</a> | ");
            sb.AppendLine("<a href=\"#\">Find a BBB</a> | ");
            sb.AppendLine("<a href=\"#\">BBB Directory</a>");
            sb.AppendLine("<div class=\"copyright\">&copy; ");
            sb.AppendLine(DateTime.Now.Year.ToString());
            sb.AppendLine(" Council of Better Business Bureaus, Inc.</div>");
            output.Write(sb.ToString());
        }
    }
}
