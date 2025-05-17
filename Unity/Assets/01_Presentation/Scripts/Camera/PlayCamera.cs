using Game.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Presentation
{
    [RequireComponent(typeof(Camera))]
    public class PlayCamera : MonoBehaviour, IService
    {
        [SerializeField] private FollowCamera followCamera;

        private Camera camera;
        private ServiceRegistry serviceRegistry;

        private void Awake()
        {
            camera = GetComponent<Camera>();
        }

        public void Initialize(ServiceRegistry serviceRegistry)
        {
            followCamera.Refresh(serviceRegistry);
            this.serviceRegistry = serviceRegistry;
        }

        public UnityEngine.Vector2 GetWorldMousePosition()
        {
            UnityEngine.Vector2 mouseScreen = Mouse.current.position.ReadValue();
            float depth = camera.transform.position.z;

            Vector3 screenPoint = new Vector3(mouseScreen.x, mouseScreen.y, depth);
            Vector3 worldPoint = camera.ScreenToWorldPoint(screenPoint);

            return worldPoint;
        }

        public Core.Vector2 GetGameWorldPosition()
        {
            UnityEngine.Vector2 position = GetWorldMousePosition();
            UnityEngine.Vector2 gamePosition = position * serviceRegistry.Get<PresentationConstant>().Scale;

            return new Core.Vector2(Fixed64.FromFloat(gamePosition.x), Fixed64.FromFloat(gamePosition.y));
        }
    }
}
