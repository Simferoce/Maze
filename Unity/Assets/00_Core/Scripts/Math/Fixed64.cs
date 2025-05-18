using System;

namespace Game.Core
{
    public readonly struct Fixed64 : IComparable<Fixed64>, IEquatable<Fixed64>
    {
        public const int FRACTIONAL_BITS = 16;
        public const long ONE = 1L << FRACTIONAL_BITS;
        public const long FRACTION_MASK = ONE - 1;

        public static readonly Fixed64 Zero = new Fixed64(0);
        public static readonly Fixed64 One = new Fixed64(ONE);
        public static readonly Fixed64 PI = new Fixed64(205_887);
        public static readonly Fixed64 TWO_PI = new Fixed64(411_774);
        public static readonly Fixed64 PI_HALF = new Fixed64(102_944);

        public long RawValue => rawValue;

        private readonly long rawValue;

        public Fixed64(long rawValue)
        {
            this.rawValue = rawValue;
        }

        public static Fixed64 FromInt(int value) => new Fixed64((long)value << FRACTIONAL_BITS);
        public static Fixed64 FromFloat(float value) => new Fixed64((long)(value * ONE));
        public static Fixed64 FromDouble(double value) => new Fixed64((long)(value * ONE));

        public int ToInt() => (int)(rawValue >> FRACTIONAL_BITS);
        public float ToFloat() => (float)rawValue / ONE;
        public double ToDouble() => (double)rawValue / ONE;

        public static Fixed64 operator +(Fixed64 a, Fixed64 b) => new Fixed64(a.rawValue + b.rawValue);
        public static Fixed64 operator -(Fixed64 a, Fixed64 b) => new Fixed64(a.rawValue - b.rawValue);
        public static Fixed64 operator -(Fixed64 a) => new Fixed64(-a.rawValue);
        public static Fixed64 operator *(Fixed64 a, Fixed64 b)
        {
            return new Fixed64((a.rawValue * b.rawValue) >> FRACTIONAL_BITS);
        }
        public static Fixed64 operator /(Fixed64 a, Fixed64 b)
        {
            if (b.rawValue == 0)
                throw new DivideByZeroException();

            return new Fixed64((a.rawValue << FRACTIONAL_BITS) / b.rawValue);
        }
        public static Fixed64 operator *(Fixed64 a, int b)
        {
            return new Fixed64((a.rawValue * b));
        }
        public static Fixed64 operator *(int a, Fixed64 b)
        {
            return new Fixed64((b.rawValue * a));
        }

        public static bool operator ==(Fixed64 a, Fixed64 b) => a.rawValue == b.rawValue;
        public static bool operator !=(Fixed64 a, Fixed64 b) => a.rawValue != b.rawValue;
        public static bool operator <(Fixed64 a, Fixed64 b) => a.rawValue < b.rawValue;
        public static bool operator >(Fixed64 a, Fixed64 b) => a.rawValue > b.rawValue;
        public static bool operator <=(Fixed64 a, Fixed64 b) => a.rawValue <= b.rawValue;
        public static bool operator >=(Fixed64 a, Fixed64 b) => a.rawValue >= b.rawValue;

        public override bool Equals(object obj) => obj is Fixed64 other && this == other;
        public bool Equals(Fixed64 other) => this == other;
        public override int GetHashCode() => rawValue.GetHashCode();
        public int CompareTo(Fixed64 other) => rawValue.CompareTo(other.rawValue);

        public override string ToString() => ToDouble().ToString("F5");
    }
}