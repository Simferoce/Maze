using System;

namespace Game.Core
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }
        public GameManager GameManager { get; private set; }
        public Transform Transform { get; private set; }
        public EntityDefinition Definition { get; private set; }

        public Entity(GameManager gameManager, EntityDefinition definition)
        {
            this.GameManager = gameManager;
            this.Transform = new Transform();
            this.Definition = definition;

            Id = Guid.NewGuid();

            gameManager.WorldManager.Register(this);
        }

        public void Move(Vector2 translation)
        {
            Transform.Translate(translation);
        }

        public void SetPosition(Vector2 position)
        {
            Transform.SetPosition(position);
        }
    }

    public abstract class Entity<T> : Entity
        where T : EntityDefinition
    {
        public new T Definition { get; private set; }

        public Entity(GameManager gameManager, T definition) : base(gameManager, definition)
        {
            Definition = definition;
        }
    }
}