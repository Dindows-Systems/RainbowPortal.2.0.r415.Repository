<%@ control autoeventwireup="false" inherits="Rainbow.Content.Web.Modules.Signin" language="c#"
    codefile="Signin.ascx.cs" %>
<table align="center" border="0" cellpadding="1" cellspacing="1" width="100%">
    <tr>
        <td class="Normal" nowrap="nowrap">
            <rbfwebui:localize id="EmailLabel" runat="server" text="Email" textkey="EMAIL">
            </rbfwebui:localize></td>
    </tr>
    <tr>
        <td nowrap="nowrap">
            <asp:textbox id="email" runat="server" columns="24" cssclass="NormalTextBox"></asp:textbox></td>
    </tr>
    <tr>
        <td class="Normal" nowrap="nowrap">
            <rbfwebui:localize id="PasswordLabel" runat="server" text="Password" textkey="PASSWORD">
            </rbfwebui:localize></td>
    </tr>
    <tr>
        <td nowrap="nowrap">
            <asp:textbox id="password" runat="server" columns="24" cssclass="NormalTextBox" textmode="password"></asp:textbox></td>
    </tr>
    <tr>
        <td nowrap="nowrap">
            <asp:checkbox id="RememberCheckBox" runat="server" cssclass="Normal" text="<%$ Resources:Rainbow, REMEMBER_LOGIN %> " /></td>
    </tr>
    <tr>
        <td nowrap="nowrap">
            <rbfwebui:button id="LoginBtn" runat="server" cssclass="CommandButton" enableviewstate="False"
                text="Sign in" textkey="SIGNIN"/></td>
    </tr>
    <tr>
        <td nowrap="nowrap">
            <rbfwebui:button id="RegisterBtn" runat="server" cssclass="CommandButton" enableviewstate="False"
                text="Register" textkey="REGISTER" /></td>
    </tr>
    <tr>
        <td nowrap="nowrap">
            <rbfwebui:button id="SendPasswordBtn" runat="server" cssclass="CommandButton" enableviewstate="False"
                text="Forgotten Password?" textkey="SIGNIN_SEND_PWD" /></td>
    </tr>
    <tr>
        <td>
            <rbfwebui:label id="Message" runat="server" cssclass="Error"></rbfwebui:label></td>
    </tr>
</table>
