using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Framework.Content
{
    class RainbowItemEventArgs : EventArgs
    {
        /// <summary>
        /// ThreadHandler, which has generated the event.
        /// </summary>
        private RainbowItem _item;

        /// <summary>
        /// The ThreadHandler from where this event was generated.
        /// </summary>
        public RainbowItem Handler
        {
            get
            {
                return _item;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="threadHandler">ThreadHandler object</param>
        public RainbowItemEventArgs(RainbowItem rainbowItem)
        {
            _item = rainbowItem;
        }
    }
}
