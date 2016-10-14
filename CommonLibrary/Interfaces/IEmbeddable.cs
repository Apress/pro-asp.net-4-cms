using System;
using CommonLibrary.Permissions;

namespace CommonLibrary.Interfaces
{
    /// <summary>
    /// Interface that Embeddables are expected to implement.
    /// </summary>
    public interface IEmbeddable
    {
        Guid ContentID { get; set; }
        string EmbeddableName { get; }
        int EmbeddableID { get; }
        EmbeddablePermissions Permissions { get; }
    }
}