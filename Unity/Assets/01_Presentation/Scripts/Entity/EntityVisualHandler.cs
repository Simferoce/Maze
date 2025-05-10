using Game.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Presentation
{
    public class EntityVisualHandler : IDisposable
    {
        private GameManager gameManager;
        private PresentationRegistry presentationRegistry;
        private Dictionary<Guid, EntityVisual> entities = new Dictionary<Guid, EntityVisual>();

        public EntityVisualHandler(PresentationRegistry presentationRegistry, GameManager gameManager)
        {
            this.gameManager = gameManager;
            this.presentationRegistry = presentationRegistry;
        }

        public void Refresh(GameManager gameManager)
        {
            Dispose();
            this.gameManager = gameManager;
        }

        public void Dispose()
        {
            foreach (var entity in entities.Values)
                GameObject.Destroy(entity.gameObject);

            entities.Clear();
        }

        public bool TryGet<T>(Guid id, out T visual)
            where T : EntityVisual
        {
            visual = null;
            if (!entities.ContainsKey(id))
                return false;

            visual = (T)entities[id];
            return visual != null;
        }

        public void Synchronize()
        {
            IEnumerable<Core.Entity> gameEntities = gameManager.WorldManager.GetEntites<Entity>();
            foreach (Game.Core.Entity entity in gameEntities)
            {
                if (entities.ContainsKey(entity.Id))
                    continue;

                EntityPresentationDefinition entityPresentationDefinition = presentationRegistry.Get<EntityPresentationDefinition>(entity.Definition.Id);
                if (entityPresentationDefinition == null)
                {
                    Debug.LogError($"Could not find a representation visual of the definition of entity with id \"{entity.Definition.Id}\" of type \"{entity.GetType().Name}\"");
                    continue;
                }

                if (!entityPresentationDefinition.HasIndependentVisual())
                    continue;

                EntityVisual entityVisual = entityPresentationDefinition.InstantiateVisual(entity);
                entityVisual.Initialize(gameManager, presentationRegistry, entity.Id);

                entities.Add(entity.Id, entityVisual);
            }

            foreach (KeyValuePair<Guid, EntityVisual> visualEntity in entities)
            {
                Entity entity = gameManager.WorldManager.GetEntityById(visualEntity.Value.EntityId);
                if (entity == null)
                {
                    GameObject.Destroy(visualEntity.Value);
                    entities.Remove(visualEntity.Key);
                }
            }
        }
    }
}
