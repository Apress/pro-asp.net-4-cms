using System;
using System.Collections.Generic;
using CommonLibrary.Interfaces;

namespace CommonLibrary.Entities
{
    /// <summary>
    /// Defines the properties of a specific revision of content
    /// </summary>
    public class ContentRevision : IContentRevision
    {
        public Guid VersionID { get; set; }
        public Guid ContentID { get; set; }
        public bool IsLive { get; set; }
        public DateTime TimeStamp { get; set; }
        public IList<IScriptedFile> scriptFiles { get; set; }
    }
}
