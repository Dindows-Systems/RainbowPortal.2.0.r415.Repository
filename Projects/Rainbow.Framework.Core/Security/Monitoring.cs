using System.Data;
using System.Data.SqlClient;
using System.Web;
using Rainbow.Framework.Settings;
using System;

namespace Rainbow.Framework.Security
{
    /// <summary>
    /// Monitoring class is called by the rainbow components to write an entry
    /// into the monitoring database table.  It is used to maintain and show
    /// site statistics such as who has logged on and at what time.
    /// Written by Paul Yarrow, paul@paulyarrow.com
    /// </summary>
    public class Monitoring
    {
        /// <summary>
        /// Logs the entry.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="portalID">The portal ID.</param>
        /// <param name="pageID">The page ID.</param>
        /// <param name="actionType">Type of the action.</param>
        /// <param name="userField">The user field.</param>
        public static void LogEntry(Guid userID, int portalID, long pageID, string actionType, string userField)
        {
            // note by manu: This exception is already managed at higher level
            // a nested try catch slows down with no real use
            //return;

            // if a tab id of 0 is received, this is the home page
            // so change the number to 1
            // A page ID of -1 is sent when logging in and out
            if (pageID == 0) pageID = 1;

            // Create Instance of Connection and Command Object
            using (SqlConnection myConnection = Config.SqlConnectionString)
            {
                using (SqlCommand myCommand = new SqlCommand("rb_AddMonitoringEntry", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;

                    SqlParameter parameterUsername = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier);
                    parameterUsername.Value = userID;
                    myCommand.Parameters.Add(parameterUsername);

                    SqlParameter parameterPortalID = new SqlParameter("@PortalID", SqlDbType.Int, 4);
                    parameterPortalID.Value = portalID;
                    myCommand.Parameters.Add(parameterPortalID);

                    SqlParameter parameterPageID = new SqlParameter("@PageID", SqlDbType.Int, 4);
                    parameterPageID.Value = pageID;
                    myCommand.Parameters.Add(parameterPageID);

                    SqlParameter parameterActionType = new SqlParameter("@ActivityType", SqlDbType.NVarChar, 50);
                    parameterActionType.Value = actionType;
                    myCommand.Parameters.Add(parameterActionType);

                    SqlParameter parameterUserField = new SqlParameter("@UserField", SqlDbType.NVarChar, 500);
                    parameterUserField.Value = userField;
                    myCommand.Parameters.Add(parameterUserField);

                    // Create the web parameters and set them to defaults.
                    // If we are in the context of a web request then
                    // record the extra information we can get
                    SqlParameter parameterUrlReferrer = new SqlParameter("@Referrer", SqlDbType.NVarChar, 255);
                    parameterUrlReferrer.Value = string.Empty;
                    myCommand.Parameters.Add(parameterUrlReferrer);

                    SqlParameter parameterUserAgent = new SqlParameter("@UserAgent", SqlDbType.NVarChar, 100);
                    parameterUserAgent.Value = string.Empty;
                    myCommand.Parameters.Add(parameterUserAgent);

                    SqlParameter parameterUserHostAddress = new SqlParameter("@UserHostAddress", SqlDbType.NVarChar, 15);
                    parameterUserHostAddress.Value = string.Empty;
                    myCommand.Parameters.Add(parameterUserHostAddress);

                    SqlParameter parameterBrowserType = new SqlParameter("@BrowserType", SqlDbType.NVarChar, 100);
                    parameterBrowserType.Value = string.Empty;
                    myCommand.Parameters.Add(parameterBrowserType);

                    SqlParameter parameterBrowserName = new SqlParameter("@BrowserName", SqlDbType.NVarChar, 100);
                    parameterBrowserName.Value = string.Empty;
                    myCommand.Parameters.Add(parameterBrowserName);

                    SqlParameter parameterBrowserVersion = new SqlParameter("@BrowserVersion", SqlDbType.NVarChar, 100);
                    parameterBrowserVersion.Value = string.Empty;
                    myCommand.Parameters.Add(parameterBrowserVersion);

                    SqlParameter parameterBrowserPlatform =
                        new SqlParameter("@BrowserPlatform", SqlDbType.NVarChar, 100);
                    parameterBrowserPlatform.Value = string.Empty;
                    myCommand.Parameters.Add(parameterBrowserPlatform);

                    SqlParameter parameterBrowserIsAOL = new SqlParameter("@BrowserIsAOL", SqlDbType.Bit, 1);
                    parameterBrowserIsAOL.Value = false;
                    myCommand.Parameters.Add(parameterBrowserIsAOL);


                    // Add the browser info if we have access
                    if (HttpContext.Current != null && HttpContext.Current.Request != null)
                    {
                        if (HttpContext.Current.Request.UrlReferrer != null)
                        {
                            parameterUrlReferrer.Value = HttpContext.Current.Request.UrlReferrer.ToString();
                        }
                        // 09_09_2003 Cory Isakson
                        // Some browsers are not sending a UserAgent header
                        if (HttpContext.Current.Request.UserAgent != null)
                        {
                            parameterUserAgent.Value = HttpContext.Current.Request.UserAgent;
                        }

                        parameterUserHostAddress.Value = HttpContext.Current.Request.UserHostAddress;
                        parameterBrowserType.Value = HttpContext.Current.Request.Browser.Type;
                        parameterBrowserName.Value = HttpContext.Current.Request.Browser.Browser;
                        parameterBrowserVersion.Value = HttpContext.Current.Request.Browser.Version;
                        parameterBrowserPlatform.Value = HttpContext.Current.Request.Browser.Platform;
                        parameterBrowserIsAOL.Value = HttpContext.Current.Request.Browser.AOL;
                    }

                    // Open the database connection and execute SQL Command
                    myConnection.Open();
                    try
                    {
                        myCommand.ExecuteNonQuery();
                    }
                    finally
                    {
                        myConnection.Close();
                    }
                }
            }
        }
    }
}