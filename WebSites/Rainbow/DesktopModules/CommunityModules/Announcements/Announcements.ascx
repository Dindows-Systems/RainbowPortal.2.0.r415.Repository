<%@ control autoeventwireup="false" inherits="Rainbow.Content.Web.Modules.Announcements"
    language="c#" codefile="Announcements.ascx.cs" %>
<%@ register assembly="Rainbow.Framework" Namespace="Rainbow.Framework.Web.UI.WebControls" tagprefix="cc2" %>
<asp:datalist id="myDataList" runat="server" cellpadding="4">
    <itemtemplate>
        <rbfwebui:hyperlink id="editLink" runat="server" imageurl='<%# CurrentTheme.GetImage("Buttons_Edit", "Edit.gif").ImageUrl %>'
            navigateurl='<%# Rainbow.Framework.HttpUrlBuilder.BuildUrl("~/DesktopModules/CommunityModules/Announcements/AnnouncementsEdit.aspx",PageID,"ItemID=" + DataBinder.Eval(Container.DataItem,"ItemID") + "&mid=" + ModuleID )%>'
            text="Edit" textkey="EDIT" visible="<%# IsEditable %>">
        </rbfwebui:hyperlink>
        <rbfwebui:label id="Label1" runat="server" cssclass="ItemTitle" text='<%# DataBinder.Eval(Container.DataItem, "Title") %>'>
        </rbfwebui:label>
        <br />
        <span class="Normal">
            <%# DataBinder.Eval(Container.DataItem,"Description") %>
            &nbsp;
            <rbfwebui:hyperlink id="moreLink" runat="server" navigateurl='<%# DataBinder.Eval(Container.DataItem,"MoreLink") %>'
                text="Read full announce" textkey="READ_FULL_ANNOUNCE" visible='<%# DataBinder.Eval(Container.DataItem,"MoreLink").ToString().Length > 0 %>'>
            </rbfwebui:hyperlink>
        </span>
        <br />
    </itemtemplate>
</asp:datalist>
<div align="center">
    <rbfwebui:paging id="pgModules" runat="server" />
</div>
