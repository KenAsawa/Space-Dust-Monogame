using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Dust
{
    class Enemy : Entity
    {
        private int timeUntilStart = 60;
        private int hitpoints;

        public bool IsActive { get { return timeUntilStart <= 0; } }
        public int pointValue { get; private set; }
        private static Random rand = new Random();
        private int enemyType; //0=Seeker, 1=Asteroid
        private List<IEnumerator<int>> behaviours = new List<IEnumerator<int>>();

        public Enemy(int enemyType, Texture2D image, Vector2 position, int pointValue, int hitpoints)
        {
            this.enemyType = enemyType;
            this.image = image;
            this.pointValue = pointValue;
            this.hitpoints = hitpoints;
            Position = position;
            Radius = image.Width / 2f;
            color = Color.Transparent;
        }

        public override void Update()
        {
            if (timeUntilStart <= 0)
            {
                ApplyBehaviours();
            }
            else
            {
                timeUntilStart--;
                color = Color.White * (1 - timeUntilStart / 60f);
            }
            Position += Velocity;
        }

        private void AddBehaviour(IEnumerable<int> behaviour)
        {
            behaviours.Add(behaviour.GetEnumerator());
        }

        private void ApplyBehaviours()
        {
            for (int i = 0; i < behaviours.Count; i++)
            {
                if (!behaviours[i].MoveNext())
                    behaviours.RemoveAt(i--);
            }
        }

        //Enemy hit
        public void WasShot()
        {
            hitpoints--;
            if(enemyType==1)
            {
                Assets.hitAsteroid.Play(0.5f, rand.NextFloat(-1f, 1f), 0);
            }
            if (enemyType == 0)
            {
                Assets.hitMetal.Play(0.2f, rand.NextFloat(-1f, -0.5f), 0);
            }
            if(hitpoints <= 0)
            {
                IsExpired = true;
                PlayerStatus.AddPoints(pointValue);
            }
        }

        //Insta-kill
        public void Kill()
        {
            IsExpired = true;
        }

        //Collide with enemy
        public void HandleCollision(Enemy other)
        {
            var d = Position - other.Position;
            Velocity += 10 * d / (d.LengthSquared() + 1);
        }

        //Create Enemies
        public static Enemy CreateLargeAsteroid(Vector2 position)
        {
            int type = rand.Next(4);
            switch(type)
            {
                case 0:{ var enemy = new Enemy(1,Assets.LargeAsteroid1, position, 50, 20);
                        enemy.AddBehaviour(enemy.MoveRandomly());
                        return enemy;}
                case 1:{ var enemy = new Enemy(1,Assets.LargeAsteroid2, position, 50, 20);
                        enemy.AddBehaviour(enemy.MoveRandomly());
                        return enemy;}
                case 2:{ var enemy = new Enemy(1,Assets.LargeAsteroid3, position, 50, 20);
                        enemy.AddBehaviour(enemy.MoveRandomly());
                        return enemy;}
                case 3:{ var enemy = new Enemy(1,Assets.LargeAsteroid4, position, 50, 20);
                        enemy.AddBehaviour(enemy.MoveRandomly());
                        return enemy;}
            }
            return null;
        }
        public static Enemy CreateSeeker(Vector2 position)
        {
            var enemy = new Enemy(0,Assets.Seeker, position, 50, 8);
            enemy.AddBehaviour(enemy.FollowPlayer());

            return enemy;
        }

        //Enemy behaviors
        IEnumerable<int> FollowPlayer(float acceleration = 1f)
        {
            while (true)
            {
                Velocity += (PlayerShip.Instance.Position - Position).ScaleTo(acceleration);
                if (Velocity != Vector2.Zero)
                    Orientation = (float)(Velocity.ToAngle() - (Math.PI / 2)); //The - Math.PI/2  corrects the angle.
                Velocity *= 0.8f; //Friction
                yield return 0;
            }
        }

        IEnumerable<int> MoveRandomly()
        {
            int Y = rand.Next(6) - 3;
            int X = rand.Next(6) - 3;
            Velocity.X = X;
            Velocity.Y = Y;
            while (true)
            {
                Orientation -= 0.005f;
                yield return 0;
            }
        }




    }
}
