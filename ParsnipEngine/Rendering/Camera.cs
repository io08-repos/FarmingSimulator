using Microsoft.Xna.Framework;

namespace ParsnipEngine.Rendering
{
    /// <summary>
    /// World viewport used to project rendered data onto the screen.
    /// </summary>
    public class Camera
    {
        /// <summary>
        /// Screen resolution used as reference for viewport scaling.
        /// </summary>
        public static GraphicsDeviceManager GraphicsDevice { get; private set; }

        /// <summary>
        /// Camera position (in world units).
        /// </summary>
        public Vector2 Position { get; private set; }

        /// <summary>
        /// Camera scale (1f = 1280x720)
        /// </summary>
        public float Scale { get; private set; }

        /// <summary>
        /// Calculates the top-left world position of this camera.
        /// </summary>
        /// <returns>The result of the calculation.</returns>
        public static Vector2 SizeOver2()
        {
            Vector2 size = new (GraphicsDevice.PreferredBackBufferWidth, GraphicsDevice.PreferredBackBufferHeight);
            return size / 2;
        }

        /// <summary>
        /// Main camera of the game engine.
        /// </summary>
        public static Camera Main { get; private set; }

        // Constructor
        private Camera(Vector2 position, float scale)
        {
            Position = position;
            Scale = scale;
        }

        /// <summary>
        /// Initializes the camera object privately.
        /// </summary>
        /// <param name="position">Initial camera world position.</param>
        /// <param name="scale">Initial camera size.</param>
        public static void Initialize(Vector2 position, float scale, GraphicsDeviceManager graphicsDevice)
        {
            GraphicsDevice = graphicsDevice;

            Main = new Camera(position, scale);
        }

        /// <summary>
        /// Sets a new world position for the given camera.
        /// </summary>
        /// <param name="position">New world position.</param>
        public void SetPosition(Vector2 position) => Position = position;

        /// <summary>
        /// Sets a new scale (or 'zoom factor') for the given camera.
        /// </summary>
        /// <param name="scale">New scale.</param>
        public void SetScale(float scale) => Scale = scale;

        /// <summary>
        /// Converts a given pixel position (with <see cref="Position"/> as the top-left anchor) into a world position by scaling.
        /// </summary>
        /// <param name="pixelPosition">Similar to a "screen position", but anchored to <see cref="Position"/> by the top-left corner.</param>
        /// <param name="scaleFactor">Scaling applied to <paramref name="pixelPosition"/>.</param>
        /// <returns>The resulting world position.</returns>
        public static Vector2 PixelToWorldPosition(Vector2 pixelPosition, float scaleFactor)
            => pixelPosition * scaleFactor;

        /// <summary>
        /// Converts a given world position into its equivalent screen position.
        /// </summary>
        /// <param name="worldPosition">The given world position.</param>
        /// <returns><paramref name="worldPosition"/> as a screen position.</returns>
        public Vector2 WorldToScreenPosition(Vector2 worldPosition)
        {
            Vector2 halfSize = SizeOver2();
            worldPosition -= Position;

            return worldPosition + halfSize;
        }

        /// <summary>
        /// Converts a pixel resolution (essentially world units) into a scale factor to use inside MonoGame's sprite batch draw calls.
        /// </summary>
        /// <param name="pixelResolution">The given pixel resolution.</param>
        /// <returns>The resulting scale factor.</returns>
        public float PixelResolutionScaleFactor(int pixelResolution)
        {
            float scaledPixelResolution = pixelResolution / Scale;
            float scaleFactor = scaledPixelResolution / pixelResolution;

            return scaleFactor;
        }
    }
}
