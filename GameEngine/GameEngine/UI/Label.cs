using GameEngine.Gaming;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.UI
{
    public class Label : UIObject
    {
        /// <summary>
        /// Texto que se desplegará en pantalla.
        /// </summary>
        private string texto;
        /// <summary>
        /// Posición del control.
        /// </summary>
        private Vector2 position;
        /// <summary>
        /// Fuente del control.
        /// </summary>
        private SpriteFont font;
        /// <summary>
        /// Color del control.
        /// </summary>
        private Color color;
        /// <summary>
        /// Ruta para obtener la fuente.
        /// </summary>
        private string fontAsset;

        /// <summary>
        /// Texto que se desplegará en pantalla.
        /// </summary>
        public string Text
        {
            get { return texto; }
            set { texto = value; }
        }

        /// <summary>
        /// Ancho del control.
        /// </summary>
        public override int Width
        {
            get
            {
                if (Image != null)
                    return base.Width;
                return font == null ? 1 : (int)font.MeasureString(texto).X;
            }
            set
            {
                if (Image != null)
                    base.Width = value;
            }
        }

        /// <summary>
        /// Largo del control.
        /// </summary>
        public override int Height
        {
            get
            {
                if (Image != null)
                    return base.Height;
                return font == null ? 1 : (int)font.MeasureString(texto).Y;
            }
            set
            {
                if (Image != null)
                    base.Height = value;
            }
        }

        /// <summary>
        /// Posición del control, si el padre es null es absoluta, si no entonces es relativa al padre.
        /// </summary>
        public override Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = Parent == null ? value : value + Parent.Position;
                ControlRectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            }
        }

        /// <summary>
        /// Color con el que se dibujará la letra.
        /// </summary>
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        /// <summary>
        /// Crea una instancia de un label sin ISprite de fondo.
        /// </summary>
        /// <param name="InputHandler">Manejador de entradas.</param>
        /// <param name="FontAsset">Dirección donde se encuentra la fuente.</param>
        /// <param name="Text">Texto que se desplegará en pantalla.</param>
        public Label(InputHandler InputHandler, string FontAsset, string Text) : 
            this(InputHandler, null, FontAsset, Text)
        {
            
        }

        /// <summary>
        /// Crea una instancia de un label con ISprite de fondo.
        /// </summary>
        /// <param name="InputHandler">Manejador de entradas.</param>
        /// <param name="Sprite">ISprite de fondo.</param>
        /// <param name="FontAsset">Dirección donde se encuentra la fuente.</param>
        /// <param name="Text">Texto que se desplegará en pantalla.</param>
        public Label(InputHandler InputHandler, ISprite Sprite, string FontAsset, string Text) : 
            base(InputHandler, Sprite)
        {
            fontAsset = FontAsset;
            color = Color.White;
            texto = Text;
        }

        /// <summary>
        /// Dibuja el IGameObject.
        /// </summary>
        /// <param name="SpriteBatch">Dibujador por default de graficos 2D de XNA.</param>
        public override void Draw(SpriteBatch SpriteBatch)
        {
            base.Draw(SpriteBatch);
            SpriteBatch.DrawString(font, texto, Position, color);
        }

        /// <summary>
        /// Carga el contenido necesario del IGameObject.
        /// </summary>
        /// <param name="ContentManager">Manejador de contenidos de XNA.</param>
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager ContentManager)
        {
            base.LoadContent(ContentManager);
            font = ContentManager.Load<SpriteFont>(fontAsset);
            ControlRectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        }
    }
}
