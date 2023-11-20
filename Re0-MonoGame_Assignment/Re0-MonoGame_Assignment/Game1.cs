﻿using System;
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
        Texture2D cross;
        float leaveTime = 5.0f;

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

        int applemiss = 0;
        int bananamiss = 0;
        int pineapplemiss = 0;
        int strawberrymiss = 0;
        int watermelonmiss = 0;
        #endregion

        #region plate
        plate gamePlate;
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
            cross = Content.Load<Texture2D>("image/gameEffecrt/cross");
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
                        applemiss += apple[i].appleMiss;
                    }
                }
            }

            if (isStart && !isEnd)
            {
                gamePlate = new plate(this);
                this.Components.Add(gamePlate);
                time -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(time <= 0)
                {
                    stage++;
                    miss = 0;
                    apple = new Apple[1];
                    for (int i = 0; i < apple.Length; i++)
                    {
                        apple[i] = new Apple(this);
                        this.Components.Add(apple[i]);
                    }
                    time = basicTime + (basicTime * (stage - 1));
                }
                for(int i = 0; i < apple.Length; i++)
                {
                    if (this.apple[i].applePosition.Y >= 270)
                    {
                        miss++;
                    }
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
                this.Components.Remove(gamePlate);
            }

            if (miss >= 3)
            {
                GameMessage = "You Lose";
                isEnd = true;
                for(int i=0; i<apple.Length; i++)
                {
                    this.Components.Remove(apple[i]);
                }
                this.Components.Remove(gamePlate);
            }
            
            //not working need to fix later
            if(isEnd)
            {
                time = leaveTime;
                time -= (int)gameTime.ElapsedGameTime.TotalSeconds;
                if(time <= 0) 
                {
                    Exit();
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
                if (isEnd)
                {                
                    spriteBatch.DrawString(font, "Game Will Leave in " + time +" second", new Vector2(300, 250), Color.Black);
                    //spriteBatch.DrawString(font, "Game Will Leave in " + leaveTime +"second", new Vector2(300, 250), Color.Black);
                }
            }
            else
            {
                spriteBatch.DrawString(font, "Stage :" + stage , new Vector2(0, 460), Color.Black);
                spriteBatch.DrawString(font, "Time Remind: " + time, new Vector2(300, 460), Color.Black);
                // spriteBatch.DrawString(font, "Miss: " + miss, new Vector2(600, 460), Color.Black);
                for (int i = 0; i <= miss; i++)
                { 
                    if (i > 0) 
                    {
                        spriteBatch.Draw(cross, new Rectangle(580 + (50*i),410 , 100,100), Color.White);
                    }
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
