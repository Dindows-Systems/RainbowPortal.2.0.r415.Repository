using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Framework.Providers.RainbowRoleProvider {

    public class RainbowRole : IComparable {

        public RainbowRole( Guid roleId, string roleName ) {
            this.id = roleId;
            this.name = roleName;
            this.description = string.Empty;
        }

        public RainbowRole( Guid roleId, string roleName, string roleDescription ) {
            this.id = roleId;
            this.name = roleName;
            this.description = roleDescription;
        }

        private Guid id;

        public Guid Id {
            get {
                return id;
            }
            set {
                id = value;
            }
        }

        private string name;

        public string Name {
            get {
                return name;
            }
            set {
                name = value;
            }
        }

        private string description;

        public string Description {
            get {
                return description;
            }
            set {
                description = value;
            }
        }

        public override bool Equals( object obj ) {
            //Check for null and compare run-time types.
            if ( obj == null || GetType() != obj.GetType() ) {
                return false;
            }

            RainbowRole role = ( RainbowRole )obj;
            return ( id == role.id ) && ( name == role.name );
        }

        #region IComparable Members

        public int CompareTo( object obj ) {
            if ( obj is RainbowRole ) {
                RainbowRole role = ( RainbowRole )obj;
                return name.CompareTo( role.name );
            }
            throw new ArgumentException( "object is not a RainbowRole" );    
        }

        #endregion
    }
}
