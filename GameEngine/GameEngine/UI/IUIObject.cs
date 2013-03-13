using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.UI
{
    /// <summary>
    /// Interfaz básica del manejo de controles de interfaces gráficas en XNA.
    /// </summary>
    public interface IUIObject : IGameObject
    {
        /// <summary>
        /// Evento cuando recibe click izquierdo.
        /// </summary>
        event System.EventHandler<MouseEventArgs> LeftClick;

        /// <summary>
        /// Evento cuando recibe click derecho.
        /// </summary>
        event System.EventHandler<MouseEventArgs> RightClick;

        /// <summary>
        /// Evento cuando se aprieta una tecla mientras se tiene el foco en este control.
        /// </summary>
        event System.EventHandler<KeyboardEventArgs> KeyPressed;

        /// <summary>
        /// Evento cuando se suelta una tecla mientras se tiene el foco en este control.
        /// </summary>
        event System.EventHandler<KeyboardEventArgs> KeyReleased;

        /// <summary>
        /// Evento cuando se mantiene el mouse encima del control.
        /// </summary>
        event System.EventHandler<MouseEventArgs> Hover;

        /// <summary>
        /// Evento cuando se mueve el botón 3 del ratón.
        /// </summary>
        event System.EventHandler<MouseEventArgs> Scroll;

        /// <summary>
        /// Evento cuando el mouse deja de estar encima del control.
        /// </summary>
        event System.EventHandler<MouseEventArgs> UnHover;

        /// <summary>
        /// Evento cuando el control obtiene el foco.
        /// </summary>
        event System.EventHandler<MouseEventArgs> Focus;

        /// <summary>
        /// Evento cuando el control pierde el foco.
        /// </summary>
        event System.EventHandler<MouseEventArgs> UnFocus;

        /// <summary>
        /// El control está habilitado o no.
        /// </summary>
        bool Enabled
        {
            get;
            set;
        }

        /// <summary>
        /// El control tiene el foco o no.
        /// </summary>
        bool HasFocus
        {
            get;
            set;
        }

        /// <summary>
        /// Determina si un evento lo maneja éste control o su padre.
        /// </summary>
        bool Handled
        {
            get;
            set;
        }

        /// <summary>
        /// Ancho del control.
        /// </summary>
        int Width
        {
            get;
            set;
        }

        /// <summary>
        /// Largo del control.
        /// </summary>
        int Height
        {
            get;
            set;
        }

        /// <summary>
        /// Controles internos al control.
        /// </summary>
        IList<IUIObject> InnerObjects
        {
            get;
            set;
        }

        /// <summary>
        /// Control padre del control actual.
        /// </summary>
        IUIObject Parent
        {
            get;
            set;
        }

        /// <summary>
        /// Imagen a desplegar en el control.
        /// </summary>
        ISprite Image
        {
            get;
            set;
        }

        /// <summary>
        /// Posición del control, si el padre es null es absoluta, si no entonces es relativa al padre.
        /// </summary>
        Microsoft.Xna.Framework.Vector2 Position
        {
            get;
            set;
        }
    }
}
