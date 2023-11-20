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
    public class Strawberry : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public static Random r = new Random();
        public Texture2D strawberryTexture;
        public Vector2 strawberryPosition, strawberryCenter, strawberryVelocity;
        private SpriteBatch spriteBatch;
        public float strawberryRotation, rotationSpeed;
        public Color[] data;
        public int strawberryMiss = 0;
        public bool isHit = false;
        public Rectangle strawberryRect;
        
        public Strawberry (Game g) : base(g)
        {
            
        }
        
        public override void Initialize()
        {
            strawberryPosition.X = r.Next(GraphicsDevice.Viewport.Width);
            strawberryPosition.Y = 0;
            strawberryVelocity.X = 0;
            strawberryVelocity.Y = r.Next(1, 5);
            rotationSpeed = strawberryVelocity.Y / 10.0f;
            
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            strawberryTexture = this.Game.Content.Load<Texture2D>("image/fruit/strawberry1");
            strawberryCenter.X = strawberryTexture.Width / 2.0f;
            strawberryCenter.Y = strawberryTexture.Height / 2.0f;
            
            data = new Color[strawberryTexture.Width * strawberryTexture.Height];
            strawberryTexture.GetData<Color>(data);
            
            base.LoadContent();
        }
        
        public override void Update(GameTime gameTime)
        {
            strawberryPosition.Y += strawberryVelocity.Y;
            strawberryRotation = (strawberryRotation + rotationSpeed) % MathHelper.TwoPi;
            if (strawberryPosition.Y > 450 || isHit)
            {
                if (strawberryPosition.Y >= 450)
                {
                    strawberryMiss++;
                }
                strawberryPosition.X = r.Next(GraphicsDevice.Viewport.Width);
                strawberryPosition.Y = 0;
                strawberryVelocity.Y = r.Next(1,5);
                isHit = false;
            }
            
            strawberryRect = new Rectangle((int)strawberryPosition.X, (int)strawberryPosition.Y, strawberryTexture.Width, strawberryTexture.Height);
            base.Update(gameTime);
        }
        
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(strawberryTexture, strawberryPosition, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}