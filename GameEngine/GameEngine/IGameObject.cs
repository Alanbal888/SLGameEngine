using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine
{
    /// <summary>
    /// Interfaz base para la sección de GameEngine.Gaming
    /// </summary>
    public interface IGameObject : IDisposable
    {
        /// <summary>
        /// Verifica si el objeto se encuentra desechado.
        /// </summary>
        bool Disposed
        {
            get;
            set;
        }
        /// <summary>
        /// Actualiza el IGameObject.
        /// </summary>
        /// <param name="GameTime">Lleva el tiempo actual del juego.</param>
        void Update(Microsoft.Xna.Framework.GameTime GameTime);

        /// <summary>
        /// Dibuja el IGameObject.
        /// </summary>
        /// <param name="SpriteBatch">Dibujador por default de graficos 2D de XNA.</param>
        void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch SpriteBatch);

        /// <summary>
        /// Carga el contenido necesario del IGameObject.
        /// </summary>
        /// <param name="ContentManager">Manejador de contenidos de XNA.</param>
        void LoadContent(Microsoft.Xna.Framework.Content.ContentManager ContentManager);
    }
}
