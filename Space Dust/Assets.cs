using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Dust
{
    static class Assets
    {
        public static Texture2D Player { get; private set; }
        public static Texture2D PlayerBullet { get; private set; }
        public static Texture2D Pointer { get; private set; }
        public static Texture2D Background { get; private set; }
        public static Texture2D Seeker { get; private set; }
        public static SpriteFont Font { get; private set; }

        public static void Load(ContentManager content)
        {
            Player = content.Load<Texture2D>("playerShip1_blue");
            PlayerBullet = content.Load<Texture2D>("laserBlue01");
            Pointer = content.Load<Texture2D>("pointerBlue");
            Background = content.Load<Texture2D>("NebulaTile");
            Seeker = content.Load<Texture2D>("enemyRed4");
            Font = content.Load<SpriteFont>("Score");
        }
    }
}
