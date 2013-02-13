using System.Security.Principal;
using System.Web.Security;
using System;
using Rainbow.Framework.Users.Data;
using Rainbow.Framework.Site.Configuration;
using System.Web;
using Rainbow.Framework.Providers.RainbowMembershipProvider;

namespace Rainbow.Framework.Security {
    /// <summary>
    /// RainbowPrincipal
    /// </summary>
    public class RainbowPrincipal : GenericPrincipal {

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RainbowPrincipal"/> class.
        /// </summary>
        /// <param name="identity">A basic implementation of <see cref="T:System.Security.Principal.IIdentity"></see> that represents any user.</param>
        /// <param name="roles">An array of role names to which the user represented by the identity parameter belongs.</param>
        /// <returns>
        /// A void value...
        /// </returns>
        public RainbowPrincipal( IIdentity identity, string[] roles )
            : base( identity, roles ) {
        }

        
        /// <summary>
        /// Get the Rainbow User
        /// </summary>
        /// <value></value>
        /// <returns>The <see cref="T:System.Security.Principal.GenericIdentity"></see> of the user represented by the <see cref="T:System.Security.Principal.GenericPrincipal"></see>.</returns>
        public new RainbowUser Identity {
            get {
                if ( base.Identity is MembershipUser ) {
                    ErrorHandler.Publish( LogLevel.Info, "RainbowPrincipal::Identity  -> base.Identity is MembershipUser" );
                    return base.Identity as RainbowUser;
                }

                UsersDB users = new UsersDB();
                return users.GetSingleUser( base.Identity.Name );
            }
        }

    }
}