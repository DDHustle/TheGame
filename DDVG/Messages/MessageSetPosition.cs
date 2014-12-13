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
    public class MessageSetPosition : PositionMessage {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="destinationEntityID"></param>
        /// <param name="position"></param>
        public MessageSetPosition(int destinationEntityID, Vector2f position) :
            base(destinationEntityID, MessageType.SetPosition, position) {
        }
    }

}
