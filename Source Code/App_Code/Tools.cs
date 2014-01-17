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
using System.Net.Sockets;
using Net.UyghurDev.Spelling;

/// <summary>
///Tools 的摘要说明
/// </summary>
public class Tools
{
	public Tools()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public static TextBasedSpellChecker LoadSpeller()
    {
        Net.UyghurDev.Spelling.TextBasedSpellChecker uspellChecker = new Net.UyghurDev.Spelling.TextBasedSpellChecker();
        Net.UyghurDev.Spelling.Interfaces.IInitialable init
            = ((Net.UyghurDev.Spelling.TextBasedSpellChecker)uspellChecker) as Net.UyghurDev.Spelling.Interfaces.IInitialable;

        System.Collections.Generic.Dictionary<string, object> paramlar = new System.Collections.Generic.Dictionary<string, object>();
        paramlar.Add("path", System.Web.HttpRuntime.AppDomainAppPath + "\\App_Data\\" + "\\Spell\\");

        bool succ = init.Intitial(paramlar);
        return succ ? uspellChecker : null;
    }

    public static bool UrlIsValid(string smtpHost)
    {
        bool br = false;
        try
        {
            IPHostEntry ipHost = Dns.Resolve(smtpHost);
            br = true;
        }
        catch (SocketException se)
        {
            br = false;
        }
        return br;
    }

    public static string GetSpellServerUrl(HostToPlace host)
    {
        switch (host)
        {
            case HostToPlace.LocalHost: { return "http://localhost:50093/UyghurDevImla/spell.aspx"; }
            case  HostToPlace.UyghurDev: { return "http://imla.uyghurdev.net/spell.aspx"; }
            case HostToPlace.IzchiBiz: { return "http://www.izchi.biz/imla/spell.aspx"; }
            case HostToPlace.Izchilar: { return "http://www.izchilar.biz:1495/spell.aspx"; }
            default: return "http://localhost:58740/UyghurDevImla/spell.aspx";
        }
    }

    public enum HostToPlace
    {
        LocalHost,
        UyghurDev,
        IzchiBiz,
        Izchilar
    }
        
}
