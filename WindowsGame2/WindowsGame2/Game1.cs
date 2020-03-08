using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame2
{
        public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        enum GameStates { Start, Playing, Paused, RightWin, LeftWin}
        GameStates State = GameStates.Playing;
        public static Texture2D Tex;
        public static SpriteFont sf;
        public Paddle LeftPaddle = new Paddle(10);
        public Paddle RightPaddle = new Paddle(762);
        public static List<Ball> Balls = new List<Ball>();
        public static List<Ball> BallRemove = new List<Ball>();
        public KeyboardState ks = new KeyboardState(), oks;
        public static int RightScore = 0, LeftScore = 0;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
                /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }
                /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            sf= Content.Load<SpriteFont>("SpriteFont");
            Tex = Content.Load<Texture2D>("Tex");
            Balls.Add(new Ball());
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            oks = ks;
            ks = Keyboard.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            switch (State)
            {
                case GameStates.Start:
                    break;
                case GameStates.Playing:
                    if (ks.IsKeyDown(Keys.Space) && oks.IsKeyUp(Keys.Space))
                    {
                        Balls.Add(new Ball());
                    }
                    foreach (var ball in Balls)
                    {
                        ball.Update(LeftPaddle, RightPaddle);
                    }
                    foreach (var item in BallRemove)
                    {
                        Balls.Remove(item);
                    }
                    BallRemove.Clear();
                    if (LeftScore > 9)
                    {
                        State = GameStates.LeftWin;
                    }
                    if (RightScore > 9)
                    {
                        State = GameStates.RightWin;
                    }
                    LeftPaddle.Update(ks);
                    RightPaddle.Update(ks);

                    break;
                case GameStates.Paused:
                    break;
                case GameStates.RightWin:
                    if (ks.GetPressedKeys().Length > 0)
                    {
                        Reset();
                        State = GameStates.Playing;
                    }
                    break;
                case GameStates.LeftWin:
                    if (ks.GetPressedKeys().Length > 0)
                    {
                        Reset();
                        State = GameStates.Playing;
                    }
                    break;
                default:
                    break;
            }
            base.Update(gameTime);
        }
        public void Reset()
        {
            LeftScore = 0;
            RightScore = 0;
            Balls.Clear();
            BallRemove.Clear();
            LeftPaddle = new Paddle(10);
            RightPaddle = new Paddle(758);
            Balls.Add(new Ball());
        }
    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        spriteBatch.Begin();
            switch (State)
            {
                case GameStates.Playing:
                    LeftPaddle.Draw(spriteBatch);
                    RightPaddle.Draw(spriteBatch);
                    foreach (var ball in Balls)
                    {
                        ball.Draw(spriteBatch);
                    }
                    break;
                case GameStates.Paused:
                    break;
                case GameStates.RightWin:
                    spriteBatch.DrawString(Game1.sf, "Right Wins ", new Vector2(3, 0), Color.White);
                    break;
                case GameStates.LeftWin:
                    spriteBatch.DrawString(Game1.sf, "Leftt Wins ", new Vector2(3, 0), Color.White);
                    break;
                default:
                    break;
            }

        spriteBatch.DrawString(Game1.sf, "Left: " + LeftScore.ToString(),new Vector2(9,0), Color.White);
        spriteBatch.DrawString(Game1.sf, "Right: " + RightScore.ToString(), new Vector2(600,0), Color.White);
       
            spriteBatch.End();
            
            base.Draw(gameTime);
            
        }
    }
}
