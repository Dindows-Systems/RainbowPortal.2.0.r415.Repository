using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration.Provider;

namespace Rainbow.Framework.Providers.RainbowRoleProvider {

    [global::System.Serializable]
    public class RainbowRoleProviderException : ProviderException {

        public RainbowRoleProviderException() {
        }

        public RainbowRoleProviderException( string message )
            : base( message ) {
        }

        public RainbowRoleProviderException( string message, Exception inner )
            : base( message, inner ) {
        }

        protected RainbowRoleProviderException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context )
            : base( info, context ) {
        }
    }
}
