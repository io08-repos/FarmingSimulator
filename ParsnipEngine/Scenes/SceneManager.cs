using Microsoft.Xna.Framework;
using ParsnipEngine.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace ParsnipEngine.Scenes
{
    /// <summary>
    /// Static manager class for loading and updating scenes.
    /// </summary>
    public static class SceneManager
    {
        /// <summary>
        /// Path to JSON scenes folder.
        /// </summary>
        public static readonly string ScenePath = Path.Combine(Environment.CurrentDirectory, @"Content/Scenes");

        /// <summary>
        /// Current scene running in the engine.
        /// </summary>
        public static Scene CurrentScene { get; private set; }

        /// <summary>
        /// Loads a JSON scene file by name, converts it into a <see cref="Scene"/> object, and sets it as <see cref="CurrentScene"/>.
        /// </summary>
        /// <param name="sceneName">File name of the JSON scene file</param>
        public static void LoadScene(string sceneName)
        {
            CurrentScene = Scene.CreateFromJSON(sceneName);
            Initialize();
        }

        /// <summary>
        /// Initializes all entities after scene is loaded.
        /// </summary>
        public static void Initialize()
        {
            foreach (var entity in CurrentScene.GetEntities())
            {
                entity.Initialize();
            }
        }

        /// <summary>
        /// Updates all entities in <see cref="CurrentScene"/>.
        /// </summary>
        /// <param name="gameTime">Time state of the game.</param>
        public static void Update(GameTime gameTime)
        {
            List<Entity> entities = CurrentScene.GetEntities();

            foreach (var entity in entities)
            {
                entity.Update(gameTime);
            }
        }
    }
}
