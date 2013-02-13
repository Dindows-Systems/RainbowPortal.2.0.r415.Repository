using System;
using System.Data;
using System.Web.UI.WebControls;
using Rainbow.Framework;
using Rainbow.Framework.Site.Configuration;
using Rainbow.Framework.Site.Data;
using Rainbow.Framework.DataTypes;
using Rainbow.Framework.Web.UI.WebControls;
using com.google.api;
//using Rainbow.DesktopModules.GoogleSearchWebservice.com.google.api;

namespace Rainbow.DesktopModules
{
	/// <summary>
	/// GoogleSearch module
	/// Written by: James Melvin, james@commercechain.co.za,
	/// http://www.commercechain.co.za
	/// Moved into Rainbow by Jakob Hansen, hansen3000@hotmail.com
    /// Moved into Rainbow 2.o by Mike Stone, namoguy@msn.com
	/// </summary>
	[History("Jakob","2003/04/30","Added !Cacheable property")]
	public partial class GoogleSearch : PortalModuleControl
	{
	
		#region Declarations
		/// <summary>
		/// 
		/// </summary>
		protected int maxResults;
		/// <summary>
		/// 
		/// </summary>
		protected string licKey, restrictToThisDomain;
		/// <summary>
		/// 
		/// </summary>
		protected bool showSnippet, showSummary, showURL, searchThisSiteOnly;
		/// <summary>
		/// 
		/// </summary>
		protected string Target;
		#endregion


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
		private void Page_Load(object sender, EventArgs e)
		{
			maxResults = int.Parse(Settings["MaxResults"].ToString());
			licKey = Settings["LicKey"].ToString();
			restrictToThisDomain = Settings["GOOGLESEARCH_RESTRICT_DOMAIN"].ToString();
			showSnippet = bool.Parse(Settings["ShowSnippet"].ToString());
			showSummary = bool.Parse(Settings["ShowSummary"].ToString());
			searchThisSiteOnly = bool.Parse(Settings["GOOGLESEARCH_SEARCH_THIS_SITE_ONLY"].ToString());
			showURL = bool.Parse(Settings["ShowURL"].ToString());
			Target = "_" + Settings["Target"].ToString();

			// Jakob Hansen
			if (this.Cacheable)
			{
				base.Cacheable = true;
				this.ModuleConfiguration.Cacheable = true;
			}
			else
			{
				base.Cacheable = false;
				this.ModuleConfiguration.Cacheable = false;
			}
		}


        /// <summary>
        /// Handles the Click event of the Search control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
		private void Search_Click(object sender, EventArgs e)
		{
			string searchText = txtSearchString.Text;
			GoogleSearchService s = new GoogleSearchService();
 
			/*
			s.doGoogleSearch(string license,string query, int start, int maxResults,
			bool filter,string restrict,bool safeSearch, string lr, string ie, string oe) 
			*/

			try 
			{
				//Overrides following
				if (restrictToThisDomain.Length != 0 && searchText.IndexOf("site:") == -1)
					searchText = searchText += " site:" + restrictToThisDomain.Trim();

				if (searchThisSiteOnly && searchText.IndexOf("site:") == -1)
					searchText = searchText += " site:" + (Request.Url.Host);

				//debug only
				//txtSearchString.Text = searchText;

				int start = (Convert.ToInt32(TextBox2.Text)-1) * 10;

				GoogleSearchResult r = s.doGoogleSearch(licKey, searchText, start, maxResults, false, string.Empty, false, string.Empty, string.Empty, string.Empty);

				// Extract the estimated number of results for the search and display it
			    int estResults = r.estimatedTotalResultsCount;

				lblHits.Text = Convert.ToString(estResults) + " Results found";

				DataSet ds1 = new DataSet();
				DataSet ds =FillGoogleDS(ds1,r);
				DataGrid1.DataSource=ds;
				DataGrid1.DataBind();
			}
			catch (Exception ex)
			{
				lblHits.Text = ex.Message;
				return;
			}

			return;
		}


        /// <summary>
        /// Creates the google DS.
        /// </summary>
        /// <param name="ds">The ds.</param>
        /// <returns></returns>
		private DataSet CreateGoogleDS(DataSet ds)
		{
			ds.Tables.Add("GoogleSearch");
			ds.Tables[0].Columns.Add("Title");
			if (showSnippet)
				ds.Tables[0].Columns.Add("Snippet");
			if (showSummary)
				ds.Tables[0].Columns.Add("Summary");
			if (showURL)
				ds.Tables[0].Columns.Add("URL");
			return ds;
		}


        /// <summary>
        /// Fills the google DS.
        /// </summary>
        /// <param name="ds">The ds.</param>
        /// <param name="srchResult">The SRCH result.</param>
        /// <returns></returns>
		private DataSet FillGoogleDS(DataSet ds, GoogleSearchResult srchResult)
		{
			try
			{
				ds = CreateGoogleDS(ds);
				int i =0;
				DataRow dr;
				string strURL=null;

				for (i = 0; i<srchResult.resultElements.Length; i++)
				{
					dr = ds.Tables["GoogleSearch"].NewRow();
					strURL=srchResult.resultElements[i].URL.ToString();
					dr["Title"] = "<a href=" + strURL + " Target='" + Target + "' >"+srchResult.resultElements[i].title.ToString()+"</a>";
					if (showSnippet)
						dr["Snippet"] = srchResult.resultElements[i].snippet.ToString();
					if (showSummary)
						dr["Summary"] = srchResult.resultElements[i].summary.ToString();
					if (showURL)
						dr["URL"] = "<a href=" + strURL + " Target='" + Target + "' >"+strURL+"</a>";
					ds.Tables["GoogleSearch"].Rows.Add(dr);
				}
			}
			catch(Exception e)
			{
				lblHits.Text=e.Message;
				return null;
			}
			return ds;
		}

        /// <summary>
        /// Constructor - set the module settings
        /// <list type="">
        /// 		<item>MaxResults</item>
        /// 		<item>licKey</item>
        /// 		<item>showSnippet</item>
        /// 		<item>showSummary</item>
        /// 		<item>showURL</item>
        /// 		<item>restrictToThisDomain</item>
        /// 		<item>searchThisSiteOnly</item>
        /// 		<item>setTarget</item>
        /// 	</list>
        /// </summary>
		public GoogleSearch() 
		{
			// modified by Hongwei Shen
			SettingItemGroup group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			int groupBase  = (int)group;

			SettingItem maxResults = new SettingItem(new IntegerDataType());
			maxResults.Required = true;
			maxResults.Group = group;
			maxResults.Order = groupBase + 20; //1;
			maxResults.Value = "10";
			maxResults.MinValue = 1;
			maxResults.MaxValue = 1000;
			this._baseSettings.Add("MaxResults", maxResults);

			SettingItem licKey = new SettingItem(new StringDataType());
			licKey.Required = true;
			licKey.Value = "Ffjju8cu0FDY3UMmGWxwgV5bfpsBFCJP";
			licKey.EnglishName = "Licence Key";
			licKey.Group = group;
			licKey.Order = groupBase + 25; //2;
			this._baseSettings.Add("LicKey", licKey);

			SettingItem showSnippet = new SettingItem(new BooleanDataType());
			showSnippet.Group = group;
			showSnippet.Order = groupBase + 30; //3;
			showSnippet.Value = "true";
			this._baseSettings.Add("ShowSnippet", showSnippet);

			SettingItem showSummary = new SettingItem(new BooleanDataType());
			showSummary.Group = group;
			showSummary.Order = groupBase + 35; //4;
			showSummary.Value = "true";
			this._baseSettings.Add("ShowSummary", showSummary);

			SettingItem showURL = new SettingItem(new BooleanDataType());
			showURL.Group = group;
			showURL.Order = groupBase + 40; //5;
			showURL.EnglishName = "Show URL";
			showURL.Value = "false";
			this._baseSettings.Add("ShowURL", showURL);
			
			SettingItem restrictToThisDomain = new SettingItem(new StringDataType());
			restrictToThisDomain.Value = "";
			restrictToThisDomain.EnglishName = "Restrict to this domain.";
			restrictToThisDomain.Description = "Set this field to restrict the search to a specific domain. This overrides 'Search this site only'";
			restrictToThisDomain.Group = group;
			restrictToThisDomain.Order = groupBase + 45; //6;
			this._baseSettings.Add("GOOGLESEARCH_RESTRICT_DOMAIN", restrictToThisDomain);

			SettingItem searchThisSiteOnly = new SettingItem(new BooleanDataType());
			searchThisSiteOnly.Group = group;
			searchThisSiteOnly.Order = groupBase + 50; //0;
			searchThisSiteOnly.EnglishName = "Search this site only";
			searchThisSiteOnly.Description = "Search this site only. For work be sure restrict to domain option is blank.";
			searchThisSiteOnly.Value = "true";
			this._baseSettings.Add("GOOGLESEARCH_SEARCH_THIS_SITE_ONLY", searchThisSiteOnly);

			SettingItem setTarget = new SettingItem(new ListDataType("blank;parent;self;top"));
			setTarget.Required = true;
			setTarget.Group = group;
			setTarget.Order = groupBase + 55; //10;
			setTarget.Value = "blank";
			this._baseSettings.Add("Target", setTarget);

		}

		
		// Jakob Hansen
        /// <summary>
        /// Overrides ModuleSetting to render this module type un-cacheable
        /// </summary>
        /// <value><c>true</c> if cacheable; otherwise, <c>false</c>.</value>
		public override bool Cacheable
		{
			get
			{
				return false;
			}
		}


        /// <summary>
        /// General Module Def GUID
        /// </summary>
        /// <value>The GUID ID.</value>
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{2502DB18-B580-4F90-8CB4-C15E6E531008}");
			}
		}
		

		#region Web Form Designer generated code
        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
		override protected void OnInit(EventArgs e)
		{
            this.Search.Click += new EventHandler(this.Search_Click);
            this.Load += new EventHandler(this.Page_Load);
//			ModuleTitle = new DesktopModuleTitle();
//			Controls.AddAt(0, ModuleTitle);
			base.OnInit(e);
		}
		#endregion
	}
}
