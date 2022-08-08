<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="ListadoPronosticos.aspx.cs" Inherits="ListadoPronosticos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style4 {
            width: 368px;
        }
        .auto-style5 {
            width: 358px;
        }
        .auto-style6 {
            width: 63px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="auto-style1">
        <tr>
            <td class="auto-style6">&nbsp;</td>
            <td class="auto-style5">Listado de Pronosticos del Tiempo</td>
            <td class="auto-style4">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style6">&nbsp;</td>
            <td class="auto-style5">
                <asp:GridView ID="GridView1" runat="server">
                </asp:GridView>
            </td>
            <td class="auto-style4">Ciudad:&nbsp;
                <asp:DropDownList ID="DropDownList1" runat="server">
                </asp:DropDownList>
                <br />
                <asp:Button ID="btnFiltroCiudad" runat="server" Text="Filtrar Ciudad" OnClick="btnFiltroCiudad_Click" />
                <br />
                <br />
                Fecha solo del año en Curso:<asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                    <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                    <TodayDayStyle BackColor="#CCCCCC" />
                </asp:Calendar>
                <asp:Button ID="btnFiltroFecha" runat="server" OnClick="btnFiltrarFecha_Click" Text="Filtrar Fecha" />
                <br />
            </td>
            <td>
                <asp:Button ID="btnResumen" runat="server" Text="Resumen de trabajo meteorologos" OnClick="btnResumen_Click" />
                <asp:GridView ID="GridView2" runat="server">
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="auto-style6">&nbsp;</td>
            <td class="auto-style5">&nbsp;</td>
            <td class="auto-style4">
                <asp:Label ID="lblError" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar Filtros" OnClick="btnLimpiar_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

