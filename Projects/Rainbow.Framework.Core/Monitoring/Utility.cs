using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Rainbow.Framework.Data;
using Rainbow.Framework.Settings;
using System.Web.Security;

namespace Rainbow.Framework.Monitoring
{
    /// <summary>
    /// strings used for actions
    /// </summary>
    public static class MonitoringAction
    {
        /// <summary>
        /// Gets the page request.
        /// </summary>
        /// <value>The page request.</value>
        static public string PageRequest
        {
            get { return "PageRequest"; }
        }
    }

    /// <summary>
    /// Utility Helper class for Rainbow Framework Monitoring purposes.
    /// You get some static methods like
    /// <list type="string">
    /// <item>int GetTotalPortalHits(int portalID)</item>
    /// <item>DataSet GetMonitoringStats(DateTime startDate</item>
    /// </list>
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// returns the total hit count for a portal
        /// </summary>
        /// <param name="portalID">portal id to get stats for</param>
        /// <returns>
        /// total number of hits to the portal of all types
        /// </returns>
        public static int GetTotalPortalHits(int portalID)
        {
            // TODO: THis should not display, login, logout, or administrator hits.
            string sql = "Select count(ID) as hits " +
                         " from rb_monitoring " +
                         " where [PortalID] = " + portalID.ToString() + " ";

            return Convert.ToInt32(DBHelper.ExecuteSQLScalar(sql));
        }

        /// <summary>
        /// Get Users Online
        /// Add to the Cache
        /// HttpContext.Current.Cache.Insert("WhoIsOnlineAnonUserCount", anonUserCount, null, DateTime.Now.AddMinutes(cacheTimeout), TimeSpan.Zero);
        /// HttpContext.Current.Cache.Insert("WhoIsOnlineRegUserCount", regUsersOnlineCount, null, DateTime.Now.AddMinutes(cacheTimeout), TimeSpan.Zero);
        /// HttpContext.Current.Cache.Insert("WhoIsOnlineRegUsersString", regUsersString, null, DateTime.Now.AddMinutes(cacheTimeout), TimeSpan.Zero);
        /// </summary>
        /// <param name="portalID">The portal ID.</param>
        /// <param name="minutesToCheckForUsers">The minutes to check for users.</param>
        /// <param name="cacheTimeout">The cache timeout.</param>
        /// <param name="anonUserCount">The anon user count.</param>
        /// <param name="regUsersOnlineCount">The reg users online count.</param>
        /// <param name="regUsersString">The reg users string.</param>
        public static void FillUsersOnlineCache(int portalID,
                                                int minutesToCheckForUsers,
                                                int cacheTimeout,
                                                out int anonUserCount,
                                                out int regUsersOnlineCount,
                                                out string regUsersString)
        {

            
            // Read from the cache if available
            if (HttpContext.Current.Cache["WhoIsOnlineAnonUserCount"] == null ||
                HttpContext.Current.Cache["WhoIsOnlineRegUserCount"] == null ||
                HttpContext.Current.Cache["WhoIsOnlineRegUsersString"] == null)
            {
                // Firstly get the logged in users
                SqlCommand sqlComm1 = new SqlCommand();
                SqlConnection sqlConn1 = new SqlConnection(Config.ConnectionString);
                sqlComm1.Connection = sqlConn1;
                sqlComm1.CommandType = CommandType.StoredProcedure;
                sqlComm1.CommandText = "rb_GetLoggedOnUsers";
                SqlDataReader result;

                // Add Parameters to SPROC
                SqlParameter parameterPortalID = new SqlParameter("@PortalID", SqlDbType.Int, 4);
                parameterPortalID.Value = portalID;
                sqlComm1.Parameters.Add(parameterPortalID);

                SqlParameter parameterMinutesToCheck = new SqlParameter("@MinutesToCheck", SqlDbType.Int, 4);
                parameterMinutesToCheck.Value = minutesToCheckForUsers;
                sqlComm1.Parameters.Add(parameterMinutesToCheck);

                sqlConn1.Open();
                result = sqlComm1.ExecuteReader();

                string onlineUsers = string.Empty;
                int onlineUsersCount = 0;
                try
                {
                    while (result.Read())
                    {
                        if (Convert.ToString(result.GetValue(2)) != "Logoff")
                        {
                            onlineUsersCount++;
                            onlineUsers += Membership.GetUser( result.GetValue(1) ).UserName + ", ";
                        }
                    }
                }
                finally
                {
                    result.Close(); //by Manu, fixed bug 807858
                }

                if (onlineUsers.Length > 0)
                {
                    onlineUsers = onlineUsers.Remove(onlineUsers.Length - 2, 2);
                }

                regUsersString = onlineUsers;
                regUsersOnlineCount = onlineUsersCount;

                result.Close();


                // Add Parameters to SPROC
                SqlParameter parameterNumberOfUsers = new SqlParameter("@NoOfUsers", SqlDbType.Int, 4);
                parameterNumberOfUsers.Direction = ParameterDirection.Output;
                sqlComm1.Parameters.Add(parameterNumberOfUsers);

                // Re-use the same result set to get the no of unregistered users
                sqlComm1.CommandText = "rb_GetNumberOfActiveUsers";

                // [The Bitland Prince] 8-1-2005
                // If this query generates an exception, connection might be left open
                try
                {
                    sqlComm1.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    // This takes care to close connection then throws a new
                    // exception (because I don't know if it's safe to go on...)
                    sqlConn1.Close();
                    throw new Exception("Unable to retrieve logged users. Error : " + Ex.Message);
                }

                int allUsersCount = Convert.ToInt32(parameterNumberOfUsers.Value);

                sqlConn1.Close();

                anonUserCount = allUsersCount - onlineUsersCount;
                if (anonUserCount < 0)
                {
                    anonUserCount = 0;
                }

                // Add to the Cache
                HttpContext.Current.Cache.Insert("WhoIsOnlineAnonUserCount", anonUserCount, null,
                                                 DateTime.Now.AddMinutes(cacheTimeout), TimeSpan.Zero);
                HttpContext.Current.Cache.Insert("WhoIsOnlineRegUserCount", regUsersOnlineCount, null,
                                                 DateTime.Now.AddMinutes(cacheTimeout), TimeSpan.Zero);
                HttpContext.Current.Cache.Insert("WhoIsOnlineRegUsersString", regUsersString, null,
                                                 DateTime.Now.AddMinutes(cacheTimeout), TimeSpan.Zero);
            }
            else
            {
                anonUserCount = (int) HttpContext.Current.Cache["WhoIsOnlineAnonUserCount"];
                regUsersOnlineCount = (int) HttpContext.Current.Cache["WhoIsOnlineRegUserCount"];
                regUsersString = (string) HttpContext.Current.Cache["WhoIsOnlineRegUsersString"];
            }
        }

        /// <summary>
        /// Return a dataset of stats for a given data range and portal
        /// Written by Paul Yarrow, paul@paulyarrow.com
        /// </summary>
        /// <param name="startDate">the first date you want to see stats from</param>
        /// <param name="endDate">the last date you want to see stats up to</param>
        /// <param name="reportType">type of report you are requesting</param>
        /// <param name="currentTabID">page id you are requesting stats for</param>
        /// <param name="includeMonitoringPage">include the monitoring page in the stats</param>
        /// <param name="includeAdminUser">include hits by admin users</param>
        /// <param name="includePageRequests">include page hits</param>
        /// <param name="includeLogon">include the logon page</param>
        /// <param name="includeLogoff">inlcude logogg page</param>
        /// <param name="includeMyIPAddress">include the current ip address</param>
        /// <param name="portalID">portal id to get stats for</param>
        /// <returns></returns>
        public static DataSet GetMonitoringStats(DateTime startDate,
                                                 DateTime endDate,
                                                 string reportType,
                                                 long currentTabID,
                                                 bool includeMonitoringPage,
                                                 bool includeAdminUser,
                                                 bool includePageRequests,
                                                 bool includeLogon,
                                                 bool includeLogoff,
                                                 bool includeMyIPAddress,
                                                 int portalID)
        {
            endDate = endDate.AddDays(1);

            // Firstly get the logged in users
            SqlConnection myConnection = Config.SqlConnectionString;
            SqlDataAdapter myCommand = new SqlDataAdapter("rb_GetMonitoringEntries", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterPortalID = new SqlParameter("@PortalID", SqlDbType.Int, 4);
            parameterPortalID.Value = portalID;
            myCommand.SelectCommand.Parameters.Add(parameterPortalID);

            SqlParameter parameterStartDate = new SqlParameter("@StartDate", SqlDbType.DateTime, 8);
            parameterStartDate.Value = startDate;
            myCommand.SelectCommand.Parameters.Add(parameterStartDate);

            SqlParameter parameterEndDate = new SqlParameter("@EndDate", SqlDbType.DateTime, 8);
            parameterEndDate.Value = endDate;
            myCommand.SelectCommand.Parameters.Add(parameterEndDate);

            SqlParameter parameterReportType = new SqlParameter("@ReportType", SqlDbType.VarChar, 50);
            parameterReportType.Value = reportType;
            myCommand.SelectCommand.Parameters.Add(parameterReportType);

            SqlParameter parameterCurrentTabID = new SqlParameter("@CurrentTabID", SqlDbType.BigInt, 8);
            parameterCurrentTabID.Value = currentTabID;
            myCommand.SelectCommand.Parameters.Add(parameterCurrentTabID);

            SqlParameter parameterIncludeMoni = new SqlParameter("@IncludeMonitorPage", SqlDbType.Bit, 1);
            parameterIncludeMoni.Value = includeMonitoringPage;
            myCommand.SelectCommand.Parameters.Add(parameterIncludeMoni);

            SqlParameter parameterIncludeAdmin = new SqlParameter("@IncludeAdminUser", SqlDbType.Bit, 1);
            parameterIncludeAdmin.Value = includeAdminUser;
            myCommand.SelectCommand.Parameters.Add(parameterIncludeAdmin);

            SqlParameter parameterIncludePageRequests = new SqlParameter("@IncludePageRequests", SqlDbType.Bit, 1);
            parameterIncludePageRequests.Value = includePageRequests;
            myCommand.SelectCommand.Parameters.Add(parameterIncludePageRequests);

            SqlParameter parameterIncludeLogon = new SqlParameter("@IncludeLogon", SqlDbType.Bit, 1);
            parameterIncludeLogon.Value = includeLogon;
            myCommand.SelectCommand.Parameters.Add(parameterIncludeLogon);

            SqlParameter parameterIncludeLogoff = new SqlParameter("@IncludeLogoff", SqlDbType.Bit, 1);
            parameterIncludeLogoff.Value = includeLogoff;
            myCommand.SelectCommand.Parameters.Add(parameterIncludeLogoff);

            SqlParameter parameterIncludeIPAddress = new SqlParameter("@IncludeIPAddress", SqlDbType.Bit, 1);
            parameterIncludeIPAddress.Value = includeMyIPAddress;
            myCommand.SelectCommand.Parameters.Add(parameterIncludeIPAddress);

            SqlParameter parameterIPAddress = new SqlParameter("@IPAddress", SqlDbType.VarChar, 16);
            parameterIPAddress.Value = HttpContext.Current.Request.UserHostAddress;
            myCommand.SelectCommand.Parameters.Add(parameterIPAddress);

            // Create and Fill the DataSet
            DataSet myDataSet = new DataSet();
            try
            {
                myCommand.Fill(myDataSet);
            }
            finally
            {
                myConnection.Close();
            }

            // Return the DataSet
            return myDataSet;
        }
    }
}