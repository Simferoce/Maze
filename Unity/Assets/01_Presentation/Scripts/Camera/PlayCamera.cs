using UnityEngine;

namespace Game.Presentation
{
    public class PlayCamera : MonoBehaviour
    {
        [SerializeField] private FollowCamera followCamera;

        public void Initialize(ServiceRegistry serviceRegistry)
        {
            followCamera.Refresh(serviceRegistry);
        }
    }
}
