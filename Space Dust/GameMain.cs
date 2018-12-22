using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Space_Dust
{
    
    public class GameMain : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private static Camera playerCamera;
        Random rand = new Random();
        private List<Component> gameComponents;
        private List<Component> endgameComponents;

        public static GameMain Instance { get; private set; }
        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }
        internal static Camera PlayerCamera { get => playerCamera; set => playerCamera = value; }

        int currentSong;
        int songNameFade;
        string[] songNames = { "Break the Targets (Remix) - by Cyber Shaman" };

        public enum GameState { menuScreen, gameScreen, endScreen};
        GameState currentState = GameState.menuScreen;
        public GameMain()
        {
            Instance = this;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1200;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 800;   // set this value to the desired height of your window
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            
        }

        protected override void Initialize()
        {
            base.Initialize();
            EntityManager.Add(PlayerShip.Instance);
            MediaPlayer.Volume = 0.4f;
            MediaPlayer.IsRepeating = true;
            List<Song> bgm = new List<Song>();
            bgm.Add(Assets.bgsong1);
            if (MediaPlayer.State != MediaState.Playing && MediaPlayer.PlayPosition.TotalSeconds == 0.0f)
            {
                
                currentSong = rand.Next(bgm.Count);
                MediaPlayer.Play(bgm[currentSong]);
            }
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            PlayerCamera = new Camera();
            Assets.Load(Content);

            var playButton = new Button()
            {
                Position = new Vector2((Viewport.Width - Assets.MenuButton.Width) / 2, (Viewport.Height - Assets.MenuButton.Height) / 2  +25),
                Text = "Play",
            };
            playButton.Click += PlayButton_Click;
            var quitButton = new Button()
            {
                Position = new Vector2((Viewport.Width- Assets.MenuButton.Width)/2, (Viewport.Height - Assets.MenuButton.Height) / 2 +75),
                Text = "Quit",
            };
            quitButton.Click += QuitButton_Click;
            var restartButton = new Button()
            {
                Position = new Vector2((Viewport.Width - Assets.MenuButton.Width) / 2, (Viewport.Height - Assets.MenuButton.Height) / 2 + 125),
                Text = "Restart",
            };
            restartButton.Click += RestartButton_Click;

            gameComponents = new List<Component>()
            {
                playButton,
                quitButton,
            };

            endgameComponents = new List<Component>()
            {
                restartButton,
            };
        }
        
        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            
            if (currentState == GameState.menuScreen)
            {
                Input.Update();
                foreach (var component in gameComponents)
                    component.Update(gameTime);
            }
            if (currentState == GameState.gameScreen)
            {
                Input.Update();
                EntityManager.Update();
                EnemySpawner.Update();
                PlayerStatus.Update(gameTime);
                PlayerCamera.Follow(PlayerShip.Instance);
            }
            if (currentState == GameState.endScreen)
            {
                Input.Update();
                foreach (var component in endgameComponents)
                    component.Update(gameTime);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            if(currentState == GameState.menuScreen)
            {
                spriteBatch.Begin();
                foreach (var component in gameComponents)
                    component.Draw(gameTime, spriteBatch);
                spriteBatch.Draw(Assets.Cursor, new Vector2(Input.MousePosition.X, Input.MousePosition.Y), Color.White); //Draw pointer.
                spriteBatch.End();
            }
            else if (currentState == GameState.gameScreen)
            {
                spriteBatch.Begin(transformMatrix: PlayerCamera.Transform);
                for(int i = -2; i < 3; i++)
                {
                    for(int j = -2; j < 3; j++)
                    {
                        spriteBatch.Draw(Assets.Background, new Vector2(i*1024f, j*1024f), Color.White);
                    }
                }
                EntityManager.Draw(spriteBatch);
                spriteBatch.End();

                spriteBatch.Begin();
                //Text
                spriteBatch.DrawString(Assets.Font, "Lives: " + PlayerStatus.Lives, new Vector2(5), Color.White);
                DrawRightAlignedString("Score: " + PlayerStatus.Score, 5);
                if (songNameFade > 0)
                {
                    spriteBatch.DrawString(Assets.Font, "Current Song: " + songNames[currentSong], new Vector2(5, ScreenSize.Y - 40), Color.White * (songNameFade / 60f));
                    songNameFade--;
                }
                if (PlayerStatus.IsGameOver)
                {
                    currentState = GameState.endScreen;
                }
                //MousePointer
                spriteBatch.Draw(Assets.Pointer, new Vector2(Input.MousePosition.X - (24f), Input.MousePosition.Y - (24f)), Color.White); //Draw pointer.
                spriteBatch.End();
            }
            else if (currentState == GameState.endScreen)
            {
                spriteBatch.Begin();
                string text = "Game Over\n" +
                        "Your Score: " + PlayerStatus.Score + "\n" +
                        "High Score: " + PlayerStatus.HighScore;

                Vector2 textSize = Assets.Font.MeasureString(text);
                spriteBatch.DrawString(Assets.Font, text, ScreenSize / 2 - textSize / 2, Color.White);
                foreach (var component in endgameComponents)
                    component.Draw(gameTime, spriteBatch);
                spriteBatch.Draw(Assets.Cursor, new Vector2(Input.MousePosition.X, Input.MousePosition.Y), Color.White); //Draw pointer.
                spriteBatch.End();
            }
            else
            {

            }
            base.Draw(gameTime);
        }

        //Other functions
        private void DrawRightAlignedString(string text, float y)
        {
            var textWidth = Assets.Font.MeasureString(text).X;
            spriteBatch.DrawString(Assets.Font, text, new Vector2(ScreenSize.X - textWidth - 5, y), Color.White);
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            currentState = GameState.gameScreen;
            songNameFade = 160;
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            PlayerStatus.Reset();
            currentState = GameState.gameScreen;
            songNameFade = 160;
        }
        
    }
}
