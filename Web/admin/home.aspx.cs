using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Admin;
using CommonLibrary.Entities;

public partial class admin_home : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadEntries();
        DisplayEditorIfSysAdmin();
    }

    private void LoadEntries()
    {
        plcEntries.Controls.Clear();

        var entries = new HomepageNews();
        IList<HomepageNewsEntry> list = entries.GetNewsEntries();

        if (list.Count == 0)
        {
            var noEntries = new Literal {Text = "<p>There is nothing to report at this time.</p>"};
            plcEntries.Controls.Add(noEntries);
            return;
        }

        foreach (HomepageNewsEntry entry in list)
        {
            string entryOutput =
                "<h4><span>by</span>&nbsp;[[ENTRYAUTHOR]]&nbsp;<span>on&nbsp;[[ENTRYDATE]]</span></h4><p class=\"newsEntry\">[[ENTRYCONTENT]]</p>";
            entryOutput = entryOutput.Replace("[[ENTRYAUTHOR]]", entry.entryAuthor);
            entryOutput = entryOutput.Replace("[[ENTRYDATE]]", entry.entryDate.ToString("D"));
            entryOutput = entryOutput.Replace("[[ENTRYCONTENT]]", entry.entryContent);

            var litEntry = new Literal {Text = entryOutput};
            plcEntries.Controls.Add(litEntry);

            // only display REMOVE links for system admins
            if (User.IsInRole("system_admin"))
            {
                var remove = new LinkButton();
                remove.Text = "[remove]";
                remove.CssClass = "adminRemoveNewsLink";
                remove.OnClientClick =
                    "javascript:return confirm('Are you sure you want to permanently delete this entry?');";
                remove.CommandArgument = entry.entryID.ToString();
                remove.Command += RemoveEntry;
                plcEntries.Controls.Add(remove);
            }
        }
    }

    private void RemoveEntry(object sender, CommandEventArgs e)
    {
        if (User.IsInRole("system_admin"))
        {
            var entry = new HomepageNews();
            var entryID = Convert.ToInt32(e.CommandArgument);
            entry.Delete(entryID);

            ReloadPage();
        }
    }

    private void DisplayEditorIfSysAdmin()
    {
        if (User.IsInRole("system_admin")) pnlSysAdmin.Visible = true;
    }

    protected void btnPost_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(txtNewsEntry.Text))
        {
            var entry = new HomepageNews();
            entry.Save(Profile.FullName, txtNewsEntry.Text);

            ReloadPage();
        }
    }

    private void ReloadPage()
    {
        Response.Redirect("home.aspx");
    }
}