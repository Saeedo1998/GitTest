<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WebApplication1.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GitTest</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
                <h1>
                        Hello
                </h1>

                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Pages/VideoTest.aspx">Video Test</asp:HyperLink>
        </div>
    </form>
</body>
</html>
