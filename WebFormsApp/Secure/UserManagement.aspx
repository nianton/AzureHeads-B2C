<%@ Page Title="User Management" Language="C#" MasterPageFile="~/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="WebFormsApp.Secure.UserManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>User Management</h1>
    <asp:GridView runat="server" ID="gvUsers" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover">
        <Columns>
            <asp:TemplateField HeaderText="UId">
                <ItemTemplate>    
                    <div style="max-width:60px;text-overflow:ellipsis;overflow-x:hidden;white-space:nowrap" title='<%# Eval("ObjectId") %>'>
                       <%# Eval("ObjectId") %>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>    
                    <div style="text-align:center">
                       <%# GetEmailTag((GraphLite.User)Container.DataItem) %>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="DisplayName" HeaderText="Display Name" />
            <asp:BoundField DataField="Surname" HeaderText="Surname" />
            <asp:BoundField DataField="GivenName" HeaderText="Given name" />
            <asp:BoundField DataField="JobTitle" HeaderText="Job Title" />
            <asp:TemplateField HeaderText="Tax Registration #">
                <ItemTemplate>                    
                    <div style="text-align:center">
                        <%# GetTaxRegistrationNumber((GraphLite.User)Container.DataItem) %>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Is Local">
                <ItemTemplate>                    
                    <div style="text-align:center">
                        <span class="glyphicon glyphicon-check" runat="server" aria-hidden="true" visible='<%# "LocalAccount".Equals(Eval("CreationType")) %>'></span>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Enabled">
                <ItemTemplate>    
                    <div style="text-align:center">
                        <span class="glyphicon glyphicon-check" runat="server" aria-hidden="true" visible='<%# Eval("AccountEnabled") %>'></span>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <div><asp:Button runat="server" ID="btnGenerateUser" CssClass="btn btn-danger" Text="Generate Random User" OnClientClick="return confirm('Continue generating a random User?');" OnClick="btnGenerateUser_Click" /></div>
</asp:Content>