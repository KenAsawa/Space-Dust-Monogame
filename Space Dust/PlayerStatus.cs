using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Dust
{
    static class PlayerStatus
    {
        public static int HighScore;

        public static int Lives { get; private set; }
        public static int Score { get; private set; }
        public static bool IsGameOver { get { return Lives == 0; } }

        private static float GameOverTimeLeft;    // time until the current multiplier expires
        private static int scoreForExtraLife;       // score required to gain an extra life

        private const string highScoreFilename = "highscore.txt";

        // Static constructor
        static PlayerStatus()
        {
            HighScore = LoadHighScore();
            Reset();
        }

        public static void Reset()
        {
            if (Score > HighScore)
                SaveHighScore(HighScore = Score);
            Score = 0;
            Lives = 3;
            scoreForExtraLife = 2000;
            GameOverTimeLeft = 5; //Same as PlayerShip.frmesUntilRespawn
        }

        public static void Update(GameTime gameTime)
        {
            if (IsGameOver)
            {
                // update the GameOverTimeLeft timer
                GameOverTimeLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                //GameOverTimeLeft--;
                if (GameOverTimeLeft <= 0)
                {
                    Reset();
                }
            }
        }

        public static void AddPoints(int basePoints)
        {
            if (PlayerShip.Instance.IsDead)
                return;

            Score += basePoints;
            while (Score >= scoreForExtraLife)
            {
                scoreForExtraLife += 2000;
                Lives++;
            }
        }

        public static void RemoveLife()
        {
            Lives--;
        }

        private static int LoadHighScore()
        {
            // return the saved high score if possible and return 0 otherwise
            int score;
            return File.Exists(highScoreFilename) && int.TryParse(File.ReadAllText(highScoreFilename), out score) ? score : 0;
        }

        private static void SaveHighScore(int score)
        {
            File.WriteAllText(highScoreFilename, score.ToString());
        }
    }
}