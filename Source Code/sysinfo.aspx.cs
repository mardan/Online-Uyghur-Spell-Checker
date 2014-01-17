using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class sysinfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Label1.Text = Application["usercount"].ToString();


            if (Application["speller"] != null)
            {
                this.Label2.Text = (Application["speller"] as Net.UyghurDev.Spelling.TextBasedSpellChecker).GetWordCount().ToString();
                this.Label3.Text = (Application["speller"] as Net.UyghurDev.Spelling.TextBasedSpellChecker).GetPearCount().ToString();
            }
            else
            {
                this.Label2.Text = "-1";
                this.Label3.Text = "-1";
            }
        }
    }
}
