namespace TheGame.Messages {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using OpenTK;
    using SFML.Window;

    /// <summary>
    /// 
    /// </summary>
    public class MessageGetPosition : PositionMessage {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="destinationEntityID"></param>
        public MessageGetPosition(int destinationEntityID) :
            base(destinationEntityID, MessageType.GetPosition, new Vector2f(0.0f, 0.0f)) {
        }
    }
}
