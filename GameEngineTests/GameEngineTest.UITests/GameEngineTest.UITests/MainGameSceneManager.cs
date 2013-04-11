using GameEngine.Gaming;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngineTest.UITests
{
    public class MainGameSceneManager : GameSceneManager
    {
        MainGameScene scene;
        public MainGameSceneManager(Game game) : base(game)
        {
            scene = new MainGameScene();
            Add("panel", scene);
            ChangeState("panel");
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            base.Draw(gameTime);
            SpriteBatch.End();
        }
    }
}
