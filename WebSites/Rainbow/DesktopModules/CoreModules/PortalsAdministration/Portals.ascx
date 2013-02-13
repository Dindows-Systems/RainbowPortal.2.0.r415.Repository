<%@ register assembly="Rainbow.Framework" namespace="Rainbow.Framework.Web.UI.WebControls"
    tagprefix="rbfwebui" %>
<%@ control autoeventwireup="false" codefile="Portals.ascx.cs" inherits="Rainbow.Content.Web.Modules.Portals"
    language="c#" %>
<table align="center" border="0" cellpadding="2" cellspacing="0">
    <tr valign="top">
        <td>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr valign="top">
                    <td>
                        <asp:listbox id="portalList" runat="server" cssclass="NormalTextBox" datasource="<%# portals %>"
                            datatextfield="Name" datavaluefield="ID" rows="8" width="200"></asp:listbox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <rbfwebui:imagebutton id="EditBtn" runat="server" alternatetext="Edit selected portal"
                                        textkey="EDIT_PORTAL" OnClick="EditBtn_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <rbfwebui:imagebutton id="DeleteBtn" runat="server" alternatetext="Delete selected portal"
                                        textkey="DELETE_PORTAL" OnClick="DeleteBtn_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
