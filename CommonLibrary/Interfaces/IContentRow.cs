using System;
using System.Collections.Generic;
using CommonLibrary.Entities;

namespace CommonLibrary.Interfaces
{
    /// <summary>
    /// Defines the interface for a row of content information.
    /// </summary>
    public interface IContentRow
    {
        int bucketID { get; set; }
        Guid contentID { get; set; }
        int embeddableID { get; set; }
    }
}
