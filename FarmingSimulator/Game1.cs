using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ParsnipEngine.Entities;
using ParsnipEngine.Rendering;
using ParsnipEngine.Scenes;

namespace FarmingSimulator
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

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
            SpriteAtlas.Instance.LoadSprite(this, "tlm_ground_big");
            SpriteAtlas.Instance.LoadSprite(this, "spr_happycircle");

            Camera.Initialize(position: Vector2.Zero, scale: 2f, graphicsDevice: _graphics);
            RenderManager.Initialize();
            EntityRegistry.Initialize();

            SceneManager.LoadScene("SceneManagerTest2.json");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(
                sortMode: SpriteSortMode.FrontToBack,
                samplerState: SamplerState.PointClamp,
                depthStencilState: null,
                rasterizerState: null,
                effect: null,
                transformMatrix: null
            );

            RenderManager.Instance.DrawAll(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
