using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Dust
{
    //Player is an Entity
    class PlayerShip : Entity
    {
        private KeyboardState keyboardState;
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
            PlayerStatus.RemoveLife();
            EnemySpawner.Reset();
            if(PlayerStatus.Lives <= 0)
            {
                framesUntilRespawn = 300;
            }
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
                //Play laser sound
                if(rand.Next(2)==0)
                {
                    Assets.laser1.Play(0.8f, rand.NextFloat(-0.2f, 0.2f), 0);
                }
                else
                {
                    Assets.laser2.Play(0.8f, rand.NextFloat(-0.2f, 0.2f), 0);
                }
                
            }
                if (cooldownRemaining > 0)
                cooldownRemaining--;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsDead)
            {
                base.Draw(spriteBatch);
                spriteBatch.Draw(Assets.fireEffect, new Vector2(Position.X, Position.Y), null, color, Orientation, new Vector2(Assets.fireEffect.Width * (.50f), -(Assets.fireEffect.Height) * (1.2f)), 1f, 0, 0);
            }
            //spriteBatch.Draw(Assets.fireEffect, new Vector2(Position.X, Position.Y), null, color, Orientation, new Vector2(Size.X-92,Size.Y-113), 1f, 0, 0);
            //spriteBatch.Draw(Assets.fireEffect, new Vector2(Position.X, Position.Y), null, color, Orientation, new Vector2(Assets.Player.Width - 92, Assets.Player.Height - 113), 1f, 0, 0);


        }
    }
}
