using ParsnipEngine.Entities;

namespace ParsnipEngine.Interfaces
{
    /// <summary>
    /// Interfaced enforced by entity components.
    /// </summary>
    public interface IComponent
    {
        /// <summary>
        /// Component state (enabled or disabled)
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// Parent entity of this component.
        /// </summary>
        Entity Parent { get; set; }
    }
}
