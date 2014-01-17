<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Message.aspx.cs" Inherits="Message" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>مەسىلە كۆرۈلدى</title>
        <style type="text/css">
        @font-face {font-family: ALKATIP Tor;src: url("image/ALKATIPTor.eot");}    
        body
        {
	        margin: 0px;
	        direction:rtl;
	        font-family:ALKATIP Tor, UKIJ Tuz Tom, UKK TZK2, Microsoft Uighur, Tahoma;
        }
            </style>
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
    <table dir="rtl" align="center" style="width:85%">
        <tr>
            <td>
            <div style="width: 100%; height:18px; text-align: left;">
              <table width= "100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="text-align: left; width:100%">
                    <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl="~/image/home16.png" 
                        NavigateUrl="~/Default.aspx" Target="_self"></asp:HyperLink>
                </td>
            </tr>
        </table>
            </div>
    <div style="width: 100%; height:18px; background-color: #D5DDF3;">
    
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td >
                    &nbsp;</td>
                <td style="text-align: left">
                    &nbsp;</td>
            </tr>
        </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" 
                    style="width:100%;background-color: #FFFFFF; margin:0; padding:0">
                    <tr>
                        <td>
                            <table style="width:100%">
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="padding:0; margin:0">
            <td style="text-align: center">
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr style="padding:0; margin:0">
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="background-color: #F0F3FB; text-align: center;">
                &nbsp;</td>
        </tr>
    </table>
    </form>
    </body>
</html>
