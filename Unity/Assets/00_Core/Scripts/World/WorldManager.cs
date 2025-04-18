using System;
using System.Collections.Generic;

namespace Game.Core
{
    public class WorldManager
    {
        private Dictionary<Guid, Entity> entities = new Dictionary<Guid, Entity>();
        private List<Agent> agents = new List<Agent>();
        private GameManager gameManager;

        public WorldManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void Register(Agent agent)
        {
            agents.Add(agent);
        }

        public void Unregister(Agent agent)
        {
            agents.Remove(agent);
        }

        public void Register(Entity entity)
        {
            entities.Add(entity.Id, entity);
        }

        public void Unregister(Entity entity)
        {
            entities.Remove(entity.Id);
        }

        public void Update()
        {
            foreach (Agent agent in agents)
                agent.Update();
        }

        public Entity GetEntityById(Guid id)
        {
            Assertion.IsTrue(entities.ContainsKey(id), $"Could not get the entity with the id \"{id}\" because it is not registered in the world.");
            return entities[id];
        }

        public IEnumerable<Entity> GetAllEntities()
        {
            return entities.Values;
        }
    }
}
