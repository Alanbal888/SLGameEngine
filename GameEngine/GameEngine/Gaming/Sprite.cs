using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Gaming;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.Gaming
{
    /// <summary>
    /// Crea, reproduce y mantiene el despliegue de una animación 2D
    /// </summary>
    public class Sprite : ISprite
    {
        /// <summary>
        /// Textura que se usará para el dibujado.
        /// </summary>
        private Texture2D textura;
        /// <summary>
        /// Dirección de la textura a cargar.
        /// </summary>
        private string textureAsset;
        /// <summary>
        /// Posición de la lista del rectángulo actual.
        /// </summary>
        private int index;
        /// <summary>
        /// Determina si la animación es cíclica o no.
        /// </summary>
        private bool cicle;
        /// <summary>
        /// Animación del sprite.
        /// </summary>
        private IList<Rectangle> frames;
        /// <summary>
        /// Rectángulo origen actual.
        /// </summary>
        private Rectangle currentFrame;
        /// <summary>
        /// Color del dibujo actual.
        /// </summary>
        private Color color;
        /// <summary>
        /// Tiempo que debe durar cada cuadro en pantalla.
        /// </summary>
        private float timePerFrame;
        /// <summary>
        /// Tiempo que ha transcurrido desde el último cambio de frame.
        /// </summary>
        private float elapsedTime;
        /// <summary>
        /// Bandera que verifica si se ha terminado a animación.
        /// </summary>
        private bool finished;
        
        /// <summary>
        /// Se dispara cuando se vaya a cambiar el frame.
        /// </summary>
        public event EventHandler ChangedFrame;

        /// <summary>
        /// Se dispara una vez que la animación haya concluído, sea cíclica o no.
        /// </summary>
        public event EventHandler Finished;

        /// <param name="textureAsset">Dirección de la textura a cargar.</param>
        public Sprite(string TextureAsset)
        {
            textureAsset = TextureAsset;   //Ponemos la dirección de la imagen para su próxima carga
            color = Color.White;           //Color por default blanco.
            timePerFrame = 500;            //Tiempo por default 500ms. 
            elapsedTime = 0;               //0 por que no ha transcurrido tiempo.
            cicle = true;                  //Cíclico por default.
            index = 0;                     //Posicion de la animación 0.
            finished = false;
            Disposed = false;
        }

        /// <summary>
        /// Lista de frames para la animación.
        /// </summary>
        public IList<Rectangle> Frames
        {
            get { return frames; }
            set { 
                frames = value;
                Reset();                    //Hace falta hacer Reset, para inicializar las posiciones.
            }
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
        /// Tiempo en milisegundos que debe durar cada frame.
        /// </summary>
        public float TimePerFrame
        {
            get { return timePerFrame; }
            set { timePerFrame = value; }
        }

        /// <summary>
        /// Frame actual.
        /// </summary>
        public Rectangle CurrentFrame
        {
            get { return currentFrame; }
        }

        /// <summary>
        /// Determina si la animación es cíclica.
        /// </summary>
        public bool Cicle
        {
            get { return cicle; }
            set { cicle = value; }
        }

        /// <summary>
        /// Color de filtro para el dibujo actual.
        /// </summary>
        public Color Color
        {
            get { return color; }
            set { color = value; }
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
        /// <param name="GameTime">Lleva el tiempo actual del juego.</param>
        public virtual void Update(GameTime GameTime)
        {
            if (Disposed) return;
            if (!cicle && finished) return;
            elapsedTime += (float)GameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsedTime >= timePerFrame)
            {
                OnChangingFrame();
                index = (index + 1) % frames.Count;
                currentFrame = frames[index];
                elapsedTime = 0;
                if (index == 0)
                    OnFinished();
            }
        }

        /// <summary>
        /// Dibuja el IGameObject.
        /// </summary>
        /// <param name="SpriteBatch">Dibujador por default de graficos 2D de XNA.</param>
        public virtual void Draw(SpriteBatch SpriteBatch)
        {
            if (!Disposed) 
                SpriteBatch.Draw(textura, Position, currentFrame, color, Rotation, Origin, 1f, SpriteEffects.None, 1f);
        }

        /// <summary>
        /// Actualiza el IGameObject.
        /// </summary>
        /// <param name="ContentManager">Manejador de contenidos de XNA.</param>
        public virtual void LoadContent(ContentManager ContentManager)
        {
            textura = ContentManager.Load<Texture2D>(textureAsset);
            frames = new List<Rectangle>();
            frames.Add(new Rectangle(0, 0, textura.Width, textura.Height));
            Reset();
        }

        /// <summary>
        /// Reinicia el tiempo y el frame actual a 0.
        /// </summary>
        public virtual void Reset()
        {
            index = 0;
            currentFrame = frames[index];
        }

        /// <summary>
        /// Llama al evento Finished.
        /// </summary>
        protected virtual void OnFinished()
        {
            if (!cicle) finished = true;
            if (Finished != null)
                Finished.Invoke(this, null);
        }

        /// <summary>
        /// Llama al evento OnChangingFrame
        /// </summary>
        protected virtual void OnChangingFrame()
        {
            if (ChangedFrame != null)
                ChangedFrame.Invoke(this, null);
        }

        /// <summary>
        /// Utilizado para ahorro de recursos.
        /// </summary>
        public virtual void Dispose()
        {
            Disposed = true;
            textura.Dispose();
        }
    }
}
