using UnityEngine;

namespace Game.Presentation
{
    [CreateAssetMenu(fileName = "PresentationConstant", menuName = "Definitions/PresentationConstant")]
    public class PresentationConstant : ScriptableObject, IService
    {
        [SerializeField] private float scale;
        [SerializeField] private float damping = 20f;

        public float Scale { get => scale; set => scale = value; }
        public float Damping { get => damping; set => damping = value; }
    }
}
