using Godot;
using System;
using Cobilas.GodotEngine.Utility.Numerics;

namespace Cobilas.GodotEngine.Utility;

/// <summary>Stores information about a screen resolution.</summary>
[Serializable]
public readonly struct Resolution : IEquatable<Resolution>,
    IEquatable<Vector2D>, IEquatable<Vector2DInt>, IEquatable<Vector2>, IEquatable<int> {
    private readonly int frequency;
    private readonly float width;
    private readonly float height;

    /// <summary>Screen frequency.</summary>
    /// <returns>Returns the frequency of this resolution.</returns>
    public int Frequency => frequency;
    /// <summary>The width of the screen.</summary>
    /// <returns>Returns the height of this resolution.</returns>
    public float Width => width;
    /// <summary>The height of the screen.</summary>
    /// <returns>Returns the width of this resolution.</returns>
    public float Height => height;

    /// <summary>Starts a new instance of the object.</summary>
    /// <param name="resolution">Screen resolution.</param>
    /// <param name="frequency">Screen frequency.</param>
    public Resolution(in Vector2D resolution, in int frequency) : this(resolution.x, resolution.y, frequency) {}

    /// <summary>Starts a new instance of the object.</summary>
    /// <param name="resolution">Screen resolution.</param>
    /// <param name="frequency">Screen frequency.</param>
    public Resolution(in Vector2DInt resolution, in int frequency) : this(resolution.x, resolution.y, frequency) {}
    
    /// <summary>Starts a new instance of the object.</summary>
    /// <param name="resolution">Screen resolution.</param>
    /// <param name="frequency">Screen frequency.</param>
    public Resolution(in Vector2 resolution, in int frequency) : this(resolution.x, resolution.y, frequency) {}

    /// <summary>Starts a new instance of the object.</summary>
    /// <param name="width">The width of the screen.</param>
    /// <param name="height">The height of the screen.</param>
    /// <param name="frequency">Screen frequency.</param>
    public Resolution(in int width, in int height, in int frequency) {
        this.width = width;
        this.height = height;
        this.frequency = frequency;
    }

    /// <summary>Starts a new instance of the object.</summary>
    /// <param name="width">The width of the screen.</param>
    /// <param name="height">The height of the screen.</param>
    /// <param name="frequency">Screen frequency.</param>
    public Resolution(in float width, in float height, in int frequency) {
        this.width = width;
        this.height = height;
        this.frequency = frequency;
    }

    /// <inheritdoc/>
    public bool Equals(int other) => other == this.frequency;
    /// <inheritdoc/>
    public bool Equals(Vector2D other) => other == (Vector2D)this;
    /// <inheritdoc/>
    public bool Equals(Vector2 other) => other == (Vector2)this;
    /// <inheritdoc/>
    public bool Equals(Vector2DInt other) => other == (Vector2DInt)this;
    /// <inheritdoc/>
    public bool Equals(Resolution other) => other.Equals((Vector2D)this) && other.Equals(this.frequency);
    /// <inheritdoc/>
    public override bool Equals(object obj)
        => (obj is Resolution rlt && Equals(rlt)) ||
        (obj is int fre && Equals(fre)) ||
        (obj is Vector2D vec2d && Equals(vec2d)) ||
        (obj is Vector2 vec2 && Equals(vec2)) ||
        (obj is Vector2DInt vec2di && Equals(vec2di));
    /// <inheritdoc/>
    public override int GetHashCode() => this.frequency >> 2 ^ width.GetHashCode() << 2 ^ height.GetHashCode();
    /// <inheritdoc/>
    public override string ToString() => $"(W:{width:N2} H:{height:N2} @{frequency})";

    /// <summary>Indicates whether this instance is equal to another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator ==(Resolution A, Resolution B) => A.Equals(B);
    /// <summary>Indicates whether this instance is different from another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator !=(Resolution A, Resolution B) => !A.Equals(B);

    /// <summary>Explicit conversion operator.(<seealso cref="Resolution"/> to <seealso cref="Int32"/>)</summary>
    /// <param name="R">Object to be converted.</param>
    public static explicit operator int(Resolution R) => R.frequency;
    /// <summary>Explicit conversion operator.(<seealso cref="Resolution"/> to <seealso cref="Vector2DInt"/>)</summary>
    /// <param name="R">Object to be converted.</param>
    public static explicit operator Vector2DInt(Resolution R) => new((int)R.width, (int)R.height);
    /// <summary>Explicit conversion operator.(<seealso cref="Resolution"/> to <seealso cref="Vector2D"/>)</summary>
    /// <param name="R">Object to be converted.</param>
    public static explicit operator Vector2D(Resolution R) => new(R.width, R.height);
    /// <summary>Explicit conversion operator.(<seealso cref="Resolution"/> to <seealso cref="Vector2"/>)</summary>
    /// <param name="R">Object to be converted.</param>
    public static explicit operator Vector2(Resolution R) => new(R.width, R.height);
}
