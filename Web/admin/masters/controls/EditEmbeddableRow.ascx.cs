using System;
using System.Web.UI;
using System.Collections.Generic;
using Business.Operations;
using CommonLibrary.Interfaces;

public partial class admin_masters_controls_EditEmbeddableRow : UserControl
{
    public int EmbeddableID { get; set; }
    public Guid UniqueRowID { get; set; } // used to permit multiple instances of this user control in a PlaceHolder

    protected void Page_Load(object sender, EventArgs e)
    {
        GetEmbeddableName();
    }

    private void GetEmbeddableName()
    {
        var p = new Plugins();
        var items = p.GetEmbeddablePlugins();
        IEmbeddable embeddable = ((List<IEmbeddable>)items).Find(i => i.EmbeddableID == EmbeddableID);
        litEmbeddableName.Text = embeddable.EmbeddableName;
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {

    }
}