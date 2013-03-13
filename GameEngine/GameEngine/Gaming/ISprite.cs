using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine
{
    /// <summary>
    /// Interfaz de la representación básica de un Sprite
    /// </summary>
    public interface ISprite : IGameObject
    {
        /// <summary>
        /// Se lanza cuando la animación ha concluído.
        /// </summary>
        event EventHandler Finished;

        /// <summary>
        /// Se lanza cuando es tiempo de cambiar de frame.
        /// </summary>
        event EventHandler ChangedFrame;
        /// <summary>
        /// Frames de la animación.
        /// </summary>
        IList<Microsoft.Xna.Framework.Rectangle> Frames
        {
            get;
            set;
        }

        /// <summary>
        /// Tiempo que debe durar cada segmento de animación en pantalla.
        /// </summary>
        float TimePerFrame
        {
            get;
            set;
        }

        /// <summary>
        /// Posicion de la imagen.
        /// </summary>
        Microsoft.Xna.Framework.Vector2 Position
        {
            get;
            set;
        }

        /// <summary>
        /// Origen de la imagen.
        /// </summary>
        Microsoft.Xna.Framework.Vector2 Origin
        {
            get;
            set;
        }

        /// <summary>
        /// Rotación de la imagen actual en radianes.
        /// </summary>
        float Rotation
        {
            get;
            set;
        }

        /// <summary>
        /// Color de filtro de la imagen actual.
        /// </summary>
        Microsoft.Xna.Framework.Color Color
        {
            get;
            set;
        }

        /// <summary>
        /// Determina si la animación es cíclica o no.
        /// </summary>
        bool Cicle
        {
            get;
            set;
        }

        /// <summary>
        /// Recuadro actual de la animación.
        /// </summary>
        Microsoft.Xna.Framework.Rectangle CurrentFrame
        {
            get;
        }

        /// <summary>
        /// Reinicia la animación.
        /// </summary>
        void Reset();
    }
}
