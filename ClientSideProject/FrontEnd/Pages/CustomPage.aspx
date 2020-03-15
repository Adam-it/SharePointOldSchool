<%@ Assembly Name="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"%> 
<%@ Page Language="C#" Inherits="Microsoft.SharePoint.WebPartPages.WikiEditPage" MasterPageFile="~masterurl/default.master" MainContentID="PlaceHolderMain" %> 
<%@ Import Namespace="Microsoft.SharePoint.WebPartPages" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content ContentPlaceHolderId="PlaceHolderPageTitle" runat="server">
	<SharePoint:ProjectProperty Property="Title" runat="server"/> - <SharePoint:ListItemProperty runat="server"/>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderAdditionalPageHead" runat="server">

    <SharePoint:ScriptLink language="javascript" Name="sp.js" runat="server" OnDemand="false" LoadAfterUI="true" Localizable="false" />

    <%-- custom css --%>
    <SharePoint:CSSRegistration name="<% $SPUrl:~SiteCollection/Style Library/Style/customStyle.css%>" After="corev5.css" runat="server"/>

    <%-- custom js --%>
    <script src="https://code.jquery.com/jquery-3.4.1.js" integrity="sha256-WpOohJOqMqqyKL9FccASB9O0KwACQJpFTUBLTYOVvVU=" crossorigin="anonymous"/>
    <SharePoint:ScriptLink language="javascript" name="~SiteCollection/Style Library/Javascript/data.js" Defer="true" LoadAfterUI="true" runat="server" />
    <SharePoint:ScriptLink language="javascript" name="~SiteCollection/Style Library/Javascript/HTMLHelpers.js" Defer="true" LoadAfterUI="true" runat="server" />
    <SharePoint:ScriptLink language="javascript" name="~SiteCollection/Style Library/Javascript/helpers.js" Defer="true" LoadAfterUI="true" runat="server" />
    <SharePoint:ScriptLink language="javascript" name="~SiteCollection/Style Library/Javascript/customJS.js" Defer="true" LoadAfterUI="true" runat="server" />
    
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderMain" runat="server">
	<span>Custom Page</span>
    <div class="pageRow">
        <p>ISAPI</p>
        <input onclick="GetListItems('ISAPI')" type="button" value="Get List Items"/>
        <input onclick="AddItem('ISAPI')" type="button" value="Add Item"/>
        <input onclick="AddItem('ISAPIElevated')" type="button" value="Add Item Elevated"/>
    </div>
    <div class="pageRow">
        <p>REST</p>
        <input onclick="GetListItems('REST')" type="button" value="Get List Items"/>
        <input onclick="AddItem('REST')" type="button" value="Add Item"/>
    </div>
    <div class="pageRow">
        <p>JSOM</p>
        <input onclick="GetListItems('JSOM')" type="button" value="Get List Items"/>
        <input onclick="AddItem('JSOM')" type="button" value="Add Item"/>
    </div>
    <div class="pageRow">
        <table class="outputTable"></table>
    </div>
</asp:Content>
