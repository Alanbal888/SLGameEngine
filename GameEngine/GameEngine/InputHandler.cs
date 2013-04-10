using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using GameEngine.Gaming;

namespace GameEngine
{
    public class InputHandler : IGameObject
    {
        /// <summary>
        /// Estado actual del mouse.
        /// </summary>
        private MouseState currentMouseState;
        /// <summary>
        /// Estado actual del teclado.
        /// </summary>
        private KeyboardState currentKeyboardState;
        /// <summary>
        /// Estado anterior del mouse.
        /// </summary>
        private MouseState lastMouseState;
        /// <summary>
        /// Estado anterior del keyboard.
        /// </summary>
        private KeyboardState lastKeyboardState;
        /// <summary>
        /// Sprite del cursor.
        /// </summary>
        private ISprite cursorSprite;
        /// <summary>
        /// Se ha movido el cursor.
        /// </summary>
        private bool cursorMoved;
        /// <summary>
        /// Posición donde se dibujará el cursor.
        /// </summary>
        private Vector2 cursorPosition;

        /// <summary>
        /// Verifica si el objeto se encuentra desechado.
        /// </summary>
        public bool Disposed
        {
            get;
            set;

        }

        /// <summary>
        /// Estado actual del teclado.
        /// </summary>
        public KeyboardState CurrentKeyboardState
        {
            get
            {
                return currentKeyboardState;
            }
        }

        /// <summary>
        /// Estado anterior del teclado.
        /// </summary>
        public KeyboardState LastKeyboardState
        {
            get
            {
                return lastKeyboardState;
            }
        }

        /// <summary>
        /// Estado actual del mouse.
        /// </summary>
        public MouseState CurrentMouseState
        {
            get
            {
                return currentMouseState;
            }
        }

        /// <summary>
        /// Estado anterior del mouse.
        /// </summary>
        public MouseState LastMouseState
        {
            get
            {
                return lastMouseState;
            }
        }

        /// <summary>
        /// Posicion donde debería estar el mouse.
        /// </summary>
        public Microsoft.Xna.Framework.Vector2 Position
        {
            get
            {
                return cursorPosition;
            }
            set
            {
                cursorPosition = value;
                if (cursorSprite != null)
                    cursorSprite.Position = cursorPosition;
            }
        }

        /// <summary>
        /// Asigna la imagen del cursor actual.
        /// </summary>
        public ISprite CursorSprite
        {
            get { return cursorSprite; }
            set { cursorSprite = value; }
        }

        /// <summary>
        /// Actualiza el IGameObject.
        /// </summary>
        public void Update(Microsoft.Xna.Framework.GameTime GameTime)
        {
            lastKeyboardState = currentKeyboardState;
            lastMouseState = currentMouseState;

            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();

            Vector2 position = new Vector2(currentMouseState.X, currentMouseState.Y);

            cursorMoved = position != Position;

            Position = position;

        }

        /// <summary>
        /// Dibuja el IGameObject.
        /// </summary>
        public virtual void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch SpriteBatch)
        {
            if (cursorSprite != null)
                cursorSprite.Draw(SpriteBatch);
        }

        /// <summary>
        /// Carga el contenido necesario del IGameObject.
        /// </summary>
        public virtual void LoadContent(Microsoft.Xna.Framework.Content.ContentManager ContentManager)
        {
            if (cursorSprite != null)
                cursorSprite.LoadContent(ContentManager);
        }

        /// <summary>
        /// Utilizado para ahorro de recursos.
        /// </summary>
        public void Dispose()
        {
            Disposed = true;
            cursorSprite.Dispose();
        }

        /// <summary>
        /// Verifica si cierta tecla ha sido apretada y liberada.
        /// </summary>
        /// <param name="Key">Tecla a evaluar</param>
        public bool IsKeyPressed(Microsoft.Xna.Framework.Input.Keys Key)
        {
            return currentKeyboardState.IsKeyDown(Key) && lastKeyboardState.IsKeyUp(Key);
        }

        /// <summary>
        /// Verifica si cierta tecla ha sido liberada.
        /// </summary>
        /// <param name="Key">Tecla a evaluar.</param>
        public bool IsKeyReleased(Microsoft.Xna.Framework.Input.Keys Key)
        {
            return currentKeyboardState.IsKeyUp(Key) && lastKeyboardState.IsKeyDown(Key);
        }

        /// <summary>
        /// Verifica si algún botón del Mouse está presionado.
        /// </summary>
        /// <param name="Button">Boton del mouse.</param>
        public bool IsMouseButtonPress(MouseButtons Button)
        {
            switch (Button)
            {
                case MouseButtons.LeftButton:
                    return lastMouseState.LeftButton == ButtonState.Released &&
                        currentMouseState.LeftButton == ButtonState.Pressed;
                case MouseButtons.MiddleButton:
                    return lastMouseState.MiddleButton == ButtonState.Released &&
                        currentMouseState.MiddleButton == ButtonState.Pressed;
                case MouseButtons.RightButton:
                    return lastMouseState.RightButton == ButtonState.Released &&
                        currentMouseState.RightButton == ButtonState.Pressed;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Verifica si se ha presionado alguna tecla nueva.
        /// </summary>
        public bool IsAnyKeyPressed()
        {
            return currentKeyboardState.GetPressedKeys().Count() >
                lastKeyboardState.GetPressedKeys().Count();
        }

        /// <summary>
        /// Verifica si se ha presionado alguna tecla nueva.
        /// </summary>
        public bool IsAnyKeyReleased()
        {
            return currentKeyboardState.GetPressedKeys().Count() <
                lastKeyboardState.GetPressedKeys().Count();
        }
    }
}
