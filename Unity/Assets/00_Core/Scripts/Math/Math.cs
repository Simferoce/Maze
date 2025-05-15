using System;

namespace Game.Core
{
    public static class Math
    {
        public static Fixed64 Sqrt(Fixed64 value)
        {
            if (value.RawValue < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Cannot calculate square root of a negative number");

            if (value.RawValue == 0)
                return Fixed64.Zero;

            ulong n = (ulong)value.RawValue;
            ulong result = 0;
            ulong bit = 1UL << 62;

            n <<= Fixed64.FRACTIONAL_BITS;

            while (bit > n)
                bit >>= 2;

            while (bit != 0)
            {
                if (n >= result + bit)
                {
                    n -= result + bit;
                    result = (result >> 1) + bit;
                }
                else
                {
                    result >>= 1;
                }
                bit >>= 2;
            }

            return new Fixed64((long)result);
        }

        public static Fixed64 Clamp01(Fixed64 value)
        {
            if (value > Fixed64.One)
                return Fixed64.One;
            else if (value < -Fixed64.One)
                return -Fixed64.One;

            return value;
        }

        public static Fixed64 Min(Fixed64 a, Fixed64 b)
        {
            return a < b ? a : b;
        }

        public static Fixed64 Max(Fixed64 a, Fixed64 b)
        {
            return a > b ? a : b;
        }

        public static Fixed64 Abs(Fixed64 a)
        {
            return a.RawValue < 0 ? new Fixed64(-a.RawValue) : a;
        }

        public static Fixed64 Sign(Fixed64 a)
        {
            return a.RawValue > 0 ? Fixed64.One : -Fixed64.One;
        }
    }
}
