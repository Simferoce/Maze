using Game.Core;
using System.Linq;
using UnityEngine;

namespace Game.Presentation
{
    [RequireComponent(typeof(Camera))]
    public class FollowCamera : MonoBehaviour
    {
        private ServiceRegistry serviceRegistry;

        public void Refresh(ServiceRegistry serviceRegistry)
        {
            this.serviceRegistry = serviceRegistry;
        }

        private void LateUpdate()
        {
            if (serviceRegistry == null)
                return;

            GameManager gameManager = serviceRegistry.Get<GameProvider>().GameManager;
            if (gameManager == null)
            {
                return;
            }

            Player player = gameManager.WorldManager.GetEntites<Player>().FirstOrDefault();
            if (player == null)
            {
                return;
            }

            Entity entity = gameManager.WorldManager.GetEntityById(player.Avatar.Id);
            if (entity == null)
            {
                return;
            }

            EntityVisualHandler entityVisualHandler = serviceRegistry.Get<EntityVisualHandler>();
            float scale = serviceRegistry.Get<PresentationConstant>().Scale;
            if (entityVisualHandler.TryGet<CharacterVisual>(entity.Id, out CharacterVisual characterVisual))
            {
                this.transform.position = new Vector3(characterVisual.transform.position.x, characterVisual.transform.position.y, this.transform.position.z);
            }
            else
            {
                this.transform.position = new Vector3(entity.LocalPosition.X.ToFloat(), entity.LocalPosition.Y.ToFloat(), this.transform.position.z) / scale;
            }
        }
    }
}
