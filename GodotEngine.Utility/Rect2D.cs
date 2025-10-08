using Godot;
using System;
using Cobilas.GodotEngine.Utility.Numerics;
using System.Globalization;

namespace Cobilas.GodotEngine.Utility;

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
    [Flags]
    public enum Point : byte {
        None = 0,
        Right = 1,
        Left = 2,
        Top = 4,
        Bottom = 8
    }

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

    public readonly Vector2D Position => new(x, y);
    public readonly Vector2D Size => new(width, height);
    public readonly Vector2D MinSize => new(minWidth, minHeight);
    public readonly float Rotation => rotation;
    public readonly Vector2D Scale => new(scaleX, scaleY);
    public readonly Vector2D Pivot => new(pivotX, pivotY);
    public readonly float Right => x;
    public readonly float Left => x + width;
    public readonly float Top => y;
    public readonly float Bottom => y + height;

    public static Rect2D Empty => _empty;

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

    public Rect2D(
        Rect2 rect, Vector2D minSize, float rotation,
        Vector2D scale, Vector2D pivot
        ) : this(
            rect.Position.x, rect.Position.y,
            rect.Size.x, rect.Size.y,
            minSize.x, minSize.y, rotation,
            scale.x, scale.y, pivot.x, pivot.y)
    { }

    public Rect2D(Rect2D rect) : this(
        rect.Position, rect.Size,
        rect.MinSize, rect.Rotation,
        rect.Scale, rect.Pivot)
    { }

    public Rect2D SetPosition(float x, float y)
    {
        this.x = x;
        this.y = y;
        return this;
    }

    public Rect2D SetPosition(Vector2D position) => SetPosition(position.x, position.y);

    public Rect2D SetPosition(Vector2DInt position) => SetPosition(position.x, position.y);

    public Rect2D SetSize(float width, float height)
    {
        this.width = width;
        this.height = height;
        return this;
    }

    public Rect2D SetSize(Vector2D size) => SetSize(size.x, size.y);

    public Rect2D SetSize(Vector2DInt size) => SetSize(size.x, size.y);

    public Rect2D SetMinSize(float minWidth, float minHeight) {
        this.minWidth = minWidth;
        this.minHeight = minHeight;
        return this;
    }

    public Rect2D SetMinSize(Vector2D minSize) => SetMinSize(minSize.x, minSize.y);

    public Rect2D SetMinSize(Vector2DInt minSize) => SetMinSize(minSize.x, minSize.y);

    public Rect2D SetRotation(float rotation) {
        this.rotation = rotation;
        return this;
    }

    public Rect2D SetScale(float scaleX, float scaleY) {
        this.scaleX = scaleX;
        this.scaleY = scaleY;
        return this;
    }

    public Rect2D SetScale(Vector2D scale) => SetScale(scale.x, scale.y);

    public Rect2D SetScale(Vector2DInt scale) => SetScale(scale.x, scale.y);

    public Rect2D SetPivot(float pivotX, float pivotY) {
        this.pivotX = pivotX;
        this.pivotY = pivotY;
        return this;
    }

    public Rect2D SetPivot(Vector2D pivot) => SetPivot(pivot.x, pivot.y);

    public Rect2D SetPivot(Vector2DInt pivot) => SetPivot(pivot.x, pivot.y);

    public bool Equals(Rect2D other)
        => other.GetHashCode() == GetHashCode();

    public override bool Equals(object obj)
        => obj is Rect2D rect && Equals(rect);

    public override int GetHashCode()
        => x.GetHashCode() ^ y.GetHashCode() >>
            width.GetHashCode() ^ height.GetHashCode() <<
            minWidth.GetHashCode() ^ minHeight.GetHashCode() >>
            rotation.GetHashCode() ^ pivotX.GetHashCode() <<
            pivotY.GetHashCode() ^ scaleX.GetHashCode() >>
            scaleY.GetHashCode();

    public Vector2D GetPoint(Point point)
    {
        return Vector2D.Zero;
    }

    public bool Contains(Vector2D point) {
        Rect2 rect = (Rect2)this;
        Quaternion quaternion = Quaternion.ToQuaternion(Vector3D.Forward * rotation);
        Vector2D diry = quaternion.GenerateDirectionUp();
        Vector2D dirx = quaternion.GenerateDirectionRight();
        Vector2D px = rect.Position + dirx * rect.Width();
        Vector2D py = rect.Position + diry * rect.Height();
        Vector2D pxy = px * Vector2.Right + py * Vector2.Down;

        return Area(rect.Position, px, py, point) || Area(pxy, px, py, point);
    }

    private static bool Area(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p) {
        float d1, d2, d3;
        bool has_neg, has_pos;

        d1 = Sign(p, p1, p2);
        d2 = Sign(p, p2, p3);
        d3 = Sign(p, p3, p1);

        has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
        has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

        return !(has_neg && has_pos);
    }

    private static float Sign(Vector2 p1, Vector2 p2, Vector2 p3)
        => (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);

    public string ToString(string format, IFormatProvider formatProvider)
        => string.Format(formatProvider, format, x, y, width, height,
            minWidth, minHeight, rotation, scaleX, scaleY, pivotX, pivotY);

    public string ToString(string format)
        => ToString(format, CultureInfo.CurrentCulture);

    public override string ToString()
        => ToString("p(x:{0:N3}, y:{1:N3}) s(x:{2:N3}, y:{3:N3}) ms(x:{4:N3}, y:{5:N3}) r({6:N3}) sc(x:{7:N3}, y:{8:N3}) pv(x:{9:N3}, y:{10:N3})");

    public static bool operator ==(Rect2D A, Rect2D B) => A.Equals(B);
    public static bool operator !=(Rect2D A, Rect2D B) => !A.Equals(B);

    public static explicit operator Rect2(Rect2D R) => new(R.Position, R.Size);
    public static explicit operator float(Rect2D R) => R.rotation;
}
