using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameEngine.UI
{
    /// <summary>
    /// Clase base para los controles  de interfaz gráfica en XNA, para mayor control implementar IUIObject
    /// </summary>
    public abstract class UIObject : IUIObject
    {
        /// <summary>
        /// Manejador por default.
        /// </summary>
        private InputHandler InputHandler;
        /// <summary>
        /// Rectangulo que rodea al control.
        /// </summary>
        protected Microsoft.Xna.Framework.Rectangle ControlRectangle;
        /// <summary>
        /// Ancho del control.
        /// </summary>
        private int width;
        /// <summary>
        /// Largo del control.
        /// </summary>
        private int height;
        /// <summary>
        /// Verifica si actualmente está el mouse encima del control.
        /// </summary>
        private bool isHover;
        /// <summary>
        /// Verifica si el padre manejará el evento.
        /// </summary>
        private bool handled;
        /// <summary>
        /// Control padre del control actual;
        /// </summary>
        private IUIObject parent;
        /// <summary>
        /// Imagen del control.
        /// </summary>
        private ISprite image;

        /// <summary>
        /// Evento cuando recibe click izquierdo.
        /// </summary>
        public event EventHandler<MouseEventArgs> LeftClick;

        /// <summary>
        /// Evento cuando recibe click derecho.
        /// </summary>
        public event EventHandler<MouseEventArgs> RightClick;

        /// <summary>
        /// Evento cuando se aprieta una tecla mientras se tiene el foco en este control.
        /// </summary>
        public event EventHandler<KeyboardEventArgs> KeyPressed;

        /// <summary>
        /// Evento cuando se suelta una tecla mientras se tiene el foco en este control.
        /// </summary>
        public event EventHandler<KeyboardEventArgs> KeyReleased;

        /// <summary>
        /// Evento cuando se mantiene el mouse encima del control.
        /// </summary>
        public event EventHandler<MouseEventArgs> Hover;

        /// <summary>
        /// Evento cuando se mueve el botón 3 del ratón.
        /// </summary>
        public event EventHandler<MouseEventArgs> Scroll;

        /// <summary>
        /// Evento cuando el mouse deja de estar encima del control.
        /// </summary>
        public event EventHandler<MouseEventArgs> UnHover;

        /// <summary>
        /// Evento cuando el control obtiene el foco.
        /// </summary>
        public event EventHandler<MouseEventArgs> Focus;

        /// <summary>
        /// Evento cuando el control pierde el foco.
        /// </summary>
        public event EventHandler<MouseEventArgs> UnFocus;

        /// <summary>
        /// Necesario para inicializar el control básico.
        /// </summary>
        /// <param name="InputHandler">Manejador de entradas.</param>
        /// <param name="Image">ISprite para el despliegue del control.</param>
        public UIObject(InputHandler InputHandler, ISprite Image)
        {
            this.InputHandler = InputHandler;
            this.Image = Image;
            HasFocus = false;
            isHover = false;
        }

        /// <summary>
        /// El control está habilitado o no.
        /// </summary>
        public virtual bool Enabled
        {
            get;
            set;
        }

        /// <summary>
        /// El control tiene el foco o no.
        /// </summary>
        public virtual bool HasFocus
        {
            get;
            set;
        }

        /// <summary>
        /// Determina si un evento lo maneja éste control o su padre.
        /// </summary>
        public virtual bool Handled
        {
            get { return handled; }
            set {
                if (value)
                {
                    handled = value;
                    if (parent != null)
                        parent.Handled = handled;
                }
            }
        }

        /// <summary>
        /// Ancho del control.
        /// </summary>
        public virtual int Width
        {
            get
            {
                return width;
            }
            set
            {
                if (value < 0) throw new Exception("No puede tener un ancho menor a 0.");
                width = value;
                ControlRectangle = new Rectangle((int)Position.X, (int)Position.Y, width, height);
            }
        }

        /// <summary>
        /// Largo del control.
        /// </summary>
        public virtual int Height
        {
            get
            {
                return height;
            }
            set
            {
                if (value < 0) throw new Exception("No se puede tener un largo menor a 0.");
                height = value;
                ControlRectangle = new Rectangle((int)Position.X, (int)Position.Y, width, height);
            }
        }

        /// <summary>
        /// Controles internos al control.
        /// </summary>
        public virtual IList<IUIObject> InnerObjects
        {
            get;
            set;
        }

        /// <summary>
        /// Control padre del control actual.
        /// </summary>
        public virtual IUIObject Parent
        {
            get
            {
                return parent;
            }
            set
            {
                if (value.InnerObjects == null)
                    throw new Exception("El padre no puede tener null en Inner Objects al agregarle controles hijos.");
                parent = value;
                Position = Position + parent.Position;
                parent.InnerObjects.Add(this);
            }
        }

        /// <summary>
        /// Imagen a desplegar en el control.
        /// </summary>
        public virtual ISprite Image
        {
            get { return image; }
            set
            {
                if (value == null) return;
                if (image != null)
                    value.Position = Position;
                image = value;
            }
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
        /// Posición del control, si el padre es null es absoluta, si no entonces es relativa al padre.
        /// </summary>
        public virtual Vector2 Position
        {
            get
            {
                return Image.Position;
            }
            set
            {
                Image.Position = Parent == null ? value : Parent.Position + value;
                ControlRectangle = new Rectangle((int)Image.Position.X, (int)Image.Position.Y, Width, Height);
            }
        }

        /// <summary>
        /// Actualiza el IGameObject.
        /// </summary>
        /// <param name="GameTime">Lleva el tiempo actual del juego.</param>
        public virtual void Update(Microsoft.Xna.Framework.GameTime GameTime)
        {
            handled = false;
            //Si el control actual tiene controles hijos, los actualizaremos a todos.
            UpdateInnerObjects(GameTime);

            //Si tenemos un control padre, le asignamos si deberá o no manejar el evento.
            if (Parent != null) Parent.Handled = Handled;

            //Creamos un rectángulo que apunta la posición de mi mouse actual.
            var currentMouseRectangle =
                new Rectangle((int)InputHandler.CurrentMouseState.X, (int)InputHandler.CurrentMouseState.Y, 1, 1);
            //Creamos un rectángulo que apunta la posición de mi mouse anterior.
            var lastMouseRectangle =
                new Rectangle((int)InputHandler.LastMouseState.X, (int)InputHandler.LastMouseState.Y, 1, 1);

            //Verifica si se tiene que mandar a llamar el evento Hover.
            CheckHover(ref currentMouseRectangle, ref lastMouseRectangle);

            //Verifica si se tiene que mandar a llamar el evento UnHover.
            CheckUnHover(ref currentMouseRectangle, ref lastMouseRectangle);
            
            //Verifica si se tiene que mandar a llamar el evento LeftClick.
            CheckLeftClick(ref currentMouseRectangle);

            //Verifica si se tiene que mandar a llamar el evento RightClick.
            CheckRightClick(ref currentMouseRectangle);

            //Verifica si se tiene que mandar a llamar el evento Scroll.
            CheckScroll(ref currentMouseRectangle);

            //Verifica si se mandan a llamar los eventos KeyPressed y KeyReleased
            CheckKeyboardInput();

            if (Image != null)
                Image.Update(GameTime);

        }

        /// <summary>
        /// Actualiza a los elementos hijos.
        /// </summary>
        /// <param name="GameTime">Lleva el tiempo actual del juego</param>
        private void UpdateInnerObjects(Microsoft.Xna.Framework.GameTime GameTime)
        {
            if (InnerObjects != null)
                foreach (var item in InnerObjects)
                {
                    item.Update(GameTime);
                }
        }

        /// <summary>
        /// Procedimiento para verificar la entrada de teclado.
        /// </summary>
        private void CheckKeyboardInput()
        {
            //Si actualmente el control tiene el foco.
            if (HasFocus)
            {
                //Si se ha apretado alguna tecla nueva desde este momento, se manda el evento KeyPressed.
                if (InputHandler.IsAnyKeyPressed())
                    OnKeyPressed();
                //Si se ha soltado alguna tecla desde este momento, se manda el evento KeyReleased.
                if (InputHandler.IsAnyKeyReleased())
                    OnKeyReleased();
            }
        }

        /// <summary>
        /// Procedimiento para verificar si el scroll se ha movido.
        /// </summary>
        /// <param name="currentMouseRectangle"></param>
        private void CheckScroll(ref Rectangle currentMouseRectangle)
        {
            //Si se ha hecho Scroll.
            if (InputHandler.CurrentMouseState.ScrollWheelValue != InputHandler.LastMouseState.ScrollWheelValue)
            {
                //Y el mouse actual se encuentra dentro de mi control
                //Se dispara el evento Scroll.
                if (ControlRectangle.Contains(currentMouseRectangle))
                    OnScroll();
            }
        }

        /// <summary>
        /// Procedimiento para verificar si se ha dado click derecho.
        /// </summary>
        /// <param name="currentMouseRectangle"></param>
        private void CheckRightClick(ref Rectangle currentMouseRectangle)
        {
            //Si se ha dado click derecho.
            if (InputHandler.IsMouseButtonPress(MouseButtons.RightButton))
            {
                //Y el mouse actual se encuentra dentro de mi control
                //Se dispara el evento RightClick, pero el control no gana el foco.
                if (ControlRectangle.Contains(currentMouseRectangle))
                    OnRightClick();
            }
        }

        /// <summary>
        /// Procedimiento para verificar si se ha dado click izquierdo.
        /// </summary>
        /// <param name="currentMouseRectangle"></param>
        private void CheckLeftClick(ref Rectangle currentMouseRectangle)
        {
            //Si se ha dado click izquierdo.
            if (InputHandler.IsMouseButtonPress(MouseButtons.LeftButton))
            {
                //Y el mouse actual se encuentra dentro de mi control.
                //Se dispara el evento LeftClick y obtiene el foco mi control actual.
                if (ControlRectangle.Contains(currentMouseRectangle))
                {
                    OnLeftClick();
                    if (!HasFocus)
                    {
                        HasFocus = true;
                        OnFocus();
                    }
                }
                //Si no se encontraba dentro del control entonces cedemos el foco.
                else if (HasFocus)
                {
                    HasFocus = false;
                    OnUnFocus();
                }
            }           
        }

        /// <summary>
        /// Procedimiento para verificar si no se encuentra encima el mouse del control.
        /// </summary>
        /// <param name="currentMouseRectangle"></param>
        /// <param name="lastMouseRectangle"></param>
        private void CheckUnHover(ref Rectangle currentMouseRectangle, ref Rectangle lastMouseRectangle)
        {
            //Si el mouse anterior estaba dentro del control y el actual no lo está entonces 
            //se dispara el evento UnHover.
            if (ControlRectangle.Contains(lastMouseRectangle) && !ControlRectangle.Contains(currentMouseRectangle))
            {
                isHover = false;
                OnUnHover();
            }
        }

        /// <summary>
        /// Procedimiento para verificar si se encuentra encima el mouse del control.
        /// </summary>
        /// <param name="currentMouseRectangle"></param>
        /// <param name="lastMouseRectangle"></param>
        private void CheckHover(ref Rectangle currentMouseRectangle, ref Rectangle lastMouseRectangle)
        {
            //Si el mouse anterior estaba dentro del control y también lo está el mouse actual entonces se dispara
            //el evento Hover.
            if (ControlRectangle.Contains(currentMouseRectangle) 
                && ControlRectangle.Contains(lastMouseRectangle) 
                && !isHover)
            {
                isHover = true;
                OnHover();
            }
        }

        /// <summary>
        /// Dibuja el IGameObject.
        /// </summary>
        /// <param name="SpriteBatch">Dibujador por default de graficos 2D de XNA.</param>
        public virtual void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch SpriteBatch)
        {
            if (Image != null)
                Image.Draw(SpriteBatch);

            //Si el control actual tiene controles hijos.
            if(InnerObjects != null)
                foreach (var item in InnerObjects)
                {
                    //Verificamos si el control hijo se encuentra contenido en su totalidad en el control padre.
                    Rectangle itemRectangle =
                        new Rectangle((int)item.Position.X, (int)item.Position.Y, item.Width, item.Height);
                    //Si es así entonces se dibujará.
                    if(ControlRectangle.Contains(itemRectangle))
                        item.Draw(SpriteBatch); 
                }
        }

        /// <summary>
        /// Carga el contenido necesario del IGameObject.
        /// </summary>
        /// <param name="ContentManager">Manejador de contenidos de XNA.</param>
        public virtual void LoadContent(Microsoft.Xna.Framework.Content.ContentManager ContentManager)
        {
            //Si tenemos controles hijos, cargamos el contenido de todos.
            if(InnerObjects != null)
                foreach (var item in InnerObjects)
                {
                    item.LoadContent(ContentManager);
                }
            if (Image != null)
            {
                Image.LoadContent(ContentManager);
                Width = Image.CurrentFrame.Width;
                Height = Image.CurrentFrame.Height;
            }
        }

        /// <summary>
        /// Utilizado para ahorro de recursos.
        /// </summary>
        public virtual void Dispose()
        {
            if (InnerObjects != null)
            {
                foreach (var item in InnerObjects)
                {
                    item.Dispose();
                }
            }
            Image.Dispose();
        }

        /// <summary>
        /// Método para encapsular la invocación de los respectivos eventos.
        /// </summary>
        /// <typeparam name="T">Tipo de argumento del evento, tiene que heredar de EventArgs.</typeparam>
        /// <param name="evento">El evento mismo que será ejecutado.</param>
        /// <param name="eventArgs">Los argumentos del evento.</param>
        private void LaunchEvent<T>(EventHandler<T> evento, T eventArgs) where T : EventArgs
        {
            if (!Handled && evento != null)
                evento.Invoke(this, eventArgs);
        }

        /// <summary>
        /// Utilizado para llamar al evento Hover.
        /// </summary>
        protected void OnHover()
        {
            LaunchEvent<MouseEventArgs>(Hover, new MouseEventArgs(InputHandler.CurrentMouseState));
        }

        /// <summary>
        /// Utilizado para llamar al evento KeyPressed
        /// </summary>
        protected virtual void OnKeyPressed()
        {
            LaunchEvent<KeyboardEventArgs>(KeyPressed, new KeyboardEventArgs(InputHandler.CurrentKeyboardState));
        }

        /// <summary>
        /// Utilizado para llamar al evento KeyReleased
        /// </summary>
        protected virtual void OnKeyReleased()
        {
            LaunchEvent<KeyboardEventArgs>(KeyReleased, new KeyboardEventArgs(InputHandler.CurrentKeyboardState));
        }

        /// <summary>
        /// Utilizado para llamar al evento LeftClick.
        /// </summary>
        protected virtual void OnLeftClick()
        {
            LaunchEvent<MouseEventArgs>(LeftClick, new MouseEventArgs(InputHandler.CurrentMouseState));
        }

        /// <summary>
        /// Utilizado para llamar al evento RightClick.
        /// </summary>
        protected virtual void OnRightClick()
        {
            LaunchEvent<MouseEventArgs>(RightClick, new MouseEventArgs(InputHandler.CurrentMouseState));
        }

        /// <summary>
        /// Utilizado para llamar al evento Scroll.
        /// </summary>
        protected virtual void OnScroll()
        {
            LaunchEvent<MouseEventArgs>(Scroll, new MouseEventArgs(InputHandler.CurrentMouseState));
        }

        /// <summary>
        /// Utilizado para llamar al evento Focus.
        /// </summary>
        protected virtual void OnFocus()
        {
            LaunchEvent<MouseEventArgs>(Focus, new MouseEventArgs(InputHandler.CurrentMouseState));
        }

        /// <summary>
        /// Utilizado para llamar al evento UnFocus.
        /// </summary>
        protected virtual void OnUnFocus()
        {
            LaunchEvent<MouseEventArgs>(UnFocus, new MouseEventArgs(InputHandler.CurrentMouseState));
        }

        /// <summary>
        /// Utilizado para llamar al evento UnHover.
        /// </summary>
        protected void OnUnHover()
        {
            LaunchEvent<MouseEventArgs>(UnHover, new MouseEventArgs(InputHandler.CurrentMouseState));
        }        
    }
}
