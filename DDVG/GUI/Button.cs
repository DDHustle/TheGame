using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;

namespace TheGame.GUI
{
    class Button : IDisposable
    {
        Vector2f Position;
        Vector2f Size;
        public String TextStr;
        public RectangleShape rect;
        Text TextObj;
        Font FontObj;
        Renderer Parent;
        public event EventHandler MouseEnter;
        public event EventHandler MouseLeave;
        public event EventHandler MouseRelease;

        //stupid public vars
        public bool increasingInSize;
        public bool soundEffectPlayed;
        

        public Button(Renderer R, Vector2f pos, Vector2f size, String text, Font f, float TextOffsetX = 0, float TextOffsetY = 0)
        {
            //pos will determine where the rectangle starts, text will be offset
            Position = pos;
            Size = size;
            TextStr = text;
            Parent = R;
            FontObj = f;

            TextObj = new Text();
            TextObj.Font = f;
            TextObj.Style = Text.Styles.Bold;
            TextObj.CharacterSize = 30;
            TextObj.DisplayedString = TextStr;
            TextObj.Color = Color.Black;
            TextObj.Position = new Vector2f(pos.X + 10 + TextOffsetX, pos.Y + 5 + TextOffsetY);

            rect = new RectangleShape();
            rect.Position = pos;
            rect.Size = size;
            rect.OutlineColor = Color.Black;
            rect.OutlineThickness = 4;

            R.MouseMoved += (S, E) => HandleMouseMove(E);
            R.MouseButtonReleased += HandleMouseRelease;
            MouseEnter = new EventHandler(MouseEnterHandler);
            MouseLeave = new EventHandler(MouseLeaveHandler);
            MouseRelease = new EventHandler(MouseReleaseHandler);
        }

        private void HandleMouseRelease(object sender, MouseButtonEventArgs e)
        {
            if (e.X >= Position.X && e.X <= Position.X + Size.X && e.Y >= Position.Y && e.Y <= Position.Y + Size.Y)
            {
                MouseRelease(this, e);
            }
        }

        private void HandleMouseMove(MouseMoveEventArgs e)
        {
            if (e.X >= Position.X && e.X <= Position.X + Size.X && e.Y >= Position.Y && e.Y <= Position.Y + Size.Y)
            {
                MouseEnter(this, e);
            }
            else
            {
                MouseLeave(this, e);
            }
        }
        
        public virtual void Dispose()
        {
        }

        public virtual void MouseEnterHandler(Object sender, EventArgs e)
        {
            
        }

        public virtual void MouseLeaveHandler(Object sender, EventArgs e)
        {
           
        }

        public virtual void MouseReleaseHandler(Object sender, EventArgs e)
        {

        }


        public virtual void Update(float T)
        {

        }

        public virtual void Render(Renderer R)
        {
            R.Draw(rect);
            R.Draw(TextObj);
        }
    }
}
