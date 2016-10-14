using System;
using System.Collections.Generic;

namespace CommonLibrary.Interfaces
{
    /// <summary>
    /// Interface that defines a revision of content.
    /// </summary>
    public interface IContentRevision
    {
        Guid ContentID { get; set; }
        bool IsLive { get; set; }
        DateTime TimeStamp { get; set; }
        Guid VersionID { get; set; }
        IList<IScriptedFile> scriptFiles { get; set; }
    }
}
