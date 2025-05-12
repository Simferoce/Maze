using Game.Core;
using UnityEngine;

namespace Game.Presentation
{
    public class PlayCamera : MonoBehaviour
    {
        [SerializeField] private FollowCamera followCamera;

        private GameManager gameManager;

        public void Refresh(GameManager gameManager)
        {
            this.gameManager = gameManager;
            followCamera.Refresh(gameManager);
        }
    }
}
