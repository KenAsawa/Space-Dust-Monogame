using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Dust
{
    //Player is an Entity
    class PlayerShip : Entity
    {
        const int cooldownFrames = 10;
        int cooldownRemaining = 0;
        static Random rand = new Random();
        int framesUntilRespawn = 0;
        public bool IsDead { get { return framesUntilRespawn > 0; } }

        private static PlayerShip instance;

        public static PlayerShip Instance
        {
            get
            {
                if (instance == null)
                    instance = new PlayerShip();

                return instance;
            }
        }

        public PlayerShip()
        {
            image = Assets.Player;
            //Starts in the middle of the screen
            Position = GameMain.ScreenSize / 2;
            Radius = 10;
        }

        public void Kill()
        {
            framesUntilRespawn = 60;
            EnemySpawner.Reset();
        }

        public override void Update()
        {
            //Respawn
            if (IsDead)
            {
                framesUntilRespawn--;
                return;
            }
            //Movement
            const float acceleration = 0.7f;
            Velocity += acceleration * Input.GetMovementDirection(); //Keeps velocity when not pressing down.
            if (Velocity.LengthSquared() > 144) //Speed limit is 12f
            {
               Velocity.Normalize();
               Velocity *= 12f;
            }
            Position += Velocity;
            Velocity *= 0.983f; //Friction
                                //Clamps ship to screen borders.
                                //Position = Vector2.Clamp(Position, Size / 2, GameMain.ScreenSize - Size / 2);
            //Orientation
            var aim = Input.GetAimDirection();
            Orientation = (float)(aim.ToAngle() + Math.PI / 2);
            //Shooting
            if (aim.LengthSquared() > 0 && cooldownRemaining <= 0 && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                cooldownRemaining = cooldownFrames;
                float aimAngle = aim.ToAngle();
                Quaternion aimQuat = Quaternion.CreateFromYawPitchRoll(0, 0, aimAngle);

                float randomSpread = rand.NextFloat(-0.04f, 0.04f) + rand.NextFloat(-0.04f, 0.04f);
                Vector2 vel = Extensions.FromPolar(aimAngle + randomSpread, 15f);

                Vector2 offset = Vector2.Transform(new Vector2(25, -8), aimQuat);
                EntityManager.Add(new Bullet(Position + offset, vel));

                offset = Vector2.Transform(new Vector2(25, 8), aimQuat);
                EntityManager.Add(new Bullet(Position + offset, vel));
            }

            if (cooldownRemaining > 0)
                cooldownRemaining--;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsDead)
                base.Draw(spriteBatch);
        }
    }
}
