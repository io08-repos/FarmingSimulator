using Microsoft.Xna.Framework.Graphics;
using ParsnipEngine.Entities;
using ParsnipEngine.Interfaces;

namespace ParsnipEngine.Rendering.Components
{
    /// <summary>
    /// Base class for all renderer components.
    /// </summary>
    public abstract class Renderer : IComponent
    {
        public bool Enabled { get; set; }
        public Entity Parent { get; set; }

        // Constructor.
        public Renderer(Entity parent)
        {
            Parent = parent;

            Register();
        }

        /// <summary>
        /// Registers this renderer to the <see cref="RenderManager"/> class.
        /// </summary>
        public virtual void Register()
            => RenderManager.Instance.Register(this);

        /// <summary>
        /// Main draw call for this renderer.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
