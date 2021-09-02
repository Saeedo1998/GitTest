<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WebApplication1.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>GitTest</title>
</head>
<body>
        <form id="form1" runat="server">
                <div>
                        <h1>Hello
                        </h1>

                        <nav class="crumbs">
                                <ol>
                                        <li class="crumb">
                                                <a href="#">
                                                        Home
                                                </a>
                                        </li>
                                        <li class="crumb">
                                                <asp:HyperLink ID="HyperLink1" runat="server"
                                                        NavigateUrl="~/Pages/VideoTest.aspx">
                                                        Video Test
                                                </asp:HyperLink>
                                        </li>
                                        <%--<li class="crumb"><a href="#">Bikes</a></li>
                                        <%--<li class="crumb"><a href="#">BMX</a></li>--%>
                                        <%--<li class="crumb">Jump Bike 3000</li>--%>
                                </ol>
                        </nav>


                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <address>
                                Written by <a href="mailto:webmaster@example.com">Jon Doe</a>.<br>
                                Visit us at:<br>
                                Example.com<br>
                                Box 564, Disneyland<br>
                                USA
                        </address>
                </div>
        </form>
</body>
</html>
