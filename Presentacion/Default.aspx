<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
      .auto-style1 {
        width: 100%;
      }
      .auto-style2 {
        width: 257px;
      }
      .auto-style3 {
        width: 966px;
      }
      .auto-style4 {
        width: 966px;
        text-align: center;
      }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
          <table class="auto-style1">
            <tr>
              <td class="auto-style2">&nbsp;</td>
              <td class="auto-style4">Formulario Principal</td>
              <td>
                  <asp:Button ID="btnLog" runat="server" OnClick="btnLog_Click" Text="Iniciar Sesion" />
                </td>
            </tr>
            <tr>
              <td class="auto-style2">
                  &nbsp;</td>
              <td class="auto-style3">
                  <asp:TextBox ID="txtCiudad" runat="server" Width="136px"></asp:TextBox>
&nbsp;&nbsp;
                  <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar Ciudad" />
                  <br />
                <asp:GridView ID="gvPronosticos" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gvPronosticos_SelectedIndexChanged" Width="275px">
                    <Columns>
                        <asp:BoundField AccessibleHeaderText="Ciudad" DataField="Ciudad" HeaderText="Ciudad" />
                        <asp:BoundField AccessibleHeaderText="Pais" DataField="Pais" HeaderText="Pais" />
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                </asp:GridView>
                  <br />
                  <asp:Xml ID="xmlCadaHora" TransformSource="~/XML/XSLTFile.xslt" runat="server"></asp:Xml>
                  <br />
              </td>
              <td>&nbsp;</td>
            </tr>
            <tr>
              <td class="auto-style2">&nbsp;</td>
              <td class="auto-style3">
                <asp:LinkButton ID="lbAtras" runat="server" OnClick="lbAtras_Click">&lt;-- Atras</asp:LinkButton>
              </td>
              <td>
                <asp:Label ID="lblError" runat="server"></asp:Label>
              </td>
            </tr>
          </table>
        </div>
    </form>
</body>
</html>
