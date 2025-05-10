namespace Game.Core
{
    public struct Vector2
    {
        public Fixed64 x;
        public Fixed64 y;

        public static readonly Vector2 Zero = new Vector2(Fixed64.Zero, Fixed64.Zero);
        public static readonly Vector2 One = new Vector2(Fixed64.One, Fixed64.One);
        public static readonly Vector2 Up = new Vector2(Fixed64.Zero, Fixed64.One);
        public static readonly Vector2 Right = new Vector2(Fixed64.One, Fixed64.Zero);

        public Vector2(Fixed64 x, Fixed64 y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2(int x, int y)
        {
            this.x = Fixed64.FromInt(x);
            this.y = Fixed64.FromInt(y);
        }

        public Fixed64 Magnitude => Math.Sqrt(x * x + y * y);
        public Fixed64 SqrMagnitude => x * x + y * y;

        public Vector2 Normalized
        {
            get
            {
                var mag = Magnitude;
                return mag.RawValue == 0 ? Zero : this / mag;
            }
        }

        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.x + b.x, a.y + b.y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.x - b.x, a.y - b.y);
        public static Vector2 operator -(Vector2 v) => new Vector2(-v.x, -v.y);
        public static Vector2 operator *(Vector2 v, Fixed64 scalar) => new Vector2(v.x * scalar, v.y * scalar);
        public static Vector2 operator *(Vector2 v, int scalar) => new Vector2(v.x * scalar, v.y * scalar);
        public static Vector2 operator *(Fixed64 scalar, Vector2 v) => new Vector2(v.x * scalar, v.y * scalar);
        public static Vector2 operator *(int scalar, Vector2 v) => new Vector2(v.x * scalar, v.y * scalar);
        public static Vector2 operator /(Vector2 v, Fixed64 scalar) => new Vector2(v.x / scalar, v.y / scalar);
        public static Vector2 operator /(Vector2 v, int scalar) => new Vector2(v.x / scalar, v.y / scalar);

        public static bool operator ==(Vector2 a, Vector2 b) => a.x == b.x && a.y == b.y;
        public static bool operator !=(Vector2 a, Vector2 b) => !(a == b);

        public override bool Equals(object obj) => obj is Vector2 other && this == other;
        public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode();
        public override string ToString() => $"({x}, {y})";

        public Fixed64 Dot(Vector2 other) => x * other.x + y * other.y;

        public Fixed64 DistanceTo(Vector2 other) => (this - other).Magnitude;

        public static Vector2 Lerp(Vector2 a, Vector2 b, Fixed64 t)
        {
            return new Vector2(
                a.x + (b.x - a.x) * t,
                a.y + (b.y - a.y) * t
            );
        }
    }
}
