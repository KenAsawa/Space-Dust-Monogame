using System;
using Microsoft.Xna.Framework;

namespace Space_Dust
{
    //Holds various methods that can be called from other classes.
    static class Extensions
    {
        public static float ToAngle(this Vector2 vector)
        {
            return (float)(Math.Atan2(vector.Y, vector.X));
        }
        public static float NextFloat(this Random rand, float minValue, float maxValue)
        {
            return (float)rand.NextDouble() * (maxValue - minValue) + minValue;
        }
        public static Vector2 FromPolar(float angle, float magnitude)
        {
            return magnitude * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }
        public static Vector2 ScaleTo(this Vector2 vector, float length)
        {
            return vector * (length / vector.Length());
        }
    }

}
