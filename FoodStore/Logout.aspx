<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="FoodStore.Logout" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Logout</title>
    <script type="text/javascript">
        function showAlert() {
            alert('You have been logged out.');
        }
    </script>
</head>
<body onload="showAlert()">
    <form id="form1" runat="server">
    </form>
</body>
</html>
