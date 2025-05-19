using Game.Core;
using System;
using System.Buffers;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Presentation
{
    public class EntityVisualHandler : MonoBehaviour, IDisposable, IService
    {
        [SerializeField] private Camera camera;

        private ServiceRegistry serviceRegistry;
        private Dictionary<Guid, EntityVisual> entities = new Dictionary<Guid, EntityVisual>();

        public void Initialize(ServiceRegistry serviceRegistry)
        {
            this.serviceRegistry = serviceRegistry;
            Dispose();
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
            IEnumerable<Core.Entity> gameEntities = serviceRegistry.Get<GameProvider>().GameManager.WorldManager.GetEntites<Entity>();
            foreach (Game.Core.Entity entity in gameEntities)
            {
                if (entities.ContainsKey(entity.Id))
                    continue;

                if (!IsVisible(entity))
                    continue;

                EntityPresentationDefinition entityPresentationDefinition = serviceRegistry.Get<PresentationRegistry>().Get<EntityPresentationDefinition>(entity.Definition.Id);
                if (entityPresentationDefinition == null)
                {
                    Debug.LogError($"Could not find a representation visual of the definition of entity with id \"{entity.Definition.Id}\" of type \"{entity.GetType().Name}\"");
                    continue;
                }

                if (!entityPresentationDefinition.HasIndependentVisual())
                    continue;

                EntityVisual entityVisual = entityPresentationDefinition.InstantiateVisual(entity);
                entityVisual.Initialize(serviceRegistry, entity.Id);

                entities.Add(entity.Id, entityVisual);
            }

            EntityVisual[] visualEntities = ArrayPool<EntityVisual>.Shared.Rent(entities.Values.Count);
            entities.Values.CopyTo(visualEntities, 0);
            for (int i = 0; i < entities.Values.Count; i++)
            {
                EntityVisual visualEntity = visualEntities[i];

                Entity entity = serviceRegistry.Get<GameProvider>().GameManager.WorldManager.GetEntityById(visualEntity.EntityId);
                if (entity == null)
                {
                    GameObject.Destroy(visualEntity.gameObject);
                    entities.Remove(visualEntity.EntityId);
                }
                else if (!IsVisible(entity))
                {
                    GameObject.Destroy(visualEntity.gameObject);
                    entities.Remove(visualEntity.EntityId);
                }
            }
            ArrayPool<EntityVisual>.Shared.Return(visualEntities);
        }

        private bool IsVisible(Entity entity)
        {
            float halfHeight = camera.orthographicSize;
            float halfWidth = camera.aspect * halfHeight;
            UnityEngine.Vector2 position = camera.transform.position;

            float scale = serviceRegistry.Get<PresentationConstant>().Scale;
            Game.Core.Bounds cameraBounds = new Core.Bounds(new Core.Vector2(Fixed64.FromFloat((position.x - halfWidth) * scale), Fixed64.FromFloat((position.y - halfHeight) * scale)), new Core.Vector2(Fixed64.FromFloat((position.x + halfWidth) * scale), Fixed64.FromFloat((position.y + halfHeight) * scale)));
            Game.Core.Bounds entityWorldBounds = new Core.Bounds(entity.LocalPosition + entity.Bounds.Min, entity.LocalPosition + entity.Bounds.Max);
            return cameraBounds.Overlaps(entityWorldBounds);
        }
    }
}
