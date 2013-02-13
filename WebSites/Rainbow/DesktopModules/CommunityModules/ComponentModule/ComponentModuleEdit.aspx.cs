using System;
using System.Collections;
using System.Data.SqlClient;
using Rainbow.Framework;
using Rainbow.Framework.Content.Data;
using Rainbow.Framework.Site.Configuration;
using Rainbow.Framework.Web.UI;
using History=Rainbow.Framework.History;

namespace Rainbow.Content.Web.Modules
{
    /// <summary>
    /// 
    /// </summary>
    [History("jminond", "2006/2/23", "Converted to partial class")]
    public partial class ComponentModuleEdit : AddEditItemPage
    {
        /// <summary>
        /// The Page_Load event on this Page is used to obtain the ModuleID
        /// and ItemID of the event to edit.
        /// It then uses the Rainbow.ComponentModuleDB() data component
        /// to populate the page's edit controls with the control details.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                // Obtain a single row of event information
                ComponentModuleDB comp = new ComponentModuleDB();
                SqlDataReader dr = comp.GetComponentModule(ModuleID);

                try
                {
                    // Read first row from database
                    if (dr.Read())
                    {
                        TitleField.Text = (string) dr["Title"];
                        ComponentField.Text = (string) dr["Component"];
                        CreatedBy.Text = (string) dr["CreatedByUser"];
                        CreatedDate.Text = ((DateTime) dr["CreatedDate"]).ToShortDateString();
                        // 15/7/2004 added localization by Mario Endara mario@softworks.com.uy
                        if (CreatedBy.Text == "unknown" || CreatedBy.Text == string.Empty)
                            CreatedBy.Text = General.GetString("UNKNOWN", "unknown");
                    }
                }
                finally
                {
                    dr.Close();
                }
            }
        }

        /// <summary>
        /// Set the module guids with free access to this page
        /// </summary>
        /// <value>The allowed modules.</value>
        protected override ArrayList AllowedModules
        {
            get
            {
                ArrayList al = new ArrayList();
                al.Add("2B113F51-FEA3-499A-98E7-7B83C192FDBC");
                return al;
            }
        }

        /// <summary>
        /// The UpdateBtn_Click event handler on this Page is used to either
        /// create or update an event.  It uses the Rainbow.EventsDB()
        /// data component to encapsulate all data functionality.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void OnUpdate(EventArgs e)
        {
            base.OnUpdate(e);

            // Only Update if the Entered Data is Valid
            if (Page.IsValid == true)
            {
                // Create an instance of the Event DB component
                ComponentModuleDB comp = new ComponentModuleDB();

                comp.UpdateComponentModule(ModuleID, PortalSettings.CurrentUser.Identity.Email, TitleField.Text,
                                           ComponentField.Text);

                // Redirect back to the portal home page
                RedirectBackToReferringPage();
            }
        }

        #region Web Form Designer generated code

        /// <summary>
        /// Raises OnInitEvent
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            //Translate
            // Added EsperantusKeys for Localization 
            // Mario Endara mario@softworks.com.uy june-1-2004 
            RequiredTitle.ErrorMessage = General.GetString("ERROR_VALID_TITLE");
            RequiredComponent.ErrorMessage = General.GetString("ERROR_VALID_DESCRIPTION");

            this.Load += new EventHandler(this.Page_Load);
            this.updateButton.Click += new EventHandler(updateButton_Click);

            base.OnInit(e);
        }

        /// <summary>
        /// Handles the Click event of the updateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void updateButton_Click(object sender, EventArgs e)
        {
            OnUpdate(e);
        }

        #endregion
    }
}