using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Gaming
{
    public abstract class GameSceneManager : DrawableGameComponent
    {
        private Dictionary<string, Scene> scenes;
        private Scene currentScene;
        private SpriteBatch spriteBatch;

        private static Game currentGame;

        public static Game CurrentGame
        {
            get { return currentGame; }
        }

        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        public GameSceneManager(Game game) : base(game)
        {
            scenes = new Dictionary<string, Scene>();
            currentScene = null;
            currentGame = Game;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            foreach (var item in scenes.Values)
            {
                item.LoadContent(Game.Content);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (currentScene != null)
                currentScene.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (currentScene != null)
                currentScene.Draw(spriteBatch);
        }

        public virtual void Add(string name, Scene scene)
        {
            scenes.Add(name, scene);
        }

        public virtual void ChangeState(string name)
        {
            if (!scenes.Keys.Contains(name))
                return;
            currentScene = scenes[name];
        }
    }
}
