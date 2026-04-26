using Microsoft.Xna.Framework;
using ParsnipEngine.Interfaces;

using System;
using System.Collections.Generic;
using System.Text.Json;

namespace ParsnipEngine.Entities
{
    /// <summary>
    /// Parent class for all world entities in the game context.
    /// </summary>
    public abstract class Entity
    {
        public static readonly Entity Empty = null!;

        /// <summary>
        /// World position of this entity.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// World layer of this entity.
        /// </summary>
        public float Layer { get; set; }

        /// <summary>
        /// List of all components linked to this entity.
        /// </summary>
        private readonly Dictionary<Type, IComponent> _components = [];

        // Constructor
        public Entity() => Initialize();

        /// <summary>
        /// Abstract method used to register entity to <see cref="EntityRegistry"/>.
        /// </summary>
        public abstract void Register();

        /// <summary>
        /// Method for the entity's load-up routine.
        /// </summary>
        public virtual void Initialize() { }

        /// <summary>
        /// Method for the entity's custom loop.
        /// </summary>
        /// <param name="gameTime">Time elapsed since last Game.Update() call.</param>
        public virtual void Update(GameTime gameTime) { }

        /// <summary>
        /// Adds component to this entity.
        /// </summary>
        /// <param name="component">Component to add.</param>
        public void AddComponent(IComponent component)
        {
            var type = component.GetType();
            if (_components.ContainsKey(type))
            {
                throw new Exception($"Component of type '{type}' already exists!");
            }

            _components[type] = component;
        }

        /// <summary>
        /// Finds component of a given type inside this entity.
        /// </summary>
        /// <typeparam name="T">Type of component.</typeparam>
        /// <returns>The component of the given type (if it exists).</returns>
        public T? GetComponent<T>() where T : IComponent
            => _components.TryGetValue(typeof(T), out IComponent value) ? (T) value : default;

        /// <summary>
        /// Converts JSON element into an array of entities.
        /// </summary>
        /// <param name="root">JSON element.</param>
        /// <returns>Array of entities listed in the JSON element.</returns>
        public static Entity[] FromJSON(JsonElement root)
        {
            JsonElement dtos = root.GetProperty("entities");
            Entity[] entities = new Entity[dtos.GetArrayLength()];

            for (int i = 0; i < entities.Length; i++)
            {
                JsonElement dtoElement = dtos[i];

                string entityType = dtoElement.GetProperty("type").GetString();
                float x = (float) dtoElement.GetProperty("x").GetDouble();
                float y = (float) dtoElement.GetProperty("y").GetDouble();
                int layer = dtoElement.GetProperty("layer").GetInt32();

                EntityDTO dto = EntityDTO.Create(entityType, x, y, layer);
                Entity entity = EntityRegistry.Instance.Create(dto);

                entities[i] = entity;
            }

            return entities;
        }
    }
}
