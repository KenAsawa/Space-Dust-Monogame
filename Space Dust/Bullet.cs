using System;
using Microsoft.Xna.Framework;
namespace Space_Dust
{
    class Bullet : Entity
    {
        int lifetimeRemaining = 100;

        public Bullet(Vector2 position, Vector2 velocity)
        {
            image = Assets.PlayerBullet;
            Position = position;
            Velocity = velocity;
            Orientation = (float)(Velocity.ToAngle()+Math.PI/2); //The +Math.PI/2  corrects the angle.
            Radius = 8;
        }

        public override void Update()
        {
            if (Velocity.LengthSquared() > 0)
                Orientation = (float)(Velocity.ToAngle() + Math.PI / 2);

            Position += Velocity;

            // delete bullets that go off-screen
            if (lifetimeRemaining > 0)
                lifetimeRemaining--;
            else
                IsExpired = true;
        }

        public void Kill()
        {
            IsExpired = true;
        }
    }
}
