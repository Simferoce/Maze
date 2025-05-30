using System;

namespace Game.Core
{
    public abstract class Entity : IDisposable
    {
        public Guid Id { get; private set; }
        public GameManager GameManager { get; private set; }
        protected Transform Transform { get; private set; }
        public EntityDefinition Definition { get; private set; }
        public Bounds Bounds { get; protected set; }
        public Vector2 LocalPosition => Transform.LocalPosition;
        public Fixed64 LocalRotation => Transform.LocalRotation;

        public Entity(GameManager gameManager, EntityDefinition definition)
        {
            this.GameManager = gameManager;
            this.Transform = new Transform();
            this.Definition = definition;

            Id = Guid.NewGuid();

            gameManager.WorldManager.Register(this);
        }

        public virtual void Dispose()
        {
        }

        public virtual void Move(Vector2 translation)
        {
            Transform.Translate(translation);
        }

        public virtual void SetPosition(Vector2 position)
        {
            Transform.SetPosition(position);
        }

        public virtual void SetRotation(Fixed64 rotation)
        {
            Transform.SetRotation(rotation);
        }

        public virtual void LookAt(Vector2 point)
        {
            Transform.LookAt(point);
        }

        public virtual void LookIn(Vector2 direction)
        {
            Transform.LookIn(direction);
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