using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;

namespace Net.UyghurDev.SpellService
{

    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void btnCheckURL_Click(object sender, EventArgs e)
        {
            string url = this.txtURL.Text.Trim();
            if (string.IsNullOrEmpty(url))
            {
                return;
            }
            if (!url.ToLower().StartsWith("http://"))
                url = "http://" + url;
            Session["url"] = url;
            Response.Redirect("webcheck.aspx");
        }


        public static void Redirect(string url, string target, string windowFeatures)
        {
            //HttpContext context = HttpContext.Current;

            //if ((String.IsNullOrEmpty(target) ||
            //    target.Equals("_self", StringComparison.OrdinalIgnoreCase)) &&
            //    String.IsNullOrEmpty(windowFeatures))
            //{

            //    context.Response.Redirect(url);
            //}
            //else
            //{
            //    Page page = (Page)context.Handler;
            //    if (page == null)
            //    {
            //        throw new InvalidOperationException(
            //            "Cannot redirect to new window outside Page context.");
            //    }
            //    url = page.ResolveClientUrl(url);

            //    string script;
            //    if (!String.IsNullOrEmpty(windowFeatures))
            //    {
            //        script = @"window.open(""{0}"", ""{1}"", ""{2}"");";
            //    }
            //    else
            //    {
            //        script = @"window.open(""{0}"", ""{1}"");";
            //    }

            //    script = String.Format(script, url, target, windowFeatures);
            //    ScriptManager.RegisterStartupScript(page,
            //        typeof(Page),
            //        "Redirect",
            //        script,
            //        true);
            //}
        }
    }
}
