namespace DDVG.Messages {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public class BaseMessage {
        public int _destinationEntityId;
        public MessageType _messageType;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="destinationEntityId"></param>
        /// <param name="messageType"></param>
        protected BaseMessage(int destinationEntityId, MessageType messageType) {
            this._destinationEntityId = destinationEntityId;
            this._messageType = messageType;
        }
    }
}
