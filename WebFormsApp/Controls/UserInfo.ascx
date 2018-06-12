<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.ascx.cs" Inherits="WebFormsApp.Controls.UserInfo" %>
<asp:LoginView runat="server" ViewStateMode="Disabled">
    <LoggedInTemplate>
        <ul class="nav navbar-nav  navbar-right">
            <li>
                <asp:LinkButton runat="server" ID="lbEditProfile" Tooltip="Edit Profile" OnClick="lbEditProfile_Click" style="display:inline-block;">Hello, <%: Context.User.Identity.Name  %></asp:LinkButton>
            </li>            
            <li>
                <asp:LinkButton runat="server" ID="lbChangePassword" OnClick="lbChangePassword_Click">Change Password</asp:LinkButton>
            </li>
            <li>
                <asp:LoginStatus runat="server" ID="lsUserStatus" LogoutAction="Redirect" LogoutText="Sign out" LogoutPageUrl="~/" OnLoggingOut="lsUserStatus_LoggingOut" />
            </li>
        </ul>
    </LoggedInTemplate>
    <AnonymousTemplate>
        <ul class="nav navbar-nav  navbar-right">
            <li>
                <asp:LinkButton Text="Sign in" runat="server" ID="lbSigninIn" OnClick="lbSigninIn_Click" />
            </li>
        </ul>
    </AnonymousTemplate>
</asp:LoginView>