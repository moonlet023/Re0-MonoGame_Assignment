using System;
using System.Diagnostics;
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
        float time = 5.0f, basicTime = 30.0f;
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
        banana[] banana;
        pineapple[] pineapple;
        Strawberry[] strawberry;
        watermelon[] watermelon;

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
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
                    apple = new Apple[1];
                    strawberry = new Strawberry[1];
                    banana = new banana[1];
                    for (int i = 0; i < apple.Length; i++)
                    {
                        apple[i] = new Apple(this);
                        this.Components.Add(apple[i]);
                        applemiss += apple[i].appleMiss;

                        strawberry[i] = new Strawberry(this);
                        this.Components.Add(strawberry[i]);
                        strawberrymiss += strawberry[i].strawberryMiss;

                        banana[i] = new banana(this);
                        this.Components.Add(banana[i]);
                        bananamiss += banana[i].bananaMiss;
                    }

                    gamePlate = new plate(this);
                    this.Components.Add(gamePlate);
                }
            }

            if (isStart && !isEnd)
            {
                time -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                for (int i = 0; i < apple.Length; i++)
                {
                    if (perfectCollision(apple[i].appleRect, apple[i].data, gamePlate.plateRect, gamePlate.data))
                    {
                        //Console.WriteLine("Hit Apple");
                        apple[i].isHit = true;
                    }

                    if (perfectCollision(strawberry[i].strawberryRect, strawberry[i].data, gamePlate.plateRect,
                            gamePlate.data))
                    {
                        //Console.WriteLine("Hit Strawberry");
                        strawberry[i].isHit = true;
                    }

                    if (perfectCollision(banana[i].bananaRect, banana[i].data, gamePlate.plateRect, gamePlate.data))
                    {
                        //Console.WriteLine("Hit Banana");
                        banana[i].isHit = true;
                    }

                    if (pineapple != null)
                        if (perfectCollision(pineapple[i].pineappleRect, pineapple[i].data, gamePlate.plateRect,
                                gamePlate.data))
                        {
                            //Console.WriteLine("Hit pineapple");
                            pineapple[i].isHit = true;
                        }

                    if (watermelon != null)
                        if (perfectCollision(watermelon[i].watermelonRect, watermelon[i].data, gamePlate.plateRect,
                                gamePlate.data))
                        {
                            Console.WriteLine("Hit watermelon");
                            watermelon[i].isHit = true;
                        }
                        else
                        {
                            break;
                        }
                }

                if (time <= 0)
                {
                    stage++;
                    miss = 0;
                    apple = new Apple[1];
                    strawberry = new Strawberry[1];
                    banana = new banana[1];
                    pineapple = new pineapple[1];
                    if (stage == 3)
                    {
                        watermelon = new watermelon[1];
                    }

                    for (int i = 0; i < apple.Length; i++)
                    {
                        apple[i] = new Apple(this);
                        this.Components.Add(apple[i]);

                        strawberry[i] = new Strawberry(this);
                        this.Components.Add(strawberry[i]);

                        banana[i] = new banana(this);
                        this.Components.Add(banana[i]);

                        if (pineapple != null)
                        {
                            pineapple[i] = new pineapple(this);
                            this.Components.Add(pineapple[i]);
                        }

                        if (watermelon != null)
                        {
                            watermelon[i] = new watermelon(this);
                            this.Components.Add(watermelon[i]);
                        }
                    }

                    time = basicTime + (basicTime * (stage - 1));
                }

                for (int i = 0; i < apple.Length; i++)
                {
                    if (this.apple[i].applePosition.Y >= 270)
                    {
                        miss++;
                    }
                }

                for (int i = 0; i < strawberry.Length; i++)
                {
                    if (this.strawberry[i].strawberryPosition.Y >= 450)
                    {
                        miss++;
                    }
                }

                for (int i = 0; i < banana.Length; i++)
                {
                    if (this.banana[i].bananaPosition.Y >= 350)
                    {
                        miss++;
                    }
                }

                if (pineapple != null)
                {
                    for (int i = 0; i < pineapple.Length; i++)
                    {
                        if (this.pineapple[i].pineapplePosition.Y >= 300)
                        {
                            miss++;
                        }
                    }
                }

                if (watermelon != null)
                {
                    for (int i = 0; i < watermelon.Length; i++)
                    {
                        if (this.watermelon[i].watermelonPosition.Y >= 270)
                        {
                            miss++;
                        }
                    }
                }
            }

            if (stage == 4)
            {
                GameMessage = "You Win";
                isEnd = true;
                for (int i = 0; i < apple.Length; i++)
                {
                    this.Components.Remove(apple[i]);
                    this.Components.Remove(strawberry[i]);
                    this.Components.Remove(banana[i]);
                    if (pineapple != null)
                    {
                        this.Components.Remove(pineapple[i]);
                    }

                    if (watermelon != null)
                    {
                        this.Components.Remove(watermelon[i]);
                    }
                }

                this.Components.Remove(gamePlate);
            }

            if (miss >= 3)
            {
                GameMessage = "You Lose";
                isEnd = true;
                for (int i = 0; i < apple.Length; i++)
                {
                    this.Components.Remove(apple[i]);
                    this.Components.Remove(strawberry[i]);
                    this.Components.Remove(banana[i]);
                    if (pineapple != null)
                    {
                        this.Components.Remove(pineapple[i]);
                    }

                    if (watermelon != null)
                    {
                        this.Components.Remove(watermelon[i]);
                    }
                }

                this.Components.Remove(gamePlate);
            }

            //not working need to fix later
            if (isEnd)
            {
                defaultLeaveTime((int)time);
                time -= (int)gameTime.ElapsedGameTime.TotalSeconds;
                Console.WriteLine(time);
                if (time <= 0)
                {
                    Exit();
                }
            }

            base.Update(gameTime);
        }

        public static Rectangle CalculateBoundingRectangle(Rectangle rectangle, Matrix transform)
        {

            Vector2 leftTop = new Vector2(rectangle.Left, rectangle.Top);
            Vector2 rightTop = new Vector2(rectangle.Right, rectangle.Top);
            Vector2 leftBottom = new Vector2(rectangle.Left, rectangle.Bottom);
            Vector2 rightBottom = new Vector2(rectangle.Right, rectangle.Bottom);

            Vector2.Transform(ref leftTop, ref transform, out leftTop);
            Vector2.Transform(ref rightTop, ref transform, out rightTop);
            Vector2.Transform(ref leftBottom, ref transform, out leftBottom);
            Vector2.Transform(ref rightBottom, ref transform, out rightBottom);

            Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop), Vector2.Min(leftBottom, rightBottom));
            Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop), Vector2.Max(leftBottom, rightBottom));

            return new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
        }

        private static bool perfectCollision(Rectangle rectA, Color[] colorA, Rectangle rectB, Color[] colorB)
        {
            Matrix trasformA = Matrix.CreateTranslation(new Vector3(-rectA.Center.X, -rectA.Center.Y, 0)) *
                               Matrix.CreateRotationZ(0) *
                               Matrix.CreateTranslation(new Vector3(rectA.Center.X, rectA.Center.Y, 0));
            Matrix teasformB = Matrix.CreateTranslation(new Vector3(-rectB.Center.X, -rectB.Center.Y, 0)) *
                               Matrix.CreateRotationZ(0) *
                               Matrix.CreateTranslation(new Vector3(rectB.Center.X, rectB.Center.Y, 0));

            Rectangle boundingRectA = CalculateBoundingRectangle(rectA, trasformA);
            Rectangle boundingRectB = CalculateBoundingRectangle(rectB, teasformB);

            int top = Math.Max(boundingRectA.Top, boundingRectB.Top);
            int bottom = Math.Min(boundingRectA.Bottom, boundingRectB.Bottom);
            int left = Math.Max(boundingRectA.Left, boundingRectB.Left);
            int right = Math.Min(boundingRectA.Right, boundingRectB.Right);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Color dataA = colorA[(x - rectA.Left) + (y - rectA.Top) * rectA.Width];
                    Color dataB = colorB[(x - rectB.Left) + (y - rectB.Top) * rectB.Width];
                    if (dataA.A != 0 && dataB.A != 0)
                    {
                        return true;
                    }
                }
            }

            return false; // No intersection found
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
            spriteBatch.Draw(background, new Rectangle(0, 0, 900, 700), Color.White);

            if (!isStart || isEnd)
            {
                spriteBatch.DrawString(font, GameMessage, new Vector2(300, 200), Color.Black);
                if (isEnd)
                {
                    spriteBatch.DrawString(font, "Game Will Leave in " + time + " second", new Vector2(300, 250),
                        Color.Black);
                    // spriteBatch.DrawString(font, "Game Will Leave in " + leaveTime +" second", new Vector2(300, 250), Color.Black);
                }
            }
            else
            {
                spriteBatch.DrawString(font, "Stage :" + stage, new Vector2(0, 460), Color.Black);
                spriteBatch.DrawString(font, "Time Remind: " + time, new Vector2(300, 460), Color.Black);
                // spriteBatch.DrawString(font, "Miss: " + miss, new Vector2(600, 460), Color.Black);
                for (int i = 0; i <= miss; i++)
                {
                    if (i > 0)
                    {
                        spriteBatch.Draw(cross, new Rectangle(580 + (50 * i), 410, 100, 100), Color.White);
                    }
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public bool defaultLeaveTime(int time)
        {
            time = 5;
            return false;
        }
    }
}
