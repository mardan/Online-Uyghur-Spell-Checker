using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

public partial class webcheck : System.Web.UI.Page
{
    private static Regex RGX_TAGS = new Regex("<(.|\n)*?>", RegexOptions.Compiled);

    private static Regex RGX_UYGHUR_WORD = new Regex(@"\b[\u0621-\u06ff]+\b", RegexOptions.Compiled);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["url"] == null)
            {
                Session["Message"] = "تەكشۈرۈش ئىناۋەتسىز";
                Response.Redirect("Message.aspx");
                return;
            }

            if (Application["speller"] == null)
            {
                Application["speller"] = Tools.LoadSpeller();
                if (Application["speller"] == null)
                {
                    Session["Message"] = "مۇلازىمېتىردا مەسىلە كۆرۈلدى";
                    Response.Redirect("Message.aspx");
                    return;
                }
            }

            string url = Session["url"] as string;
            Session["url"] = null;


            try
            {
                string html = DownloadText(url);// client.DownloadString(url);

                html = fixHTML(html, url);
                Response.Write(html);
            }
            catch
            {
                Session["Message"] = "مەزكۇر بەتكە ئۇلىنالمىدى";
                Response.Redirect("Message.aspx");
            }

        }
    }

    private  string DownloadText(string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        {
            
            return reader.ReadToEnd();
        }
    }

    public string fixHTML(string html, string url)
    {
        Regex rg0 = new Regex(@"<body[^>]*>(.*?)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        Regex rg1 = new Regex(@"(?<=<img\s+[^>]*?src=(?<q>['""]))(?<url>.+?)(?=\k<q>)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        Regex rg2 = new Regex(@"(?<=<link\s+[^>]*?href=(?<q>['""]))(?<url>.+?)(?=\k<q>)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        Regex rg3 = new Regex(@"(?<=<script\s+[^>]*?src=(?<q>['""]))(?<url>.+?)(?=\k<q>)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        Regex rg4 = new Regex(@"</head[^>]*>", RegexOptions.Compiled | RegexOptions.IgnoreCase);


        int nAll = 0, nError = 0;
        int n1 = url.LastIndexOf('/');
        string result = string.Empty;
        Dictionary<string, List<string>> buffer = new Dictionary<string, List<string>>();
        if (n1 > 6)
            url = url.Substring(0, n1 + 1);
        else
            url += '/';


        Match matchTemp = rg0.Match(html);
        int bodyIndex = -1;
        if (matchTemp.Success)
            bodyIndex = matchTemp.Index;


        //spelll check
        result = webcheck.RGX_UYGHUR_WORD.Replace(html, delegate(Match match)
        {
            if (match.Index < bodyIndex)
                return match.Value;

            nAll++;
            string fixedValue = match.Value.IndexOf('\u0640') > -1 ? match.Value.Replace("\u0640", string.Empty) : match.Value;
            Net.UyghurDev.Spelling.CheckingResult checkingResult;

            if (buffer.ContainsKey(fixedValue))
            {
                checkingResult = new Net.UyghurDev.Spelling.CheckingResult();
                checkingResult.Correct = false;
                checkingResult.Word = match.Value;
                checkingResult.Suggestions = buffer[fixedValue];
            }
            else
            {
                checkingResult = (Application["speller"] as Net.UyghurDev.Spelling.TextBasedSpellChecker).SpellCheck(fixedValue,7);
                checkingResult.Word = match.Value;
                if (checkingResult.Correct == false)
                    buffer.Add(fixedValue, checkingResult.Suggestions);
            }

            if (checkingResult.Correct)
                return match.Value;

            nError++;
            string replacement = string.Empty;
            if (checkingResult.Suggestions != null && checkingResult.Suggestions.Count > 0)
            {
                string sugesstionsLine = string.Empty;
                for (int i = 0; i < checkingResult.Suggestions.Count; i++)
                {
                    if (i == checkingResult.Suggestions.Count - 1)
                        sugesstionsLine += checkingResult.Suggestions[i];
                    else
                        sugesstionsLine += checkingResult.Suggestions[i] + " | ";
                }
                replacement = string.Format("<span style='color:Red; background-color:Yellow' title='{0}'>{1}</span>", sugesstionsLine, match.Value);
            }
            else
                replacement = string.Format("<span style='color:Red; background-color:Yellow' title='{0}'>{1}</span>", "تەۋسىيە سۆز تېپىلمىدى", match.Value);
            return replacement;
        });

        //Add statics
        Match m1 = rg0.Match(result);
        if (m1.Success)
            result = result.Insert(m1.Index + m1.Length, getStasticaHTML(nAll, nError));

        //fix images url
        result = rg1.Replace(result, delegate(Match match)
        {
            if (match.Value.ToLower().StartsWith("http://"))
                return match.Value;
            else
                return url + match.Value.TrimStart('/');
        });

        //fix css  urls
        result = rg2.Replace(result, delegate(Match match)
        {
            if (match.Value.ToLower().StartsWith("http://"))
                return match.Value;
            else
                return url + match.Value.TrimStart('/');
        });

        //fix js urls
        result = rg3.Replace(result, delegate(Match match)
        {
            if (match.Value.ToLower().StartsWith("http://"))
                return match.Value;
            else
                return url + match.Value.TrimStart('/');
        });

        //add fonts
        result = rg4.Replace(result, delegate(Match match)
        {
            return "<style type='text/css'>body,p,input,td,tr,input,textarea,htmlarea,menu,atc_content,menu_editor,content,mm_content,atc_tags,div,span,select,a{font-family:Alkatip Tor, UKIJ Tuz Tom, Alpida Unicode System, UKK Basma,UKK Tuz Tom,UKK TZK2, Microsoft Uighur, Tahoma;} @font-face {font-family: ALKATIP Tor;src: url('image/ALKATIPTor.eot');}</style>"
                + match.Value;
        });

        return result;
    }

    private  string getStasticaHTML(int all, int error)
    {
        string template = "<table dir='rtl' cellspacing='1' style=' width: 100%;font-family:{0};background-color: #000000;'> <tr><td style='text-align: center; font-size:16pt'><span style='color: #FFFFFF;'> جەمئى سۆز: {1} دانە، گۇمانلىق سۆز {2} دانە، توغرىلىق نىسپىتى: {3}</span></td> </tr> <tr> <td  style='text-align: center; font-size:11pt; font-family:{4}'><span style='font-size:11pt;color:#ffffff;'>يۇقارقى سانلار پايدىلىنىش ئۈچۈنلا سۇنۇلدى، ھەقىقەت بولۇشى ناتايىن؛ ئىملاسى خاتا سۆزنىڭ ئۈستىگە مائوسنى توغۇرلاپ تۇرسىڭىز كاندىدات سۆزلەر كۆرۈنىدۇ.</span></td></tr></table>";
        float f = (1 - (float)error / all);
        string rate =  f.ToString("%0.0");
        return string.Format(template, Setting.MainFontFamily, all, error, rate, Setting.MainFontFamily);

    }
}
