using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Dust
{
    public class Button : Component
    {
        private MouseState _currentMouse;
        private MouseState _previousMouse;
        public Rectangle rectangle { get { return new Rectangle((int)Position.X, (int)Position.Y, Assets.MenuButton.Width, Assets.MenuButton.Height); } }
        private bool _isHovering;
        public string Text { get; set; }
        public event EventHandler Click;
        public bool Clicked { get; private set; }
        public Vector2 Position { get; set; }

        public Button()
        {
        }

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(rectangle))
            {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color buttonColor = Color.White;
            if(_isHovering)
            {
                buttonColor = Color.Gray;
            }
            spriteBatch.Draw(Assets.MenuButton, rectangle, buttonColor);
            if(!string.IsNullOrEmpty(Text))
            {
                var x = (rectangle.X + (rectangle.Width / 2)) - (Assets.Font.MeasureString(Text).X / 2);
                var y = (rectangle.Y + (rectangle.Height / 2)) - (Assets.Font.MeasureString(Text).Y / 2);
                spriteBatch.DrawString(Assets.Font, Text, new Vector2(x, y), Color.Black);
            }

        }
        

    }
}
