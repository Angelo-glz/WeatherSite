<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>
  <xsl:template match="/">
    <table>
      <xsl:for-each select="Pronostico/cadaHora">
        <tr>
          <td>
            Hora: <xsl:value-of select="Hora"/>
          </td>
          <td>
            Temperatura Maxima: <xsl:value-of select="Temperatura_Maxima"/>
          </td>
          <td>
            Temperatura Minima: <xsl:value-of select="Temperatura_Minima"/>
          </td>
          <td>
            Velocidad del viento: <xsl:value-of select="Velocidad_viento"/>
          </td>
          <td>
            Tipo de cielo: <xsl:value-of select="Tipo_de_Cielo"/>
          </td>
          <td>
            Probabilidad de lluvia: <xsl:value-of select="Probabilidad_de_lluvia"/>
          </td>
          <td>
            Probabilidad de tormenta: <xsl:value-of select="Probabilidad_de_tormenta"/>
          </td>
        </tr>
      </xsl:for-each>
    </table>
  </xsl:template>
</xsl:stylesheet>
