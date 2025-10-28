﻿using System;
using System.IO;
using System.Text;

namespace Cobilas.GodotEngine.Utility.IO.Interfaces;
/// <summary>Represents a generic stream interface for reading from and writing to different data sources.</summary>
public interface IStream : IDisposable {
	/// <inheritdoc cref="Stream.CanRead"/>
	bool CanRead { get; }
	/// <inheritdoc cref="Stream.CanWrite"/>
	bool CanWrite { get; }
	bool AutoFlush { get; set; }
	/// <summary>Reads all bytes from the stream.</summary>
	/// <returns>A byte array containing the data read from the stream.</returns>
	byte[] Read();
	/// <summary>Reads the stream content as a string using the specified encoding.</summary>
	/// <param name="encoding">The character encoding to use. If null, UTF-8 will be used.</param>
	/// <param name="result">When this method returns, contains the string representation of the stream content.</param>
	void Read(Encoding? encoding, out string result);
	/// <summary>Reads the stream content into a <see cref="StringBuilder"/> using the specified encoding.</summary>
	/// <param name="encoding">The character encoding to use. If null, UTF-8 will be used.</param>
	/// <param name="result">When this method returns, contains the <see cref="StringBuilder"/> with the stream content.</param>
	void Read(Encoding? encoding, out StringBuilder result);
	/// <summary>Reads the stream content as a character array using the specified encoding.</summary>
	/// <param name="encoding">The character encoding to use. If null, UTF-8 will be used.</param>
	/// <param name="result">When this method returns, contains the character array representation of the stream content.</param>
	void Read(Encoding? encoding, out char[] result);
	/// <summary>Reads the stream content as a string using UTF-8 encoding.</summary>
	/// <param name="result">When this method returns, contains the string representation of the stream content.</param>
	void Read(out string result);
	/// <summary>Reads the stream content into a <see cref="StringBuilder"/> using UTF-8 encoding.</summary>
	/// <param name="result">When this method returns, contains the <see cref="StringBuilder"/> with the stream content.</param>
	void Read(out StringBuilder result);
	/// <summary>Reads the stream content as a character array using UTF-8 encoding.</summary>
	/// <param name="result">When this method returns, contains the character array representation of the stream content.</param>
	void Read(out char[] result);
	/// <summary>Writes a sequence of bytes to the stream.</summary>
	/// <param name="buffer">The byte array to write. If null, no data is written.</param>
	void Write(byte[]? buffer);
	/// <summary>Writes a character array to the stream using the specified encoding.</summary>
	/// <param name="buffer">The character array to write. If null, no data is written.</param>
	/// <param name="encoding">The character encoding to use. If null, UTF-8 will be used.</param>
	void Write(char[]? buffer, Encoding? encoding);
	/// <summary>Writes a string to the stream using the specified encoding.</summary>
	/// <param name="buffer">The string to write. If null, no data is written.</param>
	/// <param name="encoding">The character encoding to use. If null, UTF-8 will be used.</param>
	void Write(string? buffer, Encoding? encoding);
	/// <summary>Writes a <see cref="StringBuilder"/> to the stream using the specified encoding.</summary>
	/// <param name="buffer">The <see cref="StringBuilder"/> to write. If null, no data is written.</param>
	/// <param name="encoding">The character encoding to use. If null, UTF-8 will be used.</param>
	void Write(StringBuilder? buffer, Encoding? encoding);
	/// <summary>Writes a character array to the stream using UTF-8 encoding.</summary>
	/// <param name="buffer">The character array to write. If null, no data is written.</param>
	void Write(char[]? buffer);
	/// <summary>Writes a string to the stream using UTF-8 encoding.</summary>
	/// <param name="buffer">The string to write. If null, no data is written.</param>
	void Write(string? buffer);
	/// <summary>Writes a <see cref="StringBuilder"/> to the stream using UTF-8 encoding.</summary>
	/// <param name="buffer">The <see cref="StringBuilder"/> to write. If null, no data is written.</param>
	void Write(StringBuilder? buffer);
	/// <inheritdoc cref="Stream.Flush"/>
	void Flush();
	/// <summary>Replaces the entire stream content with the specified byte array.</summary>
	/// <param name="newBuffer">The new byte array content. If null, the stream will be cleared.</param>
	void ReplaceBuffer(byte[]? newBuffer);
	/// <summary>Replaces the entire stream content with a character array using the specified encoding.</summary>
	/// <param name="newBuffer">The new character array content. If null, the stream will be cleared.</param>
	/// <param name="encoding">The character encoding to use. If null, UTF-8 will be used.</param>
	void ReplaceBuffer(char[]? newBuffer, Encoding? encoding);
	/// <summary>Replaces the entire stream content with a string using the specified encoding.</summary>
	/// <param name="newBuffer">The new string content. If null, the stream will be cleared.</param>
	/// <param name="encoding">The character encoding to use. If null, UTF-8 will be used.</param>
	void ReplaceBuffer(string? newBuffer, Encoding? encoding);
	/// <summary>Replaces the entire stream content with a <see cref="StringBuilder"/> using the specified encoding.</summary>
	/// <param name="newBuffer">The new <see cref="StringBuilder"/> content. If null, the stream will be cleared.</param>
	/// <param name="encoding">The character encoding to use. If null, UTF-8 will be used.</param>
	void ReplaceBuffer(StringBuilder? newBuffer, Encoding? encoding);
	/// <summary>Replaces the entire stream content with a character array using UTF-8 encoding.</summary>
	/// <param name="newBuffer">The new character array content. If null, the stream will be cleared.</param>
	void ReplaceBuffer(char[]? newBuffer);
	/// <summary>Replaces the entire stream content with a string using UTF-8 encoding.</summary>
	/// <param name="newBuffer">The new string content. If null, the stream will be cleared.</param>
	void ReplaceBuffer(string? newBuffer);
	/// <summary>Replaces the entire stream content with a <see cref="StringBuilder"/> using UTF-8 encoding.</summary>
	/// <param name="newBuffer">The new <see cref="StringBuilder"/> content. If null, the stream will be cleared.</param>
	void ReplaceBuffer(StringBuilder? newBuffer);
}