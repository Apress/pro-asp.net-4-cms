using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using CommonLibrary.Interfaces;
using Data.Admin;

namespace Business.Operations
{
    public class Plugins
    {
        private CompositionContainer _container;

        [ImportMany(typeof (IEmbeddable))] private IList<IEmbeddable> _embeddables;
        [ImportMany(typeof(IEmbeddableAdmin))] private IList<IEmbeddableAdmin> _adminEmbeddables;
        [ImportMany(typeof (IRenderPlugin))] private IList<IRenderPlugin> _plugins;

        public Plugins()
        {
            _plugins = new List<IRenderPlugin>();
            _embeddables = new List<IEmbeddable>();
            _adminEmbeddables = new List<IEmbeddableAdmin>();
        }

        /// <summary>
        /// Retrieves available plugins from the predefined location.
        /// </summary>
        /// <returns>A generic list of IRenderPlugin objects.</returns>
        public IList<IRenderPlugin> GetRenderPlugins()
        {
            var catalog = new AggregateCatalog();

            try
            {
                catalog.Catalogs.Add(new DirectoryCatalog(ConfigurationManager.AppSettings["RenderPluginFolder"]));
            }
            catch
            {
                // take no actions
            }

            _container = new CompositionContainer(catalog);

            // grab all the available imports for this container
            try
            {
                _container.ComposeParts(this);
            }
            catch
            {
            }

            return _plugins;
        }

        /// <summary>
        /// Retrieves available plugins from the predefined location.
        /// </summary>
        /// <returns>A generic list of IEmbeddable objects.</returns>
        public IList<IEmbeddable> GetEmbeddablePlugins()
        {
            var catalog = new AggregateCatalog();

            try
            {
                catalog.Catalogs.Add(new DirectoryCatalog(ConfigurationManager.AppSettings["EmbeddablePluginFolder"]));
            }
            catch
            {
                // take no actions
            }

            _container = new CompositionContainer(catalog);

            // grab all the available imports for this container
            try
            {
                _container.ComposeParts(this);
            }
            catch
            {
            }

            return _embeddables;
        }

        /// <summary>
        /// Retrieves available plugins from the predefined location.
        /// </summary>
        /// <returns>A generic list of IEmbeddableAdmin objects.</returns>
        public IList<IEmbeddableAdmin> GetAdminEmbeddablePlugins()
        {
            var catalog = new AggregateCatalog();

            try
            {
                catalog.Catalogs.Add(new DirectoryCatalog(ConfigurationManager.AppSettings["EmbeddablePluginFolder"]));
            }
            catch
            {
                // take no actions
            }

            _container = new CompositionContainer(catalog);

            // grab all the available imports for this container
            try
            {
                _container.ComposeParts(this);
            }
            catch
            {
            }

            return _adminEmbeddables;
        }

        /// <summary>
        /// Executes each of the available IRenderPlugin objects, if available.
        /// </summary>
        /// <returns>A generic list of IRenderPlugin objects.</returns>
        public string ComposeRenderPlugins(string page)
        {
            _plugins = GetRenderPlugins();

            // grab all the available imports for this container
            try
            {
                page = _plugins.Aggregate(page, (current, p) => p.ModifyOutput(current));
            }
            catch (CompositionException e)
            {
                // log or handle the exception as you see fit
                string test = e.Message;
            }

            return page;
        }

        /// <summary>
        /// Loads each of the available IEmbeddable objects, if available.
        /// </summary>
        /// <returns>A generic list of IEmbeddable objects.</returns>
        public IList<Control> ComposeEmbeddables(Page page)
        {
            var embeddableList = new List<Control>();

            _embeddables = GetEmbeddablePlugins();

            // grab all the available imports for this container
            try
            {
                foreach (IEmbeddable p in _embeddables)
                {
                    // pass the content GUID to the plugin
                    var g = new Guid(page.Request.QueryString["id"]);
                    p.ContentID = g;
                    embeddableList.Add((Control) p);
                }
            }
            catch (CompositionException e)
            {
                // log or handle the exception as you see fit
                string test = e.Message;
            }

            return embeddableList;
        }

        /// <summary>
        /// Clears the Embeddables table.
        /// </summary>
        public void ClearPlugins()
        {
            var data = new PluginManager();
            data.ClearPlugins();
        }

        /// <summary>
        /// Adds an Embeddable to the Embeddables table.
        /// </summary>
        /// <param name="embeddableID">the ID assigned to the control</param>
        /// <param name="name">the friendly name of the control</param>
        public void AddEmbeddableToDatabase(int embeddableID, string name)
        {
            var data = new PluginManager();
            data.AddEmbeddableToDatabase(embeddableID, name);
        }
    }
}