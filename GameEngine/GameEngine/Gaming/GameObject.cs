using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Gaming;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.Gaming
{
    public abstract class GameObject : IGameObject
    {
        /// <summary>
        /// Cuerpo del GameObject para el manejo de física con Farseer.
        /// </summary>
        private Body body;
        /// <summary>
        /// Sprite actual que dibujar.
        /// </summary>
        protected ISprite currentSprite;

        /// <summary>
        /// Se dispara antes de que sea recogido por el recolector de basura.
        /// </summary>
        public event EventHandler Disposing;

        /// <summary>
        /// Inicializa los valores básicos.
        /// </summary>
        public GameObject()
        {
            Animations = new Dictionary<string, ISprite>();
        }
        /// <summary>
        /// Lista de animaciones disponibles para el GameObject.
        /// </summary>
        public Dictionary<string, ISprite> Animations
        {
            get;
            set;
        }

        /// <summary>
        /// Cuerpo del GameObject.
        /// </summary>
        public Body Body
        {
            get { return body; }
            set { body = value; }
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
            if (currentSprite == null) return;
            if (Disposed) return;
            
            currentSprite.Update(GameTime);
            if (body != null)
            {
                currentSprite.Position = ConvertUnits.ToDisplayUnits(body.Position);
                currentSprite.Origin = new Vector2(currentSprite.CurrentFrame.Width / 2,
                                                   currentSprite.CurrentFrame.Height / 2);
                currentSprite.Rotation = body.Rotation;
            }
        }

        /// <summary>
        /// Dibuja el IGameObject.
        /// </summary>
        /// <param name="SpriteBatch">Dibujador por default de graficos 2D de XNA.</param>
        public virtual void Draw(SpriteBatch SpriteBatch)
        {
            currentSprite.Draw(SpriteBatch);
        }

        /// <summary>
        /// Carga el contenido necesario del IGameObject.
        /// </summary>
        /// <param name="ContentManager">Manejador de contenidos de XNA.</param>
        public virtual void LoadContent(ContentManager ContentManager)
        {
            foreach (var item in Animations)
            {
                item.Value.LoadContent(ContentManager);
            }
        }

        /// <summary>
        /// Utilizado para ahorro de recursos.
        /// </summary>
        public virtual void Dispose()
        {
            OnDisposing();
            Disposed = true;
        }

        /// <summary>
        /// Método para llamar al evento Disposing.
        /// </summary>
        protected virtual void OnDisposing()
        {
            if (Disposing != null)
                Disposing.Invoke(this, null);
        }

        /// <summary>
        /// Cambia el Sprite actual al Sprite del nombre especificado.
        /// </summary>
        /// <param name="SpriteName">Nombre del Sprite que se desea acceder.</param>
        public virtual void ChangeSprite(string SpriteName)
        {
            currentSprite = Animations[SpriteName];
        }
    }
}
