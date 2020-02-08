<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomVisualWebPart.ascx.cs" Inherits="Webpart.CustomVisualWebPart.CustomVisualWebPart" %>

<div>
    <asp:Label ID="LbStaticText_userData" runat="server" Text="userData: "></asp:Label>
    <asp:Label ID="LbUserData" runat="server" Text=""></asp:Label>
    <br />

    <asp:Label ID="LbStaticText_userGroups" runat="server" Text="userGroups: "></asp:Label>
    <asp:Label ID="LbUserGroups" runat="server" Text=""></asp:Label>
    <br />

    <asp:Label ID="LbStaticText_userProperties" runat="server" Text="userProperties: "></asp:Label>
    <asp:Label ID="LbUserProperties" runat="server" Text=""></asp:Label>
    <br />

    <asp:Label ID="LbStaticText_Time" runat="server" Text="Time: "></asp:Label><asp:Label ID="LbTime" runat="server" Text=""></asp:Label>
    <br />

    <asp:Button ID="BtnGetItemFirstMethod" runat="server" Text="GetListItemsMethod1" OnClick="BtnGetItemFirstMethod_Click"/>
    <asp:Button ID="BtnGetItemSecondMethod" runat="server" Text="GetListItemsMethod2" OnClick="BtnGetItemSecondMethod_Click"/>
    <br />

    <asp:CheckBox ID="ChAddItemAsFarmAdmin" runat="server" Text="Add item as farm administrator" Checked="false"/>
    <asp:Button ID="BtnAddNewItem" runat="server" Text="AddItem" OnClick="BtnAddNewItem_Click"/>
    <br />

    <asp:Button ID="BtnAddMonitoredScopeToDevDashboard" runat="server" Text="AddMonitoredScopeToDevDashboard" OnClick="BtnAddMonitoredScopeToDevDashboard_Click"/>

    <asp:GridView ID="GridView" runat="server"></asp:GridView>
</div>