using Game.Core;
using System.Linq;
using UnityEngine;

namespace Game.Presentation
{
    [RequireComponent(typeof(Camera))]
    public class FollowCamera : MonoBehaviour
    {
        private ServiceRegistry serviceRegistry;
        private bool tracking = false;

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
                tracking = false;
                return;
            }

            Player player = gameManager.WorldManager.GetEntites<Player>().FirstOrDefault();
            if (player == null)
            {
                tracking = false;
                return;
            }

            Entity entity = gameManager.WorldManager.GetEntityById(player.Avatar.Id);
            if (entity == null)
            {
                tracking = false;
                return;
            }

            float scale = serviceRegistry.Get<PresentationConstant>().Scale;
            Vector3 target = new Vector3(entity.LocalPosition.X.ToFloat() / scale, entity.LocalPosition.Y.ToFloat() / scale, transform.position.z);
            if (!tracking)
            {
                tracking = true;
                this.transform.position = target;
            }

            this.transform.position = Vector3.Lerp(this.transform.position, target, 1 - Mathf.Exp(-serviceRegistry.Get<PresentationConstant>().Damping * Time.deltaTime));
        }
    }
}
