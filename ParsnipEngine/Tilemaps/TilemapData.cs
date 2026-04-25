using System.Text.Json;

namespace ParsnipEngine.Tilemaps
{
    /// <summary>
    /// Used to store tilemap data, with helper functions for JSON parsing.
    /// </summary>
    public class TilemapData
    {
        /// <summary>
        /// Tilemap structure, containing grid of tiles.
        /// </summary>
        public Tilemap Tilemap { get; private set; }

        /// <summary>
        /// Name of spritesheet to use when drawing the tilemap.
        /// </summary>
        public string Spritesheet { get; private set; }
        
        /// <summary>
        /// Size of sprites used in this tilemap (in pixel units).
        /// </summary>
        public int TileResolution { get; private set; }

        // Constructor.
        private TilemapData() { }

        /// <summary>
        /// Creates a TilemapData object, deriving its data from JSON text.
        /// </summary>
        /// <param name="json">JSON text.</param>
        /// <returns>Tilemap data derived from JSON.</returns>
        public static TilemapData FromJSON(JsonDocument document)
        {
            JsonElement root = document.RootElement;

            string spritesheet = root.GetProperty("spritesheet").GetString();
            int tileResolution = root.GetProperty("tileResolution").GetInt32();
            int width = root.GetProperty("width").GetInt32();
            int height = root.GetProperty("height").GetInt32();

            JsonElement tilesElement = root.GetProperty("tiles");

            int[] tiles = new int[tilesElement.GetArrayLength()];
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = tilesElement[i].GetInt32();
            }

            Tilemap tilemap = Tilemap.CreateTilemap(width, height, tiles);

            return Create(tilemap, spritesheet, tileResolution);
        }

        /// <summary>
        /// Creates a new object containing all data needed to process a tilemap in the engine.
        /// </summary>
        /// <param name="tilemap">Tilemap structure.</param>
        /// <param name="spritesheet">Tilemap spritesheet (name).</param>
        /// <param name="tileResolution">Size of sprites (in pixel units).</param>
        /// <returns>Tilemap data.</returns>
        public static TilemapData Create(Tilemap tilemap, string spritesheet, int tileResolution)
        {
            TilemapData data = new()
            {
                Tilemap = tilemap,
                Spritesheet = spritesheet,
                TileResolution = tileResolution
            };

            return data;
        }
    }
}
