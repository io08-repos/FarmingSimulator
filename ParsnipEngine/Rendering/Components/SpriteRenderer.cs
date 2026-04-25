using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParsnipEngine.Entities;

namespace ParsnipEngine.Rendering.Components
{
    /// <summary>
    /// A renderer specialised for drawing singular sprites. <br/>
    /// 
    /// NOTE: This should be used within entity objects later in development!
    /// </summary>
    public class SpriteRenderer : Renderer
    {
        /// <summary>
        /// The sprite used by this renderer component as the texture during the drawing phase.
        /// </summary>
        public Texture2D Sprite { get; private set; }

        /// <summary>
        /// Shading color applied to this renderer.
        /// </summary>
        public Color ShaderColor { get; private set; } = Color.White;


        // Constructor.
        private SpriteRenderer(Entity parent) : base(parent) { }

        /// <summary>
        /// Creates a new SpriteRenderer component, ready for use.
        /// </summary>
        /// <param name="sprite">Sprite to be used by this renderer.</param>
        public static SpriteRenderer Create(Texture2D sprite, Entity parent)
        {
            var renderer = new SpriteRenderer(parent)
            {
                Sprite = sprite
            };

            return renderer;
        }

        /// <summary>
        /// Draws this renderer's sprite.
        /// </summary>
        /// <param name="spriteBatch">Batch used to draw this renderer's sprite.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            float scaleX = Camera.Main.PixelResolutionScaleFactor(Sprite.Width);
            float scaleY = Camera.Main.PixelResolutionScaleFactor(Sprite.Height);

            Vector2 scale = new(scaleX, scaleY);
            Parent.Position = Camera.PixelToWorldPosition(Parent.Position, scale);
            Parent.Position = Camera.Main.WorldToScreenPosition(Parent.Position);

            spriteBatch.Draw(Sprite, Parent.Position, null, ShaderColor, 0f, Vector2.Zero, scale, SpriteEffects.None, Parent.Layer);
        }
    }
}
