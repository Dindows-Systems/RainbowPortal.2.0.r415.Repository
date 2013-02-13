<%@ control autoeventwireup="false" inherits="Rainbow.Content.Web.Modules.SimpleMenuType"
    language="c#" %>
<!--Simple Item Menu-->
<rbfwebui:desktopnavigation id="NavigationMenu" runat="server" autobind="false" bind="<%#MenuBindOption%>"
    cssclass="sm_SimpleMenu" enableviewstate="false" parentpageid="<%#ParentPageID%>"
    repeatdirection="<%#MenuRepeatDirection%>" showfooter="false" showheader="false"
    visible="true">
    <selecteditemstyle cssclass="sm_SelectedTab" />
    <selecteditemtemplate>
        <!--
			&#160;<a href='<%#Rainbow.Framework.HttpUrlBuilder.BuildUrl("~/Default.aspx" , GlobalPortalSettings.ActivePage.PageID,"ItemId="  + ((Rainbow.Framework.Site.Configuration.PageStripDetails) Container.DataItem).PageID)%>'>
				<%# ((Rainbow.Framework.Site.Configuration.PageStripDetails) Container.DataItem).PageName %>
				&#160;
-->
        <!-- Modified by Hongwei Shen(hongwei.shen@gmail.com) in fixing the url not correct problem, 05/07/2005 -->
        &#160;<a href='<%#Rainbow.Framework.HttpUrlBuilder.BuildUrl("~/Default.aspx" , ((Rainbow.Framework.Site.Configuration.PageStripDetails) Container.DataItem).PageID, "")%>'>
            <%# ((Rainbow.Framework.Site.Configuration.PageStripDetails) Container.DataItem).PageName %>
        </a>&#160;
        <!-- end of modification -->
    </selecteditemtemplate>
    <alternatingitemstyle cssclass="sm_OtherSubTabsAlt" />
    <alternatingitemtemplate>
        <!--
			&#160;<a href='<%#Rainbow.Framework.HttpUrlBuilder.BuildUrl("~/Default.aspx" , GlobalPortalSettings.ActivePage.PageID,"ItemId=" + ((Rainbow.Framework.Site.Configuration.PageStripDetails) Container.DataItem).PageID)%>'>
				<%# ((Rainbow.Framework.Site.Configuration.PageStripDetails) Container.DataItem).PageName %>&#160; 
-->
        <!-- Modified by Hongwei Shen(hongwei.shen@gmail.com) in fixing the url not correct problem, 05/07/2005 -->
        &#160;<a href='<%#Rainbow.Framework.HttpUrlBuilder.BuildUrl("~/Default.aspx" , ((Rainbow.Framework.Site.Configuration.PageStripDetails) Container.DataItem).PageID, "")%>'>
            <%# ((Rainbow.Framework.Site.Configuration.PageStripDetails) Container.DataItem).PageName %>
        </a>&#160;
        <!-- end of modification -->
    </alternatingitemtemplate>
    <itemstyle cssclass="sm_OtherSubTabs" />
    <itemtemplate>
        <!--
			&#160;<a href='<%#Rainbow.Framework.HttpUrlBuilder.BuildUrl("~/Default.aspx" ,GlobalPortalSettings.ActivePage.PageID, "ItemId=" + ((Rainbow.Framework.Site.Configuration.PageStripDetails) Container.DataItem).PageID)%>'>
				<%# ((Rainbow.Framework.Site.Configuration.PageStripDetails) Container.DataItem).PageName %>
			</a>&#160;
-->
        <!-- Modified by Hongwei Shen(hongwei.shen@gmail.com) in fixing the url not correct problem, 05/07/2005 -->
        &#160;<a href='<%#Rainbow.Framework.HttpUrlBuilder.BuildUrl("~/Default.aspx", ((Rainbow.Framework.Site.Configuration.PageStripDetails) Container.DataItem).PageID, "")%>'>
            <%# ((Rainbow.Framework.Site.Configuration.PageStripDetails) Container.DataItem).PageName %>
        </a>&#160;
        <!-- end of modification -->
    </itemtemplate>
    <footerstyle cssclass="sm_Footer" />
    <footertemplate>
    </footertemplate>
    <headerstyle cssclass="sm_Header" />
    <headertemplate>
    </headertemplate>
</rbfwebui:desktopnavigation>
<!--END:Simple Item Menu-->
