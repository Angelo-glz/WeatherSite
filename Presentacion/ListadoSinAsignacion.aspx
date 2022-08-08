<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="ListadoSinAsignacion.aspx.cs" Inherits="ListadoSinAsignacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style4 {
            width: 213px;
        }
        .auto-style5 {
            width: 792px;
        }
        .auto-style6 {
            width: 792px;
            text-align: center;
        }
        .auto-style7 {
            width: 213px;
            height: 23px;
        }
        .auto-style8 {
            width: 792px;
            height: 23px;
        }
        .auto-style9 {
            height: 23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="auto-style1">
        <tr>
            <td class="auto-style4">&nbsp;</td>
            <td class="auto-style6">Listados sin asignación</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style7"></td>
            <td class="auto-style8">Año:
                <asp:TextBox ID="txtAño" runat="server" MaxLength="4" TextMode="Number"></asp:TextBox>
&nbsp;
                <asp:RadioButtonList ID="rbTipo" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem>Ciudad</asp:ListItem>
                    <asp:ListItem>Meteorologo</asp:ListItem>
                </asp:RadioButtonList>
&nbsp;<asp:Button ID="btnFiltrar" runat="server" OnClick="btnFiltrar_Click" Text="Filtrar" />
            </td>
            <td class="auto-style9"></td>
        </tr>
        <tr>
            <td class="auto-style4">&nbsp;</td>
            <td class="auto-style5">&nbsp;<asp:GridView ID="gvListado" runat="server" Width="533px">
                </asp:GridView>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style4">&nbsp;</td>
            <td class="auto-style5">
                <asp:Label ID="lblError" runat="server"></asp:Label>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>

