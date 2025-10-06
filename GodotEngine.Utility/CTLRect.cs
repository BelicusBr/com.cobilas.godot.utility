using Godot;
using System;
using Cobilas.GodotEngine.Utility.Numerics;
using System.Globalization;

namespace Cobilas.GodotEngine.Utility;

[Serializable]
public struct CTLRect(float x,
                      float y,
                      float width,
                      float height,
                      float minWidth,
                      float minHeight,
                      float rotation,
                      float scaleX,
                      float scaleY,
                      float pivotX,
                      float pivotY) : IEquatable<CTLRect>, IFormattable
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

    private static readonly CTLRect _empty = new(0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);

    public readonly Vector2D Position => new(x, y);
    public readonly Vector2D Size => new(width, height);
    public readonly Vector2D MinSize => new(minWidth, minHeight);
    public readonly float Rotation => rotation;
    public readonly Vector2D Scale => new(scaleX, scaleY);
    public readonly Vector2D Pivot => new(pivotX, pivotY);

    public static CTLRect Empty => _empty;

    public CTLRect(
        Vector2D position, Vector2D size,
        Vector2D minSize, float rotation,
        Vector2D scale, Vector2D pivot
        ) : this(
            position.x, position.y,
            size.x, size.y,
            minSize.x, minSize.y, rotation,
            scale.x, scale.y, pivot.x, pivot.y)
    { }

    public CTLRect(
        Rect2 rect, Vector2D minSize, float rotation,
        Vector2D scale, Vector2D pivot
        ) : this(
            rect.Position.x, rect.Position.y,
            rect.Size.x, rect.Size.y,
            minSize.x, minSize.y, rotation,
            scale.x, scale.y, pivot.x, pivot.y)
    { }

    public CTLRect(CTLRect rect) : this(
        rect.Position, rect.Size,
        rect.MinSize, rect.Rotation,
        rect.Scale, rect.Pivot)
    { }

    public CTLRect SetPosition(float x, float y)
    {
        this.x = x;
        this.y = y;
        return this;
    }

    public CTLRect SetPosition(Vector2D position) => SetPosition(position.x, position.y);

    public CTLRect SetPosition(Vector2DInt position) => SetPosition(position.x, position.y);

    public CTLRect SetSize(float width, float height)
    {
        this.width = width;
        this.height = height;
        return this;
    }

    public CTLRect SetSize(Vector2D size) => SetSize(size.x, size.y);

    public CTLRect SetSize(Vector2DInt size) => SetSize(size.x, size.y);

    public CTLRect SetMinSize(float minWidth, float minHeight)
    {
        Quaternion quaternion = Quaternion.Identity;
        this.minWidth = minWidth;
        this.minHeight = minHeight;
        return this;
    }

    public CTLRect SetMinSize(Vector2D minSize) => SetMinSize(minSize.x, minSize.y);

    public CTLRect SetMinSize(Vector2DInt minSize) => SetMinSize(minSize.x, minSize.y);

    public CTLRect SetRotation(float rotation)
    {
        this.rotation = rotation;
        return this;
    }

    public CTLRect SetScale(float scaleX, float scaleY)
    {
        this.scaleX = scaleX;
        this.scaleY = scaleY;
        return this;
    }

    public CTLRect SetScale(Vector2D scale) => SetScale(scale.x, scale.y);

    public CTLRect SetScale(Vector2DInt scale) => SetScale(scale.x, scale.y);

    public CTLRect SetPivot(float pivotX, float pivotY)
    {
        this.pivotX = pivotX;
        this.pivotY = pivotY;
        return this;
    }

    public CTLRect SetPivot(Vector2D pivot) => SetPivot(pivot.x, pivot.y);

    public CTLRect SetPivot(Vector2DInt pivot) => SetPivot(pivot.x, pivot.y);

    public bool Equals(CTLRect other)
        => other.GetHashCode() == GetHashCode();

    public override bool Equals(object obj)
        => obj is CTLRect rect && Equals(rect);

    public override int GetHashCode()
        => x.GetHashCode() ^ y.GetHashCode() >>
            width.GetHashCode() ^ height.GetHashCode() <<
            minWidth.GetHashCode() ^ minHeight.GetHashCode() >>
            rotation.GetHashCode() ^ pivotX.GetHashCode() <<
            pivotY.GetHashCode() ^ scaleX.GetHashCode() >>
            scaleY.GetHashCode();

    public string ToString(string format, IFormatProvider formatProvider)
        => string.Format(formatProvider, format, x, y, width, height,
            minWidth, minHeight, rotation, scaleX, scaleY, pivotX, pivotY);

    public string ToString(string format)
        => ToString(format, CultureInfo.CurrentCulture);

    public override string ToString()
        => ToString("p(x:{0:N3}, y:{1:N3}) s(x:{2:N3}, y:{3:N3}) ms(x:{4:N3}, y:{5:N3}) r({6:N3}) sc(x:{7:N3}, y:{8:N3}) pv(x:{9:N3}, y:{10:N3})");

    public static bool operator ==(CTLRect A, CTLRect B) => A.Equals(B);
    public static bool operator !=(CTLRect A, CTLRect B) => !A.Equals(B);

    public static explicit operator Rect2(CTLRect R) => new(R.Position, R.Size);
    public static explicit operator float(CTLRect R) => R.rotation;
}
