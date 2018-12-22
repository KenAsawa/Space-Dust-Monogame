using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Dust
{
    static class EnemySpawner
    {
        static Random rand = new Random();
        static float seekerInverseSpawnChance = 80;
        static float largeAsteroidInverseSpawnChance = 80;
        static float mediumAsteroidInverseSpawnChance = 80;

        public static void Update()
        {
            if (!PlayerShip.Instance.IsDead && EntityManager.Count < 200)
            {
                if (rand.Next((int)seekerInverseSpawnChance) == 0)
                    EntityManager.Add(Enemy.CreateSeeker(GetSpawnPosition()));
                if (rand.Next((int)largeAsteroidInverseSpawnChance) == 0)
                    EntityManager.Add(Enemy.CreateLargeAsteroid(GetSpawnPosition()));
                if (rand.Next((int)mediumAsteroidInverseSpawnChance) == 0)
                    EntityManager.Add(Enemy.CreateMediumAsteroid(GetSpawnPosition()));
            }
        }

        public static Vector2 GetSpawnPosition()
        {
            Vector2 pos;
            do
            {
                pos = new Vector2(rand.Next((int)GameMain.ScreenSize.X*2) + PlayerShip.Instance.Position.X - GameMain.ScreenSize.X, rand.Next((int)GameMain.ScreenSize.Y*2) + PlayerShip.Instance.Position.Y - GameMain.ScreenSize.Y);
            }
            while (Vector2.DistanceSquared(pos, PlayerShip.Instance.Position) < 600 * 600);

            return pos;
        }

        public static void Reset()
        {
        }
    }
}
