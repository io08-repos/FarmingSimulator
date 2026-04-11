namespace ParsnipEngine.Tilemaps
{
    /// <summary>
    /// 
    /// <para> Stores level data in the following structure: </para>
    /// 
    /// <para> - Size of the current tilemap. <br/>
    /// - 2D integer array of size (<see cref="Width"/>, <see cref="Height"/>). <br/>
    /// - Inside of the 2D array: <br/>
    /// -- Series of indices pointing to one tile in a given spritesheet each.
    /// </para>
    /// 
    /// </summary>
    public readonly struct Tilemap
    {
        /// <summary>
        /// Tilemap size on the X axis.
        /// </summary>
        public readonly int Width;

        /// <summary>
        /// Tilemap size on the Y axis.
        /// </summary>
        public readonly int Height;

        /// <summary>
        /// Spritesheet tile indices from (0, 0) to (<see cref="Width"/> - 1, <see cref="Height"/> - 1)
        /// </summary>
        public readonly int[,] Tiles;

        // Constructor.
        private Tilemap(int width, int height)
        {
            Width = width;
            Height = height;

            Tiles = new int[width, height];
        }

        /// <summary>
        /// Standard method for generating a tilemap structure.
        /// </summary>
        /// <param name="width">Tilemap width.</param>
        /// <param name="height">Tilemap height.</param>
        /// <param name="map">Tilemap data (as array of spritesheet tile indices).</param>
        /// <returns></returns>
        public static Tilemap CreateTilemap(int width, int height, int[] map)
        {
            Tilemap tilemap = new (width, height);
            
            for (int i = 0; i < map.Length; i++)
            {
                int tileX = i % width;
                int tileY = i / width;

                tilemap.Tiles[tileX, tileY] = map[i];
            }

            return tilemap;
        }
    }
}
