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
        String TextStr;
        RectangleShape rect;
        Text TextObj;
        Font FontObj;
        Renderer Parent;
        public event EventHandler MouseEnter;
        public event EventHandler MouseLeave;
        EventHandler EventHandler;

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
        }

        private void HandleMouseMove(MouseMoveEventArgs e)
        {
            if (e.X >= Position.X && e.X <= Position.X + Size.X && e.Y >= Position.Y && e.Y <= Position.Y + Size.Y)
            {
                EventHandler = MouseEnter;
                EventHandler(this, e);
            }
            else
            {
                EventHandler = MouseLeave;
                EventHandler(this, e);
            }
        }
        
        public virtual void Dispose()
        {
        }

        public virtual void MouseEnterHandler(MouseMoveEventArgs e)
        {
            
        }

        public virtual void MouseLeaveHandler(MouseMoveEventArgs e)
        {
           
        }

        public virtual void MouseClick(MouseMoveEventArgs e)
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
