using FarseerPhysics.Dynamics;
using GameEngine;
using GameEngine.Gaming;
using GameEngine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngineTest.UITests
{
    class MainGameScene : Scene
    {
        public MainGameScene()
            : base(new World(Vector2.Zero))
        {
            var handler = new InputHandler();
            handler.CursorSprite = new Sprite("Mouse");

            Panel panelPadre = new Panel(handler, new FullColorSprite(500, 500, Color.WhiteSmoke));
            panelPadre.Hover += (o, e) => { panelPadre.Image.Color = Color.Wheat; };
            panelPadre.UnHover += (o, e) => { panelPadre.Image.Color = Color.WhiteSmoke; };

            Panel panelHijo = new Panel(handler, new FullColorSprite(50, 50, Color.Black));
            panelHijo.Parent = panelPadre;
            panelHijo.Position = new Vector2(30, 30);

            panelPadre.Position = new Vector2(200, 200);
            Add(panelPadre);
            Add(handler);
        }
    }
}
