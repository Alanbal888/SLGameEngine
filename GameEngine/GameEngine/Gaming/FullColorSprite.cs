using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Gaming
{
    public class FullColorSprite : ISprite
    {
        private int width;
        private int height;
        private Texture2D texture;

        public event EventHandler Finished;

        public event EventHandler ChangedFrame;

        public IList<Rectangle> Frames
        {
            get;
            set;
        }

        public float TimePerFrame
        {
            get;
            set;
        }

        public Vector2 Position
        {
            get;
            set;
        }

        public Vector2 Origin
        {
            get;
            set;
        }

        public float Rotation
        {
            get;
            set;
        }

        public Color Color
        {
            get;
            set;
        }

        public bool Cicle
        {
            get;
            set;
        }

        public Rectangle CurrentFrame
        {
            get { return new Rectangle(0, 0, width, height); }
            set { return; }
        }

        public FullColorSprite(int width, int height, Color color)
        {
            this.width = width;
            this.height = height;
            Color = color;
        }

        public void Reset()
        {
        }

        public bool Disposed
        {
            get;
            set;
        }

        public void Update(Microsoft.Xna.Framework.GameTime GameTime)
        {
            
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch SpriteBatch)
        {
            if (!Disposed)
            {
                SpriteBatch.Draw(texture, Position, Color);
            }
        }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager ContentManager)
        {
            texture = new Texture2D(GameSceneManager.CurrentGame.GraphicsDevice, width, height);
            var colorVector = new Color[width * height];
            for (int i = 0; i < colorVector.Length; i++)
            {
                colorVector[i] = Color;
            }
            texture.SetData(colorVector);
        }

        public void Dispose()
        {
            if (texture != null)
                texture.Dispose();
        }
    }
}
