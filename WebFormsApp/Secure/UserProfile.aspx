<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="WebFormsApp.Secure.UserProfile" %>
<%@ Register Src="~/Controls/UserClaims.ascx" TagPrefix="uc" TagName="UserClaims" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Secure Page: User Profile</h1>
    <uc:UserClaims runat="server" id="ucUserClaims" />
</asp:Content>