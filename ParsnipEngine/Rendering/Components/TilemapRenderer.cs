using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParsnipEngine.Entities;
using ParsnipEngine.Tilemaps;

namespace ParsnipEngine.Rendering.Components
{
    /* NOTE:
     * 
     * Consider optimising this renderer by caching source rectangles every time a new spritesheet is set.
     * 
     * Currently, this renderer is a burden for the garbage collector and uses more memory than necessary,
     * as it creates a new Rectangle for every tile that gets drawn every frame,
     * NOT a single Rectangle for each tile present in the spritesheet (far more memory efficient).
     * 
     * Make the constructor private to prevent direct object construction, then:
     *  - Add a private list of source rectangles to this class.
     *  - Extend the SetSpritesheet() method so that it triggers source rectangle caching.
     *  - Modify the GetSourceRectangle() method to perform an array lookup by index.
     *  - Finally, make a static function that creates a new TilemapRenderer, and returns it.
     */

    /// <summary>
    /// Renderer component used to draw tilemaps using a spritesheet as the palette.
    /// </summary>
    public class TilemapRenderer(Texture2D spritesheet, int tileResolution, int layer) : Renderer(Entity.Empty)
    {
        /// <summary>
        /// Tile palette for this renderer.
        /// </summary>
        public Texture2D Spritesheet { get; private set; } = spritesheet;
        
        /// <summary>
        /// Resolution (in pixel units) of each square tile in the palette.
        /// </summary>
        public int TileResolution { get; private set; } = tileResolution;

        /// <summary>
        /// Layer depth of this tilemap renderer.
        /// </summary>
        public float Layer { get; private set; } = layer / 10f;

        // Tilemap structure linked to this renderer.
        private Tilemap _map;

        /// <summary>
        /// Creates a source rectangle for tiles in a spritesheet using a single index value.
        /// </summary>
        /// <param name="index">Index used to extract tile position in the spritesheet.</param>
        /// <returns>Source rectangle wrapping the tile at the given index.</returns>
        private Rectangle GetSourceRectangle(int index)
        {
            int tilesPerRow = Spritesheet.Width / TileResolution;

            int tileX = index % tilesPerRow;
            int tileY = index / tilesPerRow;

            return new Rectangle
            {
                X = tileX * TileResolution,
                Y = tileY * TileResolution,
                Width = TileResolution,
                Height = TileResolution
            };
        }

        /// <summary>
        /// Replaces the currently stored spritesheet.
        /// </summary>
        /// <param name="value">New spritesheet.</param>
        public void SetSpritesheet(Texture2D value)
            => Spritesheet = value;

        /// <summary>
        /// Replaces the currently stored tilemap.
        /// </summary>
        /// <param name="value">New tilemap.</param>
        public void SetTilemap(Tilemap value)
            => _map = value;

        /// <summary>
        /// Draws tiles in a given tilemap using the currently active sprite batch.
        /// </summary>
        /// <param name="map">Tilemap data.</param>
        /// <param name="spriteBatch">Active sprite batch.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < _map.Width; x++)
            {
                for (int y = 0; y < _map.Height; y++)
                {
                    int tileIndex = _map.Tiles[x, y];
                    Vector2 position = new Vector2(x, y) * TileResolution;
                    Rectangle sourceRectangle = GetSourceRectangle(tileIndex);

                    float scale = Camera.Main.PixelResolutionScaleFactor(TileResolution);
                    position = Camera.PixelToWorldPosition(position, scale);
                    position = Camera.Main.WorldToScreenPosition(position);

                    spriteBatch.Draw(Spritesheet, position, sourceRectangle, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, Layer);
                }
            }
        }
    }
}
