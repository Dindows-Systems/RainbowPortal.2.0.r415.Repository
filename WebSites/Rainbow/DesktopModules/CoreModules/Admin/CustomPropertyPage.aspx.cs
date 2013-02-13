using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rainbow.Framework.Site.Configuration;
using Rainbow.Framework.Web.UI;
using HyperLink=Rainbow.Framework.Web.UI.WebControls.HyperLink;
using LinkButton=Rainbow.Framework.Web.UI.WebControls.LinkButton;

namespace Rainbow.Content.Web.Modules
{
    /// <summary>
    /// Summary description for Property Page
    /// </summary>
    public partial class PageCustomPropertyPage : PropertyPageCustom
    {
        protected Panel EditPanel;
        protected HyperLink adminPropertiesButton;
        protected PlaceHolder AddEditControl;
        protected LinkButton saveAndCloseButton;

        #region Web Form Designer generated code

        /// <summary>
        /// On init
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            //Controls must be created here
            updateButton = new LinkButton();
            updateButton.CssClass = "CommandButton";
            PlaceHolderButtons.Controls.Add(updateButton);

            PlaceHolderButtons.Controls.Add(new LiteralControl("&#160;"));
            saveAndCloseButton = new LinkButton();
            saveAndCloseButton.TextKey = "SAVE_AND_CLOSE";
            saveAndCloseButton.Text = "Save and close";
            saveAndCloseButton.CssClass = "CommandButton";
            PlaceHolderButtons.Controls.Add(saveAndCloseButton);
            this.saveAndCloseButton.Click += new EventHandler(this.saveAndCloseButton_Click);

            PlaceHolderButtons.Controls.Add(new LiteralControl("&#160;"));

            cancelButton = new LinkButton();
            cancelButton.CssClass = "CommandButton";
            PlaceHolderButtons.Controls.Add(cancelButton);

            this.EditTable.UpdateControl +=
                new Rainbow.Framework.Web.UI.WebControls.UpdateControlEventHandler(this.EditTable_UpdateControl);
            this.Load += new EventHandler(this.PageCustomPropertyPage_Load);
            base.OnInit(e);
        }
        #endregion

        /// <summary>
        /// Handles the Load event of the PageCustomPropertyPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PageCustomPropertyPage_Load(object sender, EventArgs e)
        {
            EditTable.DataSource =
                new SortedList(
                    ModuleSettingsCustom.GetModuleUserSettings(this.ModuleID,
                                                               (Guid)PortalSettings.CurrentUser.Identity.ProviderUserKey, this));
            EditTable.DataBind();
        }

        private void saveAndCloseButton_Click(object sender, EventArgs e)
        {
            OnUpdate(e);
            if (Page.IsValid == true)
                Response.Redirect(Rainbow.Framework.HttpUrlBuilder.BuildUrl("~/Default.aspx", PageID));
        }

        /// <summary>
        /// Persists the changes to database
        /// </summary>
        protected override void OnUpdate(EventArgs e)
        {
            base.OnUpdate(e);

            // Only Update if Input Data is Valid
            if (Page.IsValid == true)
            {
                // Update settings in the database
                EditTable.UpdateControls();
            }
        }

        protected override void OnCancel(EventArgs e)
        {
            Response.Redirect(Rainbow.Framework.HttpUrlBuilder.BuildUrl("~/Default.aspx", PageID));
        }

        private void EditTable_UpdateControl(object sender,
                                             Rainbow.Framework.Web.UI.WebControls.SettingsTableEventArgs e)
        {
            ModuleSettingsCustom.UpdateCustomModuleSetting(ModuleID, (Guid)PortalSettings.CurrentUser.Identity.ProviderUserKey,
                                                           e.CurrentItem.EditControl.ID, e.CurrentItem.Value);
        }
    }
}