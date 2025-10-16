using Godot;
using System;
using System.Globalization;
using Cobilas.GodotEngine.Utility.Numerics;

namespace Cobilas.GodotEngine.Utility;

/// <summary>Represents a 2D rectangle with advanced properties for geometric transformations.</summary>
/// <remarks>
/// This structure extends the functionality of a traditional rectangle, including
/// rotation, scale, pivot, and minimum size, making it useful for advanced UI and sprite operations in the Godot Engine.
/// </remarks>
/// <param name="x">X coordinate of the rectangle's position.</param>
/// <param name="y">Y coordinate of the rectangle's position.</param>
/// <param name="width">Width of the rectangle.</param>
/// <param name="height">Height of the rectangle.</param>
/// <param name="minWidth">Minimum width of the rectangle.</param>
/// <param name="minHeight">Minimum height of the rectangle.</param>
/// <param name="rotation">Rotation of the rectangle in degrees.</param>
/// <param name="scaleX">Scale of the rectangle on the X axis.</param>
/// <param name="scaleY">Scale of the rectangle on the Y axis.</param>
/// <param name="pivotX">X coordinate of the pivot point of the rectangle.</param>
/// <param name="pivotY">Y coordinate of the rectangle's pivot point.</param>
[Serializable]
public struct Rect2D(float x,
                      float y,
                      float width,
                      float height,
                      float minWidth,
                      float minHeight,
                      float rotation,
                      float scaleX,
                      float scaleY,
                      float pivotX,
                      float pivotY) : IEquatable<Rect2D>, IFormattable
{
    private float x = x,
                  y = y,
                  width = width,
                  height = height,
                  minWidth = minWidth,
                  minHeight = minHeight,
                  rotation = rotation,
                  scaleX = scaleX,
                  scaleY = scaleY,
                  pivotX = pivotX,
                  pivotY = pivotY;

    private static readonly Rect2D _empty = new(0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
    /// <summary>Gets the position of the top-left corner of the rectangle.</summary>
    /// <returns>A <seealso cref="Vector2D"/> representing the position of the rectangle.</returns>
    public readonly Vector2D Position => InitVector2D(x, y);
    /// <summary>Gets the size of the rectangle.</summary>
    /// <returns>A <seealso cref="Vector2D"/> representing the size of the rectangle.</returns>
    public readonly Vector2D Size => InitVector2D(width, height);
    /// <summary>Gets the minimum size of the rectangle.</summary>
    /// <returns>A <seealso cref="Vector2D"/> representing the minimum size of the rectangle.</returns>
    public readonly Vector2D MinSize => InitVector2D(minWidth, minHeight);
    /// <summary>Gets the rotation of the rectangle in degrees.</summary>
    /// <returns>Rotation angle in degrees.</returns>
    public readonly float Rotation => rotation;
    /// <summary>Gets the rotation of the rectangle in radians.</summary>
    /// <returns>Angle of rotation in radians.</returns>
    public readonly float RadianRotation => (float)Quaternion.Deg2Rad * rotation;
    /// <summary>Gets the scale of the rectangle.</summary>
    /// <returns>A <seealso cref="Vector2D"/> representing the scale of the rectangle.</returns>
    public readonly Vector2D Scale => InitVector2D(scaleX, scaleY);
    /// <summary>Gets the pivot point of the rectangle.</summary>
    /// <returns>A <seealso cref="Vector2D"/> representing the pivot point of the rectangle.</returns>
    public readonly Vector2D Pivot => InitVector2D(pivotX, pivotY);
    /// <summary>Gets the size of the rectangle applied to the scale.</summary>
    /// <returns>A <seealso cref="Vector2D"/> representing the scaled size of the rectangle.</returns>
    public readonly Vector2D SizeScale => Size * Scale;
    /// <summary>Gets the pivot of the rectangle applied to the scale.</summary>
    /// <returns>A <seealso cref="Vector2D"/> representing the scaled pivot of the rectangle.</returns>
    public readonly Vector2D PivotScale => Pivot * Scale;
    /// <summary>Gets the minimum size of the rectangle applied to the scale.</summary>
    /// <returns>A <seealso cref="Vector2D"/> representing the minimum scaled size of the rectangle.</returns>
    public readonly Vector2D MinSizeScale => MinSize * Scale;
    /// <summary>Gets the X coordinate of the right side of the rectangle.</summary>
    /// <returns>X coordinate on the right side.</returns>
    public readonly float Right => x;
    /// <summary>Gets the X coordinate of the left side of the rectangle.</summary>
    /// <returns>X coordinate on the left side.</returns>
    public readonly float Left => x + width;
    /// <summary>Gets the Y coordinate of the top of the rectangle.</summary>
    /// <returns>Y coordinate of the top.</returns>
    public readonly float Top => y;
    /// <summary>Gets the Y coordinate of the rectangle's base.</summary>
    /// <returns>Y coordinate of the base.</returns>
    public readonly float Bottom => y + height;
    /// <summary>Gets an empty Rect2D instance.</summary>
    /// <returns>An empty instance of Rect2D.</returns>
    public static Rect2D Empty => _empty;
    /// <summary>Initializes a new instance of the <seealso cref="Rect2D"/> structure using vectors.</summary>
    /// <param name="position">Position of the rectangle.</param>
    /// <param name="size">Size of the rectangle.</param>
    /// <param name="minSize">Minimum size of the rectangle.</param>
    /// <param name="rotation">Rotation in degrees.</param>
    /// <param name="scale">Scale of the rectangle.</param>
    /// <param name="pivot">Pivot point of the rectangle.</param>
    public Rect2D(
        Vector2D position, Vector2D size,
        Vector2D minSize, float rotation,
        Vector2D scale, Vector2D pivot
        ) : this(
            position.x, position.y,
            size.x, size.y,
            minSize.x, minSize.y, rotation,
            scale.x, scale.y, pivot.x, pivot.y)
    { }
    /// <summary>Initializes a new instance of the <seealso cref="Rect2D"/> structure from a Godot <seealso cref="Rect2"/>.</summary>
    /// <param name="rect">Godot's base rectangle.</param>
    /// <param name="minSize">Minimum size of the rectangle.</param>
    /// <param name="rotation">Rotation in degrees.</param>
    /// <param name="scale">Rectangle scale.</param>
    /// <param name="pivot">Rectangle pivot point.</param>
    public Rect2D(
        Rect2 rect, Vector2D minSize, float rotation,
        Vector2D scale, Vector2D pivot
        ) : this(
            rect.Position.x, rect.Position.y,
            rect.Size.x, rect.Size.y,
            minSize.x, minSize.y, rotation,
            scale.x, scale.y, pivot.x, pivot.y)
    { }
    /// <summary>Initializes a new instance of the <seealso cref="Rect2D"/> structure by copying another instance.</summary>
    /// <param name="rect"><seealso cref="Rect2D"/> to be copied.</param>
    public Rect2D(Rect2D rect) : this(
        rect.Position, rect.Size,
        rect.MinSize, rect.Rotation,
        rect.Scale, rect.Pivot)
    { }
    /// <summary>Defines the rectangle's position using individual coordinates.</summary>
    /// <param name="x">X coordinate of the position.</param>
    /// <param name="y">Y coordinate of the position.</param>
    /// <returns>The <seealso cref="Rect2D"/> itself to allow chained calls.</returns>
    public Rect2D SetPosition(float x, float y) {
        this.x = x;
        this.y = y;
        return this;
    }
    /// <summary>Sets the rectangle's position using a <seealso cref="Vector2D"/>.</summary>
    /// <param name="position">Vector containing the position.</param>
    /// <returns>The <seealso cref="Rect2D"/> itself to allow chained calls.</returns>
    public Rect2D SetPosition(Vector2D position) => SetPosition(position.x, position.y);
    /// <summary>Sets the rectangle's position using a <seealso cref="Vector2DInt"/>.</summary>
    /// <param name="position">Integer vector containing the position.</param>
    /// <returns>The <seealso cref="Rect2D"/> itself to allow chained calls.</returns>
    public Rect2D SetPosition(Vector2DInt position) => SetPosition(position.x, position.y);
    /// <summary>Sets the size of the rectangle using individual values.</summary>
    /// <param name="width">Width of the rectangle.</param>
    /// <param name="height">Height of the rectangle.</param>
    /// <returns>The <seealso cref="Rect2D"/> itself to allow chained calls.</returns>
    public Rect2D SetSize(float width, float height) {
        this.width = width;
        this.height = height;
        return this;
    }
    /// <summary>Sets the size of the rectangle using a <seealso cref="Vector2D"/>.</summary>
    /// <param name="size">Vector containing the size.</param>
    /// <returns>The <seealso cref="Rect2D"/> itself to allow chained calls.</returns>
    public Rect2D SetSize(Vector2D size) => SetSize(size.x, size.y);
    /// <summary>Sets the size of the rectangle using a <seealso cref="Vector2DInt"/>.</summary>
    /// <param name="size">Integer vector containing the size.</param>
    /// <returns>The <seealso cref="Rect2D"/> itself to allow chained calls.</returns>
    public Rect2D SetSize(Vector2DInt size) => SetSize(size.x, size.y);
    /// <summary>Sets the minimum size of the rectangle using individual values.</summary>
    /// <param name="minWidth">Minimum width of the rectangle.</param>
    /// <param name="minHeight">Minimum height of the rectangle.</param>
    /// <returns>The <seealso cref="Rect2D"/> itself to allow chained calls.</returns>
    public Rect2D SetMinSize(float minWidth, float minHeight) {
        this.minWidth = minWidth;
        this.minHeight = minHeight;
        return this;
    }
    /// <summary>Sets the minimum size of the rectangle using a <seealso cref="Vector2D"/>.</summary>
    /// <param name="minSize">Vector containing the minimum size.</param>
    /// <returns>The <seealso cref="Rect2D"/> itself to allow chained calls.</returns>
    public Rect2D SetMinSize(Vector2D minSize) => SetMinSize(minSize.x, minSize.y);
    /// <summary>Sets the minimum size of the rectangle using a <seealso cref="Vector2DInt"/>.</summary>
    /// <param name="minSize">Integer vector containing the minimum size.</param>
    /// <returns>The <seealso cref="Rect2D"/> itself to allow chained calls.</returns>
    public Rect2D SetMinSize(Vector2DInt minSize) => SetMinSize(minSize.x, minSize.y);
    /// <summary>Defines the rotation of the rectangle in degrees.</summary>
    /// <param name="rotation">Rotation angle in degrees.</param>
    /// <returns>The <seealso cref="Rect2D"/> itself to allow chained calls.</returns>
    public Rect2D SetRotation(float rotation) {
        this.rotation = rotation;
        return this;
    }
    /// <summary>Sets the rectangle's scale using individual values.</summary>
    /// <param name="scaleX">Scale on the X axis.</param>
    /// <param name="scaleY">Scale on the Y axis.</param>
    /// <returns>The <seealso cref="Rect2D"/> itself to allow chained calls.</returns>
    public Rect2D SetScale(float scaleX, float scaleY) {
        this.scaleX = scaleX;
        this.scaleY = scaleY;
        return this;
    }
    /// <summary>
    /// Define a escala do retângulo usando <seealso cref="Vector2D"/>.
    /// </summary>
    /// <param name="scale">Vetor contendo a escala.</param>
    /// <returns>The <seealso cref="Rect2D"/> itself to allow chained calls.</returns>
    public Rect2D SetScale(Vector2D scale) => SetScale(scale.x, scale.y);
    /// <summary>Sets the rectangle's scale using <seealso cref="Vector2DInt"/>.</summary>
    /// <param name="scale">Integer vector containing the scale.</param>
    /// <returns>The <seealso cref="Rect2D"/> itself to allow chained calls.</returns>
    public Rect2D SetScale(Vector2DInt scale) => SetScale(scale.x, scale.y);
    /// <summary>Sets the rectangle's pivot point using individual values.</summary>
    /// <param name="pivotX">X coordinate of the pivot.</param>
    /// <param name="pivotY">Y coordinate of the pivot.</param>
    /// <returns>The <seealso cref="Rect2D"/> itself to allow chained calls.</returns>
    public Rect2D SetPivot(float pivotX, float pivotY) {
        this.pivotX = pivotX;
        this.pivotY = pivotY;
        return this;
    }
    /// <summary>Sets the rectangle's pivot point using <seealso cref="Vector2D"/>.</summary>
    /// <param name="pivot">Vector containing the pivot.</param>
    /// <returns>The <seealso cref="Rect2D"/> itself to allow chained calls.</returns>
    public Rect2D SetPivot(Vector2D pivot) => SetPivot(pivot.x, pivot.y);
    /// <summary>Sets the rectangle's pivot point using <seealso cref="Vector2DInt"/>.</summary>
    /// <param name="pivot">Integer vector containing the pivot.</param>
    /// <returns>The <seealso cref="Rect2D"/> itself to allow chained calls.</returns>
    public Rect2D SetPivot(Vector2DInt pivot) => SetPivot(pivot.x, pivot.y);
    /// <inheritdoc/>
    public readonly bool Equals(Rect2D other)
        => other.GetHashCode() == GetHashCode();
    /// <inheritdoc/>
    public override readonly bool Equals(object obj)
        => obj is Rect2D rect && Equals(rect);
    /// <inheritdoc/>
    public override readonly int GetHashCode()
        => x.GetHashCode() ^ y.GetHashCode() >>
            width.GetHashCode() ^ height.GetHashCode() <<
            minWidth.GetHashCode() ^ minHeight.GetHashCode() >>
            rotation.GetHashCode() ^ pivotX.GetHashCode() <<
            pivotY.GetHashCode() ^ scaleX.GetHashCode() >>
            scaleY.GetHashCode();
    /// <summary>Checks if a point is inside the rectangle, considering rotation and transformations.</summary>
    /// <param name="point">The point to be checked.</param>
    /// <returns>true if the point is inside the rectangle; otherwise, false.</returns>
    public readonly bool HasPoint(Vector2D point) {
        Vector2D position = Position;
        Quaternion quaternion = Quaternion.ToQuaternion(Vector3D.Forward * RadianRotation);
        Vector2D dirx = quaternion.GenerateDirectionRight() * SizeScale.x;
        Vector2D diry = quaternion.GenerateDirectionDown() * SizeScale.y;
        Vector2D pivx = quaternion.GenerateDirectionLeft() * PivotScale.x;
        Vector2D pivy = quaternion.GenerateDirectionUp() * PivotScale.y;
        position += Pivot + pivx + pivy;
        Vector2D px = position + dirx;
        Vector2D py = position + diry;
        Vector2D pxy = position + dirx + diry;

        return IsPointInsideSquare(position, px, pxy, py, point);
    }
    /// <summary>Returns a <seealso cref="string"/> that represents the current <seealso cref="object"/> using the specified format and format provider.</summary>
    /// <param name="format">The format to use.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>A <seealso cref="string"/> that represents the current <seealso cref="object"/>.</returns>
    public readonly string ToString(string format, IFormatProvider formatProvider)
        => string.Format(formatProvider, format, x, y, width, height,
            minWidth, minHeight, rotation, scaleX, scaleY, pivotX, pivotY);
    /// <summary>Returns a <seealso cref="string"/> representing the current <seealso cref="object"/> using the specified format.</summary>
    /// <param name="format">The format to use.</param>
    /// <returns>A <seealso cref="string"/> representing the current <seealso cref="object"/>.</returns>
    public readonly string ToString(string format)
        => ToString(format, CultureInfo.CurrentCulture);
    /// <summary>Returns a <seealso cref="string"/> representing the current object.</summary>
    /// <returns>A <seealso cref="string"/> representing the current object.</returns>
    public override readonly string ToString()
        => ToString("p(x:{0:N3}, y:{1:N3}) s(x:{2:N3}, y:{3:N3}) ms(x:{4:N3}, y:{5:N3}) r({6:N3}) sc(x:{7:N3}, y:{8:N3}) pv(x:{9:N3}, y:{10:N3})");

    private static bool IsPointInsideSquare(Vector2 A, Vector2 B, Vector2 C, Vector2 D, Vector2 P)
        => IsPointInsideTriangle(A, B, C, P) || IsPointInsideTriangle(A, C, D, P);

    private static bool IsPointInsideTriangle(Vector2D A, Vector2D B, Vector2D C, Vector2D P) {
        Vector2D v0 = C - A;
        Vector2D v1 = B - A;
        Vector2D v2 = P - A;

        float dot00 = Vector2D.Dot(v0, v0);
        float dot01 = Vector2D.Dot(v0, v1);
        float dot02 = Vector2D.Dot(v0, v2);
        float dot11 = Vector2D.Dot(v1, v1);
        float dot12 = Vector2D.Dot(v1, v2);

        float denom = dot00 * dot11 - dot01 * dot01;

        if (Math.Abs(denom) < 1e-10f)
            return false;

        float u = (dot11 * dot02 - dot01 * dot12) / denom;
        float v = (dot00 * dot12 - dot01 * dot02) / denom;

        return (u >= 0) && (v >= 0) && (u + v <= 1);
    }
    
    private static Vector2D InitVector2D(float x, float y) {
        Vector2D result = Vector2D.Zero;
        result[0] = x;
        result[1] = y;
        return result;
    }
    /// <summary>Determines whether two structures <seealso cref="Rect2D"/> are equal.</summary>
    /// <param name="A">The first structure to compare.</param>
    /// <param name="B">The second structure to compare.</param>
    /// <returns>true if A and B are equal; otherwise, false.</returns>
    public static bool operator ==(Rect2D A, Rect2D B) => A.Equals(B);
    /// <summary>Determines whether two structures <seealso cref="Rect2D"/> are different.</summary>
    /// <param name="A">The first structure to compare.</param>
    /// <param name="B">The second structure to compare.</param>
    /// <returns>true if A and B are different; otherwise, false.</returns>
    public static bool operator !=(Rect2D A, Rect2D B) => !A.Equals(B);
    /// <summary>Explicitly converts a <seealso cref="Rect2D"/> to a Godot <seealso cref="Rect2"/>.</summary>
    /// <param name="R">The <seealso cref="Rect2D"/> to convert.</param>
    /// <returns>A Rect2 containing only position and size.</returns>
    public static explicit operator Rect2(Rect2D R) => new(R.Position, R.Size);
    /// <summary>Explicitly converts a <seealso cref="Rect2D"/> to a <seealso cref="float"/> value representing its rotation.</summary>
    /// <param name="R">The <seealso cref="Rect2D"/> to convert.</param>
    /// <returns>The rotation of the <seealso cref="Rect2D"/> as a <seealso cref="float"/>.</returns>
    public static explicit operator float(Rect2D R) => R.rotation;
}