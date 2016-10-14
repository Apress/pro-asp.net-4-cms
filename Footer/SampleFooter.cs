using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel.Composition;
using CommonLibrary.Entities;
using CommonLibrary.Interfaces;
using CommonLibrary.Permissions;

namespace Footer
{
    [Export(typeof(IEmbeddable))]
    [Export(typeof(IEmbeddableAdmin))]
    [ToolboxData("<{0}:Footer runat=server></{0}:Footer>")]
    public class SampleFooter : WebControl, IEmbeddable, IEmbeddableAdmin
    {
        #region Shared Members

        public int EmbeddableID
        {
            get
            {
                // assign an integer to this embeddable
                return 3;
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
                return "Sample Footer";
            }
        }

        public EmbeddablePermissions Permissions
        {
            get
            {
                // assign your EmbeddablePermissions here (opt-in methodology)
                return (EmbeddablePermissions.AllowedInFooter);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            // this is where public HTML will go
            writer.WriteLine(@"<div id=""copyright"">*Pssst! The default login information is ""admin"" / ""password"".</div>");
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
