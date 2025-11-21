using System;
using System.Text;

namespace Godot;
/// <summary>
/// Provides extension methods for Godot's <seealso cref="Label"/> class, allowing string operations to be performed fluently and efficiently using <seealso cref="StringBuilder"/> internally.
/// </summary>
/// <remarks>
/// This class uses a shared static <seealso cref="StringBuilder"/> for better performance
/// in frequent text manipulation operations in TextEdits.
/// </remarks>
public static class Label_CB_GD_Extension {
    private static readonly StringBuilder builder = new();
    /// <summary>
    /// Adds a character repeated a specified number of times to the text of <seealso cref="Label"/>.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="value">Character to be added</param>
    /// <param name="repeatCount">Number of repetitions of the character</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Append(this Label? L, char value, int repeatCount) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Append(value, repeatCount).ToString();
        return L;
    }

    /// <summary>
    /// Adds the <seealso cref="string"/> representation of a <seealso cref="bool"/> value to the <seealso cref="Label"/> text.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="value"><seealso cref="bool"/> value to be added</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Append(this Label? L, bool value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Append(value).ToString();
        return L;
    }

    /// <summary>
    /// Adds a character to the <seealso cref="Label"/> text.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="value">Character to be added</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Append(this Label? L, char value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Append(value).ToString();
        return L;
    }

    /// <summary>
    /// Adds a <seealso cref="ulong"/> value to the <seealso cref="Label"/> text.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="value"><seealso cref="ulong"/> value to be added</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Append(this Label? L, ulong value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Append(value).ToString();
        return L;
    }

    /// <summary>
    /// Adds a <seealso cref="uint"/> value to the <seealso cref="Label"/> text.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="value"><seealso cref="uint"/> value to add</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Append(this Label? L, uint value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Append(value).ToString();
        return L;
    }

    /// <summary>
    /// Adds a <seealso cref="byte"/> value to the <seealso cref="Label"/> text.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="value"><seealso cref="byte"/> value to be added</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Append(this Label? L, byte value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Append(value).ToString();
        return L;
    }

    /// <summary>
    /// Adds a substring to the <seealso cref="Label"/> text.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="value"><seealso cref="string"/> source</param>
    /// <param name="startIndex">Starting index in source <seealso cref="string"/></param>
    /// <param name="count">Number of characters to add</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Append(this Label? L, string value, int startIndex, int count) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Append(value, startIndex, count).ToString();
        return L;
    }

    /// <summary>
    /// Adds a <seealso cref="string"/> to the text of the <seealso cref="Label"/>.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="value"><seealso cref="string"/> to be added</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Append(this Label? L, string value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Append(value).ToString();
        return L;
    }

    /// <summary>
    /// Adds a <seealso cref="float"/> value to the text of the <seealso cref="Label"/>.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="value"><seealso cref="float"/> value to be added</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Append(this Label? L, float value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Append(value).ToString();
        return L;
    }

    /// <summary>
    /// Adds a <seealso cref="ushort"/> value to the text of the <seealso cref="Label"/>.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="value"><seealso cref="ushort"/> value to be added</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Append(this Label? L, ushort value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Append(value).ToString();
        return L;
    }

    /// <summary>
    /// Adds the <seealso cref="string"/> representation of a <seealso cref="object"/> to the text of the <seealso cref="Label"/>.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="value"><seealso cref="object"/> to be added</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Append(this Label? L, object value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Append(value).ToString();
        return L;
    }

    /// <summary>
    /// Adds a <seealso cref="T:char[]"/> to the text of the <seealso cref="Label"/>.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="value"><seealso cref="T:char[]"/> to be added</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Append(this Label? L, char[] value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Append(value).ToString();
        return L;
    }

    /// <summary>
    /// Adds a part of a <seealso cref="T:char[]"/> to the text of the <seealso cref="Label"/>.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="value"><seealso cref="T:char[]"/> of origin</param>
    /// <param name="startIndex">Starting index in <seealso cref="Array"/></param>
    /// <param name="charCount">Number of characters to add</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Append(this Label? L, char[] value, int startIndex, int charCount) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Append(value, startIndex, charCount).ToString();
        return L;
    }

    /// <summary>
    /// Adds a <seealso cref="sbyte"/> value to the text of <seealso cref="Label"/>.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="value"><seealso cref="sbyte"/> value to be added</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Append(this Label? L, sbyte value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Append(value).ToString();
        return L;
    }

    /// <summary>
    /// Adds a <seealso cref="decimal"/> value to the text of <seealso cref="Label"/>.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="value"><seealso cref="decimal"/> value to be added</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Append(this Label? L, decimal value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Append(value).ToString();
        return L;
    }

    /// <summary>
    /// Adds a <seealso cref="short"/> value to the text of <seealso cref="Label"/>.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="value"><seealso cref="short"/> value to be added</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Append(this Label? L, short value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Append(value).ToString();
        return L;
    }

    /// <summary>
    /// Adds a <seealso cref="int"/> value to the text of <seealso cref="Label"/>.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="value"><seealso cref="int"/> value to be added</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Append(this Label? L, int value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Append(value).ToString();
        return L;
    }

    /// <summary>
    /// Adds a <seealso cref="long"/> value to the text of <seealso cref="Label"/>.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="value"><seealso cref="long"/> value to be added</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Append(this Label? L, long value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Append(value).ToString();
        return L;
    }

    /// <summary>
    /// Adds a <seealso cref="double"/> value to the text of <seealso cref="Label"/>.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="value"><seealso cref="double"/> value to be added</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Append(this Label? L, double value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Append(value).ToString();
        return L;
    }

    /// <summary>
    /// Adds formatted text to Label using a specific format provider.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="provider">Culture-specific format provider</param>
    /// <param name="format"><seealso cref="string"/> format</param>
    /// <param name="arg0">First argument to be formatted</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label AppendFormat(this Label? L, IFormatProvider provider, string format, object arg0) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .AppendFormat(provider, format, arg0).ToString();
        return L;
    }

    /// <summary>
    /// Adds formatted text to Label using a specific format provider.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="provider">Culture-specific format provider</param>
    /// <param name="format"><seealso cref="string"/> format</param>
    /// <param name="arg0">First argument to be formatted</param>
    /// <param name="arg1">Second argument to be formatted</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label AppendFormat(this Label? L, IFormatProvider provider, string format, object arg0, object arg1) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .AppendFormat(provider, format, arg0, arg1).ToString();
        return L;
    }

    /// <summary>
    /// Adds formatted text to Label using a specific format provider.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="provider">Culture-specific format provider</param>
    /// <param name="format"><seealso cref="string"/> format</param>
    /// <param name="args"><seealso cref="Array"/> of arguments to be formatted</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label AppendFormat(this Label? L, IFormatProvider provider, string format, params object[] args) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .AppendFormat(provider, format, args).ToString();
        return L;
    }

    /// <summary>
    /// Adds formatted text to Label using a specific format provider.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="format"><seealso cref="string"/> format</param>
    /// <param name="arg0">First argument to be formatted</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label AppendFormat(this Label? L, string format, object arg0) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .AppendFormat(format, arg0).ToString();
        return L;
    }

    /// <summary>
    /// Adds formatted text to Label using a specific format provider.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="format"><seealso cref="string"/> format</param>
    /// <param name="arg0">First argument to be formatted</param>
    /// <param name="arg1">Second argument to be formatted</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label AppendFormat(this Label? L, string format, object arg0, object arg1) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .AppendFormat(format, arg0, arg1).ToString();
        return L;
    }

    /// <summary>
    /// Adds formatted text to Label using a specific format provider.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="format"><seealso cref="string"/> format</param>
    /// <param name="arg0">First argument to be formatted</param>
    /// <param name="arg1">Second argument to be formatted</param>
    /// <param name="arg2">Third argument to be formatted</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label AppendFormat(this Label? L, string format, object arg0, object arg1, object arg2) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .AppendFormat(format, arg0, arg1, arg2).ToString();
        return L;
    }

    /// <summary>
    /// Adds formatted text to Label using a specific format provider.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="format"><seealso cref="string"/> format</param>
    /// <param name="args"><seealso cref="Array"/> of arguments to be formatted</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label AppendFormat(this Label? L, string format, params object[] args) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .AppendFormat(format, args).ToString();
        return L;
    }

    /// <summary>
    /// Adds formatted text to Label using a specific format provider.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="provider">Culture-specific format provider</param>
    /// <param name="format"><seealso cref="string"/> format</param>
    /// <param name="arg0">First argument to be formatted</param>
    /// <param name="arg1">Second argument to be formatted</param>
    /// <param name="arg2">Third argument to be formatted</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label AppendFormat(this Label? L, IFormatProvider provider, string format, object arg0, object arg1, object arg2) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .AppendFormat(provider, format, arg0, arg1, arg2).ToString();
        return L;
    }

    /// <summary>
    /// Adds a <seealso cref="string"/> created by joining a <seealso cref="T:string[]"/> with a separator.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="separator">Separator between elements</param>
    /// <param name="values"><seealso cref="T:string[]"/> to be joined</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    public static Label AppendJoin(this Label? L, string separator, string[] values)
        => Append(L, string.Join(separator, values));

    /// <summary>
    /// Adds a <seealso cref="string"/> created by joining a <seealso cref="T:object[]"/> with a separator.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="separator">Separator between elements</param>
    /// <param name="values"><seealso cref="T:object[]"/> to be joined</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    public static Label AppendJoin(this Label? L, string separator, object[] values)
        => Append(L, string.Join(separator, values));

    /// <summary>
    /// Adds a <seealso cref="string"/> created by joining a <seealso cref="T:string[]"/> with a character separator.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="separator">Separator between elements</param>
    /// <param name="values"><seealso cref="T:string[]"/> to be joined</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    public static Label AppendJoin(this Label? L, char separator, string[] values)
        => AppendJoin(L, separator.ToString(), values);

    /// <summary>
    /// Adds a <seealso cref="string"/> created by joining a <seealso cref="T:object[]"/> with a character separator.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="separator">Separator between elements</param>
    /// <param name="values"><seealso cref="T:object[]"/> to be joined</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    public static Label AppendJoin(this Label? L, char separator, object[] values)
        => AppendJoin(L, separator.ToString(), values);

    /// <summary>
    /// Adds a <seealso cref="string"/> created by joining a collection of objects with a separator.
    /// </summary>
    /// <typeparam name="T"><seealso cref="Type"/> of elements in the collection</typeparam>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="separator">Separator between elements</param>
    /// <param name="values">Collection of objects to be joined</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    public static Label AppendJoin<T>(this Label? L, string separator, IEquatable<T> values)
        => Append(L, string.Join(separator, values));

    /// <summary>
    /// Adds a <seealso cref="string"/> created by joining a collection of objects with a character separator.
    /// </summary>
    /// <typeparam name="T"><seealso cref="Type"/> of elements in the collection</typeparam>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="separator">Separator between elements</param>
    /// <param name="values">Collection of objects to be joined</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    public static Label AppendJoin<T>(this Label? L, char separator, IEquatable<T> values)
        => AppendJoin<T>(L, separator, values);

    /// <summary>
    /// Adds a line break to the text of <seealso cref="Label"/>.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label AppendLine(this Label? L) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .AppendLine().ToString();
        return L;
    }

    /// <summary>
    /// Adds a <seealso cref="string"/> followed by a line break to the text of the <seealso cref="Label"/>.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="value"><seealso cref="string"/> to be added</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label AppendLine(this Label? L, string value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .AppendLine(value).ToString();
        return L;
    }

    /// <summary>
    /// Adds the <seealso cref="string"/> representation of a <seealso cref="object"/> followed by a line break to the text of the <seealso cref="Label"/>.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="value"><seealso cref="object"/> to be added</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    public static Label AppendLine(this Label? L, object value)
        => AppendLine(L, value.ToString());

    /// <summary>
    /// Inserts a portion of a <seealso cref="T:char[]"/> into the text of the <seealso cref="Label"/> at the specified position.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="index">Position where to insert</param>
    /// <param name="value"><seealso cref="T:char[]"/> of origin</param>
    /// <param name="startIndex">Starting index in <seealso cref="Array"/></param>
    /// <param name="charCount">Number of characters to enter</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Insert(this Label? L, int index, char[] value, int startIndex, int charCount) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Insert(index, value, startIndex, charCount).ToString();
        return L;
    }

    /// <summary>
    /// Inserts the <seealso cref="string"/> representation of a <seealso cref="bool"/> value into the text of the <seealso cref="Label"/> at the specified position.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="index">Position where to insert</param>
    /// <param name="value"><seealso cref="bool"/> value to be inserted</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Insert(this Label? L, int index, bool value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Insert(index, value).ToString();
        return L;
    }

    /// <summary>
    /// Inserts the <seealso cref="string"/> representation of a <seealso cref="byte"/> value into the text of the <seealso cref="Label"/> at the specified position.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="index">Position where to insert</param>
    /// <param name="value"><seealso cref="byte"/> value to be inserted</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Insert(this Label? L, int index, byte value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Insert(index, value).ToString();
        return L;
    }

    /// <summary>
    /// Inserts the <seealso cref="string"/> representation of a <seealso cref="ulong"/> value into the text of the <seealso cref="Label"/> at the specified position.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="index">Position where to insert</param>
    /// <param name="value"><seealso cref="ulong"/> value to be inserted</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Insert(this Label? L, int index, ulong value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Insert(index, value).ToString();
        return L;
    }

    /// <summary>
    /// Inserts a <seealso cref="T:char[]"/> into the text of the <seealso cref="Label"/> at the specified position.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="index">Position where to insert</param>
    /// <param name="value"><seealso cref="T:char[]"/> value to be inserted</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Insert(this Label? L, int index, char[] value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Insert(index, value).ToString();
        return L;
    }

    /// <summary>
    /// Inserts the <seealso cref="string"/> representation of a <seealso cref="ushort"/> value into the text of the <seealso cref="Label"/> at the specified position.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="index">Position where to insert</param>
    /// <param name="value"><seealso cref="ushort"/> value to be inserted</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Insert(this Label? L, int index, ushort value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Insert(index, value).ToString();
        return L;
    }

    /// <summary>
    /// Inserts a <seealso cref="string"/> repeated a specified number of times into the text of the <seealso cref="Label"/> at the specified position.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="index">Position where to insert</param>
    /// <param name="value"><seealso cref="string"/> value to be inserted</param>
    /// <param name="count">Number of repetitions</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Insert(this Label? L, int index, string value, int count) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Insert(index, value, count).ToString();
        return L;
    }

    /// <summary>
    /// Inserts a <seealso cref="char"/> into the text of the <seealso cref="Label"/> at the specified position.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="index">Position where to insert</param>
    /// <param name="value"><seealso cref="char"/> value to be inserted</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Insert(this Label? L, int index, char value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Insert(index, value).ToString();
        return L;
    }

    /// <summary>
    /// Inserts the <seealso cref="string"/> representation of a <seealso cref="uint"/> value into the text of the <seealso cref="Label"/> at the specified position.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="index">Position where to insert</param>
    /// <param name="value"><seealso cref="uint"/> value to be inserted</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Insert(this Label? L, int index, uint value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Insert(index, value).ToString();
        return L;
    }

    /// <summary>
    /// Inserts the <seealso cref="string"/> representation of a <seealso cref="sbyte"/> value into the text of the <seealso cref="Label"/> at the specified position.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="index">Position where to insert</param>
    /// <param name="value"><seealso cref="sbyte"/> value to be inserted</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Insert(this Label? L, int index, sbyte value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Insert(index, value).ToString();
        return L;
    }

    /// <summary>
    /// Insere a representação em <seealso cref="string"/> de um <seealso cref="object"/> no texto do <seealso cref="Label"/> na posição especificada.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="index">Position where to insert</param>
    /// <param name="value"><seealso cref="object"/> value to be inserted</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Insert(this Label? L, int index, object value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Insert(index, value).ToString();
        return L;
    }

    /// <summary>
    /// Inserts the <seealso cref="string"/> representation of a <seealso cref="long"/> value into the text of the <seealso cref="Label"/> at the specified position.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="index">Position where to insert</param>
    /// <param name="value"><seealso cref="long"/> value to be inserted</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Insert(this Label? L, int index, long value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Insert(index, value).ToString();
        return L;
    }

    /// <summary>
    /// Inserts the <seealso cref="string"/> representation of a <seealso cref="int"/> value into the text of the <seealso cref="Label"/> at the specified position.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="index">Position where to insert</param>
    /// <param name="value"><seealso cref="int"/> value to be inserted</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Insert(this Label? L, int index, int value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Insert(index, value).ToString();
        return L;
    }

    /// <summary>
    /// Inserts the <seealso cref="string"/> representation of a <seealso cref="short"/> value into the text of the <seealso cref="Label"/> at the specified position.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="index">Position where to insert</param>
    /// <param name="value"><seealso cref="short"/> value to be inserted</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Insert(this Label? L, int index, short value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Insert(index, value).ToString();
        return L;
    }

    /// <summary>
    /// Inserts the <seealso cref="string"/> representation of a <seealso cref="double"/> value into the text of the <seealso cref="Label"/> at the specified position.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="index">Position where to insert</param>
    /// <param name="value"><seealso cref="double"/> value to be inserted</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Insert(this Label? L, int index, double value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Insert(index, value).ToString();
        return L;
    }

    /// <summary>
    /// Inserts the <seealso cref="string"/> representation of a <seealso cref="decimal"/> value into the text of the <seealso cref="Label"/> at the specified position.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="index">Position where to insert</param>
    /// <param name="value"><seealso cref="decimal"/> value to be inserted</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Insert(this Label? L, int index, decimal value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Insert(index, value).ToString();
        return L;
    }

    /// <summary>
    /// Inserts the <seealso cref="string"/> representation of a <seealso cref="float"/> value into the text of the <seealso cref="Label"/> at the specified position.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="index">Position where to insert</param>
    /// <param name="value"><seealso cref="float"/> value to be inserted</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Insert(this Label? L, int index, float value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Insert(index, value).ToString();
        return L;
    }

    /// <summary>
    /// Inserts a <seealso cref="string"/> into the text of the <seealso cref="Label"/> at the specified position.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="index">Position where to insert</param>
    /// <param name="value"><seealso cref="string"/> value to be inserted</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Insert(this Label? L, int index, string value) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Insert(index, value).ToString();
        return L;
    }

    /// <summary>
    /// Remove um número específico de caracteres do texto do Label a partir da posição especificada.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="startIndex">Posição inicial para remoção</param>
    /// <param name="length">Número de caracteres a remover</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Remove(this Label? L, int startIndex, int length) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Remove(startIndex, length).ToString();
        return L;
    }

    /// <summary>
    /// Replaces all occurrences of one <seealso cref="char"/> with another in the text of the <seealso cref="Label"/>.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="oldChar"><seealso cref="char"/> to be replaced</param>
    /// <param name="newChar">Replaced <seealso cref="char"/></param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Replace(this Label? L, char oldChar, char newChar) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Replace(oldChar, newChar).ToString();
        return L;
    }

    /// <summary>
    /// Replaces all occurrences of one <seealso cref="char"/> with another in a specified portion of the text of the <seealso cref="Label"/>.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="oldChar"><seealso cref="char"/> to be replaced</param>
    /// <param name="newChar">Replaced <seealso cref="char"/></param>
    /// <param name="startIndex">Starting position for search</param>
    /// <param name="count">Number of characters to consider</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Replace(this Label? L, char oldChar, char newChar, int startIndex, int count) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Replace(oldChar, newChar, startIndex, count).ToString();
        return L;
    }

    /// <summary>
    /// Replaces all occurrences of one <seealso cref="string"/> with another in the text of the <seealso cref="Label"/>.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="oldValue"><seealso cref="string"/> to be replaced</param>
    /// <param name="newValue">Replaced <seealso cref="string"/></param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Replace(this Label? L, string oldValue, string newValue) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Replace(oldValue, newValue).ToString();
        return L;
    }

    /// <summary>
    /// Replaces all occurrences of one <seealso cref="string"/> with another in a specified portion of the text of the <seealso cref="Label"/>.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <param name="oldValue"><seealso cref="string"/> to be replaced</param>
    /// <param name="newValue">Replaced <seealso cref="string"/></param>
    /// <param name="startIndex">Starting position for search</param>
    /// <param name="count">Number of characters to consider</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label Replace(this Label? L, string oldValue, string newValue, int startIndex, int count) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear()
            .Append(L.Text)
            .Replace(oldValue, newValue, startIndex, count).ToString();
        return L;
    }

    /// <summary>
    /// Clears all text from <seealso cref="Label"/>.
    /// </summary>
    /// <param name="L"><seealso cref="Label"/> target (can be null)</param>
    /// <returns>The <seealso cref="Label"/> itself to allow chained calls</returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Label"/> is null</exception>
    public static Label ClearText(this Label? L) {
        if (L is null) throw new ArgumentNullException(nameof(L));
        L.Text = builder.Clear().ToString();
        return L;
    }
}
