using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Core
{
    public class WorldManager
    {
        private Dictionary<Guid, Entity> entities = new Dictionary<Guid, Entity>();
        private GameManager gameManager;

        public WorldManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void Register(Entity entity)
        {
            entities.Add(entity.Id, entity);
        }

        public void Unregister(Entity entity)
        {
            entities.Remove(entity.Id);
        }

        public Entity GetEntityById(Guid id)
        {
            Assertion.IsTrue(entities.ContainsKey(id), $"Could not get the entity with the id \"{id}\" because it is not registered in the world.");
            return entities[id];
        }

        public IEnumerable<T> GetEntites<T>()
        {
            return entities.Values.OfType<T>();
        }
    }
}
