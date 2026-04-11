using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace ParsnipEngine.Rendering
{
    /// <summary>
    /// Container for unsliced sprite assets. <br/>
    /// It loads sprites from a given game context's files, organises them, and lets you access them from anywhere in the game context.
    /// </summary>
    public class SpriteAtlas
    {
        /// <summary>
        /// Singleton reference for the SpriteAtlas.
        /// </summary>
        public static SpriteAtlas Instance { get; private set; } = new SpriteAtlas();

        // Constructor.
        private SpriteAtlas()
        {
            Instance = this;
        }

        /// <summary>
        /// Sprites indexed by asset name.
        /// </summary>
        private readonly Dictionary<string, Texture2D> _sprites = [];
        private const string _spritePath = @"Images/";

        /// <summary>
        /// Loads a sprite by asset name into the given game context.
        /// </summary>
        /// <param name="game">The game context to load the sprite into.</param>
        /// <param name="assetName">The name of the sprite (without file extension).</param>
        public void LoadSprite(Game game, string assetName)
        {
            string path = Path.Combine(_spritePath, assetName);
            Texture2D sprite = game.Content.Load<Texture2D>(path);

            _sprites[assetName] = sprite;
        }

        /// <summary>
        /// Use this to get a sprite from memory.
        /// </summary>
        /// <param name="assetName">Indexed name of the texture file.</param>
        /// <returns></returns>
        public Texture2D GetSprite(string assetName) => _sprites[assetName];
    }
}
