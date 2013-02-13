<%@ control autoeventwireup="false" codefile="GoogleSearch.ascx.cs" inherits="Rainbow.DesktopModules.GoogleSearch"
    language="c#" %>
<p>
    <asp:datagrid id="DataGrid1" runat="server" alternatingitemstyle-cssclass="Grid_AlternatingItem"
        cellpadding="3" headerstyle-cssclass="Grid_Header" itemstyle-cssclass="Grid_Item"
        width="100%">
    </asp:datagrid>
</p>
<p>
    <asp:textbox id="txtSearchString" runat="server" cssclass="NormalTextBox" width="200px"></asp:textbox>
    <rbfwebui:label id="Label1" runat="server" cssclass="Normal" text="Start" textkey="GOOGLE_START">
    </rbfwebui:label>
    <asp:textbox id="TextBox2" runat="server" cssclass="NormalTextBox" width="40px">1</asp:textbox>
    <rbfwebui:button id="Search" runat="server" text="Search" textkey="GOOGLE_SEARCH" />
    <rbfwebui:label id="lblHits" runat="server" cssclass="Normal" width="100px">
    </rbfwebui:label>
</p>
