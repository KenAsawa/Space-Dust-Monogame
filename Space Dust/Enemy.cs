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
        private List<IEnumerator<int>> behaviours = new List<IEnumerator<int>>();

        public Enemy(Texture2D image, Vector2 position, int pointValue, int hitpoints)
        {
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
            Velocity *= 0.8f; //Friction
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
        public static Enemy CreateSeeker(Vector2 position)
        {
            var enemy = new Enemy(Assets.Seeker, position, 50, 8);
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

                yield return 0;
            }
        }




    }
}
