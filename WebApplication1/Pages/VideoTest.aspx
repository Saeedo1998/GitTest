<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VideoTest.aspx.cs" Inherits="WebApplication1.Pages.VideoTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title></title>
</head>
<body>
        <form id="form1" runat="server">
                <div>
                         <nav class="crumbs">
                                <ol>
                                        <li class="crumb"><a href="../Home.aspx">Home</a></li>
                                        <li class="crumb"><a href="#">VideoTest</a></li>
                                </ol>
                        </nav>

                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                        <asp:TextBox ID="txtUrl" runat="server" TextMode="Url" Width="25%" 
                                                OnTextChanged="txtUrl_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <asp:Label ID="lbError" runat="server" Text="" ForeColor="DarkRed"></asp:Label>

                                </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:Button ID="btDownload" runat="server" Text="Download" OnClick="btDownload_Click" />
                        <br />
                        <br />
                </div>
        </form>
</body>
</html>
