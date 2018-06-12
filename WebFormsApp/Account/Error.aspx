<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="WebFormsApp.Account.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>An identity service error Occurred</h2>
    <div class="alert alert-danger">
        <strong>Error: <%: ErrorMessage %></strong>
    </div>
</asp:Content>