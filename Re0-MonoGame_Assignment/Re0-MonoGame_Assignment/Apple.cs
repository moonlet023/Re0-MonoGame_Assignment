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
    public class Apple: Microsoft.Xna.Framework.DrawableGameComponent
    {
        public static Random r = new Random();
        public Texture2D appleTexture;
        public Vector2 applePosition, appleCenter, appleVelocity;
        private SpriteBatch spriteBatch;
        public float appleRotation, rotationSpeed;
        public Color[] data;
        public int appleMiss = 0;
        bool isHit = false;
        
        public Apple (Game g) : base(g)
        {
            
        }
        
        public override void Initialize()
        {
            applePosition.X = r.Next(GraphicsDevice.Viewport.Width);
            applePosition.Y = 0;
            appleVelocity.X = 0;
            appleVelocity.Y = r.Next(1, 5);
            rotationSpeed = appleVelocity.Y / 10.0f;
            
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            appleTexture = this.Game.Content.Load<Texture2D>("image/fruit/apple2");
            appleCenter.X = appleTexture.Width / 2.0f;
            appleCenter.Y = appleTexture.Height / 2.0f;
            
            data = new Color[appleTexture.Width * appleTexture.Height];
            appleTexture.GetData<Color>(data);
            
            base.LoadContent();
        }
        
        public override void Update(GameTime gameTime)
        {
            applePosition.Y += appleVelocity.Y;
            appleRotation = (appleRotation + rotationSpeed) % MathHelper.TwoPi;
            if (applePosition.Y > 270 || isHit)
            {
                if (applePosition.Y >= 270)
                {
                    appleMiss++;
                }
                else
                {
                    isHit = false;
                }
                applePosition.X = r.Next(GraphicsDevice.Viewport.Width);
                applePosition.Y = 0;
                appleVelocity.Y = r.Next(1,5);
            }
            base.Update(gameTime);
        }
        
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(appleTexture, applePosition, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}