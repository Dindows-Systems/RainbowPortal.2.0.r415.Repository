using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using Rainbow.Context;
using Rainbow.Framework;
using Rainbow.Framework.BLL.Utils;
using Rainbow.Framework.Exceptions;
using Rainbow.Framework.Helpers;
using Rainbow.Framework.Scheduler;
using Rainbow.Framework.Security;
using Rainbow.Framework.Settings;
using Rainbow.Framework.Site.Configuration;
using History=Rainbow.Framework.History;
using Path=System.IO.Path;
using Reader=Rainbow.Context.Reader;
using System.Configuration;

namespace Rainbow
{
    /// <summary>
    /// Defines the methods, properties, and events common to all application 
    /// objects within an ASP.NET application. This class is the base class for 
    /// applications defined by the user in the global.asax file.
    /// </summary>
    /// <remarks>
    /// Instances of this class are created in the ASP.NET infrastructure, not by the user directly. One instance is used to process many requests in its lifetime but it can process only one at a time. Thus member variables can be used to store per-request data.
    /// An application executes events that are handled by modules or user code 
    /// defined in the global.asax file in the following sequence:
    /// <list type="string">
    /// <item>BeginRequest</item>
    /// <item>AuthenticateRequest</item>
    /// <item>AuthorizeRequest</item>
    /// <item>ResolveRequestCache</item>
    /// <item>[A handler (a page corresponding to the request URL) is created at this point.]</item>
    /// <item>AcquireRequestState</item>
    /// <item>PreRequestHandlerExecute</item>
    /// <item>[The handler is executed.]</item>
    /// <item>PostRequestHandlerExecute</item>
    /// <item>ReleaseRequestState</item>
    /// <item>[Response filters, if any, filter the output.]</item>
    /// <item>UpdateRequestCache</item>
    /// <item>EndRequest</item>
    /// </list>
    /// </remarks>
    [History("Yannick Smits", "2006/08/27", "added fix to prevent 302 redirects")]
    [History("jonathan minond", "2006/03/04", "cleaned up a bit for rainbow 2.0")]
    [History("jminond", "2005/03/08", "added session timeout ability. controled in rainbow.conif cookieexpire")]
    [History("john.mandia@whitelightsolutions.com", "2004/05/29", "changed portal alias checking behavior")]
    [History("manu", "2002/11/29", "changed portal alias behavior")]
    [History("cory isakson", "2003/02/13", "changed portal alias behavior: always carry portal alias in URL or QS")]
    [History("bill anderson", "2003/05/13", "track user information for anonymous (min/max/close)")]
    [History("cory isakson", "2003/09/10", "added overrides for WebCompile Feature")]
    public class Global : HttpApplication
    {
        /// <summary>
        /// Handles the BeginRequest event of the Application control.
        /// The Application_BeginRequest method is an ASP.NET event that executes
        /// on each web request into the portal application.  The below method
        /// obtains the current pageIndex and PageID from the querystring of the
        /// request -- and then obtains the configuration necessary to process
        /// and render the request.
        /// This portal configuration is stored within the application's "Context"
        /// object -- which is available to all pages, controls and components
        /// during the processing of a single request.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            Reader contextReader = new Reader(new WebContextReader());
            HttpContext context = contextReader.Current;

            // TODO: Move all URL handling to URLHandler.cs in core
            // TODO: Need to support page name with no id, search db for match
            // TODO: If not, return page with recomended matches..
            /*
             * QUERY FOR MATCHERS 
             * 			int pageID = Portal.PageID; // Get PageID from QueryString
			            string portalAlias = Portal.UniqueID; 
             * Portal alias = 
             * page name = currentURL.Substring(currentURL.LastIndexOf("/") + 1)
DECLARE @portalAlias varchar(20)
DECLARE @PageName varchar(20)

SET @portalAlias = 'rainbow'
SET @PageName = 'Manage' 

SELECT     rb_Tabs.TabID, rb_Tabs.TabName
FROM         rb_Portals INNER JOIN
                      rb_Tabs ON rb_Portals.PortalID = rb_Tabs.PortalID
WHERE     (rb_Portals.PortalAlias LIKE '%' + @portalAlias + '%') AND (rb_Tabs.TabName LIKE N'%' + @PageName + N'%')
             */
            string currentURL = context.Request.Path.ToLower();


#if DEBUG
            if (currentURL.Contains("trace.axd"))
                return;
#endif
            context.Trace.Warn("Application_BeginRequest :: " + currentURL);
            if (Portal.PageID > 0)
            {
                //Creates the physical path on the server 
                string physicalPath = context.Server.MapPath(currentURL.Substring(currentURL.LastIndexOf("/") + 1));

                // TODO: Can we enhance performance here by checking to see if it is a friedly url page
                // name instead of doing an IO check for exists?
                // checks to see if the file does not exsists.
                if (!File.Exists(physicalPath)) // Rewrites the path
                    context.RewritePath("~/default.aspx?" + context.Request.ServerVariables["QUERY_STRING"]);
            }
            else
            {
                string pname = currentURL.Substring(currentURL.LastIndexOf("/") + 1);
                pname = pname.Substring(0, (pname.Length - 5));
                if (Regex.IsMatch(pname, @"^\d+$"))
                    context.RewritePath("~/default.aspx?pageid=" + pname +
                                        context.Request.ServerVariables["QUERY_STRING"]);
            }


            // 1st Check: is it a dangerously malformed request?
            //Important patch http://support.microsoft.com/?kbid=887459
            if (context.Request.Path.IndexOf('\\') >= 0 ||
                Path.GetFullPath(context.Request.PhysicalPath) != context.Request.PhysicalPath)
                throw new RainbowRedirect(LogLevel.Warn, HttpStatusCode.NotFound, "Malformed request", null);

            #region 2nd Check: is the AllPortals Lock switched on?

            // 2nd Check: is the AllPortals Lock switched on?
            // let the user through if client IP address is in LockExceptions list, otherwise throw...
            if (Config.LockAllPortals)
            {
                string _rawUrl = context.Request.RawUrl.ToLower(CultureInfo.InvariantCulture);
                string _lockRedirect = Config.LockRedirect;
                if (!_rawUrl.EndsWith(_lockRedirect))
                {
                    // construct IPList
                    string[] lockKeyHolders = Config.LockKeyHolders.Split(new char[] {';'});
                    IPList ipList = new IPList();
                    foreach (string lockKeyHolder in lockKeyHolders)
                    {
                        if (lockKeyHolder.IndexOf("-") > -1)
                            ipList.AddRange(lockKeyHolder.Substring(0, lockKeyHolder.IndexOf("-")),
                                            lockKeyHolder.Substring(lockKeyHolder.IndexOf("-") + 1));
                        else
                            ipList.Add(lockKeyHolder);
                    }
                    // check if requestor's IP address is in allowed list
                    if (!ipList.CheckNumber(context.Request.UserHostAddress))
                        throw new PortalsLockedException();
                }
            }

            #endregion

            #region 3rd Check: is database/code version correct?

            // 3rd Check: is database/code version correct?
                        // don't check database when installer is running
            if (Request.AppRelativeCurrentExecutionFilePath.ToLower() != Config.InstallerRedirect.ToLower() &&
                Request.AppRelativeCurrentExecutionFilePath.ToLower() != "~/webresource.axd")
            {
                int versionDelta = Database.DatabaseVersion.CompareTo(Portal.CodeVersion);
                // if DB and code versions do not match
                if (versionDelta != 0)
                {
                    Uri _requestUri = context.Request.Url;
                    string _databaseUpdateRedirect = Config.DatabaseUpdateRedirect;
                    if (_databaseUpdateRedirect.StartsWith("~/"))
                        _databaseUpdateRedirect = _databaseUpdateRedirect.TrimStart(new char[] { '~' });

                    if (
                        !
                        _requestUri.AbsolutePath.ToLower(CultureInfo.InvariantCulture).EndsWith(
                            _databaseUpdateRedirect.ToLower(CultureInfo.InvariantCulture)))
                    {
                        // ...and this is not DB Update page
                        string errorMessage = "Database version: " + Database.DatabaseVersion.ToString() + " Code version: " +
                                              Portal.CodeVersion.ToString();
                        if (versionDelta < 0) // DB Version is behind Code Version
                        {
                            // Jonathan : WHy wouldnt we redirect to update page?
                            // TODO : Check with people why this was like this....
                            Response.Redirect(Framework.Settings.Path.ApplicationRoot + _databaseUpdateRedirect, true);
                            // so update?
                            ErrorHandler.Publish(LogLevel.Warn, errorMessage);
                            // throw new DatabaseVersionException(errorMessage);
                        }
                        else // DB version is ahead of Code Version
                        {
                            ErrorHandler.Publish(LogLevel.Warn, errorMessage);
                            // Jonathan : WHy wouldnt we redirect to update page?
                            // TODO : Check with people why this was like this....
                            // Who cares ?
                            // throw new CodeVersionException(errorMessage);
                        }
                    }
                    else // this is already DB Update page... 
                        return; // so skip creation of PortalSettings
                }

            #endregion

                // ************ 'calculate' response to this request ************
                //
                // Test 1 - try requested Alias and requested PageID
                // Test 2 - try requested Alias and PageID 0
                // Test 3 - try default Alias and requested PageID
                // Test 4 - try default Alias and PageID 0
                //
                // The UrlToleranceLevel determines how many times the test is allowed to fail before the request is considered
                // to be "an error" and is therefore redirected:
                //
                // UrlToleranceLevel 1 
                //		- requested Alias must be valid - if invalid, InvalidAliasRedirect page on default portal will be shown
                //		- if requested PageID is found, it is shown
                //		- if requested PageID is not found, InvalidPageIdRedirect page is shown
                // 
                // UrlToleranceLevel 2 
                //		- requested Alias must be valid - if invalid, InvalidAliasRedirect page on default portal will be shown
                //		- if requested PageID is found, it is shown
                //		- if requested PageID is not found, PageID 0 (Home page) is shown
                //
                // UrlToleranceLevel 3 - <<<<<< not working?
                //		- if requested Alias is invalid, default Alias will be used
                //		- if requested PageID is found, it is shown
                //		- if requested PageID is not found, InvalidPageIdRedirect page is shown
                // 
                // UrlToleranceLevel 4 - 
                //		- if requested Alias is invalid, default Alias will be used
                //		- if requested PageID is found, it is shown
                //		- if requested PageID is not found, PageID 0 (Home page) is shown

                PortalSettings portalSettings = null;
                int pageID = Portal.PageID; // Get PageID from QueryString
                string portalAlias = Portal.UniqueID; // Get requested alias from querystring, cookies or hostname
                string defaultAlias = Config.DefaultPortal; // get default portal from config

                // load arrays with values to test
                string[] testAlias = new string[4] { portalAlias, portalAlias, defaultAlias, defaultAlias };
                int[] testPageID = new int[4] { pageID, 0, pageID, 0 };

                int testsAllowed = Config.UrlToleranceLevel;
                int testsToRun = testsAllowed > 2 ? 4 : 2;
                // if requested alias is default alias, limit UrlToleranceLevel to max value of 2 and limit tests to 2
                if (portalAlias == defaultAlias)
                {
                    testsAllowed = testsAllowed % 2;
                    testsToRun = 2;
                }

                int testsCounter = 1;
                while (testsCounter <= testsToRun)
                {
                    //try with current values from arrays
                    portalSettings = new PortalSettings(testPageID[testsCounter - 1], testAlias[testsCounter - 1]);

                    // test returned result
                    if (portalSettings.PortalAlias != null)
                        break; // successful hit
                    else
                        testsCounter++; // increment the test counter and continue
                }

                if (portalSettings.PortalAlias == null)
                {
                    // critical error - neither requested alias nor default alias could be found in DB
                    throw new RainbowRedirect(
                        Config.NoPortalErrorRedirect,
                        LogLevel.Fatal,
                        Config.NoPortalErrorResponse,
                        "Unable to load any portal - redirecting request to ErrorNoPortal page.",
                        null);
                }

                if (testsCounter <= testsAllowed) // success
                {
                    // Portal Settings has passed the test so add it to Context
                    context.Items.Add("PortalSettings", portalSettings);
                    context.Items.Add("PortalID", portalSettings.PortalID); // jes1111
                }
                else // need to redirect
                {
                    if (portalSettings.PortalAlias != portalAlias) // we didn't get the portal we asked for
                    {
                        throw new RainbowRedirect(
                            Config.InvalidAliasRedirect,
                            LogLevel.Info,
                            HttpStatusCode.NotFound,
                            "Invalid Alias specified in request URL - redirecting (404) to InvalidAliasRedirect page.",
                            null);
                    }

                    if (portalSettings.ActivePage.PageID != pageID) // we didn't get the page we asked for
                    {
                        throw new RainbowRedirect(
                            Config.InvalidPageIdRedirect,
                            LogLevel.Info,
                            HttpStatusCode.NotFound,
                            "Invalid PageID specified in request URL - redirecting (404) to InvalidPageIdRedirect page.",
                            null);
                    }
                }

                // Save cookies
                //saveCookie = true; // Jes1111 - why is this always set to true? is it needed?
                //ExtendCookie(settings); 
                //if (saveCookie) // Jes1111 - why is this always set to true? is it needed?
                //{
                context.Response.Cookies["PortalAlias"].Path = "/";
                context.Response.Cookies["PortalAlias"].Value = portalSettings.PortalAlias;
                //}

                //Try to get alias from cookie to determine if alias has been changed
                bool refreshSite = false;
                if (context.Request.Cookies["PortalAlias"] != null &&
                    context.Request.Cookies["PortalAlias"].Value.ToLower() != Portal.UniqueID)
                    refreshSite = true; //Portal has changed since last page request
                // if switching portals then clean parameters [TipTopWeb]
                // Must be the last instruction in this method 

                // 5/7/2006 Ed Daniel
                // Added hack for Http 302 by extending condition below to check for more than 3 cookies
                if (refreshSite && context.Request.Cookies.Keys.Count > 3)
                {
                    // Signout and force the browser to refresh only once to avoid any dead-lock
                    if (context.Request.Cookies["refreshed"] == null
                        || (context.Request.Cookies["refreshed"] != null
                            && context.Response.Cookies["refreshed"].Value == "false"))
                    {
                        string rawUrl = context.Request.RawUrl;

                        // jes1111 - not needed now
                        //					//by Manu avoid endless loop when portal does not exists
                        //					if (rawUrl.EndsWith("init")) // jes1111: is this still valid/needed?
                        //						context.Response.Redirect("~/app_support/ErrorNoPortal.html", true);
                        //
                        //					// add parameter at the end of the command line to detect the dead-lock 
                        //					if (rawUrl.LastIndexOf(@"?") > 0)
                        //						rawUrl += "&init";
                        //					else rawUrl += "?init";

                        context.Response.Cookies["refreshed"].Value = "true";
                        context.Response.Cookies["refreshed"].Path = "/";
                        context.Response.Cookies["refreshed"].Expires = DateTime.Now.AddMinutes(1);

                        // sign-out, if refreshed param on the command line we will not call it again
                        PortalSecurity.SignOut(rawUrl, false);
                    }
                }

                // invalidate cookie, so the page can be refreshed when needed
                if (context.Request.Cookies["refreshed"] != null && context.Request.Cookies.Keys.Count > 3)
                {
                    context.Response.Cookies["refreshed"].Path = "/";
                    context.Response.Cookies["refreshed"].Value = "false";
                    context.Response.Cookies["refreshed"].Expires = DateTime.Now.AddMinutes(1);
                }
            }
        } // end of Application_BeginRequest


        /// <summary>
        /// Handles the AuthenticateRequest event of the Application control.
        /// If the client is authenticated with the application, then determine
        /// which security roles he/she belongs to and replace the "User" intrinsic
        /// with a custom IPrincipal security object that permits "User.IsInRole"
        /// role checks within the application
        /// Roles are cached in the browser in an in-memory encrypted cookie.  If the
        /// cookie doesn't exist yet for this session, create it.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            Reader contextReader = new Reader(new WebContextReader());
            HttpContext context = contextReader.Current;

            if (context.Items["PortalSettings"] != null)
            {
                // Obtain PortalSettings from Current Context
                PortalSettings portalSettings = (PortalSettings) context.Items["PortalSettings"];

                // Auto-login a user who has a portal Alias login cookie
                // Try to authenticate the user with the cookie value
                if (!context.Request.IsAuthenticated &&
                    (context.Request.Cookies["Rainbow_" + portalSettings.PortalAlias] != null))
                {
                    if (context.Request.Cookies["Rainbow_" + portalSettings.PortalAlias].Expires > DateTime.Now)
                    {
                        string user;
                        user = context.Request.Cookies["Rainbow_" + portalSettings.PortalAlias.ToLower()].Value;

                        //jminond - option to kill cookie after certain time always
                        int minuteAdd = Config.CookieExpire;

                        // Create the FormsAuthentication cookie
                        FormsAuthentication.SetAuthCookie(user, true);

                        // Create a FormsAuthentication ticket.
                        FormsAuthenticationTicket cTicket = new FormsAuthenticationTicket
                            (
                            1, // version
                            user, // user name
                            DateTime.Now, // issue time
                            DateTime.Now.AddMinutes(minuteAdd),
                            false, // don't persist cookie
                            string.Empty // roles
                            );

                        // Set the current User Security to the FormsAuthenticated User
                        context.User = new RainbowPrincipal(new FormsIdentity(cTicket), null);
                    }
                }
                else
                {
                    // jminond - if user asked to persist, he should have a cookie
                    if ((context.Request.IsAuthenticated) &&
                        (context.Request.Cookies["Rainbow_" + portalSettings.PortalAlias] == null))
                        PortalSecurity.KillSession();
                }

                //if (context.Request.IsAuthenticated && !(context.User is WindowsPrincipal))
                //{
                //    // added by Jonathan Fong 22/07/2004 to support LDAP 
                //    //string[] names = Context.User.Identity.Name.Split("|".ToCharArray());
                //    string[] names = context.User.Identity.Name.Split('|');
                //    if (names.Length == 3 && names[2].StartsWith("cn="))
                //    {
                //        context.User = new RainbowPrincipal(
                //            new User(context.User.Identity.Name, "LDAP"), LDAPHelper.GetRoles(names[2]));
                //    }
                //    else
                //    {
                //        // Add our own custom principal to the request containing the roles in the auth ticket
                //        context.User = new RainbowPrincipal(context.User.Identity, PortalSecurity.GetRoles());
                //    }
                //    // Remove Windows specific custom settings
                //    if (portalSettings.CustomSettings != null)
                //        portalSettings.CustomSettings.Remove("WindowsAdmins");
                //}
                //    // bja@reedtek.com - need to get a unique id for user
                //else if (Config.WindowMgmtControls)
                //{
                //    // Need a uid, even for annoymous users
                //    string annoyUser;
                //    // cookie bag
                //    IWebBagHolder abag = BagFactory.instance.create(BagFactory.BagFactoryType.CookieType);
                //    // user data already set
                //    annoyUser = (string) abag[GlobalInternalStrings.UserWinMgmtIndex];
                //    // if no cookie then let's get one
                //    if (annoyUser == null)
                //    {
                //        // new uid for window mgmt
                //        Guid guid = Guid.NewGuid();
                //        // save the data into a cookie bag
                //        abag[GlobalInternalStrings.UserWinMgmtIndex] = guid.ToString();
                //    }
                //}
            }
        } // end of Application_AuthenticateRequest

        /// <summary>
        /// Handler for unhandled exceptions
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void Application_Error(object sender, EventArgs e)
        {
            // TODO : Uncomment this after magic url's are tested to work.
//#if (!DEBUG)
            ErrorHandler.ProcessUnhandledException();
//#endif
        }

        /// <summary>
        /// Application_Start()
        /// </summary>
        protected void Application_Start()
        {
            //HttpContext context = ContextReader.Current;
            HttpContext context = HttpContext.Current;

            // moved from PortalSettings
            FileVersionInfo f = FileVersionInfo.GetVersionInfo(Assembly.GetAssembly( typeof( Rainbow.Framework.Settings.Portal ) ).Location);
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application["CodeVersion"] = f.FilePrivatePart;
            HttpContext.Current.Application.UnLock();

            ErrorHandler.Publish(LogLevel.Info, "Application Started: code version " + Portal.CodeVersion.ToString());

            if (Config.CheckForFilePermission)
            {
                try
                {
                    string myNewDir = Path.Combine(Framework.Settings.Path.ApplicationPhysicalPath, "_createnewdir");

                    if (!Directory.Exists(myNewDir))
                        Directory.CreateDirectory(myNewDir);

                    if (Directory.Exists(myNewDir))
                        Directory.Delete(myNewDir);
                }
                catch (Exception ex)
                {
                    throw new RainbowException(LogLevel.Fatal, HttpStatusCode.ServiceUnavailable,
                                               "ASPNET Account does not have rights to the filesystem", ex); // Jes1111
                }
            }

            // TODO: Need country region store handling....
            /*
			//Esperantus settings
			//If not found in web.config defaults are provided here
			try
			{
				// Get Esperantus config
				NameValueCollection mySettings = (NameValueCollection) ConfigurationManager.GetSection("Esperantus");

				//Initializes default key and countries store
				if (mySettings != null) //Esperantus section must be specified anyway
				{
					string baseDir = "/";
					if (context.Request.ApplicationPath.Length > 0)
						baseDir = context.Request.ApplicationPath;
					baseDir = Server.MapPath(baseDir);
					string binDir = Path.Combine(baseDir, "bin");
                    string resDir = Path.Combine(baseDir, "App_GlobalResources");
                    // Try to get KeyStore, if not found set defaults
                    //string dllName = Assembly.GetExecutingAssembly().Location + Assembly.GetExecutingAssembly().FullName + ".dll";

					// Try to get KeyStore, if not found set defaults
					if (mySettings["KeysStore"] == null && mySettings["KeysStoreParameters"] == null)
					{
						string esperantusConfig = @"Path=" + resDir + ";FilesPrefix=Rainbow";
						//string esperantusConfig = @"AssemblyName=" + Path.Combine(binDir, "Rainbow.dll") + ";KeysSubStore=Rainbow.Resources.Rainbow;Path=" + resDir + ";FilesPrefix=Rainbow";
						DataFactory.SetDefaultKeysStore("StoreXMLResxFiles", esperantusConfig);
						//DataFactory.SetDefaultKeysStore("Hybrid", esperantusConfig);
					}
					
					// CountryRegionsStore as rainbow default
					if (mySettings["CountryRegionsStore"] == null && mySettings["CountryRegionsStoreParameters"] == null)
					{
						// DataFactory.SetDefaultCountryRegionsStore("Resources", @"AssemblyName=" + Path.Combine(binDir, "Rainbow.AppCode.dll") + ";CountriesSubStore=Resources.Rainbow.Countries.xml");
                        DataFactory.SetDefaultCountryRegionsStore("Resources", @"AssemblyName=" + binDir + @"\App_GlobalResources.dll;CountriesSubStore=App_GlobalResources.Resources.Countries.xml");
					}

					// ***********************************************
					// * Uncomment this code for overwrite Resources/Countries.xml
					// * with you custom database list, then rebuil reainbow
					// * and comment it again
					// ***********************************************
					//				if (mySettings["CountryRegionsStore"] == null && mySettings["CountryRegionsStoreParameters"] == null)
					//				{
					//					// We try to convert a SQLProvider string in a valid OleDb SQL connection string. It is not perfect.
					//					//jes1111 - string toBeTransformedConnectionString = ConfigurationSettings.AppSettings["connectionString"].ToLower();
					//					string toBeTransformedConnectionString = Config.ConnectionString.ToLower();
					//					toBeTransformedConnectionString = toBeTransformedConnectionString.Replace("trusted_connection=true", "Integrated Security=SSPI");
					//					toBeTransformedConnectionString = toBeTransformedConnectionString.Replace("database=", "Initial Catalog=");
					//					toBeTransformedConnectionString = toBeTransformedConnectionString.Replace("server=", "Data Source=");
					//					//Rainbow.Framework.Helpers.LogHelper.Logger.Log(Rainbow.Framework.Site.Configuration.LogLevel.Info, "Running Esperantus Countries from: " + toBeTransformedConnectionString);
					//					Esperantus.Data.DataFactory.SetDefaultCountryRegionsStore("OleDB", "Provider=SQLOLEDB.1;" + toBeTransformedConnectionString);
					//
					//					Esperantus.CountryInfo.SaveCountriesAsXML(Esperantus.CountryTypes.AllCountries, Esperantus.CountryFields.Name, Path.Combine(resDir, "Countries.xml"));
					//				}
				}
			}
			catch (Exception ex)
			{
				throw new RainbowException(Rainbow.Framework.LogLevel.Fatal, HttpStatusCode.ServiceUnavailable, "Error loading Esperantus settings in Application_Start", ex); // Jes1111
			}
             */

            //Start scheduler
            if (Config.SchedulerEnable)
            {
                PortalSettings.Scheduler = CachedScheduler.GetScheduler(
                    context.Server.MapPath(Framework.Settings.Path.ApplicationRoot),
                    Config.SqlConnectionString,
                    Config.SchedulerPeriod,
                    Config.SchedulerCacheSize);
                PortalSettings.Scheduler.Start();
            }

            // Start proxy
            if (Config.UseProxyServerForServerWebRequests)
            {
                WebRequest.DefaultWebProxy = PortalSettings.GetProxy();
            }
        }

        /// <summary>
        /// Application ends.
        /// </summary>
        public void Application_OnEnd()
        {
            ErrorHandler.Publish(LogLevel.Info, "Application Ended");
        }
    }
}