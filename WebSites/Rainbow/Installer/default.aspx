<%@ Page Language="C#" Debug="true" ValidateRequest="true"  %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Collections" %>
<%@ Import Namespace="System.Collections.Specialized" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Security.Cryptography" %>
<%@ Import Namespace="System.Text" %>
<%@ Import Namespace="System.Security.Permissions" %>


<script runat="server">
/**************************************************************
Title: Rainbow Web Installer v.1.0
Author: Rahul Singh (Anant), rahul.singh@anant.us
Date: 9.8.2006

Special thanks to the following projects from which I borrowed ideas from.
1. CommunityServer.org - First ASP.NET Open Source project with a web based installer.
2. Joomla.org - Really nice open source CMS / Portal written in PHP with a nice installer.
3. WordPress.org - Great Open Source Blogging Software written in PHP with a nice installer.

The original file for this installer came from CommunityServer.
I later used a few functions from it to complete this installer. 


***************************************************************/


/**************************************************************
To enable the web based installer change the
line beneath this section to ‘true’.

After running the installer it is highly recommended that you 
set this value back to false to disable unauthorized access.
**************************************************************/
bool INSTALLER_ENABLED = true;

string PasswordForDB;

// flag indicating that the web.config file was successfully updated. This only works if you have write access
// to your virtual directory.
bool updatedConfigFile = false;

// consant string used to allow host to pass the database name to the wizard. If the database can be found in the 
// list of database returned, the wizard will skip the database selection page.
private const string QSK_DATABASE = "database"; // query string key

// arraylist of InstallerMessages. We contruct this on every page request to only keep track of the errors
// that have occurred during this web request. We don't store it in viewstate because we only want the errors
// that have happened on each page request
private ArrayList messages;

// Class to encapsulate the module (method) along with the error message that occurred within the module(method)
public class InstallerMessage {
	public string Module;
	public string Message;
	
	public InstallerMessage( string module, string message ) {
		Module = module;
		Message = message;
	}
};


public WizardPanel CurrentWizardPanel {
	get {
		if (ViewState["WizardPanel"] != null)
			return (WizardPanel) ViewState["WizardPanel"];

		return WizardPanel.PreInstall;
	}
	set {
		ViewState["WizardPanel"] = value;		
	}
}

/* Site Information */

public string SmtpServerText;
public string PortalPrefixText;
public string EmailFromText;
public string EncryptPasswordText;


/* TODO: protected string AdminPassword : randomly created admin password 
	CreateKey(8);

*/


public enum WizardPanel {
	PreInstall,
	License,
	ConnectToDb,
	SelectDb,
	SiteInformation,
	Done,
	Errors,
}


void HideAllPanels() {
	PreInstall.Visible = false;
	License.Visible = false;
	ConnectToDb.Visible = false;
	SiteInformation.Visible = false;
	Done.Visible = false;
	Errors.Visible = false;
}

public void Page_Load() {
	// We use the installer enabled flag to prevent someone from accidentally running the web installer, or
	// someone trying to maliciously trying to run the installer 
	if (!INSTALLER_ENABLED) {
		//TODO: make this error display on a nice panel
		Response.Write("<h1>Rainbow Installation Wizard is disabled.</h1>");
		Response.Flush();
		Response.End();
	}
	else {
		messages = new ArrayList();

		if (!Page.IsPostBack){
			SetActivePanel (WizardPanel.PreInstall, PreInstall);
			CheckEnvironment();
		}
	}
}

public void ReportException( string module, Exception e ) {
	ReportException( module, e.Message );
}

public void ReportException( string module, string message ) {
	messages.Add( new InstallerMessage( module, message ));
}

void SetActivePanel (WizardPanel panel, Control controlToShow) {

	Panel currentPanel = FindControl(CurrentWizardPanel.ToString()) as Panel;
	if( currentPanel != null )
		currentPanel.Visible = false;
	
	switch( panel ) {
		case WizardPanel.PreInstall:
			Previous.Enabled = false;
			License.Visible = false;
			break;
		case WizardPanel.Done:
			Next.Enabled = false;
			Previous.Enabled = false;
			break;
		case WizardPanel.Errors:
			Previous.Enabled = false;
			Next.Enabled = false;
			break;
		default:
			Previous.Enabled = true;
			Next.Enabled = true;
			break;
	}

	controlToShow.Visible = true;
	CurrentWizardPanel = panel;

}

private void CheckEnvironment(){

	string configFile = HttpContext.Current.Server.MapPath("~/web.config");
	string logsDir    = HttpContext.Current.Server.MapPath("~/rb_Logs");
	string portalsDir = HttpContext.Current.Server.MapPath("~/Portals");

	lblAspNetVersion.Text = "<span style='color:green;' >"+System.Environment.Version.ToString()+"</span>";
	lblWebConfigWritable.Text = IfFileWritable(configFile);
	lblLogsDirWritable.Text = IfDirectoryWritable(logsDir);
	lblPortalsDirWritable.Text = IfDirectoryWritable(portalsDir);
}

private string IfFileWritable(string FileName){

	string returnInfo="";

	FileInfo FileNameInfo = new FileInfo(FileName);

	if(FileNameInfo.Exists){
		returnInfo="<span style='color:green;' >Exists</span>";

		try{
			StreamWriter sw=File.AppendText(FileName);
			sw.Write(" ");
			sw.Close();
			returnInfo+=",<span style='color:green;' >Writable</span>";

		} catch (Exception e ) {

			returnInfo+=",<span style='color:red;' >Un-Writable: "+e.Message+"</span>";
		}
	}else{
		returnInfo="File Doesn't Exist";
	}

	return returnInfo;
}

private string IfDirectoryWritable(string DirectoryName){
	string returnInfo="";
	string FileName="TempFile.txt";

	DirectoryInfo DirectoryNameInfo = new DirectoryInfo(DirectoryName);

	if(DirectoryNameInfo.Exists){
		returnInfo="<span style='color:green;' >Exists</span>";

		try{
			StreamWriter sw=File.AppendText(DirectoryName+"\\"+FileName);
			sw.Write("-");
			sw.Close();
			File.Delete(DirectoryName+"\\"+FileName);
			returnInfo+=",<span style='color:green;' >Writable</span>";

		} catch (Exception e) {
			returnInfo+=",<span style='color:red;' >Un-Writable: "+e.Message+"</span>";
		} 

	} else {
		returnInfo = "Directory Doesn't Exist";
	}
	return returnInfo;	
}

/*******
private bool InstallDatabase() Deleted
TODO: do some initial database create here, let DB installer take care of rest.
take care of any aspnetdb role user assignments here using AddToRole
 
*******/

private bool InstallConfig() {


	// try to update the web.config file with the new connection string. No big deal if we can't
	UpdateWebConfig();

	return true;
}

protected bool UpdateWebConfig() {
	bool returnValue = false;
	try {
		System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
		if( doc == null ) 
			return false;
		
		doc.PreserveWhitespace  = true;
		
		string configFile = HttpContext.Current.Server.MapPath("~/web.config");
		
		doc.Load(configFile);
		bool dirty = false;
		
		// for Rainbow 2.0
		
		System.Xml.XmlNode connectionStrings = doc.SelectSingleNode("configuration/connectionStrings");
		foreach( System.Xml.XmlNode connString in connectionStrings ){
		    if (connString.Name == "add" ){
		        System.Xml.XmlAttribute attrName = connString.Attributes["name"];
		        if( attrName != null)
		        {
		            if(attrName.Value == "ConnectionString" )
		            {
		                    System.Xml.XmlAttribute attrCSTRValue = connString.Attributes["connectionString"];
		                    if( attrCSTRValue != null )
		                    {
		                        attrCSTRValue.Value = GetDatabaseConnectionString();
		                        dirty = true;
		                    } 
		            }else 
                    if(attrName.Value == "Providers.ConnectionString" )
		            {
		                    System.Xml.XmlAttribute attrPCSTRValue = connString.Attributes["connectionString"];
		                    if( attrPCSTRValue != null )
		                    {
		                        attrPCSTRValue.Value = GetDatabaseConnectionString();
		                        dirty = true;
		                    } 
		            }else if(attrName.Value == "RainbowProviders.ConnectionString" )
		            {
		                    System.Xml.XmlAttribute attrRPCSTRValue = connString.Attributes["connectionString"];
		                    if( attrRPCSTRValue != null )
		                    {
		                        attrRPCSTRValue.Value = GetDatabaseConnectionString();
		                        dirty = true;
		                    } 
		            }else if(attrName.Value == "Main.ConnectionString" )
		            {
		                    System.Xml.XmlAttribute attrMCSTRValue = connString.Attributes["connectionString"];
		                    if( attrMCSTRValue != null )
		                    {
                                attrMCSTRValue.Value = GetDatabaseConnectionString();
		                        dirty = true;
		                    } 
		            }
		        }
		    
		    }
		
		} 
		
		
		System.Xml.XmlNode appSettings = doc.SelectSingleNode("configuration/appSettings");
		foreach( System.Xml.XmlNode setting in appSettings ) {
			if( setting.Name == "add" ) {
				System.Xml.XmlAttribute attrKey = setting.Attributes["key"];
				if( attrKey != null)
				{
					/*if(attrKey.Value == "ConnectionString" ) 
					{					
						System.Xml.XmlAttribute attrSqlValue = setting.Attributes["value"];
						if( attrSqlValue != null ) 
						{
							attrSqlValue.Value = GetDatabaseConnectionString();
							dirty = true;
					
						}	
					}
					else */if(attrKey.Value == "SmtpServer")
					{
						System.Xml.XmlAttribute attrSMTPValue = setting.Attributes["value"];
						if( attrSMTPValue != null ) 
						{
							attrSMTPValue.Value = SmtpServerText;
							dirty = true;
					
						}	

					}
					else if(attrKey.Value == "EmailFrom")
					{
						System.Xml.XmlAttribute attrEFROMValue = setting.Attributes["value"];
						if( attrEFROMValue != null ) 
						{
							attrEFROMValue.Value = EmailFromText;
							dirty = true;
					
						}	

					}
					else if(attrKey.Value == "PortalTitlePrefix")
					{
						System.Xml.XmlAttribute attrPREFIXValue = setting.Attributes["value"];
						if( attrPREFIXValue != null ) 
						{
							attrPREFIXValue.Value = PortalPrefixText;
							dirty = true;
					
						}	

					}

					else if(attrKey.Value == "EncryptPassword")
					{
						System.Xml.XmlAttribute attrENCPASSValue = setting.Attributes["value"];
						if( attrENCPASSValue != null ) 
						{
							attrENCPASSValue.Value = EncryptPasswordText;
							dirty = true;
					
						}	

					}

				}
			}
		}
		
		if( dirty ) {
			// Save the document to a file and auto-indent the output.
			System.Xml.XmlTextWriter writer = new System.Xml.XmlTextWriter(configFile, System.Text.Encoding.UTF8);
			writer.Formatting = System.Xml.Formatting.Indented;
			doc.Save(writer);
			
			updatedConfigFile = true;
		}
	}
	catch(Exception e ) {
		ReportException("UpdateWebConfig", e );
	}
	
	return returnValue;
}

/*
TODO: use this to generate passwords as well.
protected string CreateKey(int len)
{
            byte[] bytes = new byte[len];
            new RNGCryptoServiceProvider().GetBytes(bytes);
            
			StringBuilder sb = new StringBuilder();
			for(int i = 0; i < bytes.Length; i++)
			{	
				sb.Append(string.Format("{0:X2}",bytes[i]));
			}
			
			return sb.ToString();
}
*/
private string GetConnectionString() {
	return String.Format("server={0};uid={1};pwd={2};Trusted_Connection={3}", db_server.Text, db_login.Text, db_password.Text, (db_Connect.SelectedIndex == 0 ? "yes" : "no"));
}

private string GetDatabaseConnectionString() {
	return String.Format("{0};database={1}", GetConnectionString(), db_name_list.SelectedValue);
}

private bool Validate_ConnectToDb(out string errorMessage) {

//	ConnectionString = "server=" + db_server.Text + ";uid="+ db_login.Text +";pwd=" + db_password.Text + ";Trusted_Connection=" + (db_Connect.SelectedIndex == 0 ? "yes" : "no");

	try {

		SqlConnection connection = new SqlConnection(GetConnectionString());
		connection.Open();
		connection.Close();
		
		errorMessage = "";
		return true;
		
	} catch (Exception e) {
		errorMessage = e.Message;
		return false;
	}
	
}

private bool Validate_SelectDb(out string errorMessage) {

	try {
	
		using( SqlConnection connection = new SqlConnection(GetDatabaseConnectionString() )) {
			connection.Open();
			connection.Close();
		}
		
		errorMessage = "";
		
		return true;
	}
	catch( SqlException se ) {
		switch( se.Number ) {
			case 4060:	// login fails
				if( db_Connect.SelectedIndex == 0) {
					errorMessage = "The installer is unable to access the specified database using the Windows credentials that the web server is running under. Contact your system administrator to have them add	" + Environment.UserName + " to the list of authorized logins";
				}
				else {
					errorMessage = "You can't login to that database. Please select another one<br />" + se.Message;
				}
				break;
			default:
				errorMessage = String.Format("Number:{0}:<br/>Message:{1}", se.Number, se.Message)+"<br/>"+GetConnectionString() ;
				break;
		}
		return false;
	}
	catch( Exception e ) {
		errorMessage = e.Message;
		return false;
	}
}

private bool Validate_SelectDb_ListDatabases(out string errorMessage) {

	try {
		SqlConnection connection = new SqlConnection(GetConnectionString());
		SqlDataReader dr;
		SqlCommand command = new SqlCommand("select name from master..sysdatabases order by name asc", connection);

		connection.Open();

		// Change to the master database
		//
		connection.ChangeDatabase("master");

		dr = command.ExecuteReader();

		db_name_list.Items.Clear();

		while (dr.Read()) 
		{
			string dbName = dr["name"] as String;
			if( dbName != null ) {
				if( dbName == "master" ||
					dbName == "msdb" ||
					dbName == "tempdb" ||
					dbName == "model" ) {
					
					// skip the system databases
					continue;
				}
				else {
					db_name_list.Items.Add( dbName );
				}
			}
		}

		connection.Close();
		
		errorMessage = "";
		
		return true;
	}
	catch( Exception e ) {
		errorMessage = e.Message;
		return false;
	}
}

private bool CheckSiteInfoValid(){
	if ( rb_smtpserver.Text.Trim().Length == 0)
	{
		req_rb_smtpserver.IsValid = false;
		return false;
	}

	if ( rb_portalprefix.Text.Trim().Length == 0)
	{
		req_rb_portalprefix.IsValid = false;
		return false;
	}

	if ( rb_emailfrom.Text.Trim().Length == 0)
	{
		req_rb_emailfrom.IsValid = false;
		return false;
	}
	return true;
}



public void NextPanel (Object sender, EventArgs e) {
	string errorMessage = "";
	
	switch (CurrentWizardPanel) {

		case WizardPanel.PreInstall:
			SetActivePanel (WizardPanel.License, License);
			break;

		case WizardPanel.License:
			if( chkIAgree.Checked )
				SetActivePanel (WizardPanel.ConnectToDb, ConnectToDb);
			break;
		
		case WizardPanel.ConnectToDb:
			if (Validate_ConnectToDb(out errorMessage)) {
				if( Validate_SelectDb_ListDatabases(out errorMessage)) {
					if( this.Request.QueryString[QSK_DATABASE] != null &&
						this.Request.QueryString[QSK_DATABASE] != String.Empty ) {
						
						try {
							db_name_list.SelectedValue = HttpUtility.UrlDecode(this.Request.QueryString[QSK_DATABASE]);
							
							SetActivePanel(WizardPanel.SiteInformation, SiteInformation );
						}
						catch {
							// an error occured setting the database, lets let the user select the database
							SetActivePanel(WizardPanel.SelectDb, SelectDb);
						}
					}
					else
						SetActivePanel (WizardPanel.SelectDb, SelectDb);
				}
				else {
					lblErrMsgConnect.Text = errorMessage;
				}
			}
			else {
				lblErrMsgConnect.Text = errorMessage;
			}
			break;
	
		case WizardPanel.SelectDb:
			if (Validate_SelectDb(out errorMessage)) {
				SetActivePanel (WizardPanel.SiteInformation, SiteInformation);

			}
			else {
				lblErrMsg.Text = errorMessage;
			}
			
			break;

		case WizardPanel.SiteInformation:
//			SetActivePanel( WizardPanel.Install, Install );

			if (CheckSiteInfoValid())
			{
				PortalPrefixText=rb_portalprefix.Text;
				SmtpServerText=rb_smtpserver.Text;
				EmailFromText=rb_emailfrom.Text;
				EncryptPasswordText=rb_encryptpassword.Checked.ToString();
				
				if (InstallConfig()) {
					SetActivePanel (WizardPanel.Done, Done);
					
					if( updatedConfigFile ) {
						pnlConfigDoneUpdating.Visible = true;
						pnlConfigNeedUpdating.Visible = false;
					}
				}
				else {
					lstMessages.DataSource = messages;
					lstMessages.DataBind();

					SetActivePanel( WizardPanel.Errors, Errors );
				}
			}
			break;

		case WizardPanel.Done:
			break;

	}
}


public void PreviousPanel (Object sender, EventArgs e) {
	switch (CurrentWizardPanel) {

		case WizardPanel.PreInstall:
			break;

		case WizardPanel.License:
			SetActivePanel (WizardPanel.PreInstall, PreInstall);
			break;
		
		case WizardPanel.ConnectToDb:
			SetActivePanel (WizardPanel.License, License);
			break;
	
		case WizardPanel.SelectDb:
			SetActivePanel (WizardPanel.ConnectToDb, ConnectToDb);
			break;
	
		case WizardPanel.SiteInformation:
			if( Page.Request.QueryString[QSK_DATABASE] != null &&
				Page.Request.QueryString[QSK_DATABASE] != String.Empty ) {
			
				SetActivePanel (WizardPanel.ConnectToDb, ConnectToDb);
			}
			else {
				SetActivePanel (WizardPanel.SelectDb, SelectDb);
			}
			break;
			
		case WizardPanel.Done:
			SetActivePanel (WizardPanel.SiteInformation, SiteInformation);
			break;

	}
}

public string StepClass(WizardPanel panelName)
{
	string returnValue="";

	if(CurrentWizardPanel!=panelName){
		returnValue="stepnotselected";
	}else{
		returnValue="stepselected";
	}

	return returnValue;
}

</script>
<html>
<head>
<title>Rainbow Web Installer</title>
<style>
body
{
	background-color: #606060;
	color: #000000;
	font-family: Tahoma, Arial, Helvetica;
}

div, td, a, a:visited {
	font-family: Tahoma, Arial, Helvetica;
	font-size: 11px;
	color: #000000;
}
h2 {
	font-family: Tahoma, Arial, Helvetica;
	font-size: 20px;

}
.buttons {
	font-family: Tahoma, Arial, Helvetica;
	font-size: 11px;
	width:90px;
}
.mainTitle {
	display: block;
	font-family: Tahoma, Arial, Helvetica;
	font-size: 14px;
	font-weight: 900;
	color: #369;
	background-color: #cecece;
	padding:10px;
	
}
.thisframe {
	font-family:  Tahoma, Arial, Helvetica;
	font-size: 11px;
}
.bold 
{
	font-weight: 900;
}
.dataentry
{
	width:150px;
}
TABLE.err {
	border: 1px solid #999999;
	margin-top: 8px;
}
TH.err {
	background-color:#999999;
	color: #000000;
	font-family:  Tahoma, Arial, Helvetica;
	font-size: 12px;
	font-weight: bold;
	padding: 2px;
}
TD.err {
	background-color:#ffffff;
	color: #000000;
	font-family:  Tahoma, Arial, Helvetica;
	font-size: 10px;
	padding: 2px;
}


.stepselected 
{
	display: block;
	color: #FFF;
	background-color: #036;
	width: 175px;
 	padding: 5px 10px;
	text-decoration: none;
}

.stepnotselected  {
	display: block;
	color: #FFF;
	background-color: #369;
	width: 175px;
 	padding: 5px 10px;
	text-decoration: none;
}

.wizardsection {

	padding:10px;
	border: 2px solid #cecece;
	background-color: #fff;
}

.wizardsectionheader{
	padding:5px;
	border: 2px solid #cecece;
	background-color: #369;
	color:#fff;	
	font-size:12px;
}
</style>
</head>
<body>
<form runat="server">
<table height="100%" width="100%" align="center"><tr><td align="center" valign="middle">
	<table height="525" cellSpacing="0" cellPadding="0" width="100%" border="0" style="background-color: #ffffff; border: solid 1px #999999;">
		<tbody>
			<tr>
				<td colspan="2" valign="top" height="75" background="images/installer_top_bg.png" style="background-repeat:repeat-x;color:#fff;font-size:20px;padding:10px;">Rainbow Web Installer</td>
			</tr>
			<tr>
				<td valign="top">
		 			<div align="center" style="style="padding-right: 10px; padding-left: 10px; padding-bottom: 10px; padding-top: 10px;">
						<div class="<%=StepClass(WizardPanel.PreInstall)%>">Requirements Check</div>
						<div class="<%=StepClass(WizardPanel.License)%>">License</div>
						<div class="<%=StepClass(WizardPanel.ConnectToDb)%>">DB Login</div>
						<div class="<%=StepClass(WizardPanel.SelectDb)%>">Choose Database</div>
						<div class="<%=StepClass(WizardPanel.SiteInformation)%>">Site Information</div>
						<div class="<%=StepClass(WizardPanel.Done)%>">Write Config File</div>
						<div class="stepnotselected">Install Rainbow DB</div>
					</div>
				</td>
				<td valign="top">

<div style="padding-right: 10px; padding-left: 10px; padding-bottom: 10px; padding-top: 0px">
<asp:panel id="PreInstall" runat="server" Visible="false">
    <div class="mainTitle">Pre-Installation Requirements Check</div>
	<div class="wizardsectionheader"><strong>Requirements:</strong></div>
    <div class="wizardsection">
		<ul>
			<li style="margin-bottom: 8px;"><div class="bold">Microsoft .NET Framework</div>
				<div>Version 1.1 or 2.0 of the .NET framework must be installed</div>
				<div><a href="http://www.asp.net/Default.aspx?tabindex=0&tabid=1" target="_blank">Get help installing the .NET framework</a></div>
			</li>
			<li style="margin-bottom: 8px;"><div class="bold">Internet Information Server</div>
				<div>IIS or compatible web server software must be installed</div>
				<div><a href="http://www.google.com/search?q=install+iis" target="_blank">Get help installing IIS</a></div>
			</li>
			<li style="margin-bottom: 8px;"><div class="bold">Microsoft SQL Server</div>
				<div>You must have either SQL Server 2000, SQL Server 2005, or SQL Server Express 2005 installed.</div>
				<div><a href="http://www.microsoft.com/sqlserver/" target="_blank">Get information on SQL Server here</a></div>
			</li>
		</ul>

	</div>
	<div class="wizardsectionheader"><strong>Environment Check:</strong></div>
	<div class="wizardsection">
		<ul>
			<li style="margin-bottom: 8px;"><div class="bold">Microsoft .NET Version : <asp:Literal EnableViewState=False id="lblAspNetVersion" Runat=server /></div></li>
			<li style="margin-bottom: 8px;"><div class="bold">Web.Config : <asp:Literal EnableViewState=False id="lblWebConfigWritable" Runat=server /></div></li>
			<li style="margin-bottom: 8px;"><div class="bold">RB Logs Directory : <asp:Literal EnableViewState=False id="lblLogsDirWritable" Runat=server /></div></li>
			<li style="margin-bottom: 8px;"><div class="bold">Portals Directory : <asp:Literal EnableViewState=False id="lblPortalsDirWritable" Runat=server /></div></li>
		</ul>
	</div>
</asp:panel>
<asp:panel id="License" runat="server" Visible="false">
	<table cellpadding="0" cellspacing="0" border="0">
		<tr>
			<td><span class="mainTitle">Rainbow License</span></td>
		</td>
		<tr>
			<td>&nbsp;</td>
		</tr>
		<tr>
			<td valign="top" align="right" colspan="2"><iframe frameborder="1" scrolling="yes" src="dpl.html" height="250" width="600"></iframe></td>
		</tr>
	</table>
	</div> <br>
	<div align="right" style="padding-right: 50px;">
		<asp:Checkbox id="chkIAgree" runat=server Text=" I Agree" Checked="false" />
	</div>
</asp:panel>
<asp:panel id="ConnectToDb" runat="server" Visible="false">
    <div class="mainTitle">Rainbow Database Login</div>
	<div class="wizardsection">
	<div>Enter the database login that Rainbow will use to connect to the database.</div>
	<div style="color:red;"><asp:Literal EnableViewState=False id="lblErrMsgConnect" Runat=server /></div>
	<div style="padding-left: 20px; padding-top: 20px">IP address or Server Name:
		<asp:textbox CssClass="dataentry" id="db_server" runat="server" value="(local)"></asp:textbox><br/>
		<asp:RadioButtonList id="db_Connect" runat="server" SelectedIndexChanged="ConnectToDb_CheckChanged">
			<asp:ListItem Value="Windows Authentication" >Windows Authentication</asp:ListItem>
			<asp:ListItem Value="SQL Server Authentication" Selected="True">SQL Server Authentication</asp:ListItem>
		</asp:RadioButtonList>
		<div style="padding-left: 20px; padding-top: 20px">
			<table>
				<tr>
					<td align="left">Username:</TD>
					<td align="left">
						<asp:textbox cssclass="dataentry" id="db_login" runat="server"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td align="left">Password:</TD>
					<td align="left">
						<asp:textbox CssClass="dataentry" id="db_password" runat="server" ></asp:textbox>
					</td>
				</tr>
			</table>
		</div>
	</div>
	</div>
</asp:panel>
<asp:panel id="SelectDb" runat="server" Visible="false">
    <div class="mainTitle">Select Database Instance</div>
	<div class="wizardsection">
    <div>Choose the database where you would like to to install Rainbow Portal.</div>
    <div style="padding-left: 20px; padding-top: 20px">
		<div style="padding-left: 20px; padding-top: 10px;color: red"><asp:Literal EnableViewState=False id="lblErrMsg" Runat=server /></div>
		<div style="padding-left: 20px; padding-top: 10px">Available databases:
			<asp:DropDownList id="db_name_list" runat="server"></asp:DropDownList>
		</div>
	</div>
	</div>
</asp:panel>
<asp:panel id="SiteInformation" runat="server" Visible="false">
	<div class="mainTitle">Enter Site Information</div>
			<div class="wizardsectionheader">Enter the following information to configure your site properly. </div>
			<div class="wizardsection">
			<table cellpadding="2" cellspacing="0" border="0">
				<tr>
					<td align="left" valign="top">
						Site Title Prefix:
					</td>
					<td align="left">
						<asp:TextBox CssClass="dataentry" id="rb_portalprefix" runat=server>My Site - </asp:TextBox>
						<asp:RequiredFieldValidator id="req_rb_portalprefix" runat="server" ControlToValidate="rb_portalprefix" enabled="true" display="Dynamic"><br>* Site Prefix is required!</asp:RequiredFieldValidator>
					</td>
					<td width="50%" nowrap>example: My Site - . This will make all your page titles: "My Site - Page Name"</td>
				</tr>
			</table>
			</div>
			<div class="wizardsectionheader">
			Enter the SMTP Server and Email address that you want portal emails sent from.
			</div>
			<div class="wizardsection">
			<table cellpadding="2" cellspacing="0" border="0">
				<tr>
					<td align="left" valign="top" width="100">SMTP Server:</TD>
					<td align="left" colspan="2">
						<asp:textbox CssClass="dataentry" id="rb_smtpserver" runat="server">localhost</asp:textbox>
						<asp:requiredfieldvalidator id="req_rb_smtpserver" runat="server" controltovalidate="rb_smtpserver" enabled="true" display="Dynamic"><br>* SMTP Server is required!</asp:requiredfieldvalidator>
					</td>
				</tr>
				<tr>
					<td align="left" valign="top" width="100">Email From:</TD>
					<td align="left">
						<asp:textbox CssClass="dataentry" id="rb_emailfrom" runat="server" >admin@portal.com</asp:textbox>
						<asp:requiredfieldvalidator id="req_rb_emailfrom" runat="server" controltovalidate="rb_emailfrom" enabled="true" display="Dynamic"><br>* Email From Address is required!</asp:requiredfieldvalidator>
					</td>
					<td valign="top">The Email From address is used on all outgoing messages from the Portal.</td>
				</tr>
			</table>
			</div>
			<div class="wizardsectionheader">
			Would you like to Encrypt Passwords?
			</div>
			<div class="wizardsection">
			<table cellpadding="2" cellspacing="0" border="0>
				<tr>
					<td width="100">&nbsp;</td>
					<td align="left">
						<asp:Checkbox id="rb_encryptpassword" runat=server Text="Encrypt User Passwords" Checked=false /><br/><br/>
						<span style="color:green;" >Check this if you want to encrypt your passwords using HASH+SALT.(Recommended, not Required)</span>
					</td>

				</tr>
			</TABLE>
			</div>
</asp:panel>
<asp:panel id="Install" runat="server" Visible="false">
	<span class="mainTitle">Writing Configuration File</span>
</asp:panel>
<asp:panel id="Done" runat="server" Visible="false">
	<div class="mainTitle">Complete!</div>
	<div class="wizardsection">
	<div>
		<asp:Panel id="pnlConfigNeedUpdating" Runat=server Visible=True>
			Please update your web.config with the following information:<br />
			<pre>
&lt;appSettings&gt;
  &lt;add key="ConnectionString" value="<span style="color:red"><%= GetDatabaseConnectionString() %></span>" /&gt;
  &lt;add key="PortalTitlePrefix" value="<span style="color:red"><%= PortalPrefixText %></span>" /&gt;
  &lt;add key="SmtpServer" value="<span style="color:red"><%= SmtpServerText %></span>" /&gt;
  &lt;add key="EmailFrom" value="<span style="color:red"><%= EmailFromText %></span>" /&gt;
  &lt;add key="EncryptPassword" value="<span style="color:red"><%= EncryptPasswordText %></span>" /&gt;
&lt;/appSettings&gt;</pre>
			<p>Once you've updated your web.config click <a href='../Setup/'/>here</a> to install the Rainbow Database.</p>
		</asp:Panel>
		<asp:Panel id="pnlConfigDoneUpdating" runat=server Visible=False>
			Your web.config file was successfully updated with the new connection string
			<p>Click <a href='../Setup/'/>here</a> to install the Rainbow Database.</p>
		</asp:Panel>
		<div style="color:red">
		<b><u>IMPORTANT</u></b>: You should now have a working Rainbow pre-installation. It is <u>strongly recommended</u> that you disable the Rainbow Web Installer to prevent unauthorized access to your server. To disable the Rainbow Web Installer open the /Installer/default.aspx file found on your web server and follow the instructions (in the file) to disable the installer.
		</div>
	</div>
	</div>
</asp:panel>
<asp:Panel id="Errors" runat=server Visible=false >
	<span class="mainTitle">Errors Occurred</span><br><br>
	<div>
		Errors occured during the execution of this wizard.
	</div>
	<asp:Repeater id="lstMessages" Runat="server">
		<HeaderTemplate>
			<table class="err" width="580px" border=0 cellpadding=0 cellspacing=0 >
				<tr>
					<th class="err" width="100px">Module</th>
					<th class="err" >Message</th>
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
				<tr valign="top">
					<td class="err"><%# ((InstallerMessage)Container.DataItem).Module %></td>
					<td class="err"><%# ((InstallerMessage)Container.DataItem).Message %></td>
				</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>
</asp:Panel>
</div>
				</td>
			</tr>
			<tr>
				<td valign="bottom" colspan="2">
				<table cellpadding="0" cellspacing="0" border="0" width="100%">
					<tr>
						<td></td>
						<td align="right"></td>
					</tr>
				</table>
				</td>
			</tr>
			<tr>
				<td colspan="2" align="right" bgcolor="#cecece" height="45"><div style="padding-right: 30px;"><asp:button id="Previous" onclick="PreviousPanel" runat="server" text="< Previous" cssClass="buttons"></asp:button>&nbsp;<asp:button id="Next" onclick="NextPanel" runat="server" text="Next >" cssClass="buttons"></asp:button></div></td>
			</tr>
		</tbody>
	</table>
</td></tr></table>
</form>
</body>
</html>
