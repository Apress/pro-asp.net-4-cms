using System;
using CommonLibrary.Interfaces;

namespace CommonLibrary.Interfaces
{
    /// <summary>
    /// Interface that Embeddables are expected to implement for admin features.
    /// </summary>
    public interface IEmbeddableAdmin
    {
        int EmbeddableID { get; }
        void SaveRevision(Guid revisionID, dynamic[] parameters);
        IContentRevision LoadRevision(Guid revisionID);
        string DisplayAdminOptions();
    }
}
