<xsl:stylesheet xmlns:x="http://www.w3.org/2001/XMLSchema"
                xmlns:d="http://schemas.microsoft.com/sharepoint/dsp"
                version="1.0"
                exclude-result-prefixes="xsl msxsl ddwrt"
                xmlns:ddwrt="http://schemas.microsoft.com/WebParts/v2/DataView/runtime"
                xmlns:asp="http://schemas.microsoft.com/ASPNET/20"
                xmlns:__designer="http://schemas.microsoft.com/WebParts/v2/DataView/designer"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:msxsl="urn:schemas-microsoft-com:xslt"
                xmlns:SharePoint="Microsoft.SharePoint.WebControls"
                xmlns:ddwrt2="urn:frontpage:internal">

  <xsl:template match="FieldRef[@Name = 'Custom']" mode="Text_body">

    <xsl:param name="thisNode" select="." />

    <span style="background-color:lightgreen;font-weight:bold">

      <xsl:value-of select="$thisNode/@*[name()=current()/@Name]" />

    </span>

  </xsl:template >

</xsl:stylesheet>