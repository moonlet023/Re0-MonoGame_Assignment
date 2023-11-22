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
    public class pineapple: Microsoft.Xna.Framework.DrawableGameComponent
    {
        public static Random r = new Random();
        public Texture2D pineappleTexture;
        public Vector2 pineapplePosition, pineappleCenter, pineappleVelocity;
        private SpriteBatch spriteBatch;
        public float pineappleRotation, rotationSpeed;
        public Color[] data;
        public int pineappleMiss = 0;
        public bool isHit = false;
        public Rectangle pineappleRect;
        
        public pineapple (Game g) : base(g)
        {
            
        }
        
        public override void Initialize()
        {
            pineapplePosition.X = r.Next(GraphicsDevice.Viewport.Width);
            pineapplePosition.Y = 0;
            pineappleVelocity.X = 0;
            pineappleVelocity.Y = r.Next(1, 5);
            rotationSpeed = pineappleVelocity.Y / 10.0f;
            
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            pineappleTexture = this.Game.Content.Load<Texture2D>("image/fruit/pineapple1");
            pineappleCenter.X = pineappleTexture.Width / 2.0f;
            pineappleCenter.Y = pineappleTexture.Height / 2.0f;
            
            data = new Color[pineappleTexture.Width * pineappleTexture.Height];
            pineappleTexture.GetData<Color>(data);
            
            base.LoadContent();
        }
        
        public override void Update(GameTime gameTime)
        {
            pineapplePosition.Y += pineappleVelocity.Y;
            pineappleRotation = (pineappleRotation + rotationSpeed) % MathHelper.TwoPi;
            if (pineapplePosition.Y > 270 || isHit)
            {
                if (pineapplePosition.Y >= 270)
                {
                    pineappleMiss++;
                }
                pineapplePosition.X = r.Next(GraphicsDevice.Viewport.Width);
                pineapplePosition.Y = 0;
                pineappleVelocity.Y = r.Next(1,5);
                isHit = false;
            }
            
            pineappleRect = new Rectangle((int)pineapplePosition.X, (int)pineapplePosition.Y, pineappleTexture.Width, pineappleTexture.Height);
            base.Update(gameTime);
        }
        
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(pineappleTexture, pineapplePosition, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}