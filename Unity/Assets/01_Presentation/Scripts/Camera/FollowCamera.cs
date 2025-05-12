using Game.Core;
using System.Linq;
using UnityEngine;

namespace Game.Presentation
{
    [RequireComponent(typeof(Camera))]
    public class FollowCamera : MonoBehaviour
    {
        private float distance = -0.5f;
        private GameManager gameManager;

        public void Refresh(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        private void LateUpdate()
        {
            if (gameManager == null)
                return;

            Player player = gameManager.WorldManager.GetEntites<Player>().FirstOrDefault();
            if (player == null)
                return;

            Entity entity = gameManager.WorldManager.GetEntityById(player.Avatar.Id);
            if (entity == null)
                return;

            this.transform.position = new Vector3(entity.Transform.LocalPosition.X / 100f, entity.Transform.LocalPosition.Y / 100f, distance);
        }
    }
}
