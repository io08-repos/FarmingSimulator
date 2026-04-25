using Microsoft.Xna.Framework.Graphics;
using ParsnipEngine.Rendering.Components;
using System.Collections.Generic;

namespace ParsnipEngine.Rendering
{
    /// <summary>
    /// Central dispatcher for renderer draw calls.
    /// </summary>
    public class RenderManager
    {
        // Singleton instance.
        public static RenderManager Instance { get; private set; }

        /// <summary>
        /// List of renderer components.
        /// </summary>
        private List<Renderer> _renderers = [];

        /// <summary>
        /// Initialises the singleton instance of <see cref="RenderManager"/>.
        /// </summary>
        public static void Initialize()
            => Instance = new RenderManager();

        /// <summary>
        /// Registers a <see cref="Renderer"/> type to this <see cref="RenderManager"/>.
        /// </summary>
        /// <param name="renderer"><see cref="Renderer"/> type to register.</param>
        public void Register(Renderer renderer)
        {
            if (_renderers.Contains(renderer))
                return;

            _renderers.Add(renderer);
        }

        /// <summary>
        /// Draws every registered renderer.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void DrawAll(SpriteBatch spriteBatch)
        {
            foreach (Renderer renderer in _renderers)
            {
                renderer.Draw(spriteBatch);
            }
        }
    }
}
