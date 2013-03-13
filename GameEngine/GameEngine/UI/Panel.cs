using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.UI
{
    public class Panel : GameEngine.UI.UIObject
    {
        /// <summary>
        /// Crea una instancia de un panel.
        /// </summary>
        /// <param name="InputHandler">Manejado de entradas.</param>
        /// <param name="Image">Imagen que deberá desplegar el control.</param>
        public Panel(InputHandler InputHandler, ISprite Image) : base(InputHandler, Image)
        {
        }
    }
}
