using System;

using Microsoft.Xna.Framework;

using ParsnipEngine.Entities;
using ParsnipEngine.Rendering;
using ParsnipEngine.Rendering.Components;

namespace FarmingSimulator.Content.Entities
{
    public class HappyFace : Entity
    {
        private Vector2 _origin;
        private readonly double _piTimesTwo = Math.PI * 2;

        public override void Register()
            => EntityRegistry.Instance.Register("HappyFace", () => new HappyFace());

        public override void Initialize()
        {
            base.Initialize();

            _origin = Position - (Vector2.One * 8f);

            var sprite = SpriteAtlas.Instance.GetSprite("spr_happycircle");
            var spriteRenderer = SpriteRenderer.Create(sprite, this);
            AddComponent(spriteRenderer);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var position = Position;
            position.X = _origin.X + (float)(Math.Sin(gameTime.TotalGameTime.TotalSeconds * _piTimesTwo) * 16f);
            position.Y = _origin.Y - (float)(Math.Cos(gameTime.TotalGameTime.TotalSeconds * _piTimesTwo) * 16f);
            Position = position;
        }
    }
}
