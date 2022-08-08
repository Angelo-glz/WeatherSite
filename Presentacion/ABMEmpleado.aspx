<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="ABMEmpleado.aspx.cs" Inherits="ABMEmpleado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    .auto-style4 {
        text-align: center;
        width: 396px;
    }
    .auto-style5 {
        width: 396px;
    }
    .auto-style6 {
        width: 68px;
    }
    .auto-style7 {
        width: 68px;
        text-align: center;
    }
        .auto-style8 {
            text-align: right;
            width: 396px;
        }
        .auto-style9 {
            text-align: left;
            width: 968px;
        }
        .auto-style10 {
            width: 968px;
        }
        .auto-style11 {
            text-align: center;
            width: 968px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="auto-style1">
        <tr>
            <td class="auto-style6">&nbsp;</td>
            <td class="auto-style5">ABM Empleados</td>
            <td class="auto-style10">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style6">&nbsp;</td>
            <td class="auto-style8">
                Nombre de Usuario:
                <asp:TextBox ID="txtNomUsu" runat="server"></asp:TextBox>
                <br />
                Contraseña:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtPass" runat="server"></asp:TextBox>
                <br />
                Nombre Completo:&nbsp;
                <asp:TextBox ID="txtNomComp" runat="server"></asp:TextBox>
                <br />
                Carga Horaria:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtCargaH" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style9">
                <br />
                <br />
                El formato de la contraseña es en orden[2 letras, 2 numeros, 1 simbolo, 2 letras, 1 numero, 1 simbolo]<br />
                <br />
                <br />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style7">
                <asp:Label ID="lblError" runat="server"></asp:Label>
            </td>
            <td class="auto-style4">
                <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar" />
                <asp:Button ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" Text="Agregar" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" />
                <asp:Button ID="btnModificar" runat="server" OnClick="btnModificar_Click" Text="Modificar" />
            </td>
            <td class="auto-style11">
                &nbsp;</td>
            <td>
                <asp:Button ID="btnLimpiarForm" runat="server" Text="Limpiar Formulario" OnClick="btnLimpiarForm_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

