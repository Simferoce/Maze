using Game.Core;
using System;
using UnityEngine;

namespace Game.Presentation
{
    public class CharacterVisual : EntityVisual
    {
        [SerializeField] private UnityEngine.Transform upwardAlignedBody;
        [SerializeField] private Animator animator;

        public override void Initialize(ServiceRegistry serviceRegistry, Guid entityId)
        {
            base.Initialize(serviceRegistry, entityId);

            Core.Character character = serviceRegistry.Get<GameProvider>().GameManager.WorldManager.GetEntityById(EntityId) as Character;
            AnimatorOverrideController animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            animator.runtimeAnimatorController = animatorOverrideController;

            foreach (CharacterAbility ability in character.Abilities)
            {
                PresentationCharacterStandardAbilityDefinition presentationCharacterAbilityDefinition = serviceRegistry.Get<PresentationRegistry>().Get<PresentationCharacterStandardAbilityDefinition>(ability.Definition.Id);
                animatorOverrideController["Slot1"] = presentationCharacterAbilityDefinition.Animation;
            }
        }

        protected override void Update()
        {
            base.Update();

            Core.Entity entity = serviceRegistry.Get<GameProvider>().GameManager.WorldManager.GetEntityById(EntityId);
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, entity.LocalRotation.ToFloat() * Mathf.Rad2Deg - 90);
            this.transform.rotation = targetRotation;

            upwardAlignedBody.eulerAngles = new Vector3(upwardAlignedBody.eulerAngles.x, upwardAlignedBody.eulerAngles.y, 0);
            SynchronizeAnimation(entity as Character);
        }

        private void SynchronizeAnimation(Character character)
        {
            if (character.StateMachine.CurrentState is StandardCharacterAbilityCharacterState state)
            {
                long elapsedTick = state.ElapsedTick;
                animator.Play("Slot1", 0, elapsedTick / (float)state.Duration);
            }
        }
    }
}

