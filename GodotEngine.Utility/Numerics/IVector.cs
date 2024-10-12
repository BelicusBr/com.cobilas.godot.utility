using System;

namespace Cobilas.GodotEngine.Utility.Numerics;
/// <summary>Standardization interface for vectors.</summary>
public interface IVector : IFormattable {
    /// <inheritdoc cref="Godot.Vector2.Length"/>
    float magnitude { get; }
    /// <summary>Returns the squared length (squared magnitude) of this vector. This method runs
    /// faster than <seealso cref="IVector.magnitude"/>, so prefer it if you need to compare vectors
    /// or need the squared length for some formula.
    /// </summary>
    /// <returns>The squared length of this vector.</returns>
    float sqrMagnitude { get; }
    /// <inheritdoc cref="Godot.Vector2.Floor"/>
    IVector floor { get; }
    /// <inheritdoc cref="Godot.Vector2.Ceil"/>
    IVector ceil { get; }
    /// <summary>Returns the aspect ratio of this vector, the ratio of <seealso cref="IVector"/>.x to <seealso cref="IVector"/>.y.</summary>
    /// <returns>The <seealso cref="IVector"/>.x component divided by the <seealso cref="IVector"/>.y component.</returns>
    float aspect { get; }
    /// <summary>Number of axles.</summary>
    /// <returns>Returns the number of axes a vector has.</returns>
    int AxisCount { get; }
    /// <inheritdoc cref="Godot.Vector2.Normalized"/>
    IVector Normalized { get; }
    /// <summary>Allows you to access the axes of a vector through an index.</summary>
    /// <param name="index">The axis index.</param>
    /// <value>Sets the value of an axis by specifying its index.</value>
    /// <returns>Returns the value of an axis by specifying its index.</returns>
    float this[int index] { get; set; }
    /// <inheritdoc cref="Godot.Vector2.Round"/>
    IVector Round();
    /// <inheritdoc/>
    string ToString(string format);
}