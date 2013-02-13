using System;
using System.Data.SqlClient;
using System.Text;
using System.Web.Mail;
using Rainbow.Framework;
using Rainbow.Framework.Content.Security;
using Rainbow.Framework.DataTypes;
using Rainbow.Framework.Helpers;
using Rainbow.Framework.Security;
using Rainbow.Framework.Settings;
using Rainbow.Framework.Users.Data;
using Rainbow.Framework.Web.UI.WebControls;

using System.Web.Security;
using System.Web.Profile;
using System.Data;

namespace Rainbow.Content.Web.Modules
{
    /// <summary>
    /// The SignIn User Control enables clients to authenticate themselves using 
    /// the ASP.NET Forms based authentication system.
    ///
    /// When a client enters their username/password within the appropriate
    /// textboxes and clicks the "Login" button, the LoginBtn_Click event
    /// handler executes on the server and attempts to validate their
    /// credentials against a SQL database.
    ///
    /// If the password check succeeds, then the LoginBtn_Click event handler
    /// sets the customers username in an encrypted cookieID and redirects
    /// back to the portal home page.
    /// 
    /// If the password check fails, then an appropriate error message
    /// is displayed.
    /// </summary>
    public partial class Signin : PortalModuleControl
    {
        protected Localize LoginTitle;

        /// <summary>
        /// Handles the Click event of the LoginBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (PortalSecurity.SignOn(email.Text, password.Text, RememberCheckBox.Checked) == null)
            {
                Message.Text = "Login failed";
                Message.TextKey = "LOGIN_FAILED";
            }
        }

        /// <summary>
        /// Handles the Click event of the RegisterBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect(HttpUrlBuilder.BuildUrl("~/DesktopModules/CoreModules/Register/Register.aspx"));
        }


        /// <summary>
        /// Handles the Click event of the SendPasswordBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SendPasswordBtn_Click( object sender, EventArgs e ) {
            if ( email.Text == string.Empty ) {
                Message.Text = "Please enter you email address";
                Message.TextKey = "SIGNIN_ENTER_EMAIL_ADDR";
                return;
            }


            // generate random password
            string randomPassword = RandomPassword.Generate( 8, 10 );

            CryptoHelper crypthelp = new CryptoHelper();
            UsersDB usersDB = new UsersDB();

            //Obtain single row of User information
            MembershipUser memberUser = usersDB.GetSingleUser( email.Text );
            //ProfileCommon profile = usersDB.GetSingleUserProfile( email.Text, portalSettings.PortalID );

            if ( true ) {
                string Pswrd;
                string AppName = portalSettings.PortalName;
                bool encrypted = Config.EncryptPassword;
                string Name = memberUser.Email;
                if ( encrypted ) {
                    Pswrd = randomPassword;
                    crypthelp.ResetPassword( Name, randomPassword );
                }
                else {
                    Pswrd = memberUser.GetPassword();
                }
                string LoginUrl = Path.ApplicationFullPath + "DesktopModules/Admin/Logon.aspx?Usr=" + Name + "&Pwd=" +
                                  Pswrd + "&Alias=" + portalSettings.PortalAlias;
                MailMessage mail = new MailMessage();

                // Geert.Audenaert@Syntegra.Com
                // Date 19 March 2003
                // We have to use a correct sender address, 
                // because most SMTP servers reject it otherwise
                //jes1111 - mail.From = ConfigurationSettings.AppSettings["EmailFrom"].ToString();
                mail.From = Config.EmailFrom;
                mail.To = email.Text;
                mail.Subject = AppName + " - " + General.GetString( "SIGNIN_SEND_PWD", "Send me password", this );

                StringBuilder sb = new StringBuilder();

                sb.Append( Name );
                sb.Append( "," );
                sb.Append( "\r\n\r\n" );
                sb.Append( General.GetString( "SIGNIN_PWD_REQUESTED", "This is the password you requested", this ) );
                sb.Append( " " );
                sb.Append( Pswrd );
                sb.Append( "\r\n\r\n" );
                sb.Append( General.GetString( "SIGNIN_THANK_YOU", "Thanks for your visit.", this ) );
                sb.Append( " " );
                sb.Append( AppName );
                sb.Append( "\r\n\r\n" );
                sb.Append( General.GetString( "SIGNIN_YOU_CAN_LOGIN_FROM", "You can login from", this ) );
                sb.Append( ":" );
                sb.Append( "\r\n" );
                sb.Append( Path.ApplicationFullPath );
                sb.Append( "\r\n\r\n" );
                sb.Append( General.GetString( "SIGNIN_USE_DIRECT_URL", "Or using direct url", this ) );
                sb.Append( "\r\n" );
                sb.Append( LoginUrl );
                sb.Append( "\r\n\r\n" );
                sb.Append(
                    General.GetString( "SIGNIN_URL_WARNING",
                                      "NOTE: The address above may not show up on your screen as one line. This would prevent you from using the link to access the web page. If this happens, just use the 'cut' and 'paste' options to join the pieces of the URL.",
                                      this ) );

                mail.Body = sb.ToString();
                mail.BodyFormat = MailFormat.Text;

                SmtpMail.SmtpServer = Config.SmtpServer;
                SmtpMail.Send( mail );

                Message.Text =
                    General.GetString( "SIGNIN_PWD_WAS_SENT", "Your password was sent to the addess you provided",
                                      this );
                Message.TextKey = "SIGNIN_PWD_WAS_SENT";
            }
            else {
                Message.Text =
                    General.GetString( "SIGNIN_PWD_MISSING_IN_DB",
                                      "The email you specified does not exists on our database", this );
                Message.TextKey = "SIGNIN_PWD_MISSING_IN_DB";
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Signin"/> class.
        /// </summary>
        public Signin()
        {
            SettingItem HideAutomatically = new SettingItem(new BooleanDataType());
            HideAutomatically.Value = "True";
            HideAutomatically.EnglishName = "Hide automatically";
            HideAutomatically.Order = 20;
            _baseSettings.Add("SIGNIN_AUTOMATICALLYHIDE", HideAutomatically);

            //1.2.8.1743b - 09/10/2003
            //New setting on Signin fo disable IE autocomplete by Mike Stone
            //If you uncheck this setting IE will not remember user name and passwords. 
            //Note that users who have memorized passwords will not be effected until their computer 
            //is reset, only new users and/or computers will honor this. 
            SettingItem AutoComplete = new SettingItem(new BooleanDataType());
            AutoComplete.Value = "True";
            AutoComplete.EnglishName = "Allow IE Autocomplete";
            AutoComplete.Description = "If Checked IE Will try to remember logins";
            AutoComplete.Order = 30;
            _baseSettings.Add("SIGNIN_ALLOW_AUTOCOMPLETE", AutoComplete);

            SettingItem RememberLogin = new SettingItem(new BooleanDataType());
            RememberLogin.Value = "True";
            RememberLogin.EnglishName = "Allow Remember Login";
            RememberLogin.Description = "If Checked allows to remember logins";
            RememberLogin.Order = 40;
            _baseSettings.Add("SIGNIN_ALLOW_REMEMBER_LOGIN", RememberLogin);

            SettingItem SendPassword = new SettingItem(new BooleanDataType());
            SendPassword.Value = "True";
            SendPassword.EnglishName = "Allow Send Password";
            SendPassword.Description = "If Checked allows user to ask to get password by email if he forgotten";
            SendPassword.Order = 50;
            _baseSettings.Add("SIGNIN_ALLOW_SEND_PASSWORD", SendPassword);
        }

        /// <summary>
        /// Overrides ModuleSetting to render this module type un-cacheable
        /// </summary>
        /// <value></value>
        public override bool Cacheable
        {
            get { return false; }
        }

        #region General Implementation

        /// <summary>
        /// GUID of module (mandatory)
        /// </summary>
        /// <value></value>
        public override Guid GuidID
        {
            get { return new Guid("{A0F1F62B-FDC7-4de5-BBAD-A5DAF31D960A}"); }
        }

        #endregion

        #region Web Form Designer generated code

        /// <summary>
        /// On init
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            // use "View = Unauthenticated Users" instead
            //			//Hide control if not needed
            //			if (Request.IsAuthenticated)
            //				this.Visible = false;
            this.LoginBtn.Click += new EventHandler(this.LoginBtn_Click);
            this.SendPasswordBtn.Click += new EventHandler(this.SendPasswordBtn_Click);
            this.RegisterBtn.Click += new EventHandler(this.RegisterBtn_Click);
            this.Load += new EventHandler(this.Signin_Load);
            base.OnInit(e);
        }

        #endregion

        /// <summary>
        /// Handles the Load event of the Signin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Signin_Load(object sender, EventArgs e)
        {
            bool hide = true;
            bool autocomplete = false;
            if (this.ModuleID == 0)
                ((SettingItem) Settings["MODULESETTINGS_SHOW_TITLE"]).Value = "false";

            if (portalSettings.CustomSettings["SITESETTINGS_ALLOW_NEW_REGISTRATION"] != null)
                if (!bool.Parse(portalSettings.CustomSettings["SITESETTINGS_ALLOW_NEW_REGISTRATION"].ToString()))
                    RegisterBtn.Visible = false;

            if (Settings["SIGNIN_AUTOMATICALLYHIDE"] != null)
                hide = bool.Parse(Settings["SIGNIN_AUTOMATICALLYHIDE"].ToString());

            if (Settings["SIGNIN_ALLOW_AUTOCOMPLETE"] != null)
                autocomplete = bool.Parse(Settings["SIGNIN_ALLOW_AUTOCOMPLETE"].ToString());

            if (Settings["SIGNIN_ALLOW_REMEMBER_LOGIN"] != null)
                RememberCheckBox.Visible = bool.Parse(Settings["SIGNIN_ALLOW_REMEMBER_LOGIN"].ToString());

            if (Settings["SIGNIN_ALLOW_SEND_PASSWORD"] != null)
                SendPasswordBtn.Visible = bool.Parse(Settings["SIGNIN_ALLOW_SEND_PASSWORD"].ToString());

            if (hide && Request.IsAuthenticated)
            {
                this.Visible = false;
            }
            else if (!autocomplete)
            {
                //New setting on Signin fo disable IE autocomplete by Mike Stone
                password.Attributes.Add("autocomplete", "off");
            }
        }
    }
}