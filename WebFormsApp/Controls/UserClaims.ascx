<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserClaims.ascx.cs" Inherits="WebFormsApp.Controls.UserClaims" %>
<h2>User Claims</h2>
<asp:GridView runat="server" ID="gvUserClaims" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover">
    <Columns>
        <asp:BoundField DataField="Type" HeaderText="Type" />
        <asp:BoundField DataField="Value" HeaderText="Value" />
    </Columns>
</asp:GridView>