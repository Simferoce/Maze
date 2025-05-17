using UnityEngine;

namespace Game.Presentation
{
    public class CharacterVisual : EntityVisual
    {
        [SerializeField] private Transform weapon;

        private void Update()
        {
            SynchronizePosition();

            Core.Entity entity = serviceRegistry.Get<GameProvider>().GameManager.WorldManager.GetEntityById(EntityId);
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, entity.LocalRotation.ToFloat() * Mathf.Rad2Deg - 90);

            weapon.rotation = Quaternion.Lerp(weapon.rotation, targetRotation, 1 - Mathf.Exp(-serviceRegistry.Get<PresentationConstant>().Damping * Time.deltaTime));
        }
    }
}

