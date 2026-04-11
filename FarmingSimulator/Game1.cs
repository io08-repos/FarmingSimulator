using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ParsnipEngine.Rendering;
using ParsnipEngine.Rendering.Components;
using ParsnipEngine.Tilemaps;

namespace FarmingSimulator
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private TilemapRenderer _tilemapRenderer;
        private Tilemap _tilemap;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;

            _graphics.ApplyChanges();

            Camera.Initialize(position: Vector2.Zero, scale: 2f, graphicsDevice: _graphics);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            SpriteAtlas.Instance.LoadSprite(this, "tlm_ground");

            var spritesheet = SpriteAtlas.Instance.GetSprite("tlm_ground");
            _tilemapRenderer = new TilemapRenderer(spritesheet, 16);

            int width = 8, height = 8;
            int[] map = [
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 1, 1, 1, 1, 1, 1, 0,
                0, 2, 2, 2, 2, 2, 2, 0,
                0, 2, 2, 2, 2, 2, 2, 0,
                0, 1, 1, 1, 1, 1, 1, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
            ];

            _tilemap = Tilemap.CreateTilemap(width, height, map);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _tilemapRenderer.DrawTilemap(_tilemap, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
