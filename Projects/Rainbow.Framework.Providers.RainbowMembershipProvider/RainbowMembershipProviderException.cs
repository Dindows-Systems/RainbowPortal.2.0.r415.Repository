using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration.Provider;

namespace Rainbow.Framework.Providers.RainbowMembershipProvider {

    /// <summary>
    /// Rainbow-specific provider exception
    /// </summary>
    [global::System.Serializable]
    public class RainbowMembershipProviderException : ProviderException {

        /// <summary>
        /// Initializes a new instance of the <see cref="RainbowMembershipProviderException"/> class.
        /// </summary>
        public RainbowMembershipProviderException() {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RainbowMembershipProviderException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public RainbowMembershipProviderException( string message )
            : base( message ) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RainbowMembershipProviderException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public RainbowMembershipProviderException( string message, Exception inner )
            : base( message, inner ) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RainbowMembershipProviderException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the information to deserialize.</param>
        /// <param name="context">Contextual information about the source or destination.</param>
        protected RainbowMembershipProviderException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context ) : base( info, context ) {
        }
    }
}
