using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Space_Dust
{
    static class Assets
    {
        public static Texture2D Player { get; private set; }
        public static Texture2D PlayerBullet { get; private set; }
        public static Texture2D Pointer { get; private set; }
        public static Texture2D Cursor { get; private set; }
        public static Texture2D MenuButton { get; private set; }
        public static Texture2D Background { get; private set; }
        public static SpriteFont Font { get; private set; }

        public static Texture2D fireEffect { get; private set; }
        public static Texture2D Seeker { get; private set; }
        public static Texture2D LargeAsteroid1 { get; private set; }
        public static Texture2D LargeAsteroid2 { get; private set; }
        public static Texture2D LargeAsteroid3 { get; private set; }
        public static Texture2D LargeAsteroid4 { get; private set; }
        public static Texture2D MediumAsteroid1 { get; private set; }
        public static Texture2D MediumAsteroid2 { get; private set; }

        public static SoundEffect laser1 { get; private set; }
        public static SoundEffect laser2 { get; private set; }
        public static SoundEffect hitAsteroid { get; private set; }
        public static SoundEffect hitMetal { get; private set; }
        public static SoundEffect explosion { get; private set; }
        public static Song bgsong1 { get; private set; }

        public static void Load(ContentManager content)
        {
            //Player
            Player = content.Load<Texture2D>("playerShip1_blue");
            PlayerBullet = content.Load<Texture2D>("laserBlue01");
            fireEffect = content.Load<Texture2D>("fire16");
            //Enemies
            Seeker = content.Load<Texture2D>("enemyRed4");
            LargeAsteroid1 = content.Load<Texture2D>("meteorBrown_big1");
            LargeAsteroid2 = content.Load<Texture2D>("meteorBrown_big2");
            LargeAsteroid3 = content.Load<Texture2D>("meteorBrown_big3");
            LargeAsteroid4 = content.Load<Texture2D>("meteorBrown_big4");
            MediumAsteroid1 = content.Load<Texture2D>("meteorBrown_med1");
            MediumAsteroid2 = content.Load<Texture2D>("meteorBrown_med3");
            
            //Sounds
            laser1 = content.Load<SoundEffect>("sfx_laser1");
            laser2 = content.Load<SoundEffect>("sfx_laser2");
            hitAsteroid = content.Load<SoundEffect>("gravel");
            hitMetal = content.Load<SoundEffect>("metal");
            explosion = content.Load<SoundEffect>("explosion-012");
            bgsong1 = content.Load<Song>("Break the Targets (Remix) - by Cyber Shaman");
            //bgsong1 = content.Load<Song>("boop");
            //Other
            Pointer = content.Load<Texture2D>("pointerBlue");
            Cursor = content.Load<Texture2D>("cursor");
            MenuButton = content.Load<Texture2D>("buttonBlue");
            Background = content.Load<Texture2D>("NebulaTile");
            Font = content.Load<SpriteFont>("Score");
        }
    }
}
