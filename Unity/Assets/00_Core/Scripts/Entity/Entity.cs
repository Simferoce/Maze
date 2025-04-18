using System;

namespace Game.Core
{
    public class Entity
    {
        public Guid Id { get; private set; }
        public GameManager GameManager { get; private set; }
        public Transform Transform { get; private set; }
        public AttributeHandler AttributeHandler { get; private set; }
        public EntityDefinition Definition { get; private set; }

        public Entity(GameManager gameManager, EntityDefinition entityDefinition)
        {
            this.GameManager = gameManager;
            this.Transform = new Transform();

            Id = Guid.NewGuid();
            Definition = entityDefinition;
            AttributeHandler = Definition.AttributeHandler.Clone();

            gameManager.WorldManager.Register(this);
        }

        public void Move(Vector2 translation)
        {
            Transform.Translate(translation);
        }
    }
}