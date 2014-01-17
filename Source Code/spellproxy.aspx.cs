/*
 * Axirqi qetim ozgertilgen waqti: 2010.10.9
 * UyghurDev Imla Mulazimetirining muwejjet oqughuchisi. Merzkur hojjet mulimetni ishletmekchi 
 * bolghan bikete qoyulidu.
 * Copyright UyghurDev 2010
 * 
 *      Aptori
 *          Merdan Hoshur (udmish@gmail.com)
 *      VERSION
 *          1.0 beta
 *  
 */
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net;
using System.IO;
using System.Text;

public partial class spellproxy : System.Web.UI.Page
{
    private static Encoding ENCODING = Encoding.UTF8;

    private static Tools.HostToPlace HostPlace = Tools.HostToPlace.UyghurDev;

    protected void Page_Load(object sender, EventArgs e)
    {
        string uri = string.Format("{0}?{1}", Tools.GetSpellServerUrl(HostPlace), Request.QueryString);
        using (WebClient webclient = new WebClient())
        {
            if (Request.UrlReferrer != null)
                webclient.Headers.Add("UrlReferrer", Server.UrlDecode(Request.UrlReferrer.AbsolutePath));
            webclient.Headers.Add("Content-Type", "text/xml");
            byte[] clientData = new byte[Request.InputStream.Length];
            if (clientData.Length == 0)
                Response.Write(" <?xml version=\"1.0\" encoding=\"UTF-8\" ?><spellresult error=\"1\" /> ");
            else
            {
                Request.InputStream.Read(clientData, 0, clientData.Length);
                byte[] responseData = webclient.UploadData(uri, "POST", clientData);
                string strResponseData = ENCODING.GetString(responseData);
                if (strResponseData != string.Empty)
                {
                    Response.Write(strResponseData);
                }
                else
                    Response.Write(" <?xml version=\"1.0\" encoding=\"UTF-8\" ?><spellresult error=\"1\" /> ");
            }
        }

    }
}
