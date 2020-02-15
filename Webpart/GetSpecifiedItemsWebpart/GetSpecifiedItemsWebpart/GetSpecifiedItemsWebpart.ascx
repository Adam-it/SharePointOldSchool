<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GetSpecifiedItemsWebpart.ascx.cs" Inherits="GetSpecifiedItemsWebpart.GetSpecifiedItemsWebpart.GetSpecifiedItemsWebpart" %>

<div>
    <asp:Label ID="LbStaticText_Time" runat="server" Text="Time: "></asp:Label>
    <asp:Label ID="LbTime" runat="server" Text=""></asp:Label>
    <br />
    <asp:Label ID="LbEmployeeIdList" runat="server" Text="EmployeeId List: "></asp:Label>
    <asp:TextBox ID="TxtEmployeeIdList" Text="1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,96,97,98,99,100" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="BtnGetItems" runat="server" Text="GetListItems" OnClick="BtnGetItems_Click" />
    <br />
    <asp:GridView ID="GridView" runat="server"></asp:GridView>
</div>