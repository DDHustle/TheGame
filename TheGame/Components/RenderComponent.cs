namespace TheGame.Components 
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public class RenderComponent : BaseComponent 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public override bool SendMessage(Messages.BaseMessage message) 
        {

            switch (message._messageType) 
            {
                case TheGame.Messages.MessageType.GetPosition: {
                    }

                    break;
                case TheGame.Messages.MessageType.SetPosition: {
                        Console.WriteLine("RenderComponent handling set position");
                    }

                    break;
                default:
                    return base.SendMessage(message);
            }
            return true;
        }

    }
}
