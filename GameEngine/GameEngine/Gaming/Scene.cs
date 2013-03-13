using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using GameEngine.Gaming;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.Gaming
{
    public class Scene : Collection<IGameObject>, IGameObject
    {
        /// <summary>
        /// Mundo donde se ejecutarán las fuerzas básicas, así como colisiones, etc.
        /// </summary>
        protected World world;

        /// <param name="World">Mundo donde se crearán los demás objetos.</param>
        public Scene(FarseerPhysics.Dynamics.World World)
        {
            world = World;
        }

        /// <summary>
        /// Mundo donde se ejecutarán las fuerzas básicas, así como colisiones, etc.
        /// </summary>
        public World World
        {
            get { return world; }
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
        /// Actualiza todos los IGameObject de la colección.
        /// </summary>
        /// <param name="GameTime">Lleva el tiempo actual del juego.</param>
        public virtual void Update(GameTime GameTime)
        {
            world.Step(Math.Min((float)GameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));
            foreach (var item in this)
            {
                item.Update(GameTime);
            }
        }

        /// <summary>
        /// Dibuja todos los IGameObject de la colección.
        /// </summary>
        /// <param name="SpriteBatch">Dibujador por default de graficos 2D de XNA.</param>
        public virtual void Draw(SpriteBatch SpriteBatch)
        {
            foreach (var item in this)
            {
                item.Draw(SpriteBatch);
            }
        }

        /// <summary>
        /// Actualiza todos los IGameObject de la colección.
        /// </summary>
        /// <param name="ContentManager">Manejador de contenidos de XNA.</param>
        public virtual void LoadContent(ContentManager ContentManager)
        {
            foreach (var item in this)
            {
                item.LoadContent(ContentManager);
            }
        }

        /// <summary>
        /// Utilizado para ahorro de recursos.
        /// </summary>
        public virtual void Dispose()
        {
            foreach (var item in this)
            {
                item.Dispose();
            }
        }
    }
}
