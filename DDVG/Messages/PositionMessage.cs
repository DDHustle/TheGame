namespace DDVG.Messages {
    using SFML.Window;

    /// <summary>
    /// 
    /// </summary>
    public class PositionMessage : BaseMessage {
        public Vector2f _position;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="destinationEntityId"></param>
        /// <param name="messageType"></param>
        /// <param name="position"></param>
        protected PositionMessage(int destinationEntityId, MessageType messageType, Vector2f position) :
            base(destinationEntityId, messageType) {
            this._position = position;
        }
    }
}
