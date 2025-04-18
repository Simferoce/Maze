using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Presentation
{
    public class EntityVisualHandler
    {
        private PresentationManager presentationManager;
        private List<EntityVisual> entities = new List<EntityVisual>();

        public EntityVisualHandler(PresentationManager presentationManager)
        {
            this.presentationManager = presentationManager;
        }

        public void Update()
        {
            List<Core.Entity> gameEntities = presentationManager.GameManager.WorldManager.GetAllEntities().ToList();
            foreach (Game.Core.Entity entity in gameEntities)
            {
                EntityVisual entityVisual = entities.FirstOrDefault(x => x.EntityId == entity.Id);
                if (entityVisual == null)
                {
                    EntityPresentationDefinition entityPresentationDefinition = presentationManager.PresentationRegistry.Get<EntityPresentationDefinition>(entity.Definition.Id);
                    entityVisual = GameObject.Instantiate(entityPresentationDefinition.Prefab);
                    entityVisual.Initialize(entity.Id);

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
