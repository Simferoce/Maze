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

        public static Fixed64 ATan2(Fixed64 y, Fixed64 x)
        {
            // Handle vertical easy cases
            if (x.RawValue == 0)
            {
                return y.RawValue > 0 ? Fixed64.PI_HALF :
                       y.RawValue < 0 ? -Fixed64.PI_HALF : Fixed64.Zero;
            }

            // Extract raw values
            long vx = x.RawValue;
            long vy = y.RawValue;

            // We will keep track of quadrant for correction after CORDIC
            bool xNeg = vx < 0;
            bool yNeg = vy < 0;

            // Use absolute values to simplify CORDIC vectoring
            vx = vx < 0 ? -vx : vx;
            vy = vy < 0 ? -vy : vy;

            long angle = 0;

            ReadOnlySpan<ushort> atanTable = stackalloc ushort[16]
            {
        51472, 30385, 16055, 8149,
        4090, 2045, 1023, 512,
        256, 128, 64, 32,
        16, 8, 4, 2
    };

            for (int i = 0; i < 16; ++i)
            {
                long shift = 1L << i;
                long nx, ny;

                if (vy > 0)
                {
                    nx = vx + (vy >> i);
                    ny = vy - (vx >> i);
                    angle += atanTable[i];
                }
                else
                {
                    nx = vx - (vy >> i);
                    ny = vy + (vx >> i);
                    angle -= atanTable[i];
                }

                vx = nx;
                vy = ny;
            }

            // Now fix the quadrant:
            // Original x and y signs:
            // Quadrant I:  x >= 0, y >= 0 => angle as is
            // Quadrant II: x <  0, y >= 0 => angle =  pi - angle
            // Quadrant III:x <  0, y <  0 => angle = -pi + angle
            // Quadrant IV: x >= 0, y <  0 => angle = -angle

            if (xNeg)
            {
                if (!yNeg)
                {
                    // Quadrant II
                    angle = -angle - Fixed64.PI.RawValue;
                }
                else
                {
                    // Quadrant III
                    angle = angle - Fixed64.PI.RawValue;
                }
            }
            else
            {
                if (yNeg)
                {
                    // Quadrant IV
                    angle = -angle;
                }
                // else Quadrant I: angle unchanged
            }

            //// Clamp angle to [-pi, pi]
            //if (angle > Fixed64.PI)
            //    angle -= (Fixed64.TWO_PI);
            //else if (angle < -Fixed64.PI)
            //    angle += (Fixed64.TWO_PI);

            return new Fixed64(angle);
        }

        public static Fixed64 ATan(Fixed64 v) => ATan2(v, Fixed64.One);
    }
}
