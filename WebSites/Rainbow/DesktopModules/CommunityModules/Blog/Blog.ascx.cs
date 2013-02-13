using System;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using Rainbow.Framework;
using Rainbow.Framework.Content.Data;
using Rainbow.Framework.Data;
using Rainbow.Framework.DataTypes;
using Rainbow.Framework.Helpers;
using Rainbow.Framework.Web.UI.WebControls;
using History=Rainbow.Framework.History;

namespace Rainbow.Content.Web.Modules
{
    /// <summary>
    /// Author:					Joe Audette
    /// Created:				1/18/2004
    /// Last Modified:			2/8/2004
    /// </summary>
    [History("jminond", "march 2005", "Changes for moving Tab to Page")]
    [History("Hongwei Shen", "September 2005", "gouped the module specific settings")]
    public partial class Blog : PortalModuleControl
    {
        protected string Feedback;

        /// <summary>
        /// 
        /// </summary>
        protected bool ShowDate
        {
            get
            {
                // Hide/show date
                return bool.Parse(Settings["ShowDate"].ToString());
            }
        }

        /// <summary>
        /// The Page_Load event handler on this User Control is used to
        /// obtain a DataReader of Blog information from the Blogs
        /// table, and then databind the results to a templated DataList
        /// server control.  It uses the Rainbow.BlogDB()
        /// data component to encapsulate all data functionality.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Load(object sender, EventArgs e)
        {
            // Added EsperantusKeys for Localization 
            // Mario Endara mario@softworks.com.uy june-1-2004 
            Feedback = General.GetString("BLOG_FEEDBACK");

            if (!IsPostBack)
            {
                lnkRSS.HRef = HttpUrlBuilder.BuildUrl("~/DesktopModules/CommunityModules/Blog/RSS.aspx", PageID, "&mID=" + ModuleID);
                imgRSS.Src = HttpUrlBuilder.BuildUrl("~/DesktopModules/CommunityModules/Blog/xml.gif");
                lblCopyright.Text = Settings["Copyright"].ToString();
                // Obtain Blogs information from the Blogs table
                // and bind to the datalist control
                BlogDB blogData = new BlogDB();
                myDataList.DataSource = blogData.GetBlogs(ModuleID);
                myDataList.DataBind();

                dlArchive.DataSource = blogData.GetBlogMonthArchive(ModuleID);
                dlArchive.DataBind();

                SqlDataReader dr = blogData.GetBlogStats(ModuleID);
                try
                {
                    if (dr.Read())
                    {
                        lblEntryCount.Text = General.GetString("BLOG_ENTRIES", "Entries", null) +
                                             " (" + (string) dr["EntryCount"].ToString() + ")";
                        lblCommentCount.Text = General.GetString("BLOG_COMMENTS", "Comments", null) +
                                               " (" + (string) dr["CommentCount"].ToString() + ")";
                    }
                }
                finally
                {
                    // close the datareader
                    dr.Close();
                }
            }
        }

        public Blog()
        {
            // by Hongwei Shen(hongwei.shen@gmail.com) 9/12/2005
            SettingItemGroup group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
            int groupBase = (int) group;
            // end of modification

            // Set Editor Settings jviladiu@portalservices.net 2004/07/30
            // by Hongwei Shen
            // HtmlEditorDataType.HtmlEditorSettings (this._baseSettings, SettingItemGroup.MODULE_SPECIAL_SETTINGS);
            HtmlEditorDataType.HtmlEditorSettings(_baseSettings, group);

            //Number of entries to display
            SettingItem EntriesToShow = new SettingItem(new IntegerDataType());
            EntriesToShow.Value = "10";
            // by Hongwei Shen
            // EntriesToShow.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
            // EntriesToShow.Order = 10;
            EntriesToShow.Group = group;
            EntriesToShow.Order = groupBase + 20;
            // end of modification
            _baseSettings.Add("Entries To Show", EntriesToShow);

            //Channel Description
            SettingItem Description = new SettingItem(new StringDataType());
            Description.Value = "Description";
            // by Hongwei Shen
            // Description.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
            // Description.Order = 20;
            Description.Group = group;
            Description.Order = groupBase + 25;
            // end of modification
            _baseSettings.Add("Description", Description);

            //Channel Copyright
            SettingItem Copyright = new SettingItem(new StringDataType());
            Copyright.Value = "Copyright";
            // by Hongwei Shen
            // Copyright.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
            // Copyright.Order = 30;
            Copyright.Group = group;
            Copyright.Order = groupBase + 30;
            // end of modification
            _baseSettings.Add("Copyright", Copyright);

            //Channel Language
            SettingItem Language = new SettingItem(new StringDataType());
            Language.Value = "en-us";
            // by Hongwei Shen
            // Language.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
            // Language.Order = 40;
            Language.Group = group;
            Language.Order = groupBase + 40;
            // end of modification
            _baseSettings.Add("Language", Language);

            //Author
            SettingItem Author = new SettingItem(new StringDataType());
            Author.Value = "Author";
            // by Hongwei Shen
            // Author.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
            // Author.Order = 50;
            Author.Group = group;
            Author.Order = groupBase + 50;
            // end of modification
            _baseSettings.Add("Author", Author);

            //Author Email
            SettingItem AuthorEmail = new SettingItem(new StringDataType());
            AuthorEmail.Value = "author@portal.com";
            // by Hongwei Shen
            // AuthorEmail.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
            // AuthorEmail.Order = 60;
            AuthorEmail.Group = group;
            AuthorEmail.Order = groupBase + 60;
            // end of modification
            _baseSettings.Add("Author Email", AuthorEmail);

            //Time to live in minutes for RSS
            //how long a channel can be cached before refreshing from the source
            SettingItem TimeToLive = new SettingItem(new IntegerDataType());
            TimeToLive.Value = "120";
            // by Hongwei Shen
            // TimeToLive.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
            // TimeToLive.Order = 70;
            TimeToLive.Group = group;
            TimeToLive.Order = groupBase + 70;
            // end of modification
            _baseSettings.Add("RSS Cache Time In Minutes", TimeToLive);
        }

        #region General Implementation

        /// <summary>
        /// Guid
        /// </summary>
        public override Guid GuidID
        {
            get { return new Guid("{55EF407B-C9D6-47e3-B627-EFA6A5EEF4B2}"); }
        }

        #region Search Implementation

        /// <summary>
        /// Searchable module
        /// </summary>
        public override bool Searchable
        {
            get { return true; }
        }

        /// <summary>
        /// Searchable module implementation
        /// </summary>
        /// <param name="portalID">The portal ID</param>
        /// <param name="userID">ID of the user is searching</param>
        /// <param name="searchString">The text to search</param>
        /// <param name="searchField">The fields where perfoming the search</param>
        /// <returns>The SELECT sql to perform a search on the current module</returns>
        public override string SearchSqlSelect(int portalID, int userID, string searchString, string searchField)
        {
            SearchDefinition s =
                new SearchDefinition("rb_Blogs", "Title", "Excerpt", "CreatedByUser", "CreatedDate", searchField);

            //Add extra search fields here, this way
            s.ArrSearchFields.Add("itm.Description");

            return s.SearchSqlSelect(portalID, userID, searchString);
        }

        #endregion

        # region Install / Uninstall Implementation

        public override void Install(IDictionary stateSaver)
        {
            string currentScriptName = Path.Combine(Server.MapPath(TemplateSourceDirectory), "install.sql");
            ArrayList errors = DBHelper.ExecuteScript(currentScriptName, true);
            if (errors.Count > 0)
            {
                // Call rollback
                throw new Exception("Error occurred:" + errors[0].ToString());
            }
        }

        public override void Uninstall(IDictionary stateSaver)
        {
            string currentScriptName = Path.Combine(Server.MapPath(TemplateSourceDirectory), "uninstall.sql");
            ArrayList errors = DBHelper.ExecuteScript(currentScriptName, true);
            if (errors.Count > 0)
            {
                // Call rollback
                throw new Exception("Error occurred:" + errors[0].ToString());
            }
        }

        # endregion

        #endregion

        #region Web Form Designer generated code

        /// <summary>
        /// Raises Init event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();

            this.EnableViewState = false;

            this.AddText = "ADD_ARTICLE";
            this.AddUrl = "~/DesktopModules/CommunityModules/Blog/BlogEdit.aspx";

            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new EventHandler(this.Page_Load);
            if (!this.Page.IsCssFileRegistered("Mod_Blogs"))
                this.Page.RegisterCssFile("Mod_Blogs");
        }

        #endregion
    }
}