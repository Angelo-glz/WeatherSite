<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logueo.aspx.cs" Inherits="Logueo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="auto-style1">
            <tr>
                <td>&nbsp;</td>
                <td>Formulario de Inicio de sesion.</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style2">Usuario:&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtNomUsu" runat="server"></asp:TextBox>
                    &nbsp;usuario001 al 005 empleado usuario006 al 012 meteorologo<br />
                    Contraseña:<asp:TextBox ID="txtPass" runat="server" TextMode="Password"></asp:TextBox>
                &nbsp; contraseña para todos ab12.ab1.</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style2">
                    <asp:Button ID="btnLogueo" runat="server" Text="Login" OnClick="btnLogueo_Click" />
&nbsp;&nbsp;
                    <asp:Label ID="lblError" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <div>
        </div>
    </form>
</body>
</html>
