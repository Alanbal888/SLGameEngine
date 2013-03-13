using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.UI
{
    /// <summary>
    /// Argumento del evento para mouse.
    /// </summary>
    public class MouseEventArgs : EventArgs
    {
        /// <summary>
        /// Informacion sobre el estado del mouse.
        /// </summary>
        MouseState mousestate;

        /// <summary>
        /// Informacion sobre el estado del mouse.
        /// </summary>
        public MouseState MouseState
        {
            get { return mousestate; }
        }

        public MouseEventArgs(MouseState mouseState)
        {
            this.mousestate = mouseState;
        }
    }
}
