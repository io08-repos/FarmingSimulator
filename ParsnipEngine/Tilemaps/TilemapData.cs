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

        /// <summary>
        /// Sorting rendering layer of this tilemap.
        /// </summary>
        public int Layer { get; private set; }

        // Constructor.
        private TilemapData() { }

        /// <summary>
        /// Creates an array of TilemapData objects, deriving their data from a JSON element.
        /// </summary>
        /// <param name="root">JSON element.</param>
        /// <returns>Array of TilemapData objects in the JSON scene.</returns>
        public static TilemapData[] ArrayFromJSON(JsonElement root)
        {
            var tilemaps = root.GetProperty("tilemaps");
            int mapsLength = tilemaps.GetArrayLength();

            TilemapData[] tilemapData = new TilemapData[mapsLength];
            for (int i = 0; i < mapsLength; i++)
            {
                JsonElement tilemapObject = tilemaps[i];

                string spritesheet = tilemapObject.GetProperty("spritesheet").GetString();
                int tileResolution = tilemapObject.GetProperty("tileResolution").GetInt32();
                int layer = tilemapObject.GetProperty("layer").GetInt32();

                int width = tilemapObject.GetProperty("width").GetInt32();
                int height = tilemapObject.GetProperty("height").GetInt32();

                JsonElement tilesElement = tilemapObject.GetProperty("tiles");
                int[] tileArray = new int[tilesElement.GetArrayLength()];

                for (int j = 0; j < tileArray.Length; j++)
                {
                    tileArray[j] = tilesElement[j].GetInt32();
                }

                Tilemap tilemap = Tilemap.CreateTilemap(width, height, tileArray);
                tilemapData[i] = Create(tilemap, spritesheet, tileResolution, layer);
            }

            return tilemapData;
        }

        /// <summary>
        /// Creates a new object containing all data needed to process a tilemap in the engine.
        /// </summary>
        /// <param name="tilemap">Tilemap structure.</param>
        /// <param name="spritesheet">Tilemap spritesheet (name).</param>
        /// <param name="tileResolution">Size of sprites (in pixel units).</param>
        /// <returns>Tilemap data.</returns>
        public static TilemapData Create(Tilemap tilemap, string spritesheet, int tileResolution, int layer)
        {
            TilemapData data = new()
            {
                Tilemap = tilemap,
                Spritesheet = spritesheet,
                TileResolution = tileResolution,
                Layer = layer
            };

            return data;
        }
    }
}
