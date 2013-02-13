using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Rainbow.Framework.Web.UI.WebControls;
using Rainbow.Framework.Security;
using Rainbow.Framework.Data;
using Rainbow.Framework;
using Rainbow.Framework.Users.Data;
using System.Text;
using Rainbow.Framework.Helpers;
using Rainbow.Framework.Settings;
using Rainbow.Framework.Site.Configuration;
using Rainbow.Framework.Providers.RainbowMembershipProvider;

namespace Rainbow.Content.Web.Modules {

    public partial class Register2 : PortalModuleControl, IEditUserProfile {
        public override Guid GuidID {
            get {
                return new Guid( "6D601CA1-BEB9-42ac-B559-4020A64A9707" );
            }
        }

        #region Private Fields

        private string _redirectPage;

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public bool EditMode {
            get {
                return ( userName.Length != 0 );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string RedirectPage {
            get {
                if ( _redirectPage == null ) {
                    // changed by Mario Endara <mario@softworks.com.uy> (2004/11/05)
                    // it's necessary the ModuleID in the URL to apply security checking in the target
                    return
                        Request.Url.Segments[Request.Url.Segments.Length - 1] + "?TabID=" + PageID + "&mID=" + ModuleID +
                        "&username=" + EmailField.Text;
                }
                return _redirectPage;
            }
            set {
                _redirectPage = value;
            }
        }

        private string userName {
            get {
                string uid = string.Empty;

                if ( Request.Params["userName"] != null )
                    uid = Request.Params["userName"];

                if ( uid.Length == 0 && HttpContext.Current.Items["userName"] != null )
                    uid = HttpContext.Current.Items["userName"].ToString();

                return uid;
            }
        }

        private Guid originalUserID {
            get {
                if ( ViewState["originalUserID"] != null )
                    return ( Guid )ViewState["originalUserID"];
                else
                    return Guid.Empty;
            }
            set {
                ViewState["originalUserID"] = value;
            }
        }

        private bool selfEdit {
            get {
                if ( ViewState["selfEdit"] != null )
                    return ( bool )ViewState["selfEdit"];
                else
                    return false;
            }
            set {
                ViewState["selfEdit"] = value;
            }
        }

        #endregion

        #region	Methods

        private void BindCountry() {
            DataTable dtCountries = new DataTable( "Countries" );
            string sql = "Select CountryID, Country From rb_Countries Order By Country";
            dtCountries = DBHelper.GetDataSet( sql ).Tables[0];

            if ( dtCountries == null || dtCountries.Rows.Count == 0 )
                ErrorHandler.Publish( LogLevel.Warn, "No Countries Returned in bind conutry" );

            //  TODO : Fix this

            //dtCountries.ReadXml(Rainbow.Framework.Settings.Path.ApplicationPhysicalPath + "\\App_GlobalResources\\Countries.xml");
            //if (CountriesFilter.Length != 0)
            //{
            //	CountryField.DataSource = CountryInfo.GetCountries(CountriesFilter);
            //}
            //else
            //{
            //	CountryField.DataSource = CountryInfo.GetCountries(CountryTypes.InhabitedCountries,CountryFields.DisplayName);
            //}

            CountryField.DataSource = dtCountries;
            CountryField.DataBind();
        }

        private void BindState() {
            StateRow.Visible = false;
            if ( CountryField.SelectedItem != null ) {
                string currentCountry = CountryField.SelectedItem.Value.ToString();
                //added next line to clear the list. 
                //The stateField seems to remember it's values even when you set the 
                //DataSource to null
                //Michel Barneveld Rainbow@MichelBarneveld.Com
                StateField.Items.Clear();
                // TODO: Check fixing country info comments in BindCountry()
                //StateField.DataSource = new CountryInfo(currentCountry).Childs;
                //StateField.DataBind();
                if ( StateField.Items.Count > 0 ) {
                    StateRow.Visible = true;
                    ThisCountryLabel.Text = CountryField.SelectedItem.Text;
                }
                else {
                    StateRow.Visible = false;
                }
            }
        }

        /// <summary>
        /// Save user data
        /// </summary>
        /// <returns></returns>
        public Guid SaveUserData() {
            Guid returnID = Guid.Empty;

            //if (PasswordField.Text.Length > 0 || ConfirmPasswordField.Text.Length > 0)
            //{
            //    if (PasswordField.Text != ConfirmPasswordField.Text)
            //        ComparePasswords.IsValid = false;
            //}

            // Only attempt a login if all form fields on the page are valid
            if ( Page.IsValid ) {
                UsersDB accountSystem = new UsersDB();

                string CountryID = string.Empty;
                if ( CountryField.SelectedItem != null )
                    CountryID = CountryField.SelectedItem.Value;

                int StateID = 0;
                if ( StateField.SelectedItem != null )
                    StateID = Convert.ToInt32( StateField.SelectedItem.Value );

                try {
                    if ( userName == string.Empty ) {
                        // Add New User to Portal User Database
                        returnID =
                            accountSystem.AddUser( NameField.Text, CompanyField.Text,
                                                  AddressField.Text, CityField.Text, ZipField.Text, CountryID, StateID,
                                                  PhoneField.Text, FaxField.Text,
                                                  PasswordField.Text, EmailField.Text, SendNewsletter.Checked );
                    }
                    else {
                        // Update user
                        if ( PasswordField.Text.Equals( string.Empty ) ) {
                            accountSystem.UpdateUser( originalUserID, NameField.Text, CompanyField.Text, AddressField.Text,
                                CityField.Text, ZipField.Text, CountryID, StateID, PhoneField.Text, FaxField.Text, EmailField.Text, SendNewsletter.Checked );
                        }
                        else {
                            accountSystem.UpdateUser( originalUserID, NameField.Text, CompanyField.Text, AddressField.Text,
                                CityField.Text, ZipField.Text, CountryID, StateID, PhoneField.Text, PasswordField.Text,
                                FaxField.Text, EmailField.Text, SendNewsletter.Checked );
                        }
                    }
                    //If we are here no error occurred
                }
                catch ( Exception ex ) {
                    Message.Text = General.GetString( "REGISTRATION_FAILED", "Registration failed", Message ) + " - ";

                    ErrorHandler.Publish( LogLevel.Error, "Error registering user", ex );
                }
            }
            return returnID;
        }


        /// <summary>
        /// Sends registration information to portal administrator.
        /// </summary>
        public void SendRegistrationNoticeToAdmin() {
            StringBuilder sb = new StringBuilder();

            sb.Append( "New User Registration\n" );
            sb.Append( "---------------------\n" );
            sb.Append( "PORTAL         : " + portalSettings.PortalTitle + "\n" );
            sb.Append( "Name           : " + NameField.Text + "\n" );
            sb.Append( "Company        : " + CompanyField.Text + "\n" );
            sb.Append( "Address        : " + AddressField.Text + "\n" );
            sb.Append( "                 " + CityField.Text + ", " );
            if ( StateField.SelectedItem != null )
                sb.Append( StateField.SelectedItem.Text + "  " );
            sb.Append( ZipField.Text + "\n" );
            sb.Append( "                 " + CountryField.SelectedItem.Text + "\n" );
            sb.Append( "                 " + PhoneField.Text + "\n" );
            sb.Append( "Fax            : " + FaxField.Text + "\n" );
            sb.Append( "Email          : " + EmailField.Text + "\n" );
            sb.Append( "Send Newsletter: " + SendNewsletter.Checked.ToString() + "\n" );

            MailHelper.SendMailNoAttachment(
                portalSettings.CustomSettings["SITESETTINGS_ON_REGISTER_SEND_TO"].ToString(),
                portalSettings.CustomSettings["SITESETTINGS_ON_REGISTER_SEND_TO"].ToString(),
                "New User Registration for " + portalSettings.PortalAlias,
                sb.ToString(),
                string.Empty,
                string.Empty,
                Config.SmtpServer );
        }

        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RegisterBtn_Click( object sender, EventArgs e ) {
            Guid returnID = SaveUserData();

            if ( returnID != Guid.Empty ) {
                if ( portalSettings.CustomSettings["SITESETTINGS_ON_REGISTER_SEND_TO"].ToString().Length > 0 )
                    SendRegistrationNoticeToAdmin();
                //Full signon
                PortalSecurity.SignOn( EmailField.Text, PasswordField.Text, false, RedirectPage );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveChangesBtn_Click( object sender, EventArgs e ) {
            Guid returnID = SaveUserData();

            if ( returnID == Guid.Empty ) {
                if ( selfEdit ) {

                    // TODO: do we need to signout? if we are hashing pwds, we can't retrieve old pwd
                    
                    ////All should be ok now
                    ////Try logoff user
                    //PortalSecurity.SignOut( string.Empty, true );

                    ////Logon user again with new settings
                    //string actualPassword;
                    //if ( PasswordField.Text.Length != 0 )
                    //    actualPassword = PasswordField.Text;
                    //else
                    //    throw new NotSupportedException( "Changing passwords is not still supported" );

                    ////Full signon
                    //PortalSecurity.SignOn( EmailField.Text, actualPassword, false, RedirectPage );
                }
                else if ( RedirectPage == string.Empty ) {
                    // Redirect browser back to home page
                    PortalSecurity.PortalHome();
                }
                else {
                    Response.Redirect( RedirectPage );
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load( object sender, EventArgs e ) {
            if ( Page.IsPostBack == false ) {

                BindCountry();
                BindState();

                // Edit check
                if ( EditMode ) // Someone requested edit this record
                {
                    //True is use is editing himself, false if is edited by an admin
                    selfEdit = ( userName == PortalSettings.CurrentUser.Identity.Email );

                    // Removed by Mario Endara <mario@softworks.com.uy> (2004/11/04)
                    //					if (PortalSecurity.IsInRoles("Admins") || selfEdit)
                    if ( PortalSecurity.HasEditPermissions( ModuleID ) || PortalSecurity.HasAddPermissions( ModuleID ) ||
                        selfEdit ) {
                        //We can edit

                        // Hide
                        RequiredPassword.Visible = false;
                        RequiredConfirm.Visible = false;
                        EditPasswordRow.Visible = true;
                        SaveChangesBtn.Visible = true;
                        RegisterBtn.Visible = false;

                        // Obtain a single row of event information
                        UsersDB accountSystem = new UsersDB();

                        RainbowUser memberUser = accountSystem.GetSingleUser( userName );

                        try {
                            originalUserID = memberUser.ProviderUserKey;
                            NameField.Text = memberUser.Name;
                            EmailField.Text = memberUser.Email;
                            CompanyField.Text = memberUser.Company;
                            AddressField.Text = memberUser.Address;
                            ZipField.Text = memberUser.Zip;
                            CityField.Text = memberUser.City;

                            CountryField.ClearSelection();
                            if ( CountryField.Items.FindByValue( memberUser.CountryID ) != null )
                                CountryField.Items.FindByValue( memberUser.CountryID ).Selected = true;
                            BindState();
                            StateField.ClearSelection();
                            if ( StateField.Items.Count > 0 &&
                                StateField.Items.FindByValue( memberUser.StateID.ToString() ) != null )
                                StateField.Items.FindByValue( memberUser.StateID.ToString() ).Selected = true;

                            FaxField.Text = memberUser.Fax;
                            PhoneField.Text = memberUser.Phone;
                            SendNewsletter.Checked = memberUser.SendNewsletter;

                            //stores original password for later check
                            // originalPassword = memberUser.GetPassword();  NOT STILL SUPPORTED
                        }
                        catch ( System.ArgumentNullException error ) {
                            // no  existe el usuario;
                        }
                    }
                    else {
                        //We do not have rights to do it!
                        PortalSecurity.AccessDeniedEdit();
                    }
                }
                else {
                    BindState();

                    //No edit
                    RequiredPassword.Visible = true;
                    RequiredConfirm.Visible = true;
                    EditPasswordRow.Visible = false;
                    SaveChangesBtn.Visible = false;
                    RegisterBtn.Visible = true;
                }

                string termsOfService = portalSettings.GetTermsOfService;

                //Verify if we have to show conditions
                if ( termsOfService.Length != 0 ) {
                    //Shows conditions
                    FieldConditions.Text = termsOfService;
                    ConditionsRow.Visible = true;
                }
                else {
                    //Hides conditions
                    ConditionsRow.Visible = false;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CountryField_SelectedIndexChanged( object sender, EventArgs e ) {
            BindState();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cancelButton_Click( object sender, EventArgs e ) {
            Page.RedirectBackToReferringPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void CheckTermsValidator_ServerValidate( object source, ServerValidateEventArgs args ) {
            args.IsValid = Accept.Checked;
        }

        #endregion

        #region Web Form Designer generated code

        /// <summary>
        /// Raises the Init event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit( EventArgs e ) {
            InitializeComponent();
            base.OnInit( e );
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.Load += new EventHandler( this.Page_Load );
        }

        #endregion
    }
}
