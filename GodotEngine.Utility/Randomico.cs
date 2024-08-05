using System;

namespace Cobilas.GodotEngine.Utility;

public static class Randomico {
    private static Random random = new();
    
    /// <summary>Returns a random number between 0.0 [inclusive] and 1.0 [inclusive] (Read Only).</summary>
    public static double value = random.NextDouble();
    /// <summary>Less than <c>0.5f</c> is false, greater than <c>0.5f</c> is true.(<c><seealso cref="Randomico"/>.value > 0.5f</c>)</summary>
    public static bool BooleanRandom => value > .5f;

    public static void InitSeed(in int seed) => random = new(seed);

    /// <summary>Fills the elements of a specified array of bytes with random numbers.</summary>
    /// <param name="buffer">An array of bytes to contain random numbers.</param>
    /// <exception cref="ArgumentNullException">buffer is null.</exception>
    public static void ByteList(byte[] buffer) {
        if (buffer is null) throw new ArgumentNullException(nameof(buffer));
        random.NextBytes(buffer);
    }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
    public static void ByteList(Span<byte> buffer) => random.NextBytes(buffer);
#endif
//=====================ByteRange================================================
    /// <summary>Return a random integer number between min [inclusive] and max [exclusive] (ReadOnly).</summary>
    public static byte ByteRange(byte min, byte max)
        => (byte)IntRange(min, max);
    /// <summary>Return a random integer number between min [0] and max [exclusive] (ReadOnly).</summary>
    public static byte ByteRange(byte max)
        => (byte)IntRange(byte.MinValue, max);
    /// <summary>Return a random integer number between min [0] and max [255] (ReadOnly).</summary>
    public static byte ByteRange()
        => (byte)IntRange(byte.MinValue, byte.MaxValue);
//======================SByteRange==============================================
    /// <summary>Return a random integer number between min [inclusive] and max [exclusive] (ReadOnly).</summary>
    public static sbyte SByteRange(sbyte min, sbyte max)
        => (sbyte)IntRange(min, max);
    /// <summary>Return a random integer number between min [-128] and max [exclusive] (ReadOnly).</summary>
    public static sbyte SByteRange(sbyte max)
        => (sbyte)IntRange(sbyte.MinValue, max);
    /// <summary>Return a random integer number between min [-128] and max [127] (ReadOnly).</summary>
    public static sbyte SByteRange()
        => (sbyte)IntRange(sbyte.MinValue, sbyte.MaxValue);
//======================ShortRange===============================================
    /// <summary>Return a random integer number between min [inclusive] and max [exclusive] (ReadOnly).</summary>
    public static short ShortRange(short min, short max)
        => (short)IntRange(min, max);
    /// <summary>Return a random integer number between min [-32768] and max [exclusive] (ReadOnly).</summary>
    public static short ShortRange(short max)
        => (short)IntRange(short.MinValue, max);
    /// <summary>Return a random integer number between min [-32768] and max [32767] (ReadOnly).</summary>
    public static short ShortRange()
        => (short)IntRange(short.MinValue, short.MaxValue);
//=======================UShortRange=============================================
    /// <summary>Return a random integer number between min [inclusive] and max [exclusive] (ReadOnly).</summary>
    public static ushort UShortRange(ushort min, ushort max)
        => (ushort)IntRange(min, max);
    /// <summary>Return a random integer number between min [0] and max [exclusive] (ReadOnly).</summary>
    public static ushort UShortRange(ushort max)
        => (ushort)IntRange(ushort.MinValue, max);
    /// <summary>Return a random integer number between min [0] and max [65535] (ReadOnly).</summary>
    public static ushort UShortRange()
        => (ushort)IntRange(ushort.MinValue, ushort.MaxValue);
//====================IntRange====================================================
    /// <summary>Return a random integer number between min [inclusive] and max [exclusive] (ReadOnly).</summary>
    public static int IntRange(int min, int max) => random.Next(min, max);
    /// <summary>Return a random integer number between min [-2147483648] and max [exclusive] (ReadOnly).</summary>
    public static int IntRange(int max) => random.Next(int.MinValue, max);
    /// <summary>Return a random integer number between min [-2147483648] and max [2147483647] (ReadOnly).</summary>
    public static int IntRange() => random.Next(int.MinValue, int.MaxValue);
//=====================LongRange==================================================
    /// <summary>Return a random integer number between min [inclusive] and max [exclusive] (ReadOnly).</summary>
    public static long LongRange(long min, long max)
        => (long)DoubleRange(min, max);
    /// <summary>Return a random integer number between min [-9223372036854775808] and max [exclusive] (ReadOnly).</summary>
    public static long LongRange(long max)
        => (long)DoubleRange(long.MinValue, max);
    /// <summary>Return a random integer number between min [-9223372036854775808] and max [9223372036854775807] (ReadOnly).</summary>
    public static long LongRange()
        => (long)DoubleRange(long.MinValue, long.MaxValue);
//======================ULongRange=================================================
    /// <summary>Return a random integer number between min [inclusive] and max [exclusive] (ReadOnly).</summary>
    public static ulong ULongRange(ulong min, ulong max)
        => (ulong)DoubleRange(min, max);
    /// <summary>Return a random integer number between min [0] and max [exclusive] (ReadOnly).</summary>
    public static ulong ULongRange(ulong max)
        => (ulong)DoubleRange(ulong.MinValue, max);
    /// <summary>Return a random integer number between min [0] and max [18446744073709551615] (ReadOnly).</summary>
    public static ulong ULongRange()
        => (ulong)DoubleRange(ulong.MinValue, ulong.MaxValue);
//======================FloatRange===============================================================
    /// <summary>Return a random float number between min [inclusive] and max [exclusive] (ReadOnly).</summary>
    public static float FloatRange(float min, float max)
        => (float)DoubleRange(min, max);
    /// <summary>Return a random float number between min [-3.4028235E+38F] and max [exclusive] (ReadOnly).</summary>
    public static float FloatRange(float max)
        => (float)DoubleRange(float.MinValue, max);
    /// <summary>Return a random float number between min [-3.4028235E+38F] and max [3.4028235E+38F] (ReadOnly).</summary>
    public static float FloatRange()
        => (float)DoubleRange(float.MinValue, float.MaxValue);
//=======================DoubleRange=============================================================
    /// <summary>Return a random float number between min [inclusive] and max [exclusive] (ReadOnly).</summary>
    public static double DoubleRange(double min, double max) {
        double smin;
        double compri;
        if ((min < 0 && max > 0) || (min > 0 && max < 0)) {
            double minPorc = min < 0d ? min / double.MinValue : min / double.MaxValue;
            double maxPorc = max < 0d ? max / double.MinValue : max / double.MaxValue;

            compri = Math.Abs((min < 0 ? -minPorc : minPorc) - (max < 0 ? -maxPorc : maxPorc));
            smin = minPorc < maxPorc ? minPorc : maxPorc;

            compri = smin + (value * compri);

            if (compri < 0) return -compri * double.MinValue;
            else if (compri > 0) return compri * double.MaxValue;
            else return 0d;
        }
        compri = Math.Abs(min - max);
        smin = min < max ? min : max;
        return smin + (value * compri);
    }
    /// <summary>Return a random float number between min [-1.7976931348623157E+308] and max [exclusive] (ReadOnly).</summary>
    public static double DoubleRange(double max)
        => DoubleRange(double.MinValue, max);
    /// <summary>Return a random float number between min [-1.7976931348623157E+308] and max [1.7976931348623157E+308] (ReadOnly).</summary>
    public static double DoubleRange()
        => DoubleRange(double.MinValue, double.MaxValue);
//========================DecimalRange===============================================================
    /// <summary>Return a random float number between min [inclusive] and max [exclusive] (ReadOnly).</summary>
    public static decimal DecimalRange(decimal min, decimal max) {
        decimal smin;
        decimal compri;
        if ((min < 0 && max > 0) || (min > 0 && max < 0)) {
            decimal minPorc = min < 0m ? min / decimal.MinValue : min / decimal.MaxValue;
            decimal maxPorc = max < 0m ? max / decimal.MinValue : max / decimal.MaxValue;

            compri = Math.Abs((min < 0 ? -minPorc : minPorc) - (max < 0 ? -maxPorc : maxPorc));
            smin = minPorc < maxPorc ? minPorc : maxPorc;

            compri = smin + ((decimal)value * compri);

            if (compri < 0) return -compri * decimal.MinValue;
            else if (compri > 0) return compri * decimal.MaxValue;
            else return decimal.Zero;
        }
        compri = Math.Abs(min - max);
        smin = min < max ? min : max;
        return smin + ((decimal)value * compri);
    }
    /// <summary>Return a random float number between min [-79228162514264337593543950335M] and max [exclusive] (ReadOnly).</summary>
    public static decimal DecimalRange(decimal max)
        => DecimalRange(decimal.MinValue, max);
    /// <summary>Return a random float number between min [-79228162514264337593543950335M] and max [79228162514264337593543950335M] (ReadOnly).</summary>
    public static decimal DecimalRange()
        => DecimalRange(decimal.MinValue, decimal.MaxValue);
}