using FarmingSimulator.Content.Entities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ParsnipEngine.Entities;
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

        private SpriteRenderer _spriteRenderer;
        private Vector2 _position = Vector2.Zero;

        private Entity _entity;
        private EntityDTO _entityDTO;

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

            SpriteAtlas.Instance.LoadSprite(this, "tlm_ground");
            SpriteAtlas.Instance.LoadSprite(this, "spr_happycircle");

            Camera.Initialize(position: Vector2.Zero, scale: 2f, graphicsDevice: _graphics);
            RenderManager.Initialize();
            EntityRegistry.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var spritesheet = SpriteAtlas.Instance.GetSprite("tlm_ground");
            _tilemapRenderer = new TilemapRenderer(spritesheet, 16, 0);

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
            _tilemapRenderer.SetTilemap(_tilemap);

            _entityDTO = EntityDTO.Create("HappyFace", 0, 0, 10);
            _entity = EntityRegistry.Instance.Create(_entityDTO);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var keyboardState = Keyboard.GetState();

            float cameraScale = Camera.Main.Scale;
            float cameraSpeed = 400f;
            Vector2 cameraPosition = Camera.Main.Position;

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                cameraPosition -= (float)gameTime.ElapsedGameTime.TotalSeconds * cameraSpeed * Vector2.UnitX;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                cameraPosition += (float)gameTime.ElapsedGameTime.TotalSeconds * cameraSpeed * Vector2.UnitX;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                cameraPosition -= (float)gameTime.ElapsedGameTime.TotalSeconds * cameraSpeed * Vector2.UnitY;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                cameraPosition += (float)gameTime.ElapsedGameTime.TotalSeconds * cameraSpeed * Vector2.UnitY;
            }

            if (keyboardState.IsKeyDown(Keys.OemComma))
            {
                cameraScale += 1f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (keyboardState.IsKeyDown(Keys.OemPeriod))
            {
                cameraScale -= 1f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            cameraScale = (cameraScale < 0) ? 0 : cameraScale;
            Camera.Main.SetScale(cameraScale);
            Camera.Main.SetPosition(cameraPosition);

            //_position.X = -8 + (float)(System.Math.Sin(gameTime.TotalGameTime.TotalSeconds) * 8f);
            //_position.Y = -8 - (float)(System.Math.Cos(gameTime.TotalGameTime.TotalSeconds) * 8f);
            //_spriteRenderer.position = _position;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(
                sortMode: SpriteSortMode.Deferred,
                samplerState: SamplerState.PointClamp,
                depthStencilState: null,
                rasterizerState: null,
                effect: null,
                transformMatrix: null
            );

            //_tilemapRenderer.Draw(_spriteBatch);

            //_spriteRenderer.Draw(_spriteBatch);

            //_entity.GetComponent<SpriteRenderer>().Draw(_spriteBatch);

            RenderManager.Instance.DrawAll(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
