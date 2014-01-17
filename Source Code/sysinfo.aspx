<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sysinfo.aspx.cs" Inherits="sysinfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            color: #0000FF;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table class="style1">
            <tr>
                <td class="style2">
                    System Info<br />
                    ---------------------------------------</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Online Users:
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Word Count:
                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                    <br />
                    Pear Count:
                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Server State: On</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
