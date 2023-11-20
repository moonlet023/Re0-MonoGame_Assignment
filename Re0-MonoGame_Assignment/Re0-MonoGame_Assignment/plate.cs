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
    public class plate: Microsoft.Xna.Framework.DrawableGameComponent
    {
        
        public Texture2D plateTexture;
        public Vector2 platePosition, plateCenter, plateVelocity;
        private SpriteBatch spriteBatch;
        public Color[] data;
        bool isHit = false;
        
        public plate(Game g) : base(g)
        {
            
        }

        public override void Initialize()
        {
            platePosition = new Vector2(300, 400);
            plateVelocity = new Vector2(0, 0);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            plateTexture = this.Game.Content.Load<Texture2D>("image/plate/plate1");
            
            plateCenter.X = plateTexture.Width / 2.0f;
            plateCenter.Y = plateTexture.Height / 2.0f;
            
            data = new Color[plateTexture.Width * plateTexture.Height];
            plateTexture.GetData<Color>(data);
            base.LoadContent();
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(plateTexture, platePosition, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}