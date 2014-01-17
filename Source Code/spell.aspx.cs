/*
 * Axirqi qetim ozgertilgen waqti: 2010.10.9
 * UyghurDev Imla Mulazimetiri
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
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Text;
using System.Net;
using System.Collections.Generic;
using System.IO;

namespace Net.UyghurDev.SpellService
{
    public partial class spell : System.Web.UI.Page
    {
        #region Static members

        private static string GOOGLE_URL = "https://www.google.com/tbproxy/spell?lang=";

        private static Regex RGX_TAGS = new Regex("<(.|\n)*?>", RegexOptions.Compiled);

        private static Regex RGX_UYGHUR_WORD = new Regex(@"\b[\p{IsArabic}]+\b", RegexOptions.Compiled);

        private static UTF8Encoding ENCODING = new UTF8Encoding();

        #endregion

        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Application["speller"] == null)
            {
                Application["speller"] = Tools.LoadSpeller();
                if (Application["speller"] == null)
                {
                    error();
                    return;
                }
            }
            

            if (Request.Params.Get("HTTP_URLREFERRER") == null)
            {
                error();
            }
            else 
                if (Request.QueryString["lang"] == null)
            {
                error();
            }
            else if (Request.QueryString["hl"] != null) // tuzutush jeryanini yollash iltimasi
            {
                System.IO.StreamReader reader = new System.IO.StreamReader(Request.InputStream, ENCODING);
                string strRequestContent = reader.ReadToEnd();
                reader.Close();
                GoogleSpell.SpellRequest spellRequest = GoogleSpell.SpellRequest.Load(strRequestContent);
                if (spellRequest == null) // Iltash uchur qurulmisi hata
                    return;
                else
                {
                    string strFeedBackContent = spellRequest.Text.Trim();
                    if (strFeedBackContent == "null" || strFeedBackContent == "" || strFeedBackContent == "##;")//tasaddibiyiqni kozde tutulup qoshup qoyuldi
                        return;
                    string[] arrPears = strFeedBackContent.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    string fileName = getFeedBackFilePath();
                    System.IO.StreamWriter writer = System.IO.File.AppendText(fileName);
                    lock (writer)
                    {
                        using (writer)
                        {
                            string urlReferrer = Request.Params.Get("HTTP_URLREFERRER");
                            urlReferrer = string.IsNullOrEmpty(urlReferrer) ? "unkown" : urlReferrer;
                            writer.WriteLine(string.Format("[{0}]-[{1}]", DateTime.Now.ToUniversalTime(),urlReferrer));
                            for (int i = 1; i < arrPears.Length; i++)
                                writer.WriteLine(arrPears[i]);
                        }
                    }
                }
            }
            else // normal imla tuzutush iltimasi
            {
                if (Request.QueryString["lang"].ToString().ToLower() != "uy")
                {
                    try
                    {
                        string strLang = Request.QueryString["lang"].ToString().ToLower().Trim();
                        System.IO.StreamReader reader = new System.IO.StreamReader(Request.InputStream, ENCODING);
                        string strRequestContent = reader.ReadToEnd();
                        reader.Close();
                        byte[] data = ENCODING.GetBytes(strRequestContent);
                        // request teyyarlash
                        if (string.IsNullOrEmpty(GOOGLE_URL))
                            GOOGLE_URL = "";
                        HttpWebRequest myRequest =
                          (HttpWebRequest)WebRequest.Create(string.Format("{0}{1}", GOOGLE_URL, strLang));
                        myRequest.Method = "POST";
                        myRequest.ContentType = "application/x-www-form-urlencoded";
                        myRequest.ContentLength = data.Length;
                        Stream stream = myRequest.GetRequestStream();
                        stream.Write(data, 0, data.Length);
                        stream.Close();
                        StreamReader responseReader = new StreamReader(myRequest.GetResponse().GetResponseStream(), ENCODING);
                        Response.Write(responseReader.ReadToEnd());
                        responseReader.Close();
                    }
                    catch
                    {
                        error();
                    }
                }
                else
                {
                    try
                    {
                        System.IO.StreamReader reader = new System.IO.StreamReader(Request.InputStream, ENCODING);
                        string strRequestContent = reader.ReadToEnd();
                        reader.Close();
                        GoogleSpell.SpellRequest spellRequest = GoogleSpell.SpellRequest.Load(strRequestContent);
                        if (spellRequest == null)
                            error();
                        else
                        {
                            GoogleSpell.SpellResult spellResult = SpellCheck(spellRequest);
                            Response.Write(spellResult.ToString());
                        }
                    }
                    catch
                    {
                        error();
                    }
                }
            }
        }
        /// <summary>
        /// Response ge hataliq uchurini yollash
        /// </summary>
        private void error()
        {
            Response.Write(" <?xml version=\"1.0\" encoding=\"UTF-8\" ?><spellresult error=\"1\" /> ");
        }

        /// <summary>
        /// Imla tekshurush
        /// </summary>
        /// <param name="spellRequest"></param>
        /// <returns></returns>
        private GoogleSpell.SpellResult SpellCheck(GoogleSpell.SpellRequest spellRequest)
        {
            MatchCollection matchCollection = RGX_UYGHUR_WORD.Matches(spellRequest.Text);
            if (matchCollection == null || matchCollection.Count == 0)
            {
                GoogleSpell.SpellResult result = new GoogleSpell.SpellResult();
                result.CharsChecked = spellRequest.Text.Length;
                return result;
            }

            GoogleSpell.SpellResult spellResult = new GoogleSpell.SpellResult();

            List<GoogleSpell.SpellCorrection> corrections = new List<GoogleSpell.SpellCorrection>();
            spellResult.CharsChecked = spellRequest.Text.Length;

            Dictionary<string, List<string>> buffer = new Dictionary<string, List<string>>();
            string fixedValue = string.Empty;

            foreach (Match match in matchCollection)
            {
                fixedValue = match.Value.IndexOf('\u0640') > -1 ? match.Value.Replace("\u0640", string.Empty) : match.Value;
                Net.UyghurDev.Spelling.CheckingResult checkResult;

                if (buffer.ContainsKey(fixedValue))
                {
                    checkResult = new Net.UyghurDev.Spelling.CheckingResult();
                    checkResult.Correct = false;
                    checkResult.Word = match.Value;
                    checkResult.Suggestions = buffer[fixedValue];
                }
                else
                {
                    checkResult = (Application["speller"] as Net.UyghurDev.Spelling.TextBasedSpellChecker).SpellCheck(fixedValue, 7);
                    checkResult.Word = match.Value;
                    if (checkResult.Correct == false)
                        buffer.Add(fixedValue, checkResult.Suggestions);
                }

                if (checkResult.Correct)
                    continue;

                GoogleSpell.SpellCorrection correction = new GoogleSpell.SpellCorrection();
                correction.Confidence = 0;
                correction.Length = match.Length;
                correction.Offset = match.Index;
                correction.Suggestions = checkResult.Suggestions.ToArray();

                corrections.Add(correction);
            }

            if (corrections.Count == 0)
                return spellResult;

            spellResult.Corrections = corrections.ToArray();
            return spellResult;
        }

        /// <summary>
        /// Bugunki tuzutush jeryan hojjitining toluq isimini qayturidu
        /// </summary>
        /// <returns></returns>
        private string getFeedBackFilePath()
        {
           return System.Web.HttpRuntime.AppDomainAppPath + "\\App_Data\\" + "\\Spell\\" + string.Format("FeedBack_{0}.txt", DateTime.Now.ToString("yyyy.MM.dd"));
        }

        #endregion
    }
}
