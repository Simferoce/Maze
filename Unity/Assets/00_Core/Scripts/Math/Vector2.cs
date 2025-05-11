namespace Game.Core
{
    public struct Vector2
    {
        public Fixed64 X;
        public Fixed64 Y;

        public static readonly Vector2 Zero = new Vector2(Fixed64.Zero, Fixed64.Zero);
        public static readonly Vector2 One = new Vector2(Fixed64.One, Fixed64.One);
        public static readonly Vector2 Up = new Vector2(Fixed64.Zero, Fixed64.One);
        public static readonly Vector2 Right = new Vector2(Fixed64.One, Fixed64.Zero);

        public Vector2(Fixed64 x, Fixed64 y)
        {
            this.X = x;
            this.Y = y;
        }

        public Vector2(int x, int y)
        {
            this.X = Fixed64.FromInt(x);
            this.Y = Fixed64.FromInt(y);
        }

        public Fixed64 Magnitude => Math.Sqrt(X * X + Y * Y);
        public Fixed64 SqrMagnitude => X * X + Y * Y;

        public Vector2 Normalized
        {
            get
            {
                var mag = Magnitude;
                return mag.RawValue == 0 ? Zero : this / mag;
            }
        }

        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.X + b.X, a.Y + b.Y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.X - b.X, a.Y - b.Y);
        public static Vector2 operator -(Vector2 v) => new Vector2(-v.X, -v.Y);
        public static Vector2 operator *(Vector2 v, Fixed64 scalar) => new Vector2(v.X * scalar, v.Y * scalar);
        public static Vector2 operator *(Vector2 v, int scalar) => new Vector2(v.X * scalar, v.Y * scalar);
        public static Vector2 operator *(Fixed64 scalar, Vector2 v) => new Vector2(v.X * scalar, v.Y * scalar);
        public static Vector2 operator *(int scalar, Vector2 v) => new Vector2(v.X * scalar, v.Y * scalar);
        public static Vector2 operator /(Vector2 v, Fixed64 scalar) => new Vector2(v.X / scalar, v.Y / scalar);
        public static Vector2 operator /(Vector2 v, int scalar) => new Vector2(v.X / scalar, v.Y / scalar);

        public static bool operator ==(Vector2 a, Vector2 b) => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(Vector2 a, Vector2 b) => !(a == b);

        public override bool Equals(object obj) => obj is Vector2 other && this == other;
        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();
        public override string ToString() => $"({X}, {Y})";

        public Fixed64 Dot(Vector2 other) => X * other.X + Y * other.Y;

        public Fixed64 DistanceTo(Vector2 other) => (this - other).Magnitude;

        public static Vector2 Lerp(Vector2 a, Vector2 b, Fixed64 t)
        {
            return new Vector2(
                a.X + (b.X - a.X) * t,
                a.Y + (b.Y - a.Y) * t
            );
        }
    }
}
