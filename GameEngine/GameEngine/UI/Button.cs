using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.UI;
using GameEngine.Gaming;

namespace GameEngine.UI
{
    /// <summary>
    /// Botón para la intefaz gráfica en XNA.
    /// </summary>
    public class Button : UIObject
    {
        /// <summary>
        /// Crea una instancia de un boton.
        /// </summary>
        /// <param name="Handler">Manejado de entradas.</param>
        /// <param name="Image">Imagen que deberá desplegar el control.</param>
        public Button(InputHandler Handler, ISprite Image) : base(Handler, Image)
        {
        }
    }
}
