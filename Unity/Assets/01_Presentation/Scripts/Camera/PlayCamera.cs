using Game.Core;
using System.Linq;
using UnityEngine;

namespace Game.Presentation
{
    public class PlayCamera : MonoBehaviour
    {
        [SerializeField] private FollowCamera followCamera;

        private GameManager gameManager;
        private EntityVisualHandler entityVisualHandler;

        public void Refresh(GameManager gameManager, EntityVisualHandler entityVisualHandler)
        {
            this.gameManager = gameManager;
            this.entityVisualHandler = entityVisualHandler;
        }

        public void Synchronize()
        {
            Player player = gameManager.WorldManager.GetEntites<Player>().FirstOrDefault();
            if (player != null)
            {
                System.Guid avatarId = player.Avatar.Id;
                if (entityVisualHandler.TryGet<EntityVisual>(avatarId, out EntityVisual visual))
                {
                    followCamera.SetTarget(visual.transform);
                }
            }
            else
            {
                followCamera.SetTarget(null);
            }
        }
    }
}
