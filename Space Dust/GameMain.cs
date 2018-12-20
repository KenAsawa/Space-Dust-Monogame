using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Space_Dust
{
    
    public class GameMain : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private static Camera playerCamera;

        public static GameMain Instance { get; private set; }
        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }
        internal static Camera PlayerCamera { get => playerCamera; set => playerCamera = value; }

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
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(Sound.Music);
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            PlayerCamera = new Camera();
            Assets.Load(Content);
            Sound.Load(Content);
        }
        
        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();
            Input.Update();
            EntityManager.Update();
            EnemySpawner.Update();
            PlayerStatus.Update(gameTime);
            PlayerCamera.Follow(PlayerShip.Instance);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            //spriteBatch.Begin(transformMatrix: PlayerCamera.Transform, SpriteSortMode.Texture, BlendState.Additive);
            //spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, transformMatrix: PlayerCamera.Transform);
            spriteBatch.Begin(transformMatrix: PlayerCamera.Transform);
            spriteBatch.Draw(Assets.Background, new Vector2(0f, 0f), Color.White);
            //spriteBatch.Draw(Assets.Background, GraphicsDevice.Viewport.Bounds, GraphicsDevice.Viewport.Bounds, Color.White);
            EntityManager.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin();
            //Text
            spriteBatch.DrawString(Assets.Font, "Lives: " + PlayerStatus.Lives, new Vector2(5), Color.White);
            DrawRightAlignedString("Score: " + PlayerStatus.Score, 5);
            if (PlayerStatus.IsGameOver)
            {
                string text = "Game Over\n" +
                    "Your Score: " + PlayerStatus.Score + "\n" +
                    "High Score: " + PlayerStatus.HighScore;

                Vector2 textSize = Assets.Font.MeasureString(text);
                spriteBatch.DrawString(Assets.Font, text, ScreenSize / 2 - textSize / 2, Color.White);
            }
            //MousePointer
            spriteBatch.Draw(Assets.Pointer, new Vector2(Input.MousePosition.X - (24f), Input.MousePosition.Y - (24f)), Color.White); //Draw pointer.
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawRightAlignedString(string text, float y)
        {
            var textWidth = Assets.Font.MeasureString(text).X;
            spriteBatch.DrawString(Assets.Font, text, new Vector2(ScreenSize.X - textWidth - 5, y), Color.White);
        }
    }
}
