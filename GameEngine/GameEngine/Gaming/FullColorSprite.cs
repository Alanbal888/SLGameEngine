using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Gaming
{
    /// <summary>
    /// Sprite de un rectángulo de color sólido.
    /// </summary>
    public class FullColorSprite : ISprite
    {
        /// <summary>
        /// Ancho del sprite.
        /// </summary>
        private int width;
        /// <summary>
        /// Alto del sprite.
        /// </summary>
        private int height;
        /// <summary>
        /// Textura del sprite.
        /// </summary>
        private Texture2D texture;

        /// <summary>
        /// Se dispara una vez que la animación haya concluído, sea cíclica o no. No se manda a llamar en ésta clase.
        /// </summary>
        public event EventHandler Finished;

        /// <summary>
        /// Se dispara cuando se vaya a cambiar el frame. No se manda a llamar en ésta clase.
        /// </summary>
        public event EventHandler ChangedFrame;

        /// <summary>
        /// Lista de frames para la animación. No aplica en ésta clase.
        /// </summary>
        public IList<Rectangle> Frames
        {
            get;
            set;
        }

        /// <summary>
        /// Tiempo en milisegundos que debe durar cada frame. No aplica en ésta clase.
        /// </summary>
        public float TimePerFrame
        {
            get;
            set;
        }

        /// <summary>
        /// Posicion para el dibujado del sprite.
        /// </summary>
        public Vector2 Position
        {
            get;
            set;
        }

        /// <summary>
        /// Origen de la imagen, usualmente el centro.
        /// </summary>
        public Vector2 Origin
        {
            get;
            set;
        }

        /// <summary>
        /// Rotacion del sprite en radianes.
        /// </summary>
        public float Rotation
        {
            get;
            set;
        }

        /// <summary>
        /// Color de filtro para el dibujo actual.
        /// </summary>
        public Color Color
        {
            get;
            set;
        }

        /// <summary>
        /// Determina si la animación es cíclica. No aplica en ésta clase.
        /// </summary>
        public bool Cicle
        {
            get;
            set;
        }

        /// <summary>
        /// Frame actual. Regresa un rectángulo con 0,0, Width, Height.
        /// </summary>
        public Rectangle CurrentFrame
        {
            get { return new Rectangle(0, 0, width, height); }
            set { return; }
        }

        /// <summary>
        /// Crea un FullColorSprite
        /// </summary>
        /// <param name="width">Ancho el sprite.</param>
        /// <param name="height">Largo del sprite.</param>
        /// <param name="color">Color del sprite.</param>
        public FullColorSprite(int width, int height, Color color)
        {
            this.width = width;
            this.height = height;
            Color = color;
            Disposed = false;
        }

        /// <summary>
        /// Reinicia el tiempo y el frame actual a 0. No aplica en ésta clase.
        /// </summary>
        public void Reset()
        {
        }

        /// <summary>
        /// Verifica si el objeto se encuentra desechado.
        /// </summary>
        public bool Disposed
        {
            get;
            set;
        }

        /// <summary>
        /// Actualiza el IGameObject.
        /// </summary>
        public void Update(Microsoft.Xna.Framework.GameTime GameTime)
        {
            
        }

        /// <summary>
        /// Dibuja el IGameObject.
        /// </summary>
        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch SpriteBatch)
        {
            if (!Disposed)
            {
                SpriteBatch.Draw(texture, Position, null, Color, Rotation, Origin, 1f, SpriteEffects.None, 1f);
                Disposed = true;
            }
        }

        /// <summary>
        /// Carga el IGameObject.
        /// </summary>
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

        /// <summary>
        /// Utilizado para ahorro de recursos.
        /// </summary>
        public void Dispose()
        {
            if (texture != null)
                texture.Dispose();
        }
    }
}
