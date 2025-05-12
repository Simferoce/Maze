using System;

namespace Game.Core
{
    public struct CircleCollision
    {
        public static CircleCollision Undefined = new CircleCollision(Vector2.Zero, Fixed64.Zero, -1);

        public Vector2 Center;
        public Fixed64 Radius;
        public int Id;

        public CircleCollision(Vector2 center, Fixed64 radius, int id)
        {
            Center = center;
            Radius = radius;
            Id = id;
        }

        public static bool operator ==(CircleCollision a, CircleCollision b) => a.Center == b.Center && a.Radius == b.Radius;
        public static bool operator !=(CircleCollision a, CircleCollision b) => a.Center != b.Center || a.Radius != b.Radius;

        public override bool Equals(object other)
        {
            return other is CircleCollision circle && circle == this;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Center, Radius);
        }
    }
}
