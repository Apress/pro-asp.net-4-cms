using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using Business.Operations;
using CommonLibrary.Interfaces;

public partial class admin_plugins : Page
{
    private readonly Plugins pluginManager = new Plugins();

    protected void Page_Load(object sender, EventArgs e)
    {
        ClearPlugins();
        LoadRenderPlugins();
        LoadEmbeddablePlugins();
    }

    private void ClearPlugins()
    {
        pluginManager.ClearPlugins();
    }

    private void LoadRenderPlugins()
    {
        var plugins = new List<IRenderPlugin>();
        plugins = (List<IRenderPlugin>)pluginManager.GetRenderPlugins();

        if (plugins.Count > 0)
        {
            foreach (IRenderPlugin p in plugins)
            {
                litPlugins.Text += "<li><strong>[Render]</strong>: " + p + "</li>";
            }
        }
        else
        {
            litPlugins.Text += "<li>No rendering plugins are currently registered with the system.</li>";
        }

        litLocation.Text += "<li><strong>Render Plugins:</strong> " +
                            ConfigurationManager.AppSettings["RenderPluginFolder"] + "</li>";
    }

    private void LoadEmbeddablePlugins()
    {
        var plugins = new List<IEmbeddable>();
        plugins = (List<IEmbeddable>)pluginManager.GetEmbeddablePlugins();

        if (plugins.Count > 0)
        {
            var repository = new Plugins();

            foreach (IEmbeddable p in plugins)
            {
                litPlugins.Text += "<li><strong>[Embeddable]</strong>: " + p + "</li>";
                repository.AddEmbeddableToDatabase(p.EmbeddableID, p.EmbeddableName);
            }
        }
        else
        {
            litPlugins.Text += "<li>No embeddable plugins are currently registered with the system.</li>";
        }

        litLocation.Text += "<li><strong>Embeddable Plugins:</strong> " +
                            ConfigurationManager.AppSettings["EmbeddablePluginFolder"] + "</li>";
    }
}