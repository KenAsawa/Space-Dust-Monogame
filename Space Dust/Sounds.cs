using System;
using System.Linq;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Space_Dust
{
    static class Sound
    {
        public static Song Music { get; private set; }
        private static readonly Random rand = new Random();

        private static SoundEffect[] explosions;
        // return a random explosion sound
        public static SoundEffect Explosion { get { return explosions[rand.Next(explosions.Length)]; } }

        private static SoundEffect[] shots;
        public static SoundEffect Shot { get { return shots[rand.Next(shots.Length)]; } }

        public static SoundEffect hitAsteroid { get; private set; }
        public static SoundEffect hitMetal { get; private set; }


        public static void Load(ContentManager content)
        {
            //Music = content.Load<Song>("Sound/Music");

            // These linq expressions are just a fancy way loading all sounds of each category into an array.
            //explosions = Enumerable.Range(1, 8).Select(x => content.Load<SoundEffect>("Sound/explosion-0" + x)).ToArray();
            shots = Enumerable.Range(1, 2).Select(x => content.Load<SoundEffect>("sfx_laser" + x)).ToArray();
            hitAsteroid = content.Load<SoundEffect>("gravel");
            hitMetal = content.Load<SoundEffect>("metal");
        }
    }
}
