using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonLibrary.Entities;
using CommonLibrary.Interfaces;
using Business.Scripting;

namespace Business.Operations
{
    public class PageAssembler
    {
        // the Page that called this class and a PlaceHolder for buckets/embeddables
        private readonly PlaceHolder _cmsControls;
        private readonly Page _page;

        // dynamic language scripts (IronPython, IronRuby, etc.)
        private readonly string _script;
        private readonly IList<IScriptedFile> _scriptFiles;
        
        // handles embeddables
        private IList<Control> _embeddables;
        private IContentEntity _entity;

        public PageAssembler(Page page)
        {
            // initialize scripts and lists of files
            _script = String.Empty;
            _scriptFiles = new List<IScriptedFile>();
            _embeddables = new List<Control>();

            // set a reference to the page being passed in, and find the PlaceHolder on it
            _page = page;
            _cmsControls = (PlaceHolder) _page.FindControl("cmsControls");
        }

        /// <summary>
        /// Returns a completed CMS page object
        /// </summary>
        /// <param name="contentID">The GUID content ID</param>
        /// <returns>A fully-assembled Page object</returns>
        public Page GetAssembledPage(Guid contentID)
        {
            ComposeEmbeddables();
            LoadContent(contentID);
            ExecuteScripts();
            LoadSEOMetaData();

            return _page;
        }

        /// <summary>
        /// Loads the content for a page.
        /// </summary>
        /// <param name="id">the ID of the page</param>
        private void LoadContent(Guid id)
        {
            var business = new Content();
            _entity = business.GetContentEntity(id);

            IList<IContentRow> content = business.LoadContent(_entity.CurrentRevision.Value);
            foreach (IContentRow c in content)
            {
                string bucket = String.Empty;
                switch (c.bucketID)
                {
                    case 1:
                        bucket = "header.ascx";
                        break;
                    case 2:
                        bucket = "primarynav.ascx";
                        break;
                    case 3:
                        bucket = "content.ascx";
                        break;
                    case 4:
                        bucket = "subnav.ascx";
                        break;
                    case 5:
                        bucket = "footer.ascx";
                        break;
                }

                LoadBuckets(bucket, c.embeddableID);
            }
        }

        /// <summary>
        /// Loads the top-level buckets for a page. There can only be one instance of a particular bucket on a page.
        /// </summary>
        /// <param name="bucketName">the name of the bucket control (ex. "header.ascx")</param>
        /// <param name="embeddableID">the ID of the embeddable we need to load as a plugin</param>
        private void LoadBuckets(string bucketName, int embeddableID)
        {
            Control bucket = _page.LoadControl("~/core/buckets/" + bucketName);
            bucket.ID = bucketName.Replace(".ascx", "");
            if (_cmsControls.FindControl(bucket.ID) == null) _cmsControls.Controls.Add(bucket);
            if (embeddableID > 0)
            {
                LoadEmbeddables(bucketName, embeddableID);
            }
        }

        /// <summary>
        /// Loads the current embeddable for a bucket. There can be multiple embeddables in a bucket.
        /// </summary>
        /// <param name="bucketName">the name of the bucket control (ex. "header.ascx")</param>
        /// <param name="embeddableID">the ID of the embeddable we need to load</param>
        private void LoadEmbeddables(string bucketName, int embeddableID)
        {
            Control parentBucket = _cmsControls.FindControl(bucketName.Replace(".ascx", ""));
            Control embeddables = parentBucket.FindControl("embeddables");
            foreach (Control e in _embeddables)
            {
                if (((IEmbeddable) e).EmbeddableID == embeddableID)
                {
                    embeddables.Controls.Add(e);
                }
            }
        }

        /// <summary>
        /// Loads a DLR engine and executes scripts for this content.
        /// </summary>
        private void ExecuteScripts()
        {
            DLRManager _scriptEngine;
            var parameters = new dynamic[1];
            parameters[0] = _page;

            // iterate over any scripts attached to this page
            foreach (IScriptedFile script in _scriptFiles)
            {
                _scriptEngine = new DLRManager(script.language);
                _scriptEngine.ExecuteFile(script.fileName, script.className, script.methodName, script.parameters);
            }
        }

        /// <summary>
        /// Composes the embeddables for a particular page
        /// </summary>
        private void ComposeEmbeddables()
        {
            var business = new Plugins();
            _embeddables = business.ComposeEmbeddables(_page);
        }

        /// <summary>
        /// Sets the SEO-valuable meta tags on the page
        /// </summary>
        private void LoadSEOMetaData()
        {
            if (!String.IsNullOrEmpty(_entity.Title))
            {
                _page.Title = _entity.Title;    
            }

            if (!String.IsNullOrEmpty(_entity.Keywords))
            {
                _page.MetaKeywords = _entity.Keywords;
            }

            if (!String.IsNullOrEmpty(_entity.Description))
            {
                _page.MetaDescription = _entity.Description;
            }

            AssembleRobotsMetaTag();
        }

        /// <summary>
        /// Assembles a meta tag depending on visbility settings for this page.
        /// </summary>
        private void AssembleRobotsMetaTag()
        {
            var m = new System.Web.UI.HtmlControls.HtmlMeta();
            m.Name = "robots";
            m.Content = String.Empty;

            if (!_entity.Visible && !_entity.FollowLinks)
            {
                m.Content = "noindex, nofollow";
            }
            if (_entity.Visible && !_entity.FollowLinks)
            {
                m.Content = "nofollow";
            }
            if (!_entity.Visible && _entity.FollowLinks)
            {
                m.Content = "noindex";
            }

            // only add it if there's something specified
            if (!String.IsNullOrEmpty(m.Content)) _page.Header.Controls.Add(m);
        }
    }
}