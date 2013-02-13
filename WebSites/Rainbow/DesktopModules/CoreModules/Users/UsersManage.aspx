<%@ Register Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" TagName="Banner"
    TagPrefix="portal" %>
<%@ Register Src="~/Design/DesktopLayouts/DesktopFooter.ascx" TagName="Footer" TagPrefix="foot" %>

<%@ Page AutoEventWireup="false" CodeFile="UsersManage.aspx.cs" Inherits="Rainbow.Content.Web.Modules.UsersManage"
    Language="c#" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body id="Body1" runat="server">
    <form id="Form1" runat="server">
        <div id="zenpanes" class="zen-main">
            <portal:Banner ID="Banner1" runat="server" showtabs="false" />
            <div class="div_ev_Table">
                <table align="center" border="0" cellpadding="4" cellspacing="0">
                    <tr valign="top">
                        <td colspan="2">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="left">
                                        <span id="title" runat="server" class="Head">
                                            <rbfwebui:Label ID="Label2" runat="server" TextKey="USER_MANAGE">Manage User</rbfwebui:Label>
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <hr noshade="noshade" size="1" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="Normal" colspan="2">
                            <!-- Start Register control -->
                            <asp:PlaceHolder ID="register" runat="server"></asp:PlaceHolder>
                            <!-- End Register control -->
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <p>
                                &nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:DropDownList ID="allRoles" runat="server" DataTextField="Name" />
                            <rbfwebui:LinkButton ID="addExisting" runat="server" CssClass="CommandButton" Text="Add user to this role" TextKey="ADDUSER" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:DataList ID="userRoles" runat="server" RepeatColumns="2">
                                <ItemStyle Width="225" />
                                <ItemTemplate>
                                    &#160;&#160;
                                    <rbfwebui:ImageButton ID="deleteBtn" runat="server" AlternateText='DELUSER' CommandName="delete"
                                        ImageUrl='<%# CurrentTheme.GetImage("Buttons_Delete", "Delete.gif").ImageUrl %>' />
                                    <rbfwebui:Label ID="Label1" runat="server" CssClass="Normal" Text='<%# Eval("Name") %>'>
                                    </rbfwebui:Label>
                                </ItemTemplate>
                            </asp:DataList></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr noshade="noshade" size="1" />
                            <rbfwebui:Label ID="ErrorLabel" runat="server" CssClass="Error" Visible="False"></rbfwebui:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <rbfwebui:LinkButton ID="saveBtn" runat="server" CssClass="CommandButton" Text="Save User Changes"
                                TextKey="SAVEUSER"></rbfwebui:LinkButton></td>
                    </tr>
                </table>
            </div>
            <div class="rb_AlternatePortalFooter">
                <foot:Footer ID="Footer" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
