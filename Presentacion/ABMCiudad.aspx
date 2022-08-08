<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="ABMCiudad.aspx.cs" Inherits="ABMCiudad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    .auto-style4 {
        text-align: center;
        width: 876px;
    }
    .auto-style5 {
        width: 876px;
    }
    .auto-style6 {
        width: 68px;
    }
    .auto-style7 {
        width: 68px;
        text-align: center;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="auto-style1">
        <tr>
            <td class="auto-style9"></td>
            <td class="auto-style11"></td>
            <td class="auto-style5">ABM Ciudad</td>
            <td class="auto-style10"></td>
        </tr>
        <tr>
            <td class="auto-style6">&nbsp;</td>
            <td class="auto-style12">
                &nbsp;</td>
            <td class="auto-style8">
                Codigo:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtCodigo" runat="server"></asp:TextBox>
                &nbsp;<br />
                NombrePais:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtNomPais" runat="server"></asp:TextBox>
                <br />
                NombreCiudad:
                <asp:TextBox ID="txtNomCiudad" runat="server"></asp:TextBox>
                <br />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style7">
                <asp:Label ID="lblError" runat="server"></asp:Label>
            </td>
            <td class="auto-style12">
                &nbsp;</td>
            <td class="auto-style4">
                <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar" />
                <asp:Button ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" Text="Agregar" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" />
                <asp:Button ID="btnModificar" runat="server" OnClick="btnModificar_Click" Text="Modificar" />
            </td>
            <td>
                <asp:Button ID="btnLimpiar" runat="server" OnClick="btnLimpiar_Click" Text="Limpiar Formulario" />
            </td>
        </tr>
    </table>
</asp:Content>