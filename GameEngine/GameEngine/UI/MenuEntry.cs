using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Gaming;

namespace GameEngine.UI
{
    public abstract class MenuEntry : UIObject
    {
        public MenuEntry(InputHandler InputHandler, ISprite Image) : base(InputHandler, Image)
        {
        }

        private void VerificaPadre()
        {
            if (Parent == null || !(Parent is Menu))
                throw new Exception("El Menu Entry tiene que pertenecer a un menu");
        }

        public override void Update(Microsoft.Xna.Framework.GameTime GameTime)
        {
            VerificaPadre();
            base.Update(GameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch SpriteBatch)
        {
            VerificaPadre();    
            base.Draw(SpriteBatch);
        }
    }
}
