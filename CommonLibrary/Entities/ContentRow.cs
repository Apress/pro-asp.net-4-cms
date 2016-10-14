using System;
using System.Collections.Generic;
using CommonLibrary.Interfaces;

namespace CommonLibrary.Entities
{
    /// <summary>
    /// Defines the properties of content within the CMS
    /// </summary>
    public class ContentRow : IContentRow
    {
        public Guid contentID { get; set; }
        public int bucketID { get; set; }
        public int embeddableID { get; set; }
    }
}