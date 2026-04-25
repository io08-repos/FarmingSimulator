namespace ParsnipEngine.Entities
{
    /// <summary>
    /// Data-transfering object (DTO) for world entities.
    /// </summary>
    public struct EntityDTO
    {
        /// <summary>
        /// Type ID of the entity.
        /// </summary>
        public string Type;
        
        /// <summary>
        /// World position in the X-axis.
        /// </summary>
        public float X;

        /// <summary>
        /// World position in the Y-axis.
        /// </summary>
        public float Y;

        /// <summary>
        /// World layer (similar to a Z-axis).
        /// </summary>
        public int Layer;

        /// <summary>
        /// Creates a data-transfering object used to build an entity from.
        /// </summary>
        /// <param name="type">Type ID (string)</param>
        /// <param name="x">World position in the X-axis.</param>
        /// <param name="y">"World position in the Y-axis.></param>
        /// <param name="layer">World layer (similar to a Z-axis).</param>
        /// <returns>The resulting DTO.</returns>
        public static EntityDTO Create(string type, float x, float y, int layer)
        {
            var dto = new EntityDTO
            {
                Type = type,
                X = x,
                Y = y,
                Layer = layer
            };

            return dto;
        }
    }
}
