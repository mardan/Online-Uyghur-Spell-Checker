using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Message : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Message"] != null)
        {
            this.Label1.Text = Session["Message"] as string;
            Session["Message"] = null;
        }
        else
            this.Label1.Text = "دېگۈدەك ئىش يوق";
    }
}
