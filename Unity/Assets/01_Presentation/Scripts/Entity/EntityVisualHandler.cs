using Game.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Presentation
{
    public class EntityVisualHandler : IDisposable
    {
        private GameManager gameManager;
        private PresentationRegistry presentationRegistry;
        private List<EntityVisual> entities = new List<EntityVisual>();

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
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                EntityVisual entityVisual = entities[i];
                GameObject.Destroy(entityVisual.gameObject);
                entities.RemoveAt(i);
            }
        }

        public void Update()
        {
            List<Core.Entity> gameEntities = gameManager.WorldManager.GetAllEntities().ToList();
            foreach (Game.Core.Entity entity in gameEntities)
            {
                EntityVisual entityVisual = entities.FirstOrDefault(x => x.EntityId == entity.Id);
                if (entityVisual == null)
                {
                    EntityPresentationDefinition entityPresentationDefinition = presentationRegistry.Get<EntityPresentationDefinition>(entity.Definition.Id);
                    entityVisual = GameObject.Instantiate(entityPresentationDefinition.Prefab);
                    entityVisual.Initialize(gameManager, entity.Id);

                    entities.Add(entityVisual);
                }
            }

            for (int i = entities.Count - 1; i >= 0; i--)
            {
                EntityVisual entityVisual = entities[i];
                Core.Entity entity = gameEntities.FirstOrDefault(x => x.Id == entityVisual.EntityId);
                if (entity == null)
                {
                    GameObject.Destroy(entityVisual.gameObject);
                    entities.RemoveAt(i);
                }
            }
        }
    }
}
