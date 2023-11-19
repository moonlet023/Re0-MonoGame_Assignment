using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Re0_MonoGame_Assignment
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        bool isStart = false;
        bool isEnd = false;
        SpriteFont font;
        KeyboardState kb = new KeyboardState();
        int stage = 1;
        int miss = 0;
        float time = 0.0f, basicTime = 30.0f;
        plate plate;

        #region Background
        Texture2D background;
        #endregion

        #region GameStart
        string GameMessage;
        #endregion

        #region fruit
        Apple[] apple;
        banana [] banana;
        pineapple [] pineapple;
        Strawberry [] strawberry;
        watermelon [] watermelon;
        #endregion
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
            graphics.IsFullScreen = true;
            IsMouseVisible = false;

            apple = null;

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
            
           

            // TODO: use this.Content to load your game content here
            background = Content.Load<Texture2D>("image/background/picnicmapYellow");
            font = Content.Load<SpriteFont>("font/Sprite");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            apple = null;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            // TODO: Add your update logic here
            if (!isStart)
            {
                GameMessage = "Press Enter to Start";
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {   
                    isStart = true;
                    time = basicTime;
                    miss = 0;
                    apple = new Apple[3];
                    for (int i = 0; i < apple.Length; i++)
                    {
                        apple[i] = new Apple(this);
                        this.Components.Add(apple[i]);
                    }
                }
            }

            if (isStart)
            {
                time -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                //miss = apple.appleMiss; //has problem now
                if(time <= 0)
                {
                    stage++;
                    apple = new Apple[1];
                    for (int i = 0; i < apple.Length; i++)
                    {
                        apple[i] = new Apple(this);
                        this.Components.Add(apple[i]);
                    }
                    time = basicTime + (basicTime * (stage - 1));
                }
            }
            
            if(stage == 4)
            {
                GameMessage = "You Win";
                isEnd = true;
                for(int i=0; i<apple.Length; i++)
                {
                    this.Components.Remove(apple[i]);
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(background,new Rectangle(0,0,900,700), Color.White);
            if (!isStart || isEnd)
            {
                spriteBatch.DrawString(font, GameMessage, new Vector2(300, 200), Color.Black);
            }
            else
            {
                spriteBatch.DrawString(font, "The Stage Now Is " +stage , new Vector2(0, 460), Color.Black);
                spriteBatch.DrawString(font, "Time Remind: " + time, new Vector2(300, 460), Color.Black);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void TimeCount()
        {
           
        }
    }
}
