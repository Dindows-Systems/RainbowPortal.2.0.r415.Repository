<%@ control autoeventwireup="false" codefile="FAQs.ascx.cs" inherits="Rainbow.Content.Web.Modules.FAQs"
    language="c#" %>
<%@ register assembly="Rainbow.Framework" namespace="Rainbow.Framework.Web.UI.WebControls"
    tagprefix="rbfwebui" %>
<asp:datalist id="myDataList" runat="server">
    <selecteditemstyle />
    <selecteditemtemplate>
        <rbfwebui:hyperlink id="HyperlinkSelected" runat="server" imageurl='<%# CurrentTheme.GetModuleImageSRC("Edit.gif") %>'
            navigateurl='<%# Rainbow.Framework.HttpUrlBuilder.BuildUrl("~/DesktopModules/CommunityModules/FAQs/FAQsEdit.aspx","ItemID=" + DataBinder.Eval(Container.DataItem,"ItemID") + "&mID=" + ModuleID) %>'
            text="Edit" textkey="EDIT" visible="<%# IsEditable %>">
        </rbfwebui:hyperlink>
        <span class="normalBold">
            <rbfwebui:localize id="Literal1" runat="server" text="Q" textkey="FAQ_Q">
            </rbfwebui:localize>:&nbsp;</span>
        <rbfwebui:linkbutton id="LinkbuttonSelected" runat="server" commandname="select"
            text='<%# DataBinder.Eval(Container.DataItem, "Question") %>' title='<%# DataBinder.Eval(Container.DataItem, "CreatedDate") %>'>
        </rbfwebui:linkbutton><br />
        <span class="normalBold">
            <rbfwebui:localize id="Literal2" runat="server" text="A" textkey="FAQ_A">
            </rbfwebui:localize>:&nbsp; </span>
        <rbfwebui:label id="LabelSelected" runat="server" cssclass="normal" text='<%# DataBinder.Eval(Container.DataItem, "Answer") %>'>:&nbsp;</span>

        </rbfwebui:label>
    </selecteditemtemplate>
    <itemtemplate>
        <rbfwebui:hyperlink id="HyperlinkItem" runat="server" imageurl='<%# CurrentTheme.GetModuleImageSRC("Edit.gif")  %>'
            navigateurl='<%# Rainbow.Framework.HttpUrlBuilder.BuildUrl("~/DesktopModules/CommunityModules/FAQs/FAQsEdit.aspx","ItemID=" + DataBinder.Eval(Container.DataItem,"ItemID") + "&mID=" + ModuleID)%>'
            text="Edit" textkey="EDIT" visible="<%# IsEditable %>">
        </rbfwebui:hyperlink>
        <span class="normalBold">
            <rbfwebui:localize id="Literal3" runat="server" text="Q" textkey="FAQ_Q">
            </rbfwebui:localize>:&nbsp;</span>
        <rbfwebui:linkbutton id="LinkbuttonItem" runat="server" commandname="select" text='<%# DataBinder.Eval(Container.DataItem, "Question") %>'
            title='<%# DataBinder.Eval(Container.DataItem, "CreatedDate") %>'>
        </rbfwebui:linkbutton>
    </itemtemplate>
</asp:datalist>
