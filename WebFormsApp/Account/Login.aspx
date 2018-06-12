<%@ Page Title="" Language="C#" MasterPageFile="~/AADB2C/AadB2cMaster.Master" Async="true" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebFormsApp.Account.Login" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <%-- This specific master page (used also by B2C) does not include a form tag -this is why a form runat="server" is hosting the content here --%>
    <form id="loginForm" runat="server">
        <div class="localAccount" role="form">
            <div class="intro">
                <h2>Sign in with your existing account</h2>
            </div>
            <div class="error pageLevel" aria-hidden="true" style="display: none;">
                <p role="alert"></p>
            </div>
            <div class="entry">
                <div class="entry-item">
                    <label for="logonIdentifier">User name or Email Address</label>
                    <div class="error itemLevel" aria-hidden="true" style="display: none;">
                        <p role="alert"></p>
                    </div>
                    <asp:TextBox runat="server" ID="tbUserName" AutoCompleteType="Email" CssClass="info-input" placeholder="Enter your email or username" />
                </div>
                <asp:PlaceHolder runat="server" ID="phLogin">
                    <div class="entry-item">
                        <label for="logonIdentifier">Password</label>
                        <div class="error itemLevel" aria-hidden="true" style="display: none;">
                            <p role="alert"></p>
                        </div>
                        <asp:TextBox runat="server" TextMode="Password" ID="tbPassword" CssClass="info-input" placeholder="Enter your passowrd" />
                    </div>
                    <div class="working"></div>
                    <div class="buttons">
                        <asp:Button runat="server" ID="btnSignIn" TabIndex="1" Text="Sign In" CssClass="continue" OnClick="btnSignIn_Click" />
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="phErrorInfo">
                    <div class="entry-item">
                        <label for="logonIdentifier">Auth Error [failed authentication]</label>
                        <div class="error itemLevel" aria-hidden="true">
                            <p role="alert" runat="server" id="pErrorCode"></p>
                        </div>
                        <asp:TextBox runat="server" ID="tbErrorDescription" CssClass="info-input"></asp:TextBox>
                    </div>

                    <div class="working"></div>
                    <div class="buttons"><a runat="server" href="~/Account/Login" class="btn btn-default">Reset</a> </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="phAuthInfo">
                    <div class="entry-item">
                        <label for="logonIdentifier">Auth Response Token</label>]
                    <asp:TextBox runat="server" ID="tbAccessToken" CssClass="info-input"></asp:TextBox>
                    </div>

                    <div class="entry-item">
                        <label for="logonIdentifier">Auth Response Json</label>
                        <pre runat="server" id="pAuthResponseJson" style="max-height: 150px; overflow-y: auto"></pre>
                    </div>

                    <div class="working"></div>
                    <div class="buttons"><a runat="server" href="~/Account/Login" class="btn btn-default">Reset</a> </div>
                </asp:PlaceHolder>
            </div>
        </div>
    </form>
</asp:Content>
