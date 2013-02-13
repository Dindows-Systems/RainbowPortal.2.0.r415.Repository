<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Register2.ascx.cs" Inherits="Rainbow.Content.Web.Modules.Register2" %>
<asp:panel id="FullProfileInformation" runat="server">
    <table border="0" cellpadding="0" cellspacing="4" class="Normal" width="100%">
        <tr>
            <td align="left" class="Head" colspan="3">
                <rbfwebui:label id="PageTitleLabel" runat="server" text="Profile Information" textkey="PROFILE_INFO"></rbfwebui:label>
                <hr noshade="noshade" size="1" />
            </td>
        </tr>
        <tr>
            <td class="Normal" colspan="3">
                <rbfwebui:label id="Message" runat="server" cssclass="NormalRed" forecolor="Red"></rbfwebui:label></td>
        </tr>
        <tr>
            <td class="Subhead" nowrap="nowrap">
                <rbfwebui:label id="NameLabel" runat="server" height="22" text="Name" textkey="NAME"
                    width="83px"></rbfwebui:label></td>
            <td class="Normal">
                &nbsp;
                <asp:textbox id="NameField" runat="server" size="25" width="350px"></asp:textbox></td>
            <td class="Normal">
                <asp:requiredfieldvalidator id="RequiredName" runat="server" controltovalidate="NameField"
                    display="Dynamic" errormessage="'Name' must not be left blank" text="<%$ Resources:Rainbow, INSERT_NAME %>">'Name' must not be left blank</asp:requiredfieldvalidator></td>
        </tr>
        <tr>
            <td class="Subhead" nowrap="nowrap">
                <rbfwebui:label id="CompanyLabel" runat="server" enableviewstate="False" text="Company"
                    textkey="COMPANY"></rbfwebui:label></td>
            <td class="Normal">
                &nbsp;
                <asp:textbox id="CompanyField" runat="server" columns="28" cssclass="NormalTextBox"
                    maxlength="50" width="350px"></asp:textbox></td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="Subhead" nowrap="nowrap">
                <rbfwebui:label id="AddressLabel" runat="server" enableviewstate="False" text="Address"
                    textkey="ADDRESS"></rbfwebui:label></td>
            <td class="Normal">
                &nbsp;
                <asp:textbox id="AddressField" runat="server" columns="28" cssclass="NormalTextBox"
                    maxlength="50" width="350px"></asp:textbox></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="Subhead" nowrap="nowrap">
                <rbfwebui:label id="CityLabel" runat="server" enableviewstate="False" text="City"
                    textkey="CITY"></rbfwebui:label></td>
            <td class="Normal">
                &nbsp;
                <asp:textbox id="CityField" runat="server" columns="28" cssclass="NormalTextBox"
                    maxlength="50" width="350px"></asp:textbox></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="Subhead" nowrap="nowrap">
                <rbfwebui:label id="ZipLabel" runat="server" enableviewstate="False" text="Postal Code/Zip"
                    textkey="ZIP"></rbfwebui:label></td>
            <td class="Normal">
                &nbsp;
                <asp:textbox id="ZipField" runat="server" columns="28" cssclass="NormalTextBox" maxlength="10"
                    width="60px"></asp:textbox></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="Subhead" nowrap="nowrap">
                <rbfwebui:label id="CountryLabel" runat="server" text="Country" textkey="COUNTRY"></rbfwebui:label></td>
            <td class="Normal">
                &nbsp;
                <asp:dropdownlist id="CountryField" runat="server" autopostback="True" datatextfield="CountryID"
                    datavaluefield="CountryID" width="350px" OnSelectedIndexChanged="CountryField_SelectedIndexChanged">
                </asp:dropdownlist></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr id="StateRow" runat="server">
            <td class="Subhead" nowrap="nowrap">
                <rbfwebui:label id="StateLabel" runat="server" text="Province/State" textkey="PROV_STATE"></rbfwebui:label></td>
            <td class="Normal">
                &nbsp;
                <asp:dropdownlist id="StateField" runat="server" datatextfield="DisplayName" datavaluefield="CountryCode"
                    width="170px">
                </asp:dropdownlist>
                <rbfwebui:label id="InLabel" runat="server" text="in" textkey="IN"></rbfwebui:label>&nbsp;
                <rbfwebui:label id="ThisCountryLabel" runat="server" font-bold="True" font-italic="True"></rbfwebui:label></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="Subhead" nowrap="nowrap">
                <rbfwebui:label id="PhoneLabel" runat="server" enableviewstate="False" text="Telephone"
                    textkey="PHONE"></rbfwebui:label></td>
            <td class="Normal">
                &nbsp;
                <asp:textbox id="PhoneField" runat="server" columns="28" cssclass="NormalTextBox"
                    maxlength="50" width="350px"></asp:textbox></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="Subhead" nowrap="nowrap">
                <rbfwebui:label id="FaxLabel" runat="server" enableviewstate="False" text="Fax" textkey="FAX"></rbfwebui:label></td>
            <td class="Normal">
                &nbsp;
                <asp:textbox id="FaxField" runat="server" columns="28" cssclass="NormalTextBox" maxlength="50"
                    width="350px"></asp:textbox></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="Subhead" nowrap="nowrap">
                <rbfwebui:label id="SendNewsletterLabel" runat="server" enableviewstate="False" text="Send Newsletter"
                    textkey="SEND_NEWSLETTER"></rbfwebui:label></td>
            <td class="Normal">
                &nbsp;
                <asp:checkbox id="SendNewsletter" runat="server" /></td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:panel>
<table border="0" cellpadding="0" cellspacing="4" class="Normal" width="100%">
    <tr>
        <td class="Head" colspan="3">
            <br />
            <rbfwebui:label id="AccountLabel" runat="server" text="Account Information" textkey="ACCOUNT_INFO"></rbfwebui:label>
            <hr noshade="noshade" size="1" />
        </td>
    </tr>
    <tr>
        <td class="Subhead" nowrap="nowrap">
            <rbfwebui:label id="EmailLabel" runat="server" text="Email Address" textkey="EMAIL"></rbfwebui:label></td>
        <td class="Normal">
            &nbsp;
            <asp:textbox id="EmailField" runat="server" size="25" width="350px"></asp:textbox></td>
        <td class="Normal">
            <asp:regularexpressionvalidator id="ValidEmail" runat="server" controltovalidate="EmailField"
                display="Dynamic" errormessage="You must use a valid email address" textkey="VALID_EMAIL"
                validationexpression="[\w\.-]+(\+[\w-]*)?@([\w-]+\.)+[\w-]+"></asp:regularexpressionvalidator>
            <asp:requiredfieldvalidator id="RequiredEmail" runat="server" controltovalidate="EmailField"
                display="Dynamic" errormessage="'Email' must not be left blank" text="'Email' must not be left blank"
                textkey="INSERT_EMAIL">'Email' must not be left blank</asp:requiredfieldvalidator></td>
    </tr>
    <tr id="EditPasswordRow" runat="server" visible="false">
        <td class="Subhead" nowrap="nowrap">
        </td>
        <td class="Normal">
            &nbsp;
            <rbfwebui:label id="Label1" runat="server" text="Password" textkey="CHANGE_PASSWORD">Change password only if you want modify it:</rbfwebui:label></td>
        <td class="Normal">
        </td>
    </tr>
    <tr>
        <td class="Subhead" nowrap="nowrap">
            <rbfwebui:label id="PasswordLabel" runat="server" text="Password" textkey="PASSWORD"></rbfwebui:label></td>
        <td class="Normal">
            &nbsp;
            <asp:textbox id="PasswordField" runat="server" maxlength="39" size="25" textmode="Password"
                width="350px"></asp:textbox></td>
        <td class="Normal">
            <asp:requiredfieldvalidator id="RequiredPassword" runat="server" controltovalidate="PasswordField"
                errormessage="'Password' must not be left blank" text="<%$ Resources:Rainbow, INSERT_PASSWORD %>"></asp:requiredfieldvalidator></td>
    </tr>
    <tr>
        <td class="Subhead" nowrap="nowrap">
            <rbfwebui:label id="ConfirmPasswordLabel" runat="server" text="Confirm Password"
                textkey="CONFIRM"></rbfwebui:label></td>
        <td class="Normal">
            &nbsp;
            <asp:textbox id="ConfirmPasswordField" runat="server" maxlength="39" size="25" textmode="Password"
                width="350px"></asp:textbox></td>
        <td class="Normal">
            <asp:requiredfieldvalidator id="RequiredConfirm" runat="server" controltovalidate="ConfirmPasswordField"
                display="Dynamic" errormessage="<%$ Resources:Rainbow, INSERT_CONFIRM %>"></asp:requiredfieldvalidator>
            <asp:comparevalidator id="ComparePasswords" runat="server" controltocompare="PasswordField"
                controltovalidate="ConfirmPasswordField" display="Dynamic" errormessage="<%$ Resources:Rainbow, PASSWORD_DO_NOT_MATCH %>"></asp:comparevalidator>
        </td>
    </tr>
    <tr id="ConditionsRow" runat="server">
        <td class="Subhead" nowrap="nowrap" valign="top">
            <rbfwebui:label id="ConditionsLabel" runat="server" text="Terms of Service" textkey="CONDITIONS"></rbfwebui:label>&nbsp;&nbsp;<br>
            &nbsp;</td>
        <td class="Normal">
            &nbsp;
            <asp:textbox id="FieldConditions" runat="server" columns="40" cssclass="Normal" enableviewstate="False"
                onfocus="this.blur()" readonly="True" rows="8" size="25" textmode="MultiLine"
                width="350px"></asp:textbox><br>
            &nbsp;
            <asp:checkbox id="Accept" runat="server" text="<%$ Resources:Rainbow, ACCEPT_TERMS %>" /></td>
        <td valign="top">
            <asp:customvalidator id="CheckTermsValidator" runat="server" errormessage="<%$ Resources:Rainbow, ACCEPT_TERMS %>" OnServerValidate="CheckTermsValidator_ServerValidate"></asp:customvalidator></td>
    </tr>
    <tr>
        <td class="Normal" colspan="3" style="height: 24px">
            <rbfwebui:linkbutton id="RegisterBtn" runat="server" class="CommandButton" text="Register and Sign In Now"
                textkey="REGISTER_NOW" OnClick="RegisterBtn_Click"></rbfwebui:linkbutton>&nbsp;
            <rbfwebui:linkbutton id="SaveChangesBtn" runat="server" class="CommandButton" text="Register and Sign In Now"
                textkey="SAVE_CHANGES" OnClick="SaveChangesBtn_Click">Save Changes</rbfwebui:linkbutton>&nbsp;
            <rbfwebui:linkbutton id="cancelButton" runat="server" causesvalidation="False" class="CommandButton"
                textkey="CANCEL" OnClick="cancelButton_Click"></rbfwebui:linkbutton>
            <asp:Button ID="Button1" runat="server" OnClick="SaveChangesBtn_Click" Text="Guardar" /></td>
    </tr>
</table>
