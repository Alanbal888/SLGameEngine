using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Gaming
{
    /// <summary>
    /// Clase manejadora de los Scenes de un juego.
    /// </summary>
    public abstract class GameSceneManager : DrawableGameComponent
    {
        /// <summary>
        /// Colección de escenas que guarda el manejador.
        /// </summary>
        private Dictionary<string, Scene> scenes;
        /// <summary>
        /// Escena actual que será puesta en actualización y en dibujado.
        /// </summary>
        private Scene currentScene;
        /// <summary>
        /// Dibujador que será usado por las escenas.
        /// </summary>
        private SpriteBatch spriteBatch;

        /// <summary>
        /// Campo estático del juego que hace referencia el GameSceneManager
        /// </summary>
        private static Game currentGame;

        /// <summary>
        /// Propiedad estátida del juego que hace referencia.
        /// </summary>
        public static Game CurrentGame
        {
            get { return currentGame; }
        }

        /// <summary>
        /// Dibujador creado por default.
        /// </summary>
        public virtual SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
            protected set { spriteBatch = value; }
        }

        /// <summary>
        /// Crea un nuevo GameSceneManager
        /// </summary>
        /// <param name="game">Referencia al juego actual.</param>
        public GameSceneManager(Game game) : base(game)
        {
            scenes = new Dictionary<string, Scene>();
            currentScene = null;
            currentGame = Game;
        }

        /// <summary>
        /// Carga el contenido de todos las escenas que se hayan agregado.
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            foreach (var item in scenes.Values)
            {
                item.LoadContent(Game.Content);
            }
        }

        /// <summary>
        /// Actualiza la escena actual.
        /// </summary>
        /// <param name="gameTime">Tiempo que pasó desde la última actualización.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (currentScene != null)
                currentScene.Update(gameTime);
        }

        /// <summary>
        /// Dibuja la escena actual.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (currentScene != null)
                currentScene.Draw(spriteBatch);
        }

        /// <summary>
        /// Agrega una nueva escena con un nombre.
        /// </summary>
        /// <param name="name">Nombre que tendrá la escena.</param>
        /// <param name="scene">Referencia a la escena.</param>
        public virtual void Add(string name, Scene scene)
        {
            scenes.Add(name, scene);
        }

        /// <summary>
        /// Permite cambiar la escena actual a travéz del nombre.
        /// </summary>
        /// <param name="name">Nombre de la escena a cambiar.</param>
        public virtual void ChangeState(string name)
        {
            if (!scenes.Keys.Contains(name))
                return;
            currentScene = scenes[name];
        }
    }
}
