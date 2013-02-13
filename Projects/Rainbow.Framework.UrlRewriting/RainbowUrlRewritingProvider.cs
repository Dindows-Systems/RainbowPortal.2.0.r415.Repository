using System;
using System.Collections.Generic;
using System.Text;
using UrlRewritingNet.Configuration.Provider;

namespace Rainbow.Framework.UrlRewriting {

    public class RainbowUrlRewritingProvider : UrlRewritingProvider {


        public override UrlRewritingNet.Web.RewriteRule CreateRewriteRule() {
            return new RainbowUrlRewritingRule();
        }
    }
}

