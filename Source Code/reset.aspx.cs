using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class reset : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.TextBox1.Text != null && this.TextBox1.Text.Trim().Equals(Setting.AdminPassword))
        {
            lock (Application)
            {
                Application["speller"] = Tools.LoadSpeller();
                this.Label1.Text = "Done!";
                this.Button1.Enabled = false;
            }
        }
    }
}
