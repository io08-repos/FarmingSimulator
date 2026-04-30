using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ParsnipEngine.Entities;
using ParsnipEngine.Rendering;
using ParsnipEngine.Rendering.Components;

namespace FarmingSimulator.Content.Entities
{
    public class Player : Entity
    {
        public override void Register()
            => EntityRegistry.Instance.Register("Player", () => new Player());

        public override void Initialize()
        {
            base.Initialize();

            var sprite = SpriteAtlas.Instance.GetSprite("spr_square");
            AddComponent(SpriteRenderer.Create(sprite, this));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Vector2 position = Position;

            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                position.X -= 128f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                position.X += 128f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                position.Y -= 128f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                position.Y += 128f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            Position = position;
        }
    }
}
