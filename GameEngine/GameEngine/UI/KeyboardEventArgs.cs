using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.UI
{
    /// <summary>
    /// Argumento del evento para teclado.
    /// </summary>
    public class KeyboardEventArgs : EventArgs
    {
        KeyboardState keyboardState;

        public KeyboardState KeyboardState
        {
            get { return keyboardState; }
        }

        public KeyboardEventArgs(KeyboardState keyboardState)
        {
            this.keyboardState = keyboardState;
        }
    }
}
