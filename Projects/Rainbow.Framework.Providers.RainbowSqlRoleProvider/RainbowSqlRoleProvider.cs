using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;

using Rainbow.Framework.Providers.RainbowRoleProvider;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using Rainbow.Framework.Providers.RainbowMembershipProvider;
using System.Collections;
using System.Configuration.Provider;

namespace Rainbow.Framework.Providers.RainbowRoleProvider {

    public class RainbowSqlRoleProvider : RainbowRoleProvider {

        protected string pApplicationName;
        protected string connectionString;

        private string eventSource = "RainbowSqlRoleProvider";
        private string eventLog = "Application";

        public override void Initialize( string name, System.Collections.Specialized.NameValueCollection config ) {

            //
            // Initialize values from web.config.
            //

            if ( config == null )
                throw new ArgumentNullException( "config" );

            if ( name == null || name.Length == 0 )
                name = "RainbowSqlRoleProvider";

            if ( String.IsNullOrEmpty( config["description"] ) ) {
                config.Remove( "description" );
                config.Add( "description", "Rainbow Sql Role provider" );
            }

            // Initialize the abstract base class.
            base.Initialize( name, config );

            pApplicationName = GetConfigValue( config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath );
            pWriteExceptionsToEventLog = Convert.ToBoolean( GetConfigValue( config["writeExceptionsToEventLog"], "true" ) );

            // Initialize SqlConnection.
            ConnectionStringSettings ConnectionStringSettings = ConfigurationManager.ConnectionStrings[config["connectionStringName"]];

            if ( ConnectionStringSettings == null || ConnectionStringSettings.ConnectionString.Trim().Equals( string.Empty ) ) {
                throw new RainbowRoleProviderException( "Connection string cannot be blank." );
            }

            connectionString = ConnectionStringSettings.ConnectionString;
        }

        #region Overriden properties

        public override string ApplicationName {
            get {
                return pApplicationName;
            }
            set {
                pApplicationName = value;
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

        #region Overriden methods

        public override void AddUsersToRoles( string[] usernames, string[] roleNames ) {
            Guid[] userIds = new Guid[usernames.Length];
            Guid[] roleIds = new Guid[roleNames.Length];

            RainbowUser user = null;
            for ( int i = 0; i < usernames.Length; i++ ) {
                user = ( RainbowUser )Membership.GetUser( usernames[i] );

                if ( user == null ) {
                    throw new RainbowMembershipProviderException( "User " + usernames[i] + " doesn't exist" );
                }

                userIds[i] = user.ProviderUserKey;
            }

            RainbowRole role = null;
            for ( int i = 0; i < roleNames.Length; i++ ) {
                role = GetRoleByName( ApplicationName, roleNames[i] );
                roleIds[i] = role.Id;
            }

            AddUsersToRoles( ApplicationName, userIds, roleIds );
        }

        public override void CreateRole( string roleName ) {
            CreateRole( ApplicationName, roleName );
        }

        public override bool DeleteRole( string roleName, bool throwOnPopulatedRole ) {
            RainbowRole role = GetRoleByName( ApplicationName, roleName );

            return DeleteRole( ApplicationName, role.Id, throwOnPopulatedRole );
        }

        public override string[] FindUsersInRole( string roleName, string usernameToMatch ) {
            return FindUsersInRole( ApplicationName, roleName, usernameToMatch );
        }

        public override string[] GetAllRoles() {
            IList<RainbowRole> roles = GetAllRoles( ApplicationName );
                        
            string[] result = new string[roles.Count];

            for ( int i = 0; i < roles.Count; i++ ) {
                result[i] = roles[i].Name;
            }

            return result;
        }

        public override string[] GetRolesForUser( string username ) {

            RainbowUser user = ( RainbowUser )Membership.GetUser( username );

            IList<RainbowRole> roles = GetRolesForUser( ApplicationName, user.ProviderUserKey );

            string[] result = new string[roles.Count];

            for ( int i = 0; i < roles.Count; i++ ) {
                result[i] = roles[i].Name;
            }

            return result;            
        }

        public override string[] GetUsersInRole( string roleName ) {
            RainbowRole role = GetRoleByName( ApplicationName, roleName );
            return GetUsersInRole( ApplicationName, role.Id );
        }

        public override bool IsUserInRole( string username, string roleName ) {

            RainbowUser user = ( RainbowUser )Membership.GetUser( username );

            if ( user == null ) {
                throw new RainbowRoleProviderException( "User doesn't exist" );
            }

            RainbowRole role = GetRoleByName( ApplicationName, roleName );

            return IsUserInRole( ApplicationName, user.ProviderUserKey, role.Id );
        }

        public override void RemoveUsersFromRoles( string[] usernames, string[] roleNames ) {
            Guid[] userIds = new Guid[usernames.Length];
            Guid[] roleIds = new Guid[roleNames.Length];

            RainbowUser user = null;
            for ( int i = 0; i < usernames.Length; i++ ) {
                user = ( RainbowUser )Membership.GetUser( usernames[i] );

                if ( user == null ) {
                    throw new RainbowMembershipProviderException( "User " + usernames[i] + " doesn't exist" );
                }

                userIds[i] = user.ProviderUserKey;
            }

            RainbowRole role = null;
            for ( int i = 0; i < roleNames.Length; i++ ) {
                role = GetRoleByName( ApplicationName, roleNames[i] );
                roleIds[i] = role.Id;
            }

            RemoveUsersFromRoles( ApplicationName, userIds, roleIds );
        }

        public override bool RoleExists( string roleName ) {
            try {
                IList<RainbowRole> allRoles = GetAllRoles( ApplicationName );
                foreach ( RainbowRole role in allRoles ) {
                    if ( role.Name.Equals( roleName ) ) {
                        return true;
                    }
                }
                return false;
            }
            catch {
                return false;
            }
        }

        #endregion

        #region Rainbow-specific Provider methods

        public override void AddUsersToRoles( string portalAlias, Guid[] userIds, Guid[] roleIds ) {
            string userIdsStr = string.Empty;
            string roleIdsStr = string.Empty;

            foreach ( Guid userId in userIds ) {
                userIdsStr += userId.ToString() + ",";
            }
            userIdsStr = userIdsStr.Substring( 0, userIdsStr.Length - 1 );

            foreach ( Guid roleId in roleIds ) {
                roleIdsStr += roleId.ToString() + ",";
            }
            roleIdsStr = roleIdsStr.Substring( 0, roleIdsStr.Length - 1 );

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_UsersInRoles_AddUsersToRoles";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = new SqlConnection( connectionString );

            cmd.Parameters.Add( "@ApplicationName", SqlDbType.NVarChar, 256 ).Value = portalAlias;
            cmd.Parameters.Add( "@UserIds", SqlDbType.VarChar, 4000 ).Value = userIdsStr;
            cmd.Parameters.Add( "@RoleIds", SqlDbType.VarChar, 4000 ).Value = roleIdsStr;

            SqlParameter returnCodeParam = cmd.Parameters.Add( "@ReturnCode", SqlDbType.Int );
            returnCodeParam.Direction = ParameterDirection.ReturnValue;

            try {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                int returnCode = ( int )returnCodeParam.Value;

                switch ( returnCode ) {
                    case 0:
                        return;
                    case 2:
                        throw new RainbowRoleProviderException( "Application " + portalAlias + " doesn't exist" );
                    case 3:
                        throw new RainbowRoleProviderException( "One of the roles doesn't exist" );
                    case 4:
                        throw new RainbowMembershipProviderException( "One of the users doesn't exist" );
                    default:
                        throw new RainbowRoleProviderException( "aspnet_UsersInRoles_AddUsersToRoles returned error code " + returnCode );
                }
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "CreateRole" );
                }

                throw new RainbowRoleProviderException( "Error executing aspnet_UsersInRoles_AddUsersToRoles stored proc", e );
            }
            finally {
                cmd.Connection.Close();
            }
        }

        public override Guid CreateRole( string portalAlias, string roleName ) {

            if ( roleName.IndexOf( ',' ) != -1 ) {
                throw new RainbowRoleProviderException( "Role name can't contain commas" );
            }

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_Roles_CreateRole";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = new SqlConnection( connectionString );

            cmd.Parameters.Add( "@ApplicationName", SqlDbType.NVarChar, 256 ).Value = portalAlias;
            cmd.Parameters.Add( "@RoleName", SqlDbType.NVarChar, 256 ).Value = roleName;

            SqlParameter newRoleIdParam = cmd.Parameters.Add( "@NewRoleId", SqlDbType.UniqueIdentifier );
            newRoleIdParam.Direction = ParameterDirection.Output;

            SqlParameter returnCodeParam = cmd.Parameters.Add( "@ReturnCode", SqlDbType.Int );
            returnCodeParam.Direction = ParameterDirection.ReturnValue;

            SqlDataReader reader = null;
            try {
                cmd.Connection.Open();

                cmd.ExecuteNonQuery();

                int returnCode = ( int )returnCodeParam.Value;

                if ( returnCode != 0 ) {
                    throw new RainbowRoleProviderException( "Error creating role " + roleName );
                }

                return ( Guid )newRoleIdParam.Value;
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "CreateRole" );
                }

                throw new RainbowRoleProviderException( "Error executing aspnet_Roles_CreateRole stored proc", e );
            }
            finally {
                if ( reader != null ) {
                    reader.Close();
                }
                cmd.Connection.Close();
            }
        }

        public override bool DeleteRole( string portalAlias, Guid roleId, bool throwOnPopulatedRole ) {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_Roles_DeleteRole";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = new SqlConnection( connectionString );

            cmd.Parameters.Add( "@ApplicationName", SqlDbType.NVarChar, 256 ).Value = portalAlias;
            cmd.Parameters.Add( "@RoleId", SqlDbType.UniqueIdentifier ).Value = roleId;
            if ( throwOnPopulatedRole ) {
                cmd.Parameters.Add( "@DeleteOnlyIfRoleIsEmpty", SqlDbType.Bit ).Value = 1;
            }
            else {
                cmd.Parameters.Add( "@DeleteOnlyIfRoleIsEmpty", SqlDbType.Bit ).Value = 0;
            }

            SqlParameter returnCodeParam = cmd.Parameters.Add( "@ReturnCode", SqlDbType.Int );
            returnCodeParam.Direction = ParameterDirection.ReturnValue;

            SqlDataReader reader = null;
            try {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                int returnCode = ( int )returnCodeParam.Value;

                switch ( returnCode ) {
                    case 0:
                        return true;
                    case 1:
                        throw new RainbowRoleProviderException( "Application " + portalAlias + " doesn't exist" );
                    case 2:
                        throw new RainbowRoleProviderException( "Role has members and throwOnPopulatedRole is true" );
                    default:
                        throw new RainbowRoleProviderException( "Error deleting role" );
                }
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "DeleteRole" );
                }

                throw new RainbowRoleProviderException( "Error executing aspnet_Roles_DeleteRole stored proc", e );
            }
            catch ( Exception e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "DeleteRole" );
                }

                throw new RainbowRoleProviderException( "Error deleting role " + roleId.ToString(), e );
            }
            finally {
                if ( reader != null ) {
                    reader.Close();
                }
                cmd.Connection.Close();
            }
        }

        public override bool RenameRole( string portalAlias, Guid roleId, string newRoleName ) {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_rbRoles_RenameRole";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = new SqlConnection( connectionString );

            cmd.Parameters.Add( "@RoleId", SqlDbType.UniqueIdentifier ).Value = roleId;
            cmd.Parameters.Add( "@NewRoleName", SqlDbType.VarChar, 256 ).Value = newRoleName;

            SqlParameter returnCodeParam = cmd.Parameters.Add( "@ReturnCode", SqlDbType.Int );
            returnCodeParam.Direction = ParameterDirection.ReturnValue;

            SqlDataReader reader = null;
            try {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                int returnCode = ( int )returnCodeParam.Value;

                switch ( returnCode ) {
                    case 0:
                        return true;
                    case 1:
                        throw new RainbowRoleProviderException( "Role doesn't exist" );
                    default:
                        throw new RainbowRoleProviderException( "Error renaming role" );
                }
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "DeleteRole" );
                }

                throw new RainbowRoleProviderException( "Error executing aspnet_rbRoles_RenameRole stored proc", e );
            }
            catch ( Exception e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "RenameRole" );
                }

                throw new RainbowRoleProviderException( "Error renaming role " + roleId.ToString(), e );
            }
            finally {
                if ( reader != null ) {
                    reader.Close();
                }
                cmd.Connection.Close();
            }
        }

        public override string[] FindUsersInRole( string portalAlias, string roleName, string usernameToMatch ) {
            throw new Exception( "The method or operation is not implemented." );
        }

        public override IList<RainbowRole> GetAllRoles( string portalAlias ) {

            IList<RainbowRole> result = new List<RainbowRole>();
            result.Insert( 0, new RainbowRole( AllUsersGuid, AllUsersRoleName, AllUsersRoleName ) );
            result.Insert( 1, new RainbowRole( AuthenticatedUsersGuid, AuthenticatedUsersRoleName, AuthenticatedUsersRoleName ) );
            result.Insert( 2, new RainbowRole( UnauthenticatedUsersGuid, UnauthenticatedUsersRoleName, UnauthenticatedUsersRoleName ) );

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_Roles_GetAllRoles";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = new SqlConnection( connectionString );

            cmd.Parameters.Add( "@ApplicationName", SqlDbType.NVarChar, 256 ).Value = portalAlias;

            SqlDataReader reader = null;
            try {
                cmd.Connection.Open();

                using ( reader = cmd.ExecuteReader() ) {

                    while ( reader.Read() ) {
                        RainbowRole role = GetRoleFromReader( reader );

                        result.Add( role );
                    }
                    reader.Close();
                }

                return result;
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "GetAllRoles" );
                }

                throw new RainbowRoleProviderException( "Error executing aspnet_Roles_GetAllRoles stored proc", e );
            }
            catch ( Exception e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "GetAllRoles" );
                }

                throw new RainbowRoleProviderException( "Error getting all roles", e );
            }
            finally {
                if ( reader != null ) {
                    reader.Close();
                }
                cmd.Connection.Close();
            }
        }

        public override IList<RainbowRole> GetRolesForUser( string portalAlias, Guid userId ) {
            IList<RainbowRole> result = new List<RainbowRole>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_UsersInRoles_GetRolesForUser";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = new SqlConnection( connectionString );

            cmd.Parameters.Add( "@ApplicationName", SqlDbType.NVarChar, 256 ).Value = portalAlias;
            cmd.Parameters.Add( "@UserId", SqlDbType.UniqueIdentifier ).Value = userId;

            SqlParameter returnCode = cmd.Parameters.Add( "@ReturnCode", SqlDbType.Int );
            returnCode.Direction = ParameterDirection.ReturnValue;

            SqlDataReader reader = null;
            try {
                cmd.Connection.Open();

                using ( reader = cmd.ExecuteReader() ) {

                    while ( reader.Read() ) {

                        RainbowRole role = GetRoleFromReader( reader );

                        result.Add( role );
                    }
                    reader.Close();
                    if ( ( ( int )returnCode.Value ) == 1 ) {
                        throw new RainbowRoleProviderException( "User doesn't exist" );
                    }
                }

                return result;
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "GetAllRoles" );
                }

                throw new RainbowRoleProviderException( "Error executing aspnet_Roles_GetAllRoles stored proc", e );
            }
            catch ( RainbowRoleProviderException ) {
                throw;
            }
            catch ( Exception e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "GetAllRoles" );
                }

                throw new RainbowRoleProviderException( "Error getting roles for user " + userId.ToString(), e );
            }
            finally {
                if ( reader != null ) {
                    reader.Close();
                }
                cmd.Connection.Close();
            }
        }

        public override string[] GetUsersInRole( string portalAlias, Guid roleId ) {
            ArrayList result = new ArrayList();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_UsersInRoles_GetUsersInRoles";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = new SqlConnection( connectionString );

            cmd.Parameters.Add( "@ApplicationName", SqlDbType.NVarChar, 256 ).Value = portalAlias;
            cmd.Parameters.Add( "@RoleId", SqlDbType.UniqueIdentifier ).Value = roleId;

            SqlParameter returnCode = cmd.Parameters.Add( "@ReturnCode", SqlDbType.Int );
            returnCode.Direction = ParameterDirection.ReturnValue;

            SqlDataReader reader = null;
            try {
                cmd.Connection.Open();

                using ( reader = cmd.ExecuteReader() ) {

                    while ( reader.Read() ) {
                        result.Add( reader.GetString( 0 ) );
                    }
                    reader.Close();
                    if ( ( ( int )returnCode.Value ) == 1 ) {
                        throw new RainbowRoleProviderException( "Role doesn't exist" );
                    }
                }

                return ( string[] )result.ToArray( typeof( string ) );
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "GetAllRoles" );
                }

                throw new RainbowRoleProviderException( "Error executing aspnet_UsersInRoles_GetUsersInRoles stored proc", e );
            }
            catch ( Exception e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "GetAllRoles" );
                }

                throw new RainbowRoleProviderException( "Error getting users for role " + roleId.ToString(), e );
            }
            finally {
                if ( reader != null ) {
                    reader.Close();
                }
                cmd.Connection.Close();
            }
        }

        public override bool IsUserInRole( string portalAlias, Guid userId, Guid roleId ) {

            if ( roleId.Equals( AllUsersGuid ) || roleId.Equals( AuthenticatedUsersGuid ) ) {
                return true;
            }
            else if ( roleId.Equals( UnauthenticatedUsersGuid ) ) {
                return false;
            }
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_UsersInRoles_IsUserInRole";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = new SqlConnection( connectionString );

            cmd.Parameters.Add( "@ApplicationName", SqlDbType.NVarChar, 256 ).Value = portalAlias;
            cmd.Parameters.Add( "@RoleId", SqlDbType.UniqueIdentifier ).Value = roleId;
            cmd.Parameters.Add( "@userId", SqlDbType.UniqueIdentifier ).Value = userId;

            SqlParameter returnCodeParam = cmd.Parameters.Add( "@ReturnCode", SqlDbType.Int );
            returnCodeParam.Direction = ParameterDirection.ReturnValue;

            try {
                cmd.Connection.Open();

                cmd.ExecuteNonQuery();

                int returnCode = ( int )returnCodeParam.Value;

                switch ( returnCode ) {
                    case 0:
                        return false;
                    case 1:
                        return true;
                    case 2:
                        throw new RainbowRoleProviderException( "User with Id = " + userId + " does not exist" );
                    case 3:
                        throw new RainbowRoleProviderException( "Role with Id = " + roleId + " does not exist" );
                    default:
                        throw new RainbowRoleProviderException( "Error executing IsUserInRole" );
                }
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "IsUserInRole" );
                }

                throw new RainbowRoleProviderException( "Error executing aspnet_UsersInRoles_IsUserInRole stored proc", e );
            }
            finally {
                cmd.Connection.Close();
            }
        }

        public override void RemoveUsersFromRoles( string portalAlias, Guid[] userIds, Guid[] roleIds ) {
            string userIdsStr = string.Empty;
            string roleIdsStr = string.Empty;

            foreach ( Guid userId in userIds ) {
                userIdsStr += userId.ToString() + ",";
            }
            userIdsStr = userIdsStr.Substring( 0, userIdsStr.Length - 1 );

            foreach ( Guid roleId in roleIds ) {
                roleIdsStr += roleId.ToString() + ",";
            }
            roleIdsStr = roleIdsStr.Substring( 0, roleIdsStr.Length - 1 );

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_UsersInRoles_RemoveUsersFromRoles";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = new SqlConnection( connectionString );

            cmd.Parameters.Add( "@ApplicationName", SqlDbType.NVarChar, 256 ).Value = portalAlias;
            cmd.Parameters.Add( "@UserIds", SqlDbType.VarChar, 4000 ).Value = userIdsStr;
            cmd.Parameters.Add( "@RoleIds", SqlDbType.VarChar, 4000 ).Value = roleIdsStr;

            SqlParameter returnCodeParam = cmd.Parameters.Add( "@ReturnCode", SqlDbType.Int );
            returnCodeParam.Direction = ParameterDirection.ReturnValue;

            try {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                int returnCode = ( int )returnCodeParam.Value;

                switch ( returnCode ) {
                    case 0:
                        return;
                    case 1:
                        throw new RainbowRoleProviderException( "One of the users is not in one of the specified roles" );
                    case 2:
                        throw new RainbowRoleProviderException( "Application " + portalAlias + " doesn't exist" );
                    case 3:
                        throw new RainbowRoleProviderException( "One of the roles doesn't exist" );
                    case 4:
                        throw new RainbowMembershipProviderException( "One of the users doesn't exist" );
                    default:
                        throw new RainbowRoleProviderException( "aspnet_UsersInRoles_RemoveUsersToRoles returned error code " + returnCode );
                }
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "CreateRole" );
                }

                throw new RainbowRoleProviderException( "Error executing aspnet_UsersInRoles_RemoveUsersToRoles stored proc", e );
            }
            finally {
                cmd.Connection.Close();
            }
        }

        public override bool RoleExists( string portalAlias, Guid roleId ) {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_Roles_RoleExists";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = new SqlConnection( connectionString );

            cmd.Parameters.Add( "@ApplicationName", SqlDbType.NVarChar, 256 ).Value = portalAlias;
            cmd.Parameters.Add( "@RoleId", SqlDbType.UniqueIdentifier ).Value = roleId;

            SqlParameter returnCodeParam = cmd.Parameters.Add( "@ReturnCode", SqlDbType.Int );
            returnCodeParam.Direction = ParameterDirection.ReturnValue;

            try {
                cmd.Connection.Open();

                cmd.ExecuteNonQuery();

                int returnCode = ( int )returnCodeParam.Value;
                return ( returnCode == 1 );
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "RoleExists" );
                }

                throw new RainbowRoleProviderException( "Error executing aspnet_Roles_RoleExists stored proc", e );
            }
            finally {
                cmd.Connection.Close();
            }
        }

        public override RainbowRole GetRoleByName( string portalAlias, string roleName ) {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "aspnet_Roles_GetRoleByName";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = new SqlConnection( connectionString );

            cmd.Parameters.Add( "@ApplicationName", SqlDbType.NVarChar, 256 ).Value = portalAlias;
            cmd.Parameters.Add( "@RoleName", SqlDbType.NVarChar, 256 ).Value = roleName;

            SqlParameter returnCodeParam = cmd.Parameters.Add( "@ReturnCode", SqlDbType.Int );
            returnCodeParam.Direction = ParameterDirection.ReturnValue;

            SqlDataReader reader = null;
            try {
                cmd.Connection.Open();

                using ( reader = cmd.ExecuteReader() ) {

                    if ( reader.Read() ) {

                        return GetRoleFromReader( reader );
                    }
                    else {
                        throw new RainbowRoleProviderException( "Role doesn't exist" );
                    }
                }
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "CreateRole" );
                }

                throw new RainbowRoleProviderException( "Error executing aspnet_UsersInRoles_IsUserInRole stored proc", e );
            }
            finally {
                if ( reader != null ) {
                    reader.Close();
                }
                cmd.Connection.Close();
            }
        }

        public override RainbowRole GetRoleById( Guid roleId ) {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT RoleId, RoleName, Description FROM aspnet_Roles WHERE RoleId=@RoleId";
            cmd.Parameters.Add( "@RoleId", SqlDbType.UniqueIdentifier ).Value = roleId;

            cmd.Connection = new SqlConnection( connectionString );

            SqlDataReader reader = null;
            try {
                cmd.Connection.Open();

                using ( reader = cmd.ExecuteReader() ) {

                    if ( reader.Read() ) {

                        return GetRoleFromReader( reader );
                    }
                    else {
                        throw new RainbowRoleProviderException( "Role doesn't exist" );
                    }
                }
            }
            catch ( SqlException e ) {
                if ( WriteExceptionsToEventLog ) {
                    WriteToEventLog( e, "GetRoleById" );
                }

                throw new RainbowRoleProviderException( "Error executing method GetRoleById", e );
            }
            finally {
                if ( reader != null ) {
                    reader.Close();
                }
                cmd.Connection.Close();
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


        private RainbowRole GetRoleFromReader( SqlDataReader reader ) {
            Guid roleId = reader.GetGuid( 0 );
            string roleName = reader.GetString( 1 );

            string roleDescription =  string.Empty;
            if ( !reader.IsDBNull( 2 ) ) {
                roleDescription = reader.GetString( 2 );
            }

            return new RainbowRole( roleId, roleName, roleDescription );
        }

        #endregion
    }
}
