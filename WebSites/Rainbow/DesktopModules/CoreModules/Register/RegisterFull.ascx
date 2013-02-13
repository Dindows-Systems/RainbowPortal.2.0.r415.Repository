<%@ Control AutoEventWireup="false" Inherits="Rainbow.Content.Web.Modules.RegisterFull"
    Language="c#" %>
<asp:Panel ID="FullProfileInformation" runat="server">
    <table border="0" cellpadding="0" cellspacing="4" class="Normal" width="100%">
        <tr>
            <td align="left" class="Head" colspan="3">
                <rbfwebui:Label ID="PageTitleLabel" runat="server" Text="Profile Information" TextKey="PROFILE_INFO"></rbfwebui:Label>
                <hr noshade="noshade" size="1" />
            </td>
        </tr>
        <tr>
            <td class="Normal" colspan="3">
                <rbfwebui:Label ID="Message" runat="server" CssClass="NormalRed" ForeColor="Red"></rbfwebui:Label></td>
        </tr>
        <tr>
            <td class="Subhead" nowrap="nowrap">
                <rbfwebui:Label ID="NameLabel" runat="server" Height="22" Text="Name" TextKey="NAME"
                    Width="83px"></rbfwebui:Label></td>
            <td class="Normal">
                &nbsp;
                <asp:TextBox ID="NameField" runat="server" size="25" Width="350px"></asp:TextBox></td>
            <td class="Normal">
                <asp:RequiredFieldValidator ID="RequiredName" runat="server" ControlToValidate="NameField"
                    Display="Dynamic" ErrorMessage="'Name' must not be left blank" Text="<%$ Resources:Rainbow, INSERT_NAME %>">'Name' must not be left blank</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td class="Subhead" nowrap="nowrap">
                <rbfwebui:Label ID="CompanyLabel" runat="server" EnableViewState="False" Text="Company"
                    TextKey="COMPANY"></rbfwebui:Label></td>
            <td class="Normal">
                &nbsp;
                <asp:TextBox ID="CompanyField" runat="server" Columns="28" CssClass="NormalTextBox"
                    MaxLength="50" Width="350px"></asp:TextBox></td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="Subhead" nowrap="nowrap">
                <rbfwebui:Label ID="AddressLabel" runat="server" EnableViewState="False" Text="Address"
                    TextKey="ADDRESS"></rbfwebui:Label></td>
            <td class="Normal">
                &nbsp;
                <asp:TextBox ID="AddressField" runat="server" Columns="28" CssClass="NormalTextBox"
                    MaxLength="50" Width="350px"></asp:TextBox></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="Subhead" nowrap="nowrap">
                <rbfwebui:Label ID="CityLabel" runat="server" EnableViewState="False" Text="City"
                    TextKey="CITY"></rbfwebui:Label></td>
            <td class="Normal">
                &nbsp;
                <asp:TextBox ID="CityField" runat="server" Columns="28" CssClass="NormalTextBox"
                    MaxLength="50" Width="350px"></asp:TextBox></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="Subhead" nowrap="nowrap">
                <rbfwebui:Label ID="ZipLabel" runat="server" EnableViewState="False" Text="Postal Code/Zip"
                    TextKey="ZIP"></rbfwebui:Label></td>
            <td class="Normal">
                &nbsp;
                <asp:TextBox ID="ZipField" runat="server" Columns="28" CssClass="NormalTextBox" MaxLength="10"
                    Width="60px"></asp:TextBox></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="Subhead" nowrap="nowrap">
                <rbfwebui:Label ID="CountryLabel" runat="server" Text="Country" TextKey="COUNTRY"></rbfwebui:Label></td>
            <td class="Normal">
                &nbsp;
                <asp:DropDownList ID="CountryField" runat="server" AutoPostBack="True" DataTextField="Name"
                    DataValueField="CountryID" Width="350px" /></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr id="StateRow" runat="server">
            <td class="Subhead" nowrap="nowrap">
                <rbfwebui:Label ID="StateLabel" runat="server" Text="State" /></td>
            <td class="Normal">
                &nbsp;
                <asp:DropDownList ID="StateField" runat="server" DataTextField="Name" DataValueField="StateID"
                    Width="170px">
                </asp:DropDownList>
                <rbfwebui:Label ID="InLabel" runat="server" Text="in" TextKey="IN"></rbfwebui:Label>&nbsp;
                <rbfwebui:Label ID="ThisCountryLabel" runat="server" Font-Bold="True" Font-Italic="True"></rbfwebui:Label></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="Subhead" nowrap="nowrap">
                <rbfwebui:Label ID="PhoneLabel" runat="server" EnableViewState="False" Text="Telephone"
                    TextKey="PHONE"></rbfwebui:Label></td>
            <td class="Normal">
                &nbsp;
                <asp:TextBox ID="PhoneField" runat="server" Columns="28" CssClass="NormalTextBox"
                    MaxLength="50" Width="350px"></asp:TextBox></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="Subhead" nowrap="nowrap">
                <rbfwebui:Label ID="FaxLabel" runat="server" EnableViewState="False" Text="Fax" TextKey="FAX"></rbfwebui:Label></td>
            <td class="Normal">
                &nbsp;
                <asp:TextBox ID="FaxField" runat="server" Columns="28" CssClass="NormalTextBox" MaxLength="50"
                    Width="350px"></asp:TextBox></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="Subhead" nowrap="nowrap">
                <rbfwebui:Label ID="SendNewsletterLabel" runat="server" EnableViewState="False" Text="Send Newsletter"
                    TextKey="SEND_NEWSLETTER"></rbfwebui:Label></td>
            <td class="Normal">
                &nbsp;
                <asp:CheckBox ID="SendNewsletter" runat="server" /></td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Panel>
<table border="0" cellpadding="0" cellspacing="4" class="Normal" width="100%">
    <tr>
        <td class="Head" colspan="3">
            <br />
            <rbfwebui:Label ID="AccountLabel" runat="server" Text="Account Information" TextKey="ACCOUNT_INFO"></rbfwebui:Label>
            <hr noshade="noshade" size="1" />
        </td>
    </tr>
    <tr id="UserIDRow" runat="server" visible="false">
        <td class="Subhead" nowrap="nowrap">
            <rbfwebui:Label ID="UseridLabel" runat="server" Height="22" Text="Name" TextKey="USERID"
                Width="83px">UserID</rbfwebui:Label></td>
        <td class="Normal">
            &nbsp;
            <asp:TextBox ID="UseridField" runat="server" size="25" Width="350px">0</asp:TextBox></td>
        <td class="Normal">
            <asp:CompareValidator ID="CheckID" runat="server" ControlToValidate="UseridField"
                Display="Dynamic" ErrorMessage="<%$ Resources:Rainbow, ERROR_VALID_ID %>" Operator="DataTypeCheck"
                Type="Integer"></asp:CompareValidator></td>
    </tr>
    <tr>
        <td class="Subhead" nowrap="nowrap">
            <rbfwebui:Label ID="EmailLabel" runat="server" Text="Email Address" TextKey="EMAIL"></rbfwebui:Label></td>
        <td class="Normal">
            &nbsp;
            <asp:TextBox ID="EmailField" runat="server" size="25" Width="350px"></asp:TextBox></td>
        <td class="Normal">
            <asp:RegularExpressionValidator ID="ValidEmail" runat="server" ControlToValidate="EmailField"
                Display="Dynamic" ErrorMessage="You must use a valid email address" textkey="VALID_EMAIL"
                ValidationExpression="[\w\.-]+(\+[\w-]*)?@([\w-]+\.)+[\w-]+"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredEmail" runat="server" ControlToValidate="EmailField"
                Display="Dynamic" ErrorMessage="'Email' must not be left blank" Text="'Email' must not be left blank"
                textkey="INSERT_EMAIL">'Email' must not be left blank</asp:RequiredFieldValidator></td>
    </tr>
    <tr id="EditPasswordRow" runat="server" visible="false">
        <td class="Subhead" nowrap="nowrap">
        </td>
        <td class="Normal">
            &nbsp;
            <rbfwebui:Label ID="Label1" runat="server" Text="Password" TextKey="CHANGE_PASSWORD">Change password only if you want modify it:</rbfwebui:Label></td>
        <td class="Normal">
        </td>
    </tr>
    <tr>
        <td class="Subhead" nowrap="nowrap">
            <rbfwebui:Label ID="PasswordLabel" runat="server" Text="Password" TextKey="PASSWORD"></rbfwebui:Label></td>
        <td class="Normal">
            &nbsp;
            <asp:TextBox ID="PasswordField" runat="server" MaxLength="39" size="25" TextMode="Password"
                Width="350px"></asp:TextBox></td>
        <td class="Normal">
            <asp:RequiredFieldValidator ID="RequiredPassword" runat="server" ControlToValidate="PasswordField"
                ErrorMessage="'Password' must not be left blank" Text="<%$ Resources:Rainbow, INSERT_PASSWORD %>"></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td class="Subhead" nowrap="nowrap">
            <rbfwebui:Label ID="ConfirmPasswordLabel" runat="server" Text="Confirm Password"
                TextKey="CONFIRM"></rbfwebui:Label></td>
        <td class="Normal">
            &nbsp;
            <asp:TextBox ID="ConfirmPasswordField" runat="server" MaxLength="39" size="25" TextMode="Password"
                Width="350px"></asp:TextBox></td>
        <td class="Normal">
            <asp:RequiredFieldValidator ID="RequiredConfirm" runat="server" ControlToValidate="ConfirmPasswordField"
                Display="Dynamic" ErrorMessage="<%$ Resources:Rainbow, INSERT_CONFIRM %>"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="ComparePasswords" runat="server" ControlToCompare="PasswordField"
                ControlToValidate="ConfirmPasswordField" Display="Dynamic" ErrorMessage="<%$ Resources:Rainbow, PASSWORD_DO_NOT_MATCH %>"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="ConditionsRow" runat="server">
        <td class="Subhead" nowrap="nowrap" valign="top">
            <rbfwebui:Label ID="ConditionsLabel" runat="server" Text="Terms of Service" TextKey="CONDITIONS"></rbfwebui:Label>&nbsp;&nbsp;<br>
            &nbsp;</td>
        <td class="Normal">
            &nbsp;
            <asp:TextBox ID="FieldConditions" runat="server" Columns="40" CssClass="Normal" EnableViewState="False"
                onfocus="this.blur()" ReadOnly="True" Rows="8" size="25" TextMode="MultiLine"
                Width="350px"></asp:TextBox><br>
            &nbsp;
            <asp:CheckBox ID="Accept" runat="server" Text="<%$ Resources:Rainbow, ACCEPT_TERMS %>" /></td>
        <td valign="top">
            <asp:CustomValidator ID="CheckTermsValidator" runat="server" ErrorMessage="<%$ Resources:Rainbow, ACCEPT_TERMS %>"></asp:CustomValidator></td>
    </tr>
    <tr>
        <td class="Normal" colspan="3">
            <rbfwebui:LinkButton ID="RegisterBtn" runat="server" class="CommandButton" Text="Register and Sign In Now"
                TextKey="REGISTER_NOW"></rbfwebui:LinkButton>&nbsp;
            <rbfwebui:LinkButton ID="SaveChangesBtn" runat="server" class="CommandButton" Text="Register and Sign In Now"
                TextKey="SAVE_CHANGES">Save Changes</rbfwebui:LinkButton>&nbsp;
            <rbfwebui:LinkButton ID="cancelButton" runat="server" CausesValidation="False" class="CommandButton"
                TextKey="CANCEL"></rbfwebui:LinkButton></td>
    </tr>
</table>
