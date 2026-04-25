using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ParsnipEngine.Entities
{
    /// <summary>
    /// Registry for world entity types.
    /// </summary>
    public class EntityRegistry
    {
        // Register of entities.
        private readonly Dictionary<string, Func<Entity>> _entities = [];

        // Singleton instance.
        public static EntityRegistry Instance { get; private set; }

        /// <summary>
        /// Initialises the singleton instance of EntityRegistry.
        /// </summary>
        public static void Initialize()
        {
            Instance ??= new();
            RegisterAll();
        }

        /// <summary>
        /// Scans the currently executed assembly for entity types, creates an instance for each, and registers them.
        /// </summary>
        private static void RegisterAll()
        {
            var entityType = typeof(Entity);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> types = [];

            foreach (var assembly in assemblies)
            {
                types.AddRange(
                    assembly // Assembly scan.
                    .GetTypes()
                    .Where(type => type.IsClass && !type.IsAbstract && entityType.IsAssignableFrom(type))
                );
            }

            foreach (var entity in types)
            {
                var instance = (Entity)Activator.CreateInstance(entity); // Creates instance from type.
                instance.Register();
                instance = null;
            }
        }

        /// <summary>
        /// Registers an entity by string key and constructor value.
        /// </summary>
        /// <param name="key">Entity ID.</param>
        /// <param name="entityFunc">Entity constructor.</param>
        public void Register(string key, Func<Entity> entityFunc)
        {
            if (_entities.ContainsKey(key))
            {
                throw new InvalidOperationException($"Entity with key '{key}' already exists!");
            }

            _entities[key] = entityFunc;
        }

        /// <summary>
        /// Creates an entity using the register's data and a DTO for generic entity data.
        /// </summary>
        /// <param name="dto">Data-transferring object.</param>
        /// <returns>An entity of type <see cref="EntityDTO.Type"/>, with custom data.</returns>
        public Entity Create(EntityDTO dto)
        {
            string type = dto.Type;
            var entity = _entities[type]();

            entity.Position = new Vector2(dto.X, dto.Y);
            entity.Layer = dto.Layer / 1000f;

            return entity;
        }
    }
}
