<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="GenerarPronostico.aspx.cs" Inherits="GenerarPronostico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style4 {
            width: 120px;
        }
        .auto-style9 {
            text-align: center;
        }
        .auto-style10 {
            width: 288px;
            text-align: left;
            height: 288px;
        }
        .auto-style11 {
            text-align: right;
            height: 52px;
        }
        .auto-style12 {
            width: 277px;
            text-align: left;
            height: 41px;
        }
        .auto-style13 {
            text-align: left;
            height: 41px;
        }
        .auto-style14 {
            width: 120px;
            height: 288px;
        }
        .auto-style15 {
            width: 277px;
            height: 288px;
        }
        .auto-style16 {
            height: 288px;
        }
        .auto-style17 {
            width: 288px;
            text-align: center;
        }
        .auto-style18 {
            width: 288px;
            height: 41px;
        }
        .auto-style19 {
            width: 277px;
            text-align: center;
        }
        .auto-style20 {
            width: 277px;
            height: 52px;
        }
        .auto-style21 {
            width: 120px;
            height: 52px;
        }
        .auto-style22 {
            width: 288px;
            height: 52px;
        }
        .auto-style23 {
            width: 120px;
            height: 41px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="auto-style1">
        <tr>
            <td class="auto-style4">Generar Pronostico</td>
            <td class="auto-style19">Fecha y Ciudad</td>
            <td class="auto-style17">Cada Hora (Al menos 2)</td>
            <td class="auto-style9">Vista Previa</td>
        </tr>
        <tr>
            <td class="auto-style14"></td>
            <td class="auto-style15">Fecha:<asp:Calendar ID="clFecha" runat="server"></asp:Calendar>
            </td>
            <td class="auto-style10">Hora:
                <asp:TextBox ID="txtHora" runat="server" TextMode="Number"></asp:TextBox>
                <br />
                Temperatura Maxima:
                <asp:TextBox ID="txtTempMax" runat="server" TextMode="Number"></asp:TextBox>
                <br />
                Temperatura Minima:&nbsp;
                <asp:TextBox ID="txtTempMinima" runat="server" TextMode="Number"></asp:TextBox>
                <br />
                Velocidad Viento:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtVelViento" runat="server" TextMode="Number"></asp:TextBox>
                <br />
                Tipo Cielo:
                <asp:DropDownList ID="ddlTipoCielo" runat="server" Width="136px">
                    <asp:ListItem>Despejado</asp:ListItem>
                    <asp:ListItem>Parcialmente Nuboso</asp:ListItem>
                    <asp:ListItem>Nuboso</asp:ListItem>
                </asp:DropDownList>
                <br />
                Probabilidad Lluvia:
                <asp:TextBox ID="txtProbLluvia" runat="server" Width="39px"></asp:TextBox>
&nbsp;%<br />
                Probabilidad Tormenta:
                <asp:TextBox ID="txtProbTormenta" runat="server" Width="35px"></asp:TextBox>
                %</td>
            <td class="auto-style16">
                <asp:GridView ID="gvCadaHora" runat="server" Width="317px">
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="auto-style21">
                <asp:Button ID="btnLimpiar" runat="server" OnClick="btnLimpiar_Click" Text="Limpiar" />
            </td>
            <td class="auto-style20">Codigo ciudad:
                <asp:TextBox ID="txtCiudad" runat="server" Width="149px"></asp:TextBox>
            </td>
            <td class="auto-style22">
                <asp:Button ID="btnAgregarHora" runat="server" Text="Agregar Pronostico Hora" OnClick="btnAgregarHora_Click" />
            </td>
            <td class="auto-style11">
                <asp:Button ID="btnAlta" runat="server" OnClick="btnAlta_Click" Text="Alta Pronostico Completo" />
            </td>
        </tr>
        <tr>
            <td class="auto-style23"></td>
            <td class="auto-style12">
                <asp:Label ID="lblErrorFYC" runat="server"></asp:Label>
            </td>
            <td class="auto-style18">
                <asp:Label ID="lblErrorCH" runat="server"></asp:Label>
            </td>
            <td class="auto-style13">
                <asp:Label ID="lblErrorPreview" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

