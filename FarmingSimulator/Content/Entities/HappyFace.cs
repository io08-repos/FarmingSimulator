using Microsoft.Xna.Framework;
using ParsnipEngine.Entities;
using ParsnipEngine.Rendering;
using ParsnipEngine.Rendering.Components;

namespace FarmingSimulator.Content.Entities
{
    public class HappyFace : Entity
    {
        public override void Register()
            => EntityRegistry.Instance.Register("HappyFace", () => new HappyFace());

        public override void Initialize()
        {
            base.Initialize();

            Position = Vector2.Zero;

            var sprite = SpriteAtlas.Instance.GetSprite("spr_happycircle");
            var spriteRenderer = SpriteRenderer.Create(sprite, this);
            AddComponent(spriteRenderer);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Nothing to put here for now...
        }
    }
}
