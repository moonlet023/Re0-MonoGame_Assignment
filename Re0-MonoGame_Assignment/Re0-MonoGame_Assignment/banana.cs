using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Re0_MonoGame_Assignment 
{
    public class banana: Microsoft.Xna.Framework.DrawableGameComponent
    {
        public static Random r = new Random();
        public Texture2D bananaTexture;
        public Vector2 bananaPosition, bananaCenter, bananaVelocity;
        private SpriteBatch spriteBatch;
        public float bananaRotation, rotationSpeed;
        public Color[] data;
        public int bananaMiss = 0;
        public bool isHit = false;
        public Rectangle bananaRect;
        
        public banana (Game g) : base(g)
        {
            
        }
        
        public override void Initialize()
        {
            bananaPosition.X = r.Next(GraphicsDevice.Viewport.Width);
            bananaPosition.Y = 0;
            bananaVelocity.X = 0;
            bananaVelocity.Y = r.Next(1, 5);
            rotationSpeed = bananaVelocity.Y / 10.0f;
            
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            bananaTexture = this.Game.Content.Load<Texture2D>("image/fruit/banana");
            bananaCenter.X = bananaTexture.Width / 2.0f;
            bananaCenter.Y = bananaTexture.Height / 2.0f;
            
            data = new Color[bananaTexture.Width * bananaTexture.Height];
            bananaTexture.GetData<Color>(data);
            
            base.LoadContent();
        }
        
        public override void Update(GameTime gameTime)
        {
            bananaPosition.Y += bananaVelocity.Y;
            bananaRotation = (bananaRotation + rotationSpeed) % MathHelper.TwoPi;
            if (bananaPosition.Y > 270 || isHit)
            {
                if (bananaPosition.Y >= 350)
                {
                    bananaMiss++;
                }
                bananaPosition.X = r.Next(GraphicsDevice.Viewport.Width);
                bananaPosition.Y = 0;
                bananaVelocity.Y = r.Next(1,5);
                isHit = false;
            }
            
            bananaRect = new Rectangle((int)bananaPosition.X, (int)bananaPosition.Y, bananaTexture.Width, bananaTexture.Height);
            base.Update(gameTime);
        }
        
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(bananaTexture, bananaPosition, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}