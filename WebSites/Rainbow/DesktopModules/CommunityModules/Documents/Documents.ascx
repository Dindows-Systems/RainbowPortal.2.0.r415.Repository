<%@ control autoeventwireup="false" codefile="Documents.ascx.cs" inherits="Rainbow.Content.Web.Modules.Documents"
    language="c#" %>
<asp:datagrid id="myDataGrid" runat="server" allowsorting="True" autogeneratecolumns="false"
    borderwidth="0px" cellpadding="3" enableviewstate="false">
    <columns>
        <rbfwebui:templatecolumn>
            <itemtemplate>
                <rbfwebui:HyperLink TextKey="EDIT" Text="Edit" ID="editLink" ImageUrl='<%# CurrentTheme.GetImage("Buttons_Edit", "Edit.gif").ImageUrl %>'
                    NavigateUrl='<%# Rainbow.Framework.HttpUrlBuilder.BuildUrl("~/DesktopModules/CommunityModules/Documents/DocumentsEdit.aspx","ItemID=" + DataBinder.Eval(Container.DataItem,"ItemID")  + "&mid=" + ModuleID) %>'
                    runat="server" />
            </itemtemplate>
        </rbfwebui:templatecolumn>
        <rbfwebui:templatecolumn>
            <itemtemplate>
                <rbfwebui:HyperLink TextKey="CONTENTTYPE" Text="Content Type" ID="contentType" ImageUrl='<%# "~/aspnet_client/Ext/" + DataBinder.Eval(Container.DataItem,"contentType")%>'
                    NavigateUrl='<%# GetBrowsePath(DataBinder.Eval(Container.DataItem,"FileNameUrl").ToString(), DataBinder.Eval(Container.DataItem,"ContentSize"), (int) DataBinder.Eval(Container.DataItem,"ItemID")) %>'
                    Target="_new" runat="server" />
            </itemtemplate>
        </rbfwebui:templatecolumn>
        <rbfwebui:templatecolumn headerstyle-cssclass="NormalBold" headertext="Title" sortexpression="FileFriendlyName">
            <itemtemplate>
				<rbfwebui:HyperLink id="docLink" Text='<%# DataBinder.Eval(Container.DataItem,"FileFriendlyName") %>' NavigateUrl='<%# GetBrowsePath(DataBinder.Eval(Container.DataItem,"FileNameUrl").ToString(), DataBinder.Eval(Container.DataItem,"ContentSize"), (int) DataBinder.Eval(Container.DataItem,"ItemID")) %>' CssClass="Normal" Target="_new" runat="server" />
			</itemtemplate>
        </rbfwebui:templatecolumn>
        <rbfwebui:boundcolumn datafield="CreatedByUser" headerstyle-cssclass="NormalBold"
            headertext="<%$ Resources:Rainbow, DOCUMENT_OWNER %>" itemstyle-cssclass="Normal"
            sortexpression="CreatedByUser">
        </rbfwebui:boundcolumn>
        <rbfwebui:boundcolumn datafield="Category" headerstyle-cssclass="NormalBold" headertext="<%$ Resources:Rainbow, DOCUMENT_AREA %>"
            itemstyle-cssclass="Normal" itemstyle-wrap="false" sortexpression="Category">
        </rbfwebui:boundcolumn>
        <rbfwebui:boundcolumn datafield="CreatedDate" dataformatstring="{0:d}" headerstyle-cssclass="NormalBold"
            headertext="<%$ Resources:Rainbow, DOCUMENT_LAST_UPDATED %>" itemstyle-cssclass="Normal"
            sortexpression="CreatedDate">
        </rbfwebui:boundcolumn>
    </columns>
</asp:datagrid>