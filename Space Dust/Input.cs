using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Space_Dust
{
    static class Input
    {
        private static KeyboardState keyboardState;
        private static MouseState mouseState;

        public static Vector2 MousePosition { get { return new Vector2(mouseState.X, mouseState.Y); } }

        public static void Update()
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }

        public static Vector2 GetMovementDirection()
        {
            Vector2 direction = new Vector2();
            if (keyboardState.IsKeyDown(Keys.A))
                direction.X -= 1;
            if (keyboardState.IsKeyDown(Keys.D))
                direction.X += 1;
            if (keyboardState.IsKeyDown(Keys.W))
                direction.Y -= 1;
            if (keyboardState.IsKeyDown(Keys.S))
                direction.Y += 1;

            // Clamp the length of the vector to a maximum of 1.
            if (direction.LengthSquared() > 1)
                direction.Normalize();

            return direction;
        }

        public static Vector2 GetAimDirection()
        {
            Vector2 WorldPosition = Vector2.Transform(MousePosition, Matrix.Invert(GameMain.PlayerCamera.Transform));
            Vector2 direction = WorldPosition - PlayerShip.Instance.Position;

            if (direction == Vector2.Zero)
                return Vector2.Zero;
            else
                return Vector2.Normalize(direction);
        }
    }
}
