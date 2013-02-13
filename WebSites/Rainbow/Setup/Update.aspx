<%@ Page Language="c#" codefile="Update.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.Setup.Update" %>
<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'DTD/xhtml1-transitional.dtd' >
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
    <title>Rainbow Portal 2.0 Beta 2 Install | Update</title>
   
</head>
<body >
    <form id="Setup" method="post" runat="server">
        <table id="Table1" cellspacing="0" style="height:100%;" cellpadding="0" width="100%" border="0">
            <tr>
                <td align="right" style="background:#101010;height:92px;">
                    <img alt="Rainbow Portal - Content Management Framework" height="110" src="logo_big.gif" width="300" /></td>
            </tr>
            <tr>
                <td valign="middle" align="center"  style="background:#bababa;">
                    <asp:Panel ID="InfoPanel" runat="server" Visible="true">
                        <table class="Normal" id="infopaneltable" cellspacing="1" cellpadding="1" width="450"
                            border="0">
                            <tr>
                                <td><b><u>Rainbow Portal 2.0 Beta 2 Install | Update</u></b>
                                    <p>
                                        Welcome to the portal update page. This page is shown automatically&nbsp;at first
                                        setup and when an update to the database is needed.
                                        You can protect unauthorized access to this page by setting user name and password
                                        in /Setup/web.config.
                                    </p>
                                    <b><u>Connection String Examples:</u></b><br />
                                    <table  border="1" class="Normal" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td class="Border-Top Border-Left">
                                                            Server</td>
                                                        <td class="Border-Top Border-Left Border-Right">
                                                            Connection String</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Border-Left">
                                                            SQL Server<br />
                                                            SQL Authentication</td>
                                                        <td class="Border-Left Border-Right">
                                                            &lt;add name="ConnectionString" value="server=localhost;database=Rainbow;uid=sa;pwd=[password
                                                            here]" /&gt;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Border-Left">
                                                            SQL Server<br />
                                                            Windows Authentication</td>
                                                        <td class="Border-Left Border-Right">
                                                            &lt;add name="ConnectionString" value="server=localhost;database=Rainbow;Trusted_Connection=true;"
                                                            /&gt;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Border-Left">
                                                            MSDE<br />
                                                            SQL Authentication</td>
                                                        <td class="Border-Left Border-Right">
                                                            &lt;add name="ConnectionString" value="server=(local)\NetSDK;database=Rainbow;uid=sa;pwd=[password
                                                            here]" /&gt;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Border-Left">
                                                            MSDE<br />
                                                            Windows Authentication</td>
                                                        <td class="Border-Left Border-Right">
                                                            &lt;add name="ConnectionString" value="server=(local)\NetSDK;database=Rainbow;Trusted_Connection=true;"
                                                            /&gt;
                                                        </td>
                                                    </tr>
                                                </table>
                                    <ol>
                                        
                                            <li>Verify that you have correctly set the database connection string in the web.config
                                                file. For remote servers replace the server value with the IP
                                                of the SQL Server and replace the username, password and database name as needed.<br />
                                                <strong>Remote hosting will almost never allow Trusted connections!!</strong></li>
                                                
                                                
                                                
                                                <li>Verify the folder where you copied the Rainbow files to is set as an application
                                                    directory
                                                    <br />
                                                    1:: start menu-&gt;run-&gt;cmd
                                                    <br />
                                                    2: type in mmc and press enter
                                                    <br />
                                                    3: In the outside console, go to console menu, then "add/remove snapin"
                                                    <br />
                                                    4: Click on the "add" button, go to "Internet Information Services" and click add
                                                    again.
                                                    <br />
                                                    5: click on close then OK.
                                                    <br />
                                                    6: Expand out the tree until you get to "default web server".
                                                    <br />
                                                    7: right click on that, select "new -&gt; virtual directory"
                                                    <br />
                                                    8: specify your folder location(that you already created), go through the wizard,
                                                    etc. Give it "read" and "run scripts" permissions and finish out.
                                                    <br />
                                                    9: expand the "default web server" branch, right click on the new item you just
                                                    created and go to properties.
                                                    <br />
                                                    10: click on button titled "create".
                                                    <br />
                                                    11: save changes and exit out. You are done, just create a "bin" directory inside
                                                    of the file location you speicfied earlier. </li>
                                          
                                    </ol>
                                    <b><u>Support Resources</u></b><br />
                                    <ul>
                                        
                                        <li><a href="http://community.rainbowportal.net/" title="Rainbow Portal Community"target="_blank">Rainbow Portal Community Site</a></li>
                                        <li><a href="http://community.rainbowportal.net/forums/1390/ShowThread.aspx#1390" target="_blank"> Forum Thread Concerning this Alpha 3 Build</a></li>
                                        <li><a href="http://community.rainbowportal.net/forums/default.aspx" target="_blank">Support Forums</a></li>
                                        <li><a href="http://groups.yahoo.com/group/rainbowportal/" target="_blank">Rainbow Portal Support Yahoo Group</a> </li>
                                    </ul>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="AuthenticationPanel" runat="server" Visible="false">
                        <table class="Normal" id="authenticatetable" cellspacing="1" cellpadding="1" width="450"
                            border="0">
                            <tr>
                                <td>
                                    <b><u>Portal Setup/Update Authentication</u></b>
                                    
                                    <p>
                                        Welcome to the portal update page.This page is shown automatically at first setup
                                        and when an update to the database is needed.<br />
                                        Please enter your special "Portal Update" username and password.&nbsp;<br />
                                        Password is case sensitive so "Secret" and "SecrET" are different.<br />
                                        If you see this message your system administrator has decided to protect the update
                                        process, please ask him/her for more details.<br />
                                        You'll then be able to see if there are any updates available and apply them.&nbsp;<br />
                                        &nbsp;&nbsp;
                                    </p>
                                    <p>
                                        <rbfwebui:label ID="loginError" runat="server" Visible="False" CssClass="Error">I'm sorry but the username / password you entered was not correct. Please try again or contact the portal administrator.</rbfwebui:label></p>
                                    <p>
                                        <b>Username:</b>&nbsp;
                                        <asp:TextBox ID="updateUsername" runat="server"></asp:TextBox><br />
                                        <b>Password: </b>&nbsp;
                                        <asp:TextBox ID="updatePassword" runat="server" TextMode="Password"></asp:TextBox></p>
                                    <p>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <rbfwebui:Button ID="authenticateUser" runat="server" Text="Login" CausesValidation="False">
                                        </rbfwebui:Button></p>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="UpdatePanel" runat="server" Visible="False">
                        <table class="Normal" id="Table2" cellspacing="1" cellpadding="1" width="450" border="0">
                            <tr id="dbNoUpdate" runat="server">
                                <td>
                                    <p>
                                        Your database is up-to-date.</p>
                                    <p align="right">
                                        <rbfwebui:Button ID="Button1" runat="server" Text="Finish" Width="75px"></rbfwebui:Button></p>
                                </td>
                            </tr>
                            <tr id="dbNeedsUpdate" runat="server">
                                <td>
                                    <p>
                                        Rainbow has detected that Database Version does not match Code Version.
                                    </p>
                                    <p align="center">
                                        <rbfwebui:label ID="lblVersion" runat="server" CssClass="Normal" Font-Bold="True"></rbfwebui:label></p>
                                    <p>
                                        Update will now apply these patches to your database:</p>
                                    <p align="center">
                                    </p>
                                    <div style="overflow: auto; height: 200px">
                                        <asp:DataList ID="dlScripts" runat="server">
                                            <ItemTemplate>
                                                <rbfwebui:label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem") %>'
                                                    CssClass="Normal" Font-Bold="True">
                                                </rbfwebui:label>
                                            </ItemTemplate>
                                        </asp:DataList></div>
                                    &nbsp;
                                    <p>
                                    </p>
                                    <p align="left">
                                        Please BACKUP your data before proceeding.</p>
                                    <p align="right">
                                        <rbfwebui:Button ID="UpdateDatabaseCommand" runat="server" Text="Next >" Width="75px"></rbfwebui:Button></p>
                                </td>
                            </tr>
                            <tr id="Status" runat="server" visible="false">
                                <td>
                                    <p>
                                        <rbfwebui:label ID="lblStatus" runat="server">Database successfully updated!</rbfwebui:label>&nbsp;</p>
                                    <p align="right">
                                        <rbfwebui:Button ID="FinishButton" runat="server" Text="Finish" Width="75px"></rbfwebui:Button></p>
                                </td>
                            </tr>
                            <tr id="dbUpdateResult" runat="server" visible="false">
                                <td>
                                    <p>
                                        <rbfwebui:label ID="lblError" runat="server" CssClass="Error"></rbfwebui:label></p>
                                    <div style="overflow: auto; width: 445px; height: 90px">
                                        <p>
                                            <asp:DataList ID="dlErrors" runat="server">
                                                <ItemTemplate>
                                                    <rbfwebui:label ID="ErrorLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem") %>'
                                                        Font-Bold="True" Font-Size="8">
                                                    </rbfwebui:label>
                                                </ItemTemplate>
                                            </asp:DataList></p>
                                        <p>
                                            <asp:DataList ID="dlMessages" runat="server">
                                                <ItemTemplate>
                                                    <rbfwebui:label ID="MessageLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem") %>'
                                                        Font-Bold="True" Font-Size="8">
                                                    </rbfwebui:label>
                                                </ItemTemplate>
                                            </asp:DataList></p>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="background: #101010; height:10px;">
                    <table cellspacing="5" cellpadding="1" width="100%" border="0">
                        <tr>
                            <td class="Normal" style="color: white">
                                &nbsp;<strong>&copy; 2006 Rainbow Portal</strong></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
