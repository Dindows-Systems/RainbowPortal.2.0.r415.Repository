using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using Rainbow.Framework.Settings;
using Rainbow.Framework.Settings.Cache;
using Rainbow.Framework;
using Rainbow.Framework.Web.UI.WebControls;

namespace Rainbow.Framework.Site.Configuration
{
	/// <summary>
	/// ModuleSettings Class encapsulates the detailed settings 
	/// for a specific Module in the Portal.
	/// </summary>
	[History("gman3001", "2004/10/06", "Added GetModuleDesktopSrc method to return the source path of a Module specified by its ID")]
	[History("Jes1111", "2003/04/24", "Added Cacheable property")]
	public class ModuleSettings
	{
		const string strAdmin = "Admin;";
		const string strDesktopSrc = "DesktopSrc";
		const string strATModuleID = "@ModuleID";

        #region Public Fields
        /// <summary>
		/// ModuleID
		/// </summary>
		public int ModuleID;
		/// <summary>
		/// ModuleDefID
		/// </summary>
		public int ModuleDefID;
		/// <summary>
		/// PageID
		/// </summary>
		public int PageID;
		/// <summary>
		/// CacheTime
		/// </summary>
		public int CacheTime;
		/// <summary>
		/// ModuleOrder
		/// </summary>
		public int ModuleOrder;
		/// <summary>
		/// PaneName
		/// </summary>
		public string PaneName;
		/// <summary>
		/// ModuleTitle
		/// </summary>
		public string ModuleTitle;
		/// <summary>
		/// AuthorizedEditRoles
		/// </summary>
		public string AuthorizedEditRoles;
		/// <summary>
		/// AuthorizedViewRoles
		/// </summary>
		public string AuthorizedViewRoles;
		/// <summary>
		/// AuthorizedAddRoles
		/// </summary>
		public string AuthorizedAddRoles;
		/// <summary>
		/// AuthorizedDeleteRoles
		/// </summary>
		public string AuthorizedDeleteRoles;
		/// <summary>
		/// AuthorizedPropertiesRoles
		/// </summary>
		public string AuthorizedPropertiesRoles;
		/// <summary>
		/// AuthorizedDeleteModuleRoles 
		/// </summary>
		public string AuthorizedDeleteModuleRoles; // Added by jviladiu@portalServices.net 19/8/2004
		/// <summary>
		/// AuthorizedMoveModuleRoles
		/// </summary>
		public string AuthorizedMoveModuleRoles; // Added by jviladiu@portalServices.net 19/8/2004
		/// <summary>
		/// AuthorizedPublishingRoles
		/// </summary>
		public string AuthorizedPublishingRoles; // Change by Geert.Audenaert@Syntegra.Com - Date: 6/2/2003
		/// <summary>
		/// AuthorizedApproveRoles
		/// </summary>
		public string AuthorizedApproveRoles; // Change by Geert.Audenaert@Syntegra.Com - Date: 27/2/2003
		/// <summary>
		/// WorkflowStatus
		/// </summary>
		public WorkflowState WorkflowStatus; // Change by Geert.Audenaert@Syntegra.Com - Date: 27/2/2003
		/// <summary>
		/// SupportWorkflow
		/// </summary>
		public bool SupportWorkflow; // Change by Geert.Audenaert@Syntegra.Com - Date: 6/2/2003
		/// <summary>
		/// ShowMobile
		/// </summary>
		public bool ShowMobile;
		/// <summary>
		/// ShowEveryWhere
		/// </summary>
		public bool ShowEveryWhere; // Change by john.mandia@whitelightsolutions.com - Date: 5/24/2003
		/// <summary>
		/// SupportCollapsable
		/// </summary>
		public bool SupportCollapsable; // Change by bja@reedtek.com - Date: 5/12/2003
		/// <summary>
		/// DesktopSrc
		/// </summary>
		public string DesktopSrc;
		/// <summary>
		/// MobileSrc
		/// </summary>
		public string MobileSrc;
		/// <summary>
		/// GuidID
		/// </summary>
		public Guid GuidID;
		/// <summary>
		/// Is Admin?
		/// </summary>
		public bool Admin;
		// Change 28/Feb/2003 - Jeremy Esland - Cache
		// used to store list of files for cache dependency -
		// optionally filled by module code
		// read by code in CachedPortalModuleControl
		/// <summary>
		/// String array of cache dependency files
		/// </summary>
		public ArrayList CacheDependency = new ArrayList();
		// Jes1111
		/// <summary>
		/// Is Cacheable?
		/// </summary>
		public bool Cacheable;
        #endregion

        /// <summary>
		/// ModuleSettings
		/// </summary>
		public ModuleSettings()
		{
			ModuleID = 0;
			PaneName = "no pane";
			ModuleTitle = string.Empty;
			AuthorizedEditRoles = strAdmin;
			AuthorizedViewRoles = "All Users;";
			AuthorizedAddRoles = strAdmin;
			AuthorizedDeleteRoles = strAdmin;
			AuthorizedPropertiesRoles = strAdmin;
			AuthorizedMoveModuleRoles = strAdmin;
			AuthorizedDeleteModuleRoles = strAdmin;
			CacheTime = 0;
			ModuleOrder = 0;
			ShowMobile = false;
			DesktopSrc = string.Empty;
			MobileSrc = string.Empty;
			SupportCollapsable = false;
			SupportWorkflow = false;
		}

		/// <summary>
		/// The GetModuleDesktopSrc Method returns a string of
		/// the specified Module's Desktop Src, which can be used to
		/// Load this modules control into a page.
		/// </summary>
        /// <remarks>
        /// Added by gman3001 10/06/2004, to allow for the retrieval of a Module's Desktop src path
        /// in order to dynamically load a module control by its Module ID
        /// </remarks>
		/// <param name="moduleID">The module ID.</param>
		/// <returns>The desktop source path string</returns>
		public static string GetModuleDesktopSrc(int moduleID)
		{
			string ControlPath = string.Empty;

			using (SqlDataReader dr = GetModuleDefinitionByID(moduleID))
			{
				if (dr.Read())
				{
					ControlPath = Path.WebPathCombine(Path.ApplicationRoot, dr[strDesktopSrc].ToString());
				}
			}
			return ControlPath;
		}

		/// <summary>
		/// The GetModuleSettings Method returns a hashtable of
		/// custom module specific settings from the database.  This method is
		/// used by some user control modules to access misc settings.
		/// </summary>
		/// <param name="moduleID">The module ID.</param>
		/// <param name="_baseSettings">The _base settings.</param>
		/// <returns></returns>
		public static Hashtable GetModuleSettings(int moduleID, Hashtable _baseSettings)
		{
			if (!CurrentCache.Exists(Key.ModuleSettings(moduleID)))
			{
				// Get Settings for this module from the database
				Hashtable _settings = new Hashtable();

				// Create Instance of Connection and Command Object
				using (SqlConnection myConnection = Config.SqlConnectionString)
				{
					using (SqlCommand myCommand = new SqlCommand("rb_GetModuleSettings", myConnection))
					{
						// Mark the Command as a SPROC
						myCommand.CommandType = CommandType.StoredProcedure;

						// Add Parameters to SPROC
						SqlParameter parameterModuleID = new SqlParameter(strATModuleID, SqlDbType.Int, 4);
						parameterModuleID.Value = moduleID;
						myCommand.Parameters.Add(parameterModuleID);

						// Execute the command
						myConnection.Open();
						using (SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
						{
							while (dr.Read())
							{
								_settings[dr["SettingName"].ToString()] = dr["SettingValue"].ToString();
							}
						}
					}
				}

				foreach (string key in _baseSettings.Keys)
				{
					if (_settings[key] != null)
					{
						SettingItem s = ((SettingItem)_baseSettings[key]);
						if (_settings[key].ToString().Length != 0)
							s.Value = _settings[key].ToString();
					}
				}

				CurrentCache.Insert(Key.ModuleSettings(moduleID), _baseSettings);
			}
			else
			{
				_baseSettings = (Hashtable)CurrentCache.Get(Key.ModuleSettings(moduleID));
			}
			return _baseSettings;
		}

		/// <summary>
		/// The GetModuleSettings Method returns a hashtable of
		/// custom module specific settings from the database. This method is
		/// used by some user control modules to access misc settings.
		/// </summary>
		/// <param name="moduleID">The module ID.</param>
		/// <param name="page">The page.</param>
		/// <returns></returns>
		public static Hashtable GetModuleSettings(int moduleID, Page page)
		{
			string ControlPath = Path.ApplicationRoot + "/";

			using (SqlDataReader dr = GetModuleDefinitionByID(moduleID))
			{
				if (dr.Read())
				{
					ControlPath += dr[strDesktopSrc].ToString();
				}
			}

			PortalModuleControl portalModule;
			Hashtable setting;
			try
			{
				portalModule = (PortalModuleControl)page.LoadControl(ControlPath);
				setting = GetModuleSettings(moduleID, portalModule.BaseSettings);
			}
			catch (Exception ex)
			{
				throw new Exception("There was a problem loading: '" + ControlPath + "'", ex); // Jes1111
				//Rainbow.Framework.Configuration.ErrorHandler.HandleException("There was a problem loading: '" + ControlPath + "'", ex);
				//throw;
			}
			return setting;
		}

		/// <summary>
		/// The GetModuleSettings Method returns a hashtable of
		/// custom module specific settings from the database. This method is
		/// used by some user control modules to access misc settings.
		/// </summary>
		/// <param name="moduleID">The module ID.</param>
		/// <returns></returns>
		public static Hashtable GetModuleSettings(int moduleID)
		{
			return (GetModuleSettings(moduleID, new Rainbow.Framework.Web.UI.Page()));
		}

		/// <summary>
		/// The UpdateModuleSetting Method updates a single module setting
		/// in the ModuleSettings database table.
		/// </summary>
		/// <param name="moduleID">The module ID.</param>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		public static void UpdateModuleSetting(int moduleID, string key, string value)
		{
			// Create Instance of Connection and Command Object
			using (SqlConnection myConnection = Config.SqlConnectionString)
			{
				using (SqlCommand myCommand = new SqlCommand("rb_UpdateModuleSetting", myConnection))
				{
					// Mark the Command as a SPROC
					myCommand.CommandType = CommandType.StoredProcedure;

					// Add Parameters to SPROC
					SqlParameter parameterModuleID = new SqlParameter(strATModuleID, SqlDbType.Int, 4);
					parameterModuleID.Value = moduleID;
					myCommand.Parameters.Add(parameterModuleID);

					SqlParameter parameterKey = new SqlParameter("@SettingName", SqlDbType.NVarChar, 50);
					parameterKey.Value = key;
					myCommand.Parameters.Add(parameterKey);

					SqlParameter parameterValue = new SqlParameter("@SettingValue", SqlDbType.NVarChar, 1500);
					parameterValue.Value = value;
					myCommand.Parameters.Add(parameterValue);

					myConnection.Open();
					myCommand.ExecuteNonQuery();
				}
			}
			//Invalidate cache
			CurrentCache.Remove(Key.ModuleSettings(moduleID));
		}

		/// <summary>
		/// GetModuleDefinitionByID
		/// </summary>
		/// <param name="ModuleID">ModuleID</param>
		/// <returns>A SqlDataReader</returns>
		public static SqlDataReader GetModuleDefinitionByID(int ModuleID)
		{
			// Create Instance of Connection and Command Object
			SqlConnection myConnection = Config.SqlConnectionString;
			SqlCommand myCommand = new SqlCommand("rb_GetModuleDefinitionByID", myConnection);

			// Mark the Command as a SPROC
			myCommand.CommandType = CommandType.StoredProcedure;

			// Add Parameters to SPROC
			SqlParameter parameterModuleID = new SqlParameter(strATModuleID, SqlDbType.Int);
			parameterModuleID.Value = ModuleID;
			myCommand.Parameters.Add(parameterModuleID);

			// Execute the command
			myConnection.Open();
			SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

			// Return the datareader
			return result;
		}
	}
}