using System;
using System.Collections;
using System.Data.SqlClient;
using System.Threading;
using System.Web.UI.WebControls;
using Rainbow.Framework;
using Rainbow.Framework.Helpers;
using Rainbow.Framework.Settings;
using Rainbow.Framework.Site.Data;
using Rainbow.Framework.Web.UI;

namespace Rainbow.AdminAll
{
    /// <summary>
    /// Add/Remove modules, assign modules to portals
    /// </summary>
    public partial class ModuleDefinitions : EditItemPage
    {
        private Guid defID;

        /// <summary>
        /// The Page_Load server event handler on this page is used
        /// to populate the role information for the page
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Page_Load(object sender, EventArgs e)
        {
            // Verify that the current user has access to access this page
            // Removed by Mario Endara <mario@softworks.com.uy> (2004/11/04)
//			if (PortalSecurity.IsInRoles("Admins") == false) 
//				PortalSecurity.AccessDeniedEdit();

            // Calculate security defID
            if (Request.Params["defID"] != null)
                defID = new Guid(Request.Params["defID"]);

            ModulesDB modules = new ModulesDB();

            // If this is the first visit to the page, bind the definition data 
            if (Page.IsPostBack == false)
            {
                if (defID.Equals(Guid.Empty))
                {
                    ShowInstaller(true);
                    // new module definition
                    InstallerFileName.Text = "DesktopModules/MyModuleFolder/install.xml";
                    FriendlyName.Text = "New Definition";
                    DesktopSrc.Text = "DesktopModules/MyModule.ascx";
                    MobileSrc.Text = "MobileModules/MyModule.ascx";
                }
                else
                {
                    ShowInstaller(false);
                    // Obtain the module definition to edit from the database
                    SqlDataReader dr = modules.GetSingleModuleDefinition(defID);

                    // Read in first row from database
                    while (dr.Read())
                    {
                        FriendlyName.Text = dr["FriendlyName"].ToString();
                        DesktopSrc.Text = dr["DesktopSrc"].ToString();
                        MobileSrc.Text = dr["MobileSrc"].ToString();
                        lblGUID.Text = dr["GeneralModDefID"].ToString();
                    }
                    dr.Close(); //by Manu, fixed bug 807858
                }

                // Populate checkbox list with all portals
                // and "check" the ones already configured for this tab
                SqlDataReader portals = modules.GetModuleInUse(defID);

                // Clear existing items in checkboxlist
                PortalsName.Items.Clear();

                while (portals.Read())
                {
                    if (Convert.ToInt32(portals["PortalID"]) >= 0)
                    {
                        ListItem item = new ListItem();
                        item.Text = (string) portals["PortalName"];
                        item.Value = portals["PortalID"].ToString();

                        if ((portals["checked"].ToString()) == "1")
                            item.Selected = true;
                        else
                            item.Selected = false;

                        PortalsName.Items.Add(item);
                    }
                }
                portals.Close(); //by Manu, fixed bug 807858
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
                al.Add("D04BB5EA-A792-4E87-BFC7-7D0ED3ADD582");
                return al;
            }
        }

        /// <summary>
        /// OnUpdate installs or refresh module definiton on db
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void OnUpdate(EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    if (!btnUseInstaller.Visible)
                        ModuleInstall.InstallGroup(Server.MapPath(Path.ApplicationRoot + "/" + InstallerFileName.Text),
                                                   lblGUID.Text == string.Empty);
                    else
                        ModuleInstall.Install(FriendlyName.Text, DesktopSrc.Text, MobileSrc.Text,
                                              lblGUID.Text == string.Empty);

                    ModulesDB modules = new ModulesDB();

                    // Update the module definition
                    for (int i = 0; i < PortalsName.Items.Count; i++)
                    {
                        modules.UpdateModuleDefinitions(defID, Convert.ToInt32(PortalsName.Items[i].Value),
                                                        PortalsName.Items[i].Selected);
                    }

                    // Redirect back to the portal admin page
                    RedirectBackToReferringPage();
                }
                catch (ThreadAbortException)
                {
                    //normal with redirect 
                }
                catch (Exception ex)
                {
                    lblErrorDetail.Text =
                        General.GetString("MODULE_DEFINITIONS_INSTALLING", "An error occurred installing.", this) +
                        "<br>";
                    lblErrorDetail.Text += ex.Message + "<br>";
                    if (!btnUseInstaller.Visible)
                        lblErrorDetail.Text += " Installer: " +
                                               Server.MapPath(Path.ApplicationRoot + "/" + InstallerFileName.Text);
                    else
                        lblErrorDetail.Text += " Module: '" + FriendlyName.Text + "' - Source: '" + DesktopSrc.Text +
                                               "' - Mobile: '" + MobileSrc.Text + "'";
                    lblErrorDetail.Visible = true;

                    ErrorHandler.Publish(LogLevel.Error, lblErrorDetail.Text, ex);
                }
            }
        }


        /// <summary>
        /// Delete a Module definition
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void OnDelete(EventArgs e)
        {
            try
            {
                if (!btnUseInstaller.Visible)
                    ModuleInstall.UninstallGroup(Server.MapPath(Path.ApplicationRoot + "/" + InstallerFileName.Text));
                else
                    ModuleInstall.Uninstall(DesktopSrc.Text, MobileSrc.Text);

                // Redirect back to the portal admin page
                RedirectBackToReferringPage();
            }
            catch (ThreadAbortException)
            {
                //normal with redirect 
            }
            catch (Exception ex)
            {
                lblErrorDetail.Text =
                    General.GetString("MODULE_DEFINITIONS_DELETE_ERROR", "An error occurred deleting module.", this);
                lblErrorDetail.Visible = true;
                ErrorHandler.Publish(LogLevel.Error, lblErrorDetail.Text, ex);
            }
        }

        /// <summary>
        /// Handles the Click event of the selectAllButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void selectAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < PortalsName.Items.Count; i++)
            {
                PortalsName.Items[i].Selected = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the selectNoneButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void selectNoneButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < PortalsName.Items.Count; i++)
            {
                PortalsName.Items[i].Selected = false;
            }
        }

        #region Web Form Designer generated code

        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            this.btnUseInstaller.Click += new EventHandler(this.btnUseInstaller_Click);
            this.btnDescription.Click += new EventHandler(this.btnDescription_Click);
            this.selectAllButton.Click += new EventHandler(this.selectAllButton_Click);
            this.selectNoneButton.Click += new EventHandler(this.selectNoneButton_Click);
            this.Load += new EventHandler(this.Page_Load);
            base.OnInit(e);
        }

        #endregion

        /// <summary>
        /// Handles the Click event of the btnUseInstaller control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void btnUseInstaller_Click(object sender, EventArgs e)
        {
            ShowInstaller(true);
        }

        /// <summary>
        /// Shows the installer.
        /// </summary>
        /// <param name="installer">if set to <c>true</c> [installer].</param>
        private void ShowInstaller(bool installer)
        {
            if (installer)
            {
                tableInstaller.Visible = true;
                tableManual.Visible = false;
                btnUseInstaller.Visible = false;
                btnDescription.Visible = true;
            }
            else
            {
                tableInstaller.Visible = false;
                tableManual.Visible = true;
                btnUseInstaller.Visible = true;
                btnDescription.Visible = false;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnDescription control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void btnDescription_Click(object sender, EventArgs e)
        {
            ShowInstaller(false);
        }
    }
}