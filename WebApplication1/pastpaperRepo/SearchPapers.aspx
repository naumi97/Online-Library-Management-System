<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchPapers.aspx.cs" Inherits="WebApplication1.pastpaperRepo.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBoxSC" runat="server" Height="31px" OnTextChanged="TextBoxSC_TextChanged" Width="232px"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Height="33px" Text="Search" Width="158px" />
        </div>
    </form>
</body>
</html>
