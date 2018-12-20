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
        public static SpriteFont Font { get; private set; }

        public static Texture2D Seeker { get; private set; }
        public static Texture2D LargeAsteroid1 { get; private set; }
        public static Texture2D LargeAsteroid2 { get; private set; }
        public static Texture2D LargeAsteroid3 { get; private set; }
        public static Texture2D LargeAsteroid4 { get; private set; }

        public static void Load(ContentManager content)
        {
            Player = content.Load<Texture2D>("playerShip1_blue");
            PlayerBullet = content.Load<Texture2D>("laserBlue01");
            Pointer = content.Load<Texture2D>("pointerBlue");
            Background = content.Load<Texture2D>("NebulaTile");
            Font = content.Load<SpriteFont>("Score");
            //Enemies
            Seeker = content.Load<Texture2D>("enemyRed4");
            LargeAsteroid1 = content.Load<Texture2D>("meteorBrown_big1");
            LargeAsteroid2 = content.Load<Texture2D>("meteorBrown_big2");
            LargeAsteroid3 = content.Load<Texture2D>("meteorBrown_big3");
            LargeAsteroid4 = content.Load<Texture2D>("meteorBrown_big4");
        }
    }
}
