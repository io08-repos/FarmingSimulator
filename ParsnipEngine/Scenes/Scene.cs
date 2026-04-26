using Microsoft.Xna.Framework.Graphics;
using ParsnipEngine.Entities;
using ParsnipEngine.Rendering;
using ParsnipEngine.Rendering.Components;
using ParsnipEngine.Tilemaps;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ParsnipEngine.Scenes
{
    /// <summary>
    /// Stores data about the entities and tilemaps present in a given scene.
    /// </summary>
    public class Scene
    {
        /// <summary>
        /// Entities associated with this scene.
        /// </summary>
        private List<Entity> _entities;

        /// <summary>
        /// Tilemap data associated with this scene.
        /// </summary>
        private List<TilemapData> _tilemaps;

        // Constructor.
        private Scene(List<Entity> entities, List<TilemapData> tilemaps)
        {
            _entities = entities;
            _tilemaps = tilemaps;

            CreateTilemapRenderers();
        }

        /// <summary>
        /// Factory function used to create scene objects.
        /// </summary>
        /// <param name="sceneName">Name of the scene JSON file.</param>
        /// <returns>Resulting scene object.</returns>
        public static Scene CreateFromJSON(string sceneName)
        {
            string scenePath = Path.Combine(SceneManager.ScenePath, sceneName);
            string json = File.ReadAllText(scenePath);

            JsonDocument document = JsonDocument.Parse(json);
            JsonElement root = document.RootElement;

            List<Entity> entities = [.. Entity.FromJSON(root)];
            List<TilemapData> tilemaps = [.. TilemapData.ArrayFromJSON(root)];
            Scene jsonScene = new (entities, tilemaps);
            
            return jsonScene;
        }

        /// <summary>
        /// Creates a tilemap renderer for every tilemap in <see cref="_tilemaps"/>.
        /// </summary>
        private void CreateTilemapRenderers()
        {
            foreach (var data in _tilemaps)
            {
                Texture2D spritesheet = SpriteAtlas.Instance.GetSprite(data.Spritesheet);
                _ = new TilemapRenderer(spritesheet, data.Tilemap, data.TileResolution, data.Layer);
            }
        }

        /// <summary>
        /// Public get accessor for <see cref="_entities"/>.
        /// </summary>
        /// <returns><see cref="_entities"/></returns>
        public List<Entity> GetEntities() => _entities;

        /// <summary>
        /// Public get accessor for <see cref="_tilemaps"/>.
        /// </summary>
        /// <returns><see cref="_entities"/></returns>
        public List<TilemapData> GetTilemapData() => _tilemaps;

        /// <summary>
        /// Adds <paramref name="entity"/> to <see cref="_entities"/>.
        /// </summary>
        /// <param name="entity">Entity to add to <see cref="_entities"/>.</param>
        public void Add(Entity entity) => _entities.Add(entity);
    }
}
