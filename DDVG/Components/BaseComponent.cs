namespace DDVG.Components {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DDVG.Messages;

    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseComponent {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual bool SendMessage(BaseMessage message) {
            return false;
        }
    }
}
