namespace TheGame.Entities 
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using OpenTK.Graphics.OpenGL4;
    using SFML.Graphics;
    using SFML.Window;
    using TheGame.Components;
    using TheGame.Messages;
    /// <summary>
    /// 
    /// </summary>
    public class Entity 
    {
        public Entity(int uniqueID) {
            this.uniqueID = uniqueID;
        }

        public int UniqueID {
            get {
                return this.uniqueID;
            }
            set {
                this.uniqueID = value;
            }
        }

        public virtual void Update(float T) {
        }

        public virtual void Render(Renderer R) {
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        public void AddComponent(BaseComponent component) {
            this.components.Add(component);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool SendMessage(BaseMessage message) {
            bool messageHandled = false;

            // Entity has a switch for any messages it cares about
            switch (message._messageType) {
                case MessageType.SetPosition: {
                        MessageSetPosition msgSetPos = message as MessageSetPosition;
                        position = msgSetPos._position;

                        messageHandled = true;
                        Console.WriteLine("Entity handled SetPosition");
                    }
                    break;
                case MessageType.GetPosition: {
                        MessageGetPosition msgGetPos = message as MessageGetPosition;
                        msgGetPos._position = position;
           
                        messageHandled = true;
                        Console.WriteLine("Entity handled GetPosition");
                    }
                    break;
                default:
                    return PassMessageToComponents(message);
            }

            // If the entity didn't handle the message but the component
            // did, we return true to signify it was handled by something.
            messageHandled |= PassMessageToComponents(message);

            return messageHandled;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool PassMessageToComponents(BaseMessage msg) {
            bool messageHandled = false;

            this.components.ForEach(c => messageHandled |= c.SendMessage(msg));

            return messageHandled;
        }

        private int uniqueID;
        private Vector2f position = new Vector2f();
        private List<BaseComponent> components = new List<BaseComponent>();
    }
}
