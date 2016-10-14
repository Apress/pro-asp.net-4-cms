using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel.Composition;
using CommonLibrary.Entities;
using CommonLibrary.Interfaces;
using CommonLibrary.Permissions;

namespace SampleHeader
{
    [Export(typeof(IEmbeddable))]
    [Export(typeof(IEmbeddableAdmin))]
    [ToolboxData("<{0}:Content runat=server></{0}:Content>")]
    public class EmbeddableControl : WebControl, IEmbeddable, IEmbeddableAdmin
    {
        #region Shared Members

        public int EmbeddableID
        {
            get
            {
                // assign an integer to this embeddable
                return -1;
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
                return "EmbeddableNameHere";
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
            base.Render(writer);
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
            throw new NotImplementedException();
        }

        public string DisplayAdminOptions()
        {
            // handle displaying admin HTML here
            throw new NotImplementedException();
        }

        #endregion
    }
}
