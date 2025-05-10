using UnityEngine;

namespace Game.Presentation
{
    [RequireComponent(typeof(Camera))]
    public class FollowCamera : MonoBehaviour
    {
        private float distance = -0.5f;
        private Camera camera;
        private Transform target;

        private void Awake()
        {
            camera = GetComponent<Camera>();
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        private void LateUpdate()
        {
            if (target == null)
                return;

            camera.transform.position = target.position + Vector3.forward * distance;
        }
    }
}
