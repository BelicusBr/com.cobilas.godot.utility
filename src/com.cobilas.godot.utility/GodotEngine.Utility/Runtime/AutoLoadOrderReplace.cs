using System;
using System.Collections;
using Cobilas.Collections;
using System.Collections.Generic;

namespace Cobilas.GodotEngine.Utility.Runtime;
/// <summary>
/// Provides a base class for configuring and overriding the automatic loading order 
/// of classes marked with the <see cref="AutoLoadScriptAttribute"/>.
/// </summary>
/// <remarks>
/// This class allows custom ordering of autoload classes by implementing the <see cref="AutoLoadList"/> property.
/// The order is defined by associating each type with an <see cref="OrderValue"/>.
/// </remarks>
public abstract class AutoLoadOrderReplace : IEnumerable<KeyValuePair<AutoLoadOrderReplace.OrderValue, Type>> {
	/// <summary>Gets the array of key-value pairs that define the autoload order.</summary>
	/// <returns>An array of <see cref="OrderValue"/> and <see cref="Type"/> pairs that specify the autoload order.</returns>
	public abstract KeyValuePair<OrderValue, Type>[] AutoLoadList { get; }
	/// <inheritdoc/>
	public IEnumerator<KeyValuePair<OrderValue, Type>> GetEnumerator()
		=> new ArrayToIEnumerator<KeyValuePair<OrderValue, Type>>(AutoLoadList);
	/// <inheritdoc/>
	IEnumerator IEnumerable.GetEnumerator()
		=> new ArrayToIEnumerator<KeyValuePair<OrderValue, Type>>(AutoLoadList);
	/// <summary>
	/// Represents an order value for autoload classes, which can be implicitly 
	/// converted from and to an integer.
	/// </summary>
	/// <remarks>
	/// This structure is used to define the order in which autoload classes are loaded.
	/// Lower values are loaded first.
	/// </remarks>
	public readonly struct OrderValue : IEquatable<int>, IEquatable<OrderValue> {
		private readonly int index;

		private OrderValue(int index) => this.index = index;
		/// <summary>Determines whether the current order value equals the specified integer.</summary>
		/// <param name="other">The integer to compare with the current order value.</param>
		/// <returns><c>true</c> if the current order value equals the specified integer; otherwise, <c>false</c>.</returns>
		public bool Equals(int other) => index == other;
		/// <summary>Determines whether the current order value equals another <see cref="OrderValue"/>.</summary>
		/// <param name="other">The <see cref="OrderValue"/> to compare with the current order value.</param>
		/// <returns><c>true</c> if the current order value equals the specified <see cref="OrderValue"/>; otherwise, <c>false</c>.</returns>
		public bool Equals(OrderValue other) => Equals(other.index);
		/// <inheritdoc/>
		public override bool Equals(object obj)
			=> obj is OrderValue ov && Equals(ov) ||
			obj is int ovi && Equals(ovi);
		/// <inheritdoc/>
		public override int GetHashCode() => base.GetHashCode();
		/// <summary>Determines whether two <see cref="OrderValue"/> instances are equal.</summary>
		/// <param name="A">The first <see cref="OrderValue"/> to compare.</param>
		/// <param name="B">The second <see cref="OrderValue"/> to compare.</param>
		/// <returns><c>true</c> if the two <see cref="OrderValue"/> instances are equal; otherwise, <c>false</c>.</returns>
		public static bool operator ==(OrderValue A, OrderValue B) => A.Equals(B);
		/// <summary>Determines whether two <see cref="OrderValue"/> instances are not equal.</summary>
		/// <param name="A">The first <see cref="OrderValue"/> to compare.</param>
		/// <param name="B">The second <see cref="OrderValue"/> to compare.</param>
		/// <returns><c>true</c> if the two <see cref="OrderValue"/> instances are not equal; otherwise, <c>false</c>.</returns>
		public static bool operator !=(OrderValue A, OrderValue B) => !A.Equals(B);
		/// <summary>Determines whether an <see cref="OrderValue"/> and an integer are equal.</summary>
		/// <param name="A">The <see cref="OrderValue"/> to compare.</param>
		/// <param name="B">The integer to compare.</param>
		/// <returns><c>true</c> if the <see cref="OrderValue"/> and the integer are equal; otherwise, <c>false</c>.</returns>
		public static bool operator ==(OrderValue A, int B) => A.Equals(B);
		/// <summary>Determines whether an <see cref="OrderValue"/> and an integer are not equal.</summary>
		/// <param name="A">The <see cref="OrderValue"/> to compare.</param>
		/// <param name="B">The integer to compare.</param>
		/// <returns><c>true</c> if the <see cref="OrderValue"/> and the integer are not equal; otherwise, <c>false</c>.</returns>
		public static bool operator !=(OrderValue A, int B) => !A.Equals(B);
		/// <summary>Determines whether an integer and an <see cref="OrderValue"/> are equal.</summary>
		/// <param name="A">The integer to compare.</param>
		/// <param name="B">The <see cref="OrderValue"/> to compare.</param>
		/// <returns><c>true</c> if the integer and the <see cref="OrderValue"/> are equal; otherwise, <c>false</c>.</returns>
		public static bool operator ==(int A, OrderValue B) => B.Equals(A);
		/// <summary>Determines whether an integer and an <see cref="OrderValue"/> are not equal.</summary>
		/// <param name="A">The integer to compare.</param>
		/// <param name="B">The <see cref="OrderValue"/> to compare.</param>
		/// <returns><c>true</c> if the integer and the <see cref="OrderValue"/> are not equal; otherwise, <c>false</c>.</returns>
		public static bool operator !=(int A, OrderValue B) => !B.Equals(A);
		/// <summary>Implicitly converts an integer to an <see cref="OrderValue"/>.</summary>
		/// <param name="index">The integer to convert.</param>
		/// <returns>An <see cref="OrderValue"/> that represents the integer.</returns>
		public static implicit operator OrderValue(int index) => new(index);
		/// <summary>Implicitly converts an <see cref="OrderValue"/> to an integer.</summary>
		/// <param name="value">The <see cref="OrderValue"/> to convert.</param>
		/// <returns>The integer value of the <see cref="OrderValue"/>.</returns>
		public static implicit operator int(OrderValue value) => value.index;
	}
}