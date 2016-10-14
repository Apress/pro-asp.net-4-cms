using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel.Composition;
using CommonLibrary.Entities;
using CommonLibrary.Interfaces;
using CommonLibrary.Permissions;

namespace Content
{
    [Export(typeof(IEmbeddable))]
    [Export(typeof(IEmbeddableAdmin))]
    [ToolboxData("<{0}:Content runat=server></{0}:Content>")]
    public class SampleContent : WebControl, IEmbeddable, IEmbeddableAdmin
    {
        #region Shared Members

        public int EmbeddableID
        {
            get
            {
                // assign an integer to this embeddable
                return 2;
            }
        }

        #endregion

        #region IEmbeddable Members

        public Guid ContentID { get; set; }

        public string EmbeddableName
        {
            get
            {
                // assign a friendly name for use in the admin editor
                return "Sample Body Content";
            }
        }

        public EmbeddablePermissions Permissions
        {
            get
            {
                // assign your EmbeddablePermissions here (opt-in methodology)
                return (EmbeddablePermissions.AllowedInContent);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            // this is where public HTML will go
            writer.WriteLine(@"<div id=""main"">");
            writer.WriteLine(@"<h2>Welcome</h2>");
            writer.WriteLine(@"<p>This is a sample website created with the content management system described in <a href=""http://apress.com/book/view/9781430227120"" target=""_blank"">Pro ASP .NET 4 CMS</a>.</p>");
            writer.WriteLine(@"<p>A handful of basic controls have been created to demonstrate how embeddables work within the system, as well as provide a simple homepage by default.</p>");
            writer.WriteLine(@"<p>If you view the source to this page, you will note the use of some HTML5 elements (header, nav and so on). Additionally, browsers capable of rendering CSS3 will create a drop-shadow below the book image.</p><p>Feel free to <a href=""/login.aspx"">login</a>* and play around for a bit.</p>");
            writer.WriteLine(@"<p class=""signatureIntro"">Best regards,</p>");
            writer.WriteLine(@"<p class=""signature"">Alan Harris</p>");
            writer.WriteLine(@"</div>");
        }

        #endregion

        #region IEmbeddableAdmin Members

        public void SaveRevision(Guid revisionID, dynamic[] parameters)
        {
            // handle saving revision here
            throw new NotImplementedException();
        }

        public IContentRevision LoadRevision(Guid revisionID)
        {
            // handle loading a revision here
            IContentRevision r = new ContentRevision();
            return r;
        }

        public string DisplayAdminOptions()
        {
            // handle displaying admin HTML here
            return "There are no admin options for this control.";
        }

        #endregion
    }
}
