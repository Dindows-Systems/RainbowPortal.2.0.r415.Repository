using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

using Rainbow.Framework.Providers.RainbowMembershipProvider;
using System.Web.Security;
using System.Configuration;
using System.Security.Cryptography;
using System.Diagnostics;

namespace Rainbow.Framework.Providers.RainbowMembershipProvider {

    /// <summary>
    /// SQL-specific implementation of <code>RainbowMembershipProvider</code> API
    /// </summary>
    public class RainbowSqlMembershipProvider : RainbowMembershipProvider {

        private const int _errorCode_UserNotFound = 1;
        private const int _errorCode_IncorrectPasswordAnswer = 3;
        private const int _errorCode_UserLockedOut = 99;

        private const int _newPasswordLength = 8;
        private const string _encryptionKey = "BE09F72BFF7A4566";
        private string eventSource = "RainbowSqlMembershipProvider";
        private string eventLog = "Application";

        protected string connectionString;
        protected string pApplicationName;
        protected bool pEnablePasswordReset;
        protected bool pEnablePasswordRetrieval;
        protected bool pRequiresQuestionAndAnswer;
        protected bool pRequiresUniqueEmail;
        protected int pMaxInvalidPasswordAttempts;
        protected int pPasswordAttemptWindow;
        protected MembershipPasswordFormat pPasswordFormat;

        #region System.Web.Security.MembershipProvider overriden properties

        /// <summary>
        /// The name of the application using the membership provider. 
        /// ApplicationName is used to scope membership data so that applications can choose whether to share membership data with other applications. 
        /// This property can be read and written.
        /// </summary>
        public override string ApplicationName {
            get {
                return pApplicationName;
            }
            set {
                pApplicationName = value;
            }
        }

        /// <summary>
        /// Indicates whether passwords can be reset using the provider's ResetPassword method. This property is read-only.
        /// </summary>
        public override bool EnablePasswordReset {
            get {
                return pEnablePasswordReset;
            }
        }

        /// <summary>
        /// Indicates whether passwords can be retrieved using the provider's GetPassword method. This property is read-only.
        /// </summary>
        public override bool EnablePasswordRetrieval {
            get {
                return pEnablePasswordRetrieval;
            }
        }

        /// <summary>
        /// Indicates whether a password answer must be supplied when calling the provider's GetPassword and ResetPassword methods. This property is read-only.
        /// </summary>
        public override bool RequiresQuestionAndAnswer {
            get {
                return pRequiresQuestionAndAnswer;
            }
        }

        /// <summary>
        /// Indicates whether each registered user must have a unique e-mail address. This property is read-only.
        /// </summary>
        public override bool RequiresUniqueEmail {
            get {
                return pRequiresUniqueEmail;
            }
        }

        /// <summary>
        /// Works in conjunction with PasswordAttemptWindow to provide a safeguard against password guessing. 
        /// If the number of consecutive invalid passwords or password questions ("invalid attempts") submitted 
        /// to the provider for a given user reaches MaxInvalidPasswordAttempts within the number of minutes specified 
        /// by PasswordAttemptWindow, the user is locked out of the system. The user remains locked out until the 
        /// provider's UnlockUser method is called to remove the lock.
        /// The count of consecutive invalid attempts is incremented when an invalid password or password answer is 
        /// submitted to the provider's ValidateUser, ChangePassword, ChangePasswordQuestionAndAnswer, GetPassword, and ResetPassword methods.
        /// If a valid password or password answer is supplied before the MaxInvalidPasswordAttempts is reached, 
        /// the count of consecutive invalid attempts is reset to zero. 
        /// If the RequiresQuestionAndAnswer property is false, invalid password answer attempts are not tracked.
        /// This property is read-only.
        /// </summary>
        public override int MaxInvalidPasswordAttempts {
            get {
                return pMaxInvalidPasswordAttempts;
            }
        }

        /// <summary>
        /// For a description, see MaxInvalidPasswordAttempts. This property is read-only.
        /// </summary>
        /// <see cref="MaxInvalidPasswordAttempts"/>
        public override int PasswordAttemptWindow {
            get {
                return pPasswordAttemptWindow;
            }
        }

        /// <summary>
        /// Indicates what format that passwords are stored in: clear (plaintext), encrypted, or hashed.
        /// Clear and encrypted passwords can be retrieved; hashed passwords cannot. This property is read-only.
        /// </summary>
        public override MembershipPasswordFormat PasswordFormat {
            get {
                return pPasswordFormat;
            }
        }

        private int pMinRequiredNonAlphanumericCharacters;

        /// <summary>
        /// The minimum number of non-alphanumeric characters required in a password. This property is read-only.
        /// </summary>
        public override int MinRequiredNonAlphanumericCharacters {
            get {
                return pMinRequiredNonAlphanumericCharacters;
            }
        }

        private int pMinRequiredPasswordLength;

        /// <summary>
        /// The minimum number of characters required in a password. This property is read-only.
        /// </summary>
        public override int MinRequiredPasswordLength {
            get {
                return pMinRequiredPasswordLength;
            }
        }

        private string pPasswordStrengthRegularExpression;

        /// <summary>
        /// A regular expression specifying a pattern to which passwords must conform. This property is read-only.
        /// </summary>
        public override string PasswordStrengthRegularExpression {
            get {
                return pPasswordStrengthRegularExpression;
            }
        }

        #endregion

        #region Properties

        //
        // If false, exceptions are thrown to the caller. If true,
        // exceptions are written to the event log.
        //

        private bool pWriteExceptionsToEventLog;

        public bool WriteExceptionsToEventLog {
            get {
                return pWriteExceptionsToEventLog;
            }
            set {
                pWriteExceptionsToEventLog = value;
            }
        }

        #endregion

        #region System.Web.Security.MembershipProvider overriden methods

        public override void Initialize( string name, System.Collections.Specialized.NameValueCollection config ) {

            //
            // Initialize values from web.config.
            //

            if ( config == null )
                throw new ArgumentNullException( "config" );

            if ( name == null || name.Length == 0 )
                name = "RainbowSqlMembershipProvider";

            if ( String.IsNullOrEmpty( config["description"] ) ) {
                config.Remove( "description" );
                config.Add( "description", "Rainbow Sql Membership provider" );
            }

            // Initialize the abstract base class.
            base.Initialize( name, config );

            pApplicationName = GetConfigValue( config["applicationName"],
                                            System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath );
            pMaxInvalidPasswordAttempts = Convert.ToInt32( GetConfigValue( config["maxInvalidPasswordAttempts"], "5" ) );
            pPasswordAttemptWindow = Convert.ToInt32( GetConfigValue( config["passwordAttemptWindow"], "10" ) );
            pMinRequiredNonAlphanumericCharacters = Convert.ToInt32( GetConfigValue( config["minRequiredNonAlphanumericCharacters"], "1" ) );
            pMinRequiredPasswordLength = Convert.ToInt32( GetConfigValue( config["minRequiredPasswordLength"], "7" ) );
            pPasswordStrengthRegularExpression = Convert.ToString( GetConfigValue( config["passwordStrengthRegularExpression"], "" ) );
            pEnablePasswordReset = Convert.ToBoolean( GetConfigValue( config["enablePasswordReset"], "true" ) );
            pEnablePasswordRetrieval = Convert.ToBoolean( GetConfigValue( config["enablePasswordRetrieval"], "true" ) );
            pRequiresQuestionAndAnswer = Convert.ToBoolean( GetConfigValue( config["requiresQuestionAndAnswer"], "false" ) );
            pRequiresUniqueEmail = Convert.ToBoolean( GetConfigValue( config["requiresUniqueEmail"], "true" ) );
            pWriteExceptionsToEventLog = Convert.ToBoolean( GetConfigValue( config["writeExceptionsToEventLog"], "true" ) );

            string temp_format = config["passwordFormat"];
            if ( temp_format == null ) {
                temp_format = "Hashed";
            }

            switch ( temp_format ) {
                case "Hashed":
                    pPasswordFormat = MembershipPasswordFormat.Hashed;
                    break;
                case "Encrypted":
                    pPasswordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Clear":
                    pPasswordFormat = MembershipPasswordFormat.Clear;
                    break;
                default:
                    throw new RainbowMembershipProviderException( "Password format not supported." );
            }

            // Initialize SqlConnection.
            ConnectionStringSettings ConnectionStringSettings = ConfigurationManager.ConnectionStrings[config["connectionStringName"]];

            if ( ConnectionStringSettings == null || ConnectionStringSettings.ConnectionString.Trim().Equals( string.Empty ) ) {
                throw new RainbowMembershipProviderException( "Connection string cannot be blank." );
            }

            connectionString = ConnectionStringSettings.ConnectionString;

            if ( EnablePasswordRetrieval && ( PasswordFormat == MembershipPasswordFormat.Hashed ) ) {
                throw new RainbowMembershipProviderException( "Can't enable password retrieval when using hashed passwords" );
            }
        }

        public override bool ChangePassword( string username, string oldPassword, string newPassword ) {
            return ChangePassword( ApplicationName, username, oldPassword, newPassword );
        }

        public override bool ChangePasswordQuestionAndAnswer( string username, string password, string newPasswordQuestion, string newPasswordAnswer ) {
            return ChangePasswordQuestionAndAnswer( ApplicationName, username, password, newPasswordQuestion, newPasswordAnswer );
        }

        public override MembershipUser CreateUser( string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status ) {
            return CreateUser( ApplicationName, username, password, email, passwordQuestion, passwordAnswer, isApproved, out status );
        }

        public override string GetPassword( string username, string answer ) {
            return GetPassword( ApplicationName, username, answer );
        }

        public override string ResetPassword( string username, string answer ) {
            return ResetPassword( ApplicationName, username, answer );
        }

        public override void UpdateUser( MembershipUser user ) {
            UpdateUser( ApplicationName, user );
        }

        public override bool ValidateUser( string username, string password ) {
            return ValidateUser( ApplicationName, username, password );
        }

        public override bool UnlockUser( string userName ) {
            return UnlockUser( ApplicationName, userName );
        }

        public override MembershipUser GetUser( object providerUserKey, bool userIsOnline ) {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_Membership_GetUserByUserId";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = new SqlConnection( connectionString );

            cmd.Parameters.Add( "@UserId", SqlDbType.UniqueIdentifier ).Value = providerUserKey;
            cmd.Parameters.Add( "@CurrentTimeUtc", SqlDbType.DateTime ).Value = DateTime.Now;
            if ( userIsOnline ) {
                cmd.Parameters.Add( "@UpdateLastActivity", SqlDbType.Bit ).Value = 1;
            }
            else {
                cmd.Parameters.Add( "@UpdateLastActivity", SqlDbType.Bit ).Value = 0;
            }

            RainbowUser u = null;
            SqlDataReader reader = null;

            try {
                cmd.Connection.Open();

                using ( reader = cmd.ExecuteReader() ) {
                    if ( reader.HasRows ) {
                        reader.Read();

                        string email = reader.IsDBNull( 0 ) ? string.Empty : reader.GetString( 0 );
                        string passwordQuestion = reader.IsDBNull( 1 ) ? string.Empty : reader.GetString( 1 );
                        string comment = reader.IsDBNull( 2 ) ? string.Empty : reader.GetString( 2 );
                        bool isApproved = reader.IsDBNull( 3 ) ? false : reader.GetBoolean( 3 );
                        DateTime creationDate = reader.IsDBNull( 4 ) ? DateTime.Now : reader.GetDateTime( 4 );
                        DateTime lastLoginDate = reader.IsDBNull( 5 ) ? DateTime.Now : reader.GetDateTime( 5);
                        DateTime lastActivityDate = reader.IsDBNull( 6 ) ? DateTime.Now : reader.GetDateTime( 6 );
                        DateTime lastPasswordChangedDate = reader.IsDBNull( 7 ) ? DateTime.Now : reader.GetDateTime( 7 );
                        string userName = reader.IsDBNull( 8 ) ? string.Empty : reader.GetString( 8 );
                        bool isLockedOut = reader.IsDBNull( 9 ) ? false : reader.GetBoolean( 9 );
                        DateTime lastLockedOutDate = reader.IsDBNull( 10 ) ? DateTime.Now : reader.GetDateTime( 10 );

                        u = InstanciateNewUser( this.Name, userName, ( Guid )providerUserKey, email, passwordQuestion, comment, isApproved,
                             isLockedOut, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, lastLockedOutDate );

                        LoadUserProfile( u );

                    }
                }
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "GetUser(object, Boolean)" );
                }
                throw new RainbowMembershipProviderException( "Error executing aspnet_Membership_GetUserByUserId stored proc", e );
            }
            finally {
                if ( reader != null ) {
                    reader.Close();
                }

                cmd.Connection.Close();
            }

            return u;
        }

        public override MembershipUser GetUser( string username, bool userIsOnline ) {
            return GetUser( ApplicationName, username, userIsOnline );
        }

        public override string GetUserNameByEmail( string email ) {
            return GetUserNameByEmail( ApplicationName, email );
        }

        public override bool DeleteUser( string username, bool deleteAllRelatedData ) {
            return DeleteUser( ApplicationName, username, deleteAllRelatedData );
        }

        public override MembershipUserCollection FindUsersByEmail( string emailToMatch, int pageIndex, int pageSize, out int totalRecords ) {
            return FindUsersByEmail( ApplicationName, emailToMatch, pageIndex, pageSize, out totalRecords );
        }

        public override MembershipUserCollection FindUsersByName( string usernameToMatch, int pageIndex, int pageSize, out int totalRecords ) {
            return FindUsersByName( ApplicationName, usernameToMatch, pageIndex, pageSize, out totalRecords );
        }

        public override MembershipUserCollection GetAllUsers( int pageIndex, int pageSize, out int totalRecords ) {
            return GetAllUsers( ApplicationName, pageIndex, pageSize, out totalRecords );
        }

        public override int GetNumberOfUsersOnline() {
            return GetNumberOfUsersOnline( ApplicationName );
        }

        #endregion

        #region Rainbow-specific Provider methods

        public override bool ChangePassword( string portalAlias, string username, string oldPassword, string newPassword ) {
            if ( !ValidateUser( username, oldPassword ) )
                return false;

            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs( username, newPassword, true );

            OnValidatingPassword( args );

            if ( args.Cancel ) {
                if ( args.FailureInformation != null ) {
                    throw args.FailureInformation;
                }
                else {
                    throw new MembershipPasswordException( "Change password canceled due to new password validation failure." );
                }
            }

            string passwordSalt = string.Empty;
            string encodedPassword;
            if ( PasswordFormat == MembershipPasswordFormat.Hashed ) {
                encodedPassword = EncodePassword( passwordSalt + newPassword );
            }
            else {
                encodedPassword = EncodePassword( newPassword );
            }

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_Membership_SetPassword";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = new SqlConnection( connectionString );
            
            cmd.Parameters.Add( "@ApplicationName", SqlDbType.NVarChar, 256 ).Value = portalAlias;
            cmd.Parameters.Add( "@Username", SqlDbType.NVarChar, 255 ).Value = username;
            cmd.Parameters.Add( "@NewPassword", SqlDbType.NVarChar, 255 ).Value = encodedPassword;
            cmd.Parameters.Add( "@PasswordSalt", SqlDbType.NVarChar, 255 ).Value = passwordSalt;
            cmd.Parameters.Add( "@CurrentTimeUtc", SqlDbType.DateTime ).Value = DateTime.Now;
            cmd.Parameters.Add( "@PasswordFormat", SqlDbType.Int ).Value = PasswordFormat;

            SqlParameter returnCode = cmd.Parameters.Add( "@ReturnCode", SqlDbType.Int );
            returnCode.Direction = ParameterDirection.ReturnValue;

            try {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                return ( ( int )returnCode.Value ) == 0;
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "ChangePassword" );
                }

                throw new RainbowMembershipProviderException( "Error executing aspnet_Membership_SetPassword stored proc", e );
            }
            finally {
                cmd.Connection.Close();
            }
        }

        public override bool ChangePasswordQuestionAndAnswer( string portalAlias, string username, string password, string newPasswordQuestion, string newPasswordAnswer ) {
            if ( !ValidateUser( username, password ) )
                return false;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_Membership_ChangePasswordQuestionAndAnswer";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = new SqlConnection( connectionString );

            cmd.Parameters.Add( "@ApplicationName", SqlDbType.NVarChar, 256 ).Value = portalAlias;
            cmd.Parameters.Add( "@Username", SqlDbType.NVarChar, 255 ).Value = username;
            cmd.Parameters.Add( "@NewPasswordQuestion", SqlDbType.NVarChar, 255 ).Value = newPasswordQuestion;
            cmd.Parameters.Add( "@NewPasswordAnswer", SqlDbType.NVarChar, 255 ).Value = newPasswordAnswer;

            SqlParameter returnCode = cmd.Parameters.Add( "@ReturnCode", SqlDbType.Int );
            returnCode.Direction = ParameterDirection.ReturnValue;

            try {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                return ( ( int )returnCode.Value ) == 0;

            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "ChangePasswordQuestionAndAnswer" );
                }
                throw new RainbowMembershipProviderException( "Error executing aspnet_Membership_ChangePasswordQuestionAndAnswer stored proc", e );
            }
            finally {
                cmd.Connection.Close();
            }
        }

        public override MembershipUser CreateUser( string portalAlias, string username, string password, string email,
            string passwordQuestion, string passwordAnswer, bool isApproved, out MembershipCreateStatus status ) {

            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs( username, password, true );
            OnValidatingPassword( args );

            if ( args.Cancel ) {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            string passwordSalt = string.Empty;
            string encodedPassword;
            if ( PasswordFormat == MembershipPasswordFormat.Hashed ) {
                encodedPassword = EncodePassword( passwordSalt + password );
            }
            else {
                encodedPassword = EncodePassword( password );
            }

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_Membership_CreateUser";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = new SqlConnection( connectionString );

            cmd.Parameters.Add( "@ApplicationName", SqlDbType.NVarChar, 256 ).Value = portalAlias;
            cmd.Parameters.Add( "@Username", SqlDbType.NVarChar, 255 ).Value = username;
            cmd.Parameters.Add( "@Password", SqlDbType.NVarChar, 255 ).Value = encodedPassword;
            cmd.Parameters.Add( "@PasswordSalt", SqlDbType.NVarChar, 255 ).Value = passwordSalt;
            cmd.Parameters.Add( "@Email", SqlDbType.NVarChar, 256 ).Value = email;
            cmd.Parameters.Add( "@PasswordQuestion", SqlDbType.NVarChar, 255 ).Value = passwordQuestion;
            cmd.Parameters.Add( "@PasswordAnswer", SqlDbType.NVarChar, 255 ).Value = passwordAnswer == null ? null : passwordAnswer;
            cmd.Parameters.Add( "@IsApproved", SqlDbType.Bit ).Value = isApproved;
            cmd.Parameters.Add( "@UniqueEmail", SqlDbType.Int ).Value = RequiresUniqueEmail;
            cmd.Parameters.Add( "@PasswordFormat", SqlDbType.Int ).Value = PasswordFormat;
            cmd.Parameters.Add( "@CreateDate", SqlDbType.DateTime ).Value = DateTime.Now;
            cmd.Parameters.Add( "@CurrentTimeUTC", SqlDbType.DateTime ).Value = DateTime.Now;

            SqlParameter newUserIdParam = cmd.Parameters.Add( "@UserId", SqlDbType.UniqueIdentifier );
            newUserIdParam.Direction = ParameterDirection.Output;

            SqlParameter returnCode = cmd.Parameters.Add( "@ReturnCode", SqlDbType.Int );
            returnCode.Direction = ParameterDirection.ReturnValue;

            try {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                status = ( MembershipCreateStatus )Enum.Parse( typeof( MembershipCreateStatus ), returnCode.Value.ToString() );

                if ( ( ( int )returnCode.Value ) == 0 ) {
                    // everything went OK

                    RainbowUser user = ( RainbowUser )this.GetUser( newUserIdParam.Value, false );
                    this.SaveUserProfile( user );
                    return user;
                }
                else {
                    return null;
                }
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "CreateUser" );
                }

                status = MembershipCreateStatus.ProviderError;
                return null;
            }
            finally {
                cmd.Connection.Close();
            }
        }

        public override bool DeleteUser( string portalAlias, string username, bool deleteAllRelatedData ) {
            bool profileDeleted = this.DeleteUserProfile( username );

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_Users_DeleteUser";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = new SqlConnection( connectionString );

            cmd.Parameters.Add( "@ApplicationName", SqlDbType.NVarChar, 256 ).Value = portalAlias;
            cmd.Parameters.Add( "@Username", SqlDbType.NVarChar, 255 ).Value = username;
            if ( deleteAllRelatedData ) {
                cmd.Parameters.Add( "@TablesToDeleteFrom", SqlDbType.Int ).Value = 0xF;
            }
            else {
                cmd.Parameters.Add( "@TablesToDeleteFrom", SqlDbType.Int ).Value = 1;
            }

            SqlParameter tablesDeletedFrom = cmd.Parameters.Add( "@NumTablesDeletedFrom", SqlDbType.Int );
            tablesDeletedFrom.Direction = ParameterDirection.Output;

            SqlParameter returnCode = cmd.Parameters.Add( "@ReturnCode", SqlDbType.Int );
            returnCode.Direction = ParameterDirection.ReturnValue;

            try {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                return ( ( ( int )tablesDeletedFrom.Value ) > 0 ) && ( ( ( int )returnCode.Value ) == 0 );
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "DeleteUser" );
                }

                throw new RainbowMembershipProviderException( "Error executing aspnet_Users_DeleteUser stored proc", e );
            }
            finally {
                cmd.Connection.Close();
            }
        }

        public override MembershipUserCollection GetAllUsers( string portalAlias ) {
            int records;
            return GetAllUsers( portalAlias, 0, int.MaxValue, out records );
        }

        public override MembershipUserCollection GetAllUsers( string portalAlias, int pageIndex, int pageSize, out int totalRecords ) {
            MembershipUserCollection users = new MembershipUserCollection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_Membership_GetAllUsers";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = new SqlConnection( connectionString );

            cmd.Parameters.Add( "@ApplicationName", SqlDbType.NVarChar, 256 ).Value = portalAlias;
            cmd.Parameters.Add( "@PageIndex", SqlDbType.Int ).Value = pageIndex;
            cmd.Parameters.Add( "@PageSize", SqlDbType.Int ).Value = pageSize;

            SqlParameter totalRecordsParam = cmd.Parameters.Add( "@TotalRecords", SqlDbType.Int );
            totalRecordsParam.Direction = ParameterDirection.ReturnValue;

            SqlDataReader reader = null;
            try {
                cmd.Connection.Open();

                using ( reader = cmd.ExecuteReader() ) {

                    while ( reader.Read() ) {
                        RainbowUser u = GetUserFromReader( reader );
                        LoadUserProfile( u );
                        users.Add( u );
                    }

                    reader.Close();
                    totalRecords = ( int )totalRecordsParam.Value;
                }
                return users;
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "GetAllUsers" );
                }

                throw new RainbowMembershipProviderException( "Error executing aspnet_Membership_GetAllUsers stored proc", e );
            }
            finally {
                if ( reader != null ) {
                    reader.Close();
                }
                cmd.Connection.Close();
            }
        }

        public override int GetNumberOfUsersOnline( string portalAlias ) {

            TimeSpan onlineSpan = new TimeSpan( 0, System.Web.Security.Membership.UserIsOnlineTimeWindow, 0 );
            DateTime compareTime = DateTime.Now.Subtract( onlineSpan );

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_Membership_GetNumberOfUsersOnline";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = new SqlConnection( connectionString );

            cmd.Parameters.Add( "@ApplicationName", SqlDbType.NVarChar, 256 ).Value = portalAlias;
            cmd.Parameters.Add( "@MinutesSinceLastInActive", SqlDbType.Int ).Value = Membership.UserIsOnlineTimeWindow;
            cmd.Parameters.Add( "@CurrentTimeUtc", SqlDbType.DateTime ).Value = DateTime.Now;

            int numOnline = 0;

            try {
                cmd.Connection.Open();

                numOnline = Convert.ToInt32( cmd.ExecuteScalar() );
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "GetNumberOfUsersOnline" );
                }
                throw new RainbowMembershipProviderException( "Error executing aspnet_Membership_GetNumberOfUsersOnline stored proc", e );
            }
            finally {
                cmd.Connection.Close();
            }

            return numOnline;
        }

        public override string GetPassword( string portalAlias, string username, string answer ) {
            if ( !EnablePasswordRetrieval ) {
                throw new RainbowMembershipProviderException( "Password Retrieval Not Enabled." );
            }

            if ( PasswordFormat == MembershipPasswordFormat.Hashed ) {
                throw new RainbowMembershipProviderException( "Cannot retrieve Hashed passwords." );
            }

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_Membership_GetPassword";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = new SqlConnection( connectionString );

            cmd.Parameters.Add( "@ApplicationName", SqlDbType.NVarChar, 256 ).Value = portalAlias;
            cmd.Parameters.Add( "@UserName", SqlDbType.NVarChar, 256 ).Value = username;
            cmd.Parameters.Add( "@MaxInvalidPasswordAttempts", SqlDbType.Int ).Value = MaxInvalidPasswordAttempts;
            cmd.Parameters.Add( "@PasswordAttemptWindow", SqlDbType.Int ).Value = PasswordAttemptWindow;
            cmd.Parameters.Add( "@CurrentTimeUtc", SqlDbType.DateTime ).Value = DateTime.Now;
            cmd.Parameters.Add( "@PasswordAnswer", SqlDbType.NVarChar, 128).Value = answer;

            SqlParameter returnCodeParam = cmd.Parameters.Add( "@ReturnCode", SqlDbType.Int );
            returnCodeParam.Direction = ParameterDirection.ReturnValue;

            string password = string.Empty;
            string passwordAnswer = string.Empty;
            MembershipPasswordFormat passwordFormat;
            SqlDataReader reader = null;

            try {
                cmd.Connection.Open();

                using ( reader = cmd.ExecuteReader( CommandBehavior.SingleRow ) ) {
                    if ( reader.HasRows ) {
                        reader.Read();

                        password = reader.GetString( 0 );
                        passwordFormat = ( MembershipPasswordFormat )Enum.Parse( typeof( MembershipPasswordFormat ), reader.GetInt32( 1 ).ToString() );

                    }
                    reader.Close();

                    int returnCode = (int)returnCodeParam.Value;
                    switch ( returnCode ) {
                        case _errorCode_UserNotFound:
                            throw new RainbowMembershipProviderException( "The supplied user name was not found." );
                        case _errorCode_IncorrectPasswordAnswer:
                            throw new MembershipPasswordException( "Incorrect password answer." );
                        case _errorCode_UserLockedOut:
                            throw new MembershipPasswordException( "User is currently locked out" );
                        case -1:
                            throw new RainbowMembershipProviderException( "Error executing aspnet_Membership_GetPassword stored proc" );
                    }

                    if ( PasswordFormat == MembershipPasswordFormat.Encrypted ) {
                        password = UnEncodePassword( password );
                    }
                    return password;
                }
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "GetPassword" );
                }

                throw new RainbowMembershipProviderException( "Error executing aspnet_Membership_GetPassword stored proc", e );
            }
            finally {
                if ( reader != null ) {
                    reader.Close();
                }
                cmd.Connection.Close();
            }
        }

        public override MembershipUser GetUser( string portalAlias, string username, bool userIsOnline ) {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_Membership_GetUserByName";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = new SqlConnection( connectionString );

            cmd.Parameters.Add( "@ApplicationName", SqlDbType.NVarChar, 256 ).Value = portalAlias;
            cmd.Parameters.Add( "@UserName", SqlDbType.NVarChar, 256 ).Value = username;
            cmd.Parameters.Add( "@CurrentTimeUtc", SqlDbType.DateTime ).Value = DateTime.Now;
            if ( userIsOnline ) {
                cmd.Parameters.Add( "@UpdateLastActivity", SqlDbType.Bit ).Value = 1;
            }
            else {
                cmd.Parameters.Add( "@UpdateLastActivity", SqlDbType.Bit ).Value = 0;
            }

            RainbowUser u = null;
            SqlDataReader reader = null;

            try {
                cmd.Connection.Open();

                using ( reader = cmd.ExecuteReader() ) {
                    if ( reader.HasRows ) {
                        reader.Read();

                        string email = reader.IsDBNull( 0 ) ? string.Empty : reader.GetString( 0 );
                        string passwordQuestion = reader.IsDBNull( 1 ) ? string.Empty : reader.GetString( 1 );
                        string comment = reader.IsDBNull( 2 ) ? string.Empty : reader.GetString( 2 );
                        bool isApproved = reader.IsDBNull( 3 ) ? false : reader.GetBoolean( 3 );
                        DateTime creationDate = reader.IsDBNull( 4 ) ? DateTime.Now : reader.GetDateTime( 4 );
                        DateTime lastLoginDate = reader.IsDBNull( 5 ) ? DateTime.Now : reader.GetDateTime( 5 );
                        DateTime lastActivityDate = reader.IsDBNull( 6 ) ? DateTime.Now : reader.GetDateTime( 6 );
                        DateTime lastPasswordChangedDate = reader.IsDBNull( 7 ) ? DateTime.Now : reader.GetDateTime( 7 );

                        Guid providerUserKey = new Guid( reader.GetValue( 8 ).ToString() );
                        bool isLockedOut = reader.IsDBNull( 9 ) ? false : reader.GetBoolean( 9);
                        DateTime lastLockedOutDate = reader.IsDBNull( 10 ) ? DateTime.Now : reader.GetDateTime( 10 );

                        u = InstanciateNewUser( this.Name, username, providerUserKey, email, passwordQuestion, comment, isApproved,
                             isLockedOut, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, lastLockedOutDate);
                        LoadUserProfile( u );
                    }
                }
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "GetUser(String, Boolean)" );
                }
                throw new RainbowMembershipProviderException( "Error executing aspnet_Membership_GetUserByName stored proc", e );
            }
            finally {
                if ( reader != null ) {
                    reader.Close();
                }

                cmd.Connection.Close();
            }

            return u;
        }

        public override bool UnlockUser( string portalAlias, string username ) {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_Membership_UnlockUser";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = new SqlConnection( connectionString );

            cmd.Parameters.Add( "@ApplicationName", SqlDbType.NVarChar, 256 ).Value = portalAlias;
            cmd.Parameters.Add( "@UserName", SqlDbType.NVarChar, 256 ).Value = username;

            SqlParameter returnCodeParam = cmd.Parameters.Add( "@ReturnCode", SqlDbType.Int );
            returnCodeParam.Direction = ParameterDirection.ReturnValue;

            int rowsAffected = 0;

            try {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                int returnCode = ( int )returnCodeParam.Value;
                return (returnCode == 0);
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "UnlockUser" );
                }
             
                throw new RainbowMembershipProviderException( "Error executing aspnet_Membership_UnlockUser stored proc", e );
            }
            finally {
                cmd.Connection.Close();
            }
        }

        public override string GetUserNameByEmail( string portalAlias, string email ) {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_Membership_GetUserByEmail";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = new SqlConnection( connectionString );

            cmd.Parameters.Add( "@ApplicationName", SqlDbType.NVarChar, 256 ).Value = portalAlias;
            cmd.Parameters.Add( "@Email", SqlDbType.NVarChar ).Value = email;

            string username = string.Empty;

            try {
                cmd.Connection.Open();

                username = ( string )cmd.ExecuteScalar();
                if ( username == null ) {
                    username = string.Empty;
                }
                return username;
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "GetUserNameByEmail" );
                }
             
                throw new RainbowMembershipProviderException( "Error executing aspnet_Membership_GetUserByEmail stored proc", e );
            }
            finally {
                cmd.Connection.Close();
            }
        }

        public override void UpdateUser( string portalAlias, MembershipUser user ) {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_Membership_UpdateUser";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = new SqlConnection( connectionString );

            cmd.Parameters.Add( "@ApplicationName", SqlDbType.NVarChar, 256 ).Value = portalAlias;
            cmd.Parameters.Add( "@UserName", SqlDbType.NVarChar, 256 ).Value = user.UserName;
            cmd.Parameters.Add( "@Email", SqlDbType.NVarChar, 256 ).Value = user.Email;
            cmd.Parameters.Add( "@Comment", SqlDbType.NText ).Value = user.Comment;
            cmd.Parameters.Add( "@IsApproved", SqlDbType.Bit ).Value = user.IsApproved;
            cmd.Parameters.Add( "@LastLoginDate", SqlDbType.DateTime).Value = user.LastLoginDate;
            cmd.Parameters.Add( "@LastActivityDate", SqlDbType.DateTime).Value = user.LastActivityDate;
            cmd.Parameters.Add( "@UniqueEmail", SqlDbType.Bit).Value = RequiresUniqueEmail;
            cmd.Parameters.Add( "@CurrentTimeUtc", SqlDbType.DateTime).Value = DateTime.Now;

            SqlParameter totalRecordsParam = cmd.Parameters.Add( "@TotalRecords", SqlDbType.Int );
            totalRecordsParam.Direction = ParameterDirection.ReturnValue;

            try {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                SaveUserProfile( ( RainbowUser )user );
                if ( ( ( int )totalRecordsParam.Value ) != 0 ) {
                    throw new RainbowMembershipProviderException( "Error updating user" );
                }
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "UpdateUser" );
                }

                throw new RainbowMembershipProviderException( "Error executing aspnet_Membership_UpdateUser stored proc", e );
            }
            catch ( Exception e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "UpdateUser" );
                }
                
                throw new RainbowMembershipProviderException( "Error updating user", e );
            }
            finally {
                cmd.Connection.Close();
            }
        }

        public override bool ValidateUser( string portalAlias, string username, string password ) {
            SqlConnection conn = new SqlConnection( connectionString );

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_Membership_GetPasswordWithFormat";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = conn;

            cmd.Parameters.Add( "@ApplicationName", SqlDbType.NVarChar, 256 ).Value = portalAlias;
            cmd.Parameters.Add( "@UserName", SqlDbType.NVarChar, 256 ).Value = username;
            cmd.Parameters.Add( "@UpdateLastLoginActivityDate", SqlDbType.Int ).Value = 1;
            cmd.Parameters.Add( "@CurrentTimeUtc", SqlDbType.DateTime ).Value = DateTime.Now;

            SqlParameter returnCode = cmd.Parameters.Add( "@ReturnCode", SqlDbType.Int );
            returnCode.Direction = ParameterDirection.ReturnValue;

            SqlDataReader reader = null;
            string dbPassword = string.Empty;
            string dbPasswordSalt = string.Empty;
            MembershipPasswordFormat passwordFormat = MembershipPasswordFormat.Clear;

            try {
                cmd.Connection.Open();

                using ( reader = cmd.ExecuteReader( CommandBehavior.SingleRow ) ) {
                    if ( reader.HasRows ) {
                        reader.Read();

                        dbPassword = reader.GetString( 0 );
                        passwordFormat = ( MembershipPasswordFormat )Enum.Parse( typeof( MembershipPasswordFormat ), reader.GetInt32( 1 ).ToString() );
                        dbPasswordSalt = reader.GetString( 2 );
                    }

                    reader.Close();
                    if ( ( ( int )returnCode.Value ) > 0 ) {
                        return false;
                    }
                }

                return CheckPassword( password, dbPassword, dbPasswordSalt, passwordFormat );
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "ValidateUser" );
                }
                throw new RainbowMembershipProviderException( "Error validating user", e );
            }
            finally {
                if ( reader != null ) {
                    reader.Close();
                }
                conn.Close();
            }
        }

        public override MembershipUserCollection FindUsersByName( string portalAlias, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords ) {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_Membership_FindUsersByName";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = new SqlConnection( connectionString );

            cmd.Parameters.Add( "@ApplicationName", SqlDbType.NVarChar, 256 ).Value = portalAlias;
            cmd.Parameters.Add( "@UserNameToMatch", SqlDbType.NVarChar, 256 ).Value = usernameToMatch;
            cmd.Parameters.Add( "@PageIndex", SqlDbType.Int ).Value = pageIndex;
            cmd.Parameters.Add( "@PageSize", SqlDbType.Int ).Value = pageSize;

            SqlParameter returnCode = cmd.Parameters.Add( "@ReturnCode", SqlDbType.Int );
            returnCode.Direction = ParameterDirection.ReturnValue;

            MembershipUserCollection users = new MembershipUserCollection();

            SqlDataReader reader = null;

            try {
                cmd.Connection.Open();

                using ( reader = cmd.ExecuteReader() ) {

                    while ( reader.Read() ) {
                        RainbowUser u = GetUserFromReader( reader );
                        LoadUserProfile( u );
                        users.Add( u );
                    }

                    reader.Close();
                    totalRecords = ( int )returnCode.Value;
                }
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "FindUsersByName" );
                }

                throw new RainbowMembershipProviderException( "Error executing aspnet_Membership_FindUsersByName stored proc", e );
            }
            finally {
                if ( reader != null ) {
                    reader.Close();
                }

                cmd.Connection.Close();
            }

            return users;
        }

        public override MembershipUserCollection FindUsersByEmail( string portalAlias, string emailToMatch, int pageIndex, int pageSize, out int totalRecords ) {
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_Membership_FindUsersByEmail";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = new SqlConnection( connectionString );

            cmd.Parameters.Add( "@ApplicationName", SqlDbType.NVarChar, 256 ).Value = portalAlias;
            cmd.Parameters.Add( "@EmailToMatch", SqlDbType.NVarChar, 256 ).Value = emailToMatch;
            cmd.Parameters.Add( "@PageIndex", SqlDbType.Int ).Value = pageIndex;
            cmd.Parameters.Add( "@PageSize", SqlDbType.Int ).Value = pageSize;

            SqlParameter returnValue = cmd.Parameters.Add( "@ReturnValue", SqlDbType.Int );
            returnValue.Direction = ParameterDirection.ReturnValue;

            MembershipUserCollection users = new MembershipUserCollection();

            SqlDataReader reader = null;

            try {
                cmd.Connection.Open();

                using ( reader = cmd.ExecuteReader() ) {

                    while ( reader.Read() ) {
                        RainbowUser u = GetUserFromReader( reader );
                        LoadUserProfile( u );
                        users.Add( u );
                    }

                    reader.Close();
                    totalRecords = ( int )returnValue.Value;
                }
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "FindUsersByEmail" );

                    throw new RainbowMembershipProviderException( "Error executing aspnet_Membership_FindUsersByEmail stored proc", e );
                }
                else {
                    throw e;
                }
            }
            finally {
                if ( reader != null ) {
                    reader.Close();
                }

                cmd.Connection.Close();
            }

            return users;
        }

        public override string ResetPassword( string portalAlias, string username, string answer ) {
            if ( !EnablePasswordReset ) {
                throw new NotSupportedException( "Password reset is not enabled." );
            }

            if ( answer == null ) {
                answer = string.Empty;
            }

            string newPassword = Membership.GeneratePassword( _newPasswordLength, MinRequiredNonAlphanumericCharacters );

            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs( username, newPassword, false );

            OnValidatingPassword( args );

            if ( args.Cancel ) {
                if ( args.FailureInformation != null ) {
                    throw args.FailureInformation;
                }
                else {
                    throw new RainbowMembershipProviderException( "Reset password canceled due to password validation failure." );
                }
            }

            string passwordSalt = string.Empty;
            string encodedPassword;
            if ( PasswordFormat == MembershipPasswordFormat.Hashed ) {
                encodedPassword = EncodePassword( passwordSalt +  newPassword );
            }
            else {
                encodedPassword = EncodePassword( newPassword );
            }

            SqlConnection conn = new SqlConnection( connectionString );

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_Membership_ResetPassword";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = conn;

            cmd.Parameters.Add( "@ApplicationName", SqlDbType.NVarChar, 256 ).Value = portalAlias;
            cmd.Parameters.Add( "@UserName", SqlDbType.NVarChar, 256 ).Value = username;
            cmd.Parameters.Add( "@NewPassword", SqlDbType.NVarChar, 128 ).Value = encodedPassword;
            cmd.Parameters.Add( "@MaxInvalidPasswordAttempts", SqlDbType.Int ).Value = MaxInvalidPasswordAttempts;
            cmd.Parameters.Add( "@PasswordAttemptWindow", SqlDbType.Int ).Value = PasswordAttemptWindow;
            cmd.Parameters.Add( "@PasswordSalt", SqlDbType.NVarChar, 128 ).Value = passwordSalt;
            cmd.Parameters.Add( "@CurrentTimeUtc", SqlDbType.DateTime ).Value = DateTime.Now;
            cmd.Parameters.Add( "@PasswordFormat", SqlDbType.Int ).Value = PasswordFormat;
            cmd.Parameters.Add( "@PasswordAnswer", SqlDbType.NVarChar, 128 ).Value = answer;

            SqlParameter returnCodeParam = cmd.Parameters.Add( "@ReturnCode", SqlDbType.Int );
            returnCodeParam.Direction = ParameterDirection.ReturnValue;

            try {
                conn.Open();

                cmd.ExecuteNonQuery();

                int returnCode = ( int )returnCodeParam.Value;

                switch ( returnCode ) {
                    case _errorCode_UserNotFound:
                        throw new RainbowMembershipProviderException( "The supplied user name is not found." );
                    case _errorCode_IncorrectPasswordAnswer:
                        throw new MembershipPasswordException( "The supplied password answer is incorrect." );
                    case _errorCode_UserLockedOut:
                        throw new RainbowMembershipProviderException( "The supplied user is locked out." );
                    case -1:
                        throw new RainbowMembershipProviderException( "Error resetting password" );
                }

                return newPassword;
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "ResetPassword" );
                }

                throw new RainbowMembershipProviderException( "Error executing aspnet_Membership_ResetPassword stored proc", e );
            }
            finally {
                conn.Close();
            }
        }

        #endregion

        #region Private helper methods 
        
        /// <summary>
        /// A helper function to retrieve config values from the configuration file. 
        /// </summary>
        /// <param name="configValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private string GetConfigValue( string configValue, string defaultValue ) {
            if ( String.IsNullOrEmpty( configValue ) )
                return defaultValue;

            return configValue;
        }

        /// <summary>
        /// Encrypts, Hashes, or leaves the password clear based on the PasswordFormat.
        /// </summary>
        /// <param name="password">the password</param>
        /// <returns></returns>
        private string EncodePassword( string password ) {
            string encodedPassword = password;

            switch ( PasswordFormat ) {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Encrypted:
                    encodedPassword = Convert.ToBase64String( EncryptPassword( Encoding.Unicode.GetBytes( password ) ) );
                    break;
                case MembershipPasswordFormat.Hashed:
                    System.Security.Cryptography. HMACSHA1 hash = new HMACSHA1();
                    hash.Key = HexToByte( _encryptionKey );
                    encodedPassword = Convert.ToBase64String( hash.ComputeHash( Encoding.Unicode.GetBytes( password ) ) );
                    break;
                default:
                    throw new RainbowMembershipProviderException( "Unsupported password format." );
            }

            return encodedPassword;
        }

        /// <summary>
        /// Decrypts or leaves the password clear based on the PasswordFormat.
        /// </summary>
        /// <param name="encodedPassword"></param>
        /// <returns></returns>
        private string UnEncodePassword( string encodedPassword ) {
            string password = encodedPassword;

            switch ( PasswordFormat ) {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Encrypted:
                    password =
                      Encoding.Unicode.GetString( DecryptPassword( Convert.FromBase64String( password ) ) );
                    break;
                case MembershipPasswordFormat.Hashed:
                    throw new RainbowMembershipProviderException( "Cannot unencode a hashed password." );
                default:
                    throw new RainbowMembershipProviderException( "Unsupported password format." );
            }

            return password;
        }

        /// <summary>
        /// A helper function that writes exception detail to the event log. Exceptions are written to the event log as a security 
        /// measure to avoid private database details from being returned to the browser. If a method does not return a status
        /// or boolean indicating the action succeeded or failed, a generic exception is also thrown by the caller.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="action"></param>
        private void WriteToEventLog( Exception e, string action ) {
            EventLog log = new EventLog();
            log.Source = eventSource;
            log.Log = eventLog;

            string message = "An exception occurred communicating with the data source.\n\n";
            message += "Action: " + action + "\n\n";
            message += "Exception: " + e.ToString();

            log.WriteEntry( message );
        }

        /// <summary>
        /// Converts a hexadecimal string to a byte array. Used to convert encryption key values from the configuration.
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private byte[] HexToByte( string hexString ) {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for ( int i = 0; i < returnBytes.Length; i++ )
                returnBytes[i] = Convert.ToByte( hexString.Substring( i * 2, 2 ), 16 );
            return returnBytes;
        }

        /// <summary>
        /// A helper function that takes the current row from the SqlDataReader and hydrates a MembershiUser from the values. 
        /// Called by the MembershipUser.GetUser implementation.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected virtual RainbowUser GetUserFromReader( SqlDataReader reader ) {
            string username = reader.IsDBNull( 0 ) ? string.Empty : reader.GetString( 0 );
            string email = reader.IsDBNull( 1 ) ? string.Empty : reader.GetString( 1 );
            string passwordQuestion = reader.IsDBNull( 2 ) ? string.Empty : reader.GetString( 2 );
            string comment = reader.IsDBNull( 3 ) ? string.Empty : reader.GetString( 3 );
            bool isApproved = reader.IsDBNull( 4 ) ? false : reader.GetBoolean( 4 );
            DateTime creationDate = reader.IsDBNull( 5 ) ? DateTime.Now : reader.GetDateTime( 5 );
            DateTime lastLoginDate = reader.IsDBNull( 6 ) ? DateTime.Now : reader.GetDateTime( 6 );
            DateTime lastActivityDate = reader.IsDBNull( 7 ) ? DateTime.Now : reader.GetDateTime( 7 );
            DateTime lastPasswordChangedDate = reader.IsDBNull( 8 ) ? DateTime.Now : reader.GetDateTime( 8 );

            Guid providerUserKey = reader.GetGuid( 9 );
            bool isLockedOut = reader.IsDBNull( 10 ) ? false : reader.GetBoolean( 10 );
            DateTime lastLockedOutDate = reader.IsDBNull( 11 ) ? DateTime.Now : reader.GetDateTime( 11 );

            RainbowUser u = InstanciateNewUser( this.Name, username, providerUserKey, email, passwordQuestion, comment, isApproved,
                 isLockedOut, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, lastLockedOutDate );

            return u;
        }

        /// <summary>
        /// Compares password values based on the MembershipPasswordFormat.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="dbpassword"></param>
        /// <returns></returns>
        private bool CheckPassword( string password, string dbpassword, string passwordSalt, MembershipPasswordFormat passwordFormat ) {
            string pass1 = password;
            string pass2 = dbpassword;

            switch ( passwordFormat ) {
                case MembershipPasswordFormat.Encrypted:
                    pass1 = EncodePassword( dbpassword );
                    break;
                case MembershipPasswordFormat.Hashed:
                    pass1 = this.EncodePassword( passwordSalt + password );
                    break;
                default:
                    break;
            }

            return ( pass1.Equals( pass2 ) );
        }

        #endregion
    }
}