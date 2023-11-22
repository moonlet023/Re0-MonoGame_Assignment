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
    public class watermelon: Microsoft.Xna.Framework.DrawableGameComponent
    {
        public static Random r = new Random();
        public Texture2D watermelonTexture;
        public Vector2 watermelonPosition, watermelonCenter, watermelonVelocity;
        private SpriteBatch spriteBatch;
        public float watermelonRotation, rotationSpeed;
        public Color[] data;
        public int watermelonMiss = 0;
        public bool isHit = false;
        public Rectangle watermelonRect;
        
        public watermelon (Game g) : base(g)
        {
            
        }
        
        public override void Initialize()
        {
            watermelonPosition.X = r.Next(GraphicsDevice.Viewport.Width);
            watermelonPosition.Y = 0;
            watermelonVelocity.X = 0;
            watermelonVelocity.Y = r.Next(1, 5);
            rotationSpeed = watermelonVelocity.Y / 10.0f;
            
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            watermelonTexture = this.Game.Content.Load<Texture2D>("image/fruit/watermelon1");
            watermelonCenter.X = watermelonTexture.Width / 2.0f;
            watermelonCenter.Y = watermelonTexture.Height / 2.0f;
            
            data = new Color[watermelonTexture.Width * watermelonTexture.Height];
            watermelonTexture.GetData<Color>(data);
            
            base.LoadContent();
        }
        
        public override void Update(GameTime gameTime)
        {
            watermelonPosition.Y += watermelonVelocity.Y;
            watermelonRotation = (watermelonRotation + rotationSpeed) % MathHelper.TwoPi;
            if (watermelonPosition.Y > 300 || isHit)
            {
                if (watermelonPosition.Y >= 300)
                {
                    watermelonMiss++;
                }
                watermelonPosition.X = r.Next(GraphicsDevice.Viewport.Width);
                watermelonPosition.Y = 0;
                watermelonVelocity.Y = r.Next(1,5);
                isHit = false;
            }
            
            watermelonRect = new Rectangle((int)watermelonPosition.X, (int)watermelonPosition.Y, watermelonTexture.Width, watermelonTexture.Height);
            base.Update(gameTime);
        }
        
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(watermelonTexture, watermelonPosition, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}