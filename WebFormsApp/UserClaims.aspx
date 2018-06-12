<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="UserClaims.aspx.cs" Inherits="WebFormsApp.UserClaims" %>
<%@ Register Src="~/Controls/UserClaims.ascx" TagPrefix="uc" TagName="UserClaims" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:LoginView runat="server" ID="lvUser">
        <AnonymousTemplate>
            <h2>No User is autheticated currently.</h2>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <uc:UserClaims runat="server" id="ucUserClaims" />
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>