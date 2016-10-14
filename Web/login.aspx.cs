using System;
using System.Web.Security;
using System.Web.UI;

public partial class login : Page
{
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (Membership.ValidateUser(txtUsername.Text, txtPassword.Text))
        {
            FormsAuthentication.RedirectFromLoginPage(txtUsername.Text, false);
        }
    }
}