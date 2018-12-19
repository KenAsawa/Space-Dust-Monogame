using System;
using Microsoft.Xna.Framework;

namespace Space_Dust
{
    class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Entity target)
        {
            Transform = Matrix.CreateTranslation(
                //-target.Position.X - (target.Rectangle.Width / 2),
                //-target.Position.Y - (target.Rectangle.Height / 2),
                -target.Position.X - (target.Radius / 2),
                -target.Position.Y - (target.Radius / 2),
                0) * Matrix.CreateTranslation(
                    GameMain.Viewport.Width / 2,
                    GameMain.Viewport.Height / 2,
                    0);
        }
    }
}
