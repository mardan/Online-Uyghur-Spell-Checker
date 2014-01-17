<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Net.UyghurDev.SpellService._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>تور ئىملاچىسى | 维吾尔文在线校对器 | Uyghur Online Spell Checking</title>
    <meta name="Description" content="تور بەت يۈزىدە ئۇيغۇرچە ئىملا تەكشۈرۈش ۋە توغۇرلاش ئېلىپ بارالايسىز| 让您能够在线进行维吾尔文校对 | Enable you to check Uighur spelling online" />
    <meta name="Keywords" content="ئىملا تۈزەش, ئىملا تۈزىتىش, كوررېكتورلۇق, كوررېكتور, ئىملا تەكشۈرۈش, ئۇيغۇرچە, ئۇيغۇر, 维吾尔文, 校对, 在先校对, 拼写错误, Uighur, Uyghur, Spelling, Online Spell checking, Web Page Spell Checking" />

    <link rel="shortcut icon" href="favicon.ico" >
    
   <link rel="icon" type="image/gif" href="animated_favicon1.gif" >
     
         <style type="text/css">
         @font-face
        {
            font-family: ALKATIP Tor;
            src: url("image/ALKATIPTor.eot");
        } 
    </style>
     
    <script type="text/javascript" src="googiespell/AJS.js"></script>

    <script type="text/javascript" src="googiespell/googiespell.js"></script>

    <script type="text/javascript" src="googiespell/cookiesupport.js"></script>

    <link href="googiespell/googiespell.css" rel="stylesheet" type="text/css" media="all" />

    <script src="googiespell/bedit.js" type="text/javascript"></script>

    <script type="text/javascript">

        function copyToClipboard() {
            if (window.clipboardData && clipboardData.setData) {
                clipboardData.setData("Text", document.getElementById('ta1').value);
            }
        }

        function pasteText() {
            document.getElementById('ta1').focus();
            document.execCommand("Paste");

        }

        function clearText() {
            document.getElementById('ta1').value = "";
        }

        function dictSearch() {
            var keyword = encodeURI(document.getElementById("txtDict").value);
            var ref = window.open("http://www.tilbil.com/mini.aspx?word=" + keyword, "Tilbil.com", "width=400,height=400,directories=0,toolbar=0,scrollbars=1,resizable=1", null);
        }

        function resize(code) {
            var ta = document.getElementById("ta1");
            if (code == 1)
                ta.style.height = (parseInt(ta.style.height) + 50) + "px";
        }
    </script>

    <script type="text/javascript">

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-23401397-1']);
        _gaq.push(['_trackPageview']);

        (function() {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();

    </script>


</head>
<body>
    <form id="form1" runat="server">
    <table dir="rtl" align="center" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td>
                <div style="width: 100%; height: 20px; background-color: #D5DDF3;">
                    <table style="font-size: small; padding-top: 2px" cellpadding="0" cellspacing="0"
                        width="100%">
                        <tr>
                            <td>
                                &nbsp;&nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Help.aspx"
                                    Target="_blank" ForeColor="#333333">ياردەم</asp:HyperLink>
                                &nbsp;&nbsp;
                                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Warning.aspx" Target="_blank"
                                    ForeColor="#333333">ئەسكەرتىش</asp:HyperLink>
                            </td>
                            <td style="text-align: left;">
                                ئەپچىل كىرىش ئادرېسلىرى: <span style="font-family: Arial; font-weight: bold; color: Blue">
                                    &nbsp;imla.izchi.biz&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;imla.uyghurdev.net&nbsp;</span>&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" style="width: 100%; background-color: #FFFFFF;
                    margin: 0; padding: 0">
                    <tr>
                        <td>
                            <table style="width: 100%; font-size: small">
                                <tr>
                                    <td>
                                        .
                                    </td>
                                    <td>
                                        بەت يۈزىدە خېتىڭىزنىڭ ۋە تور بېتىڭىزنىڭ ئىملاسىنى تەكشۈرەلەيدۇ
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        .
                                    </td>
                                    <td>
                                        <span>ئىملانى تېخمۇ ياخشى تەكشۈرسۇن دېسىڭىز&nbsp;&nbsp;</span><a href="About.aspx"
                                            target="_blank"><span style="color: #8EB43B;">سۆز ئامبىرى تەمىنلەڭ</span></a><span
                                                style="color: #8EB43C;">! </span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="text-align: left; margin: 0; padding: 0; vertical-align: middle">
                            &nbsp;<span style="color: #8EB43C;"><asp:Image ID="Image1" runat="server" ImageAlign="Middle"
                                ImageUrl="~/image/spell_check.jpg" />
                            </span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="padding: 0; margin: 0">
            <td>
                <div style="width: 100%; height: 18px; background-color: #D5DDF3;">
                </div>
            </td>
        </tr>
        <tr>
            <td style="background-color: #F0F3FB;">
                <table style="width: 80%;" align="center">
                    <tr style="padding: 2">
                        <td>
                            <textarea id="ta1" class="textAreaNormal" onkeypress="return addchar(this,event);"
                                onkeydown="return proc_kd(event);" style="width: 100%; height: 350px"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" style="width: 100%; height: 24px;">
                                <tr>
                                    <td valign="top">
                                        <input id="btnPaste" onclick="pasteText();" class="buttons" type="button" value="چاپلاش" /><input
                                            id="btnCopy" class="buttons" type="button" onclick="copyToClipboard();" value="كۆچۈرۈش" /><input
                                                id="btnClear" onclick="clearText();" class="buttons" type="button" value="تازىلاش" />&nbsp;&nbsp;&nbsp;&nbsp;
                                        <input id="btnZoomIn" onclick="resize('1');" class="buttons" type="button" value="رامكىنى چوڭايتىش" />&nbsp;
                                    </td>
                                    <td valign="top" style="text-align: left; font-size: small; color: Gray">
                                        <span style="font-family: Arial, Helvetica, sans-serif;">ctrl+k</span> بېسىلسا كىرگۈزگۈچ
                                        تىلى ئالمىشىدۇ
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div style="width: 100%; background-color: #D5DDF3; text-align: center;">
                    <table align="center" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td style="font-size: medium; vertical-align: middle">
                                تور بەتتىكى ئىملانى تەكشۈرۈش
                            </td>
                        </tr>
                    </table>
                </div>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <table cellpadding="0" width="100%">
                                <tr>
                                    <td style="text-align: left; padding: 1px; color: Gray; width: 25%">
                                        تور بەت ئادرېسى:&nbsp;&nbsp;
                                    </td>
                                    <td dir="ltr" style="width: 50%; vertical-align: middle">
                                        <asp:TextBox ID="txtURL" runat="server" Width="100%" Font-Size="Medium" Style="direction: ltr;
                                            font-family: Arial; font-weight: normal" MaxLength="1000"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; vertical-align: middle; width: 25%">
                                        <asp:Button ID="btnCheckURL" runat="server" Font-Names="ALKATIP Tor, UKIJ Tuz Tom, UKK TZK2, Microsoft Uighur, Tahoma"
                                            OnClick="btnCheckURL_Click" Text="تەكشۈرۈش" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 5px" colspan="3">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; padding: 1px; color: Gray" colspan="3">
                                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Warning.aspx" Style="text-align: center;
                                            font-size: small;" Target="_blank">نېمىشقا بەزى توغرا سۆزنىمۇ خاتا دەپ ھۆكۈم قىلىدۇ؟</asp:HyperLink>
                                    &nbsp;
                                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/UDevImlaLogo.gif" Style="text-align: center;
                                            font-size: small;" Target="_blank">مەزكۇر بېكەت لوگوسى لازىممۇ؟</asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px" colspan="3">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div style="width: 100%;  background-color: #D5DDF3; text-align: center;
                    font-size: 9pt; ;">
                    维吾尔软件创作网实验室&nbsp;&nbsp;ئۇيغۇر يۇمشاق دېتال ئىجادىيەت تورى تەجرىبىخانىسى&nbsp;&nbsp;<span style=" font-family:Arial">Uyghur Software Developer Network Laboratory</span></div>
            </td>
        </tr>
    </table>
    </form>

    <script type="text/javascript">
        var googie1 = new GoogieSpell("googiespell/", "http://imla.uyghurdev.net/spellproxy.aspx?lang=");
        googie1.decorateTextarea("ta1");   
    </script>

</body>
</html>
