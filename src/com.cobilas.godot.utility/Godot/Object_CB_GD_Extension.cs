using System;
using static System.ExceptionMessages;

namespace Godot;
/// <summary>Extensions for <seealso cref="Godot.Object"/> class.</summary>
public static class Object_CB_GD_Extension {
    /// <inheritdoc cref="GD.Print(object[])"/>
    /// <param name="N">The <see cref="Godot.Node"/> that will be used.</param>
    /// <param name="args">The arguments passed will be printed to the console.</param>
    public static void Print(this Godot.Object? N, params object[] args) {
        if (N is null) throw new System.ArgumentNullException(nameof(N));
        GD.Print(args);
	}
    /// <summary>Permite definir scripts de forma segura.</summary>
    /// <param name="obj">O objeto alvo.</param>
    /// <param name="resource">O script que será definido.</param>
    /// <typeparam name="T">O tipo do objeto que será retornado.</typeparam>
    /// <returns>Retorna o objeto alvo modificado.</returns>
    public static T? SafelySetScript<T>(this Object obj, Resource resource) where T : Object {
        ulong godotObjectId = obj.GetInstanceId();
        obj.SetScript(resource);
        return GD.InstanceFromId(godotObjectId) as T;
    }
    /// <inheritdoc cref="SafelySetScript{T}(Object, Resource)"/>
    /// <param name="obj">O objeto alvo.</param>
    /// <param name="resource">O caminho do script que será definido.</param>
    public static T? SafelySetScript<T>(this Object obj, string resource) where T : Object
        => SafelySetScript<T>(obj, ResourceLoader.Load(resource));
	/// <summary>
	/// Safely connects a signal from a source object to a method on a target object with optional binding parameters and connection flags.
	/// </summary>
	/// <param name="obj">The source object that emits the signal.</param>
	/// <param name="signal">The name of the signal to connect to.</param>
	/// <param name="target">The target object that contains the method to be called when the signal is emitted.</param>
	/// <param name="method">The name of the method on the target object to be called when the signal is emitted.</param>
	/// <param name="binds">An array of parameters to be bound to the method call, passed after any parameters from <see cref="Object.EmitSignal"/>.</param>
	/// <param name="flags">Connection flags to control the connection behavior (e.g., deferred, one-shot, reference counted).</param>
	/// <returns><c>true</c> if the connection was successfully established; otherwise, <c>false</c> if already connected.</returns>
	/// <exception cref="ArgumentNullException">Thrown when <paramref name="obj"/>, <paramref name="target"/>, <paramref name="signal"/>, or <paramref name="method"/> is null or empty.</exception>
	/// <remarks>
	/// This method performs validation checks before attempting to connect and avoids duplicate connections.
	/// It returns <c>true</c> only when a new connection is established. If the connection already exists,
	/// it returns <c>false</c> without attempting to connect again.
	/// </remarks>
	public static bool SafeConnect(this Object? obj, string? signal, Object? target, string? method, Collections.Array? binds, uint flags) {
		ThrowIfNull(obj, nameof(obj));
		ThrowIfNull(target, nameof(target));
		ThrowIfNullOrEmpty(signal, nameof(signal));
		ThrowIfNullOrEmpty(method, nameof(method));
		if (!obj.IsConnected(signal, target, method))
			return obj.Connect(signal, target, method, binds, flags) == Error.Ok;
		return false;
	}
	/// <summary>
	/// Safely connects a signal from a source object to a method on a target object with optional binding parameters and default connection flags.
	/// </summary>
	/// <param name="obj">The source object that emits the signal.</param>
	/// <param name="signal">The name of the signal to connect to.</param>
	/// <param name="target">The target object that contains the method to be called when the signal is emitted.</param>
	/// <param name="method">The name of the method on the target object to be called when the signal is emitted.</param>
	/// <param name="binds">An array of parameters to be bound to the method call, passed after any parameters from <see cref="Object.EmitSignal"/>.</param>
	/// <returns><c>true</c> if the connection was successfully established; otherwise, <c>false</c> if already connected.</returns>
	/// <exception cref="ArgumentNullException">Thrown when <paramref name="obj"/>, <paramref name="target"/>, <paramref name="signal"/>, or <paramref name="method"/> is null or empty.</exception>
	public static bool SafeConnect(this Object? obj, string? signal, Object? target, string? method, Collections.Array? binds)
		=> SafeConnect(obj, signal, target, method, binds, 0U);
	/// <summary>
	/// Safely connects a signal from a source object to a method on a target object with specified connection flags and no binding parameters.
	/// </summary>
	/// <param name="obj">The source object that emits the signal.</param>
	/// <param name="signal">The name of the signal to connect to.</param>
	/// <param name="target">The target object that contains the method to be called when the signal is emitted.</param>
	/// <param name="method">The name of the method on the target object to be called when the signal is emitted.</param>
	/// <param name="flags">Connection flags to control the connection behavior (e.g., deferred, one-shot, reference counted).</param>
	/// <returns><c>true</c> if the connection was successfully established; otherwise, <c>false</c> if already connected.</returns>
	/// <exception cref="ArgumentNullException">Thrown when <paramref name="obj"/>, <paramref name="target"/>, <paramref name="signal"/>, or <paramref name="method"/> is null or empty.</exception>
	public static bool SafeConnect(this Object? obj, string? signal, Object? target, string? method, uint flags)
		=> SafeConnect(obj, signal, target, method, null, flags);
	/// <summary>
	/// Safely connects a signal from a source object to a method on a target object with default connection settings.
	/// </summary>
	/// <param name="obj">The source object that emits the signal.</param>
	/// <param name="signal">The name of the signal to connect to.</param>
	/// <param name="target">The target object that contains the method to be called when the signal is emitted.</param>
	/// <param name="method">The name of the method on the target object to be called when the signal is emitted.</param>
	/// <returns><c>true</c> if the connection was successfully established; otherwise, <c>false</c> if already connected.</returns>
	/// <exception cref="ArgumentNullException">Thrown when <paramref name="obj"/>, <paramref name="target"/>, <paramref name="signal"/>, or <paramref name="method"/> is null or empty.</exception>
	public static bool SafeConnect(this Object? obj, string? signal, Object? target, string? method)
		=> SafeConnect(obj, signal, target, method, null, 0U);
	/// <summary>
	/// Safely disconnects a signal from a method on a target object if the connection exists.
	/// </summary>
	/// <param name="obj">The source object that emits the signal.</param>
	/// <param name="signal">The name of the signal to disconnect.</param>
	/// <param name="target">The target object that contains the method to be disconnected.</param>
	/// <param name="method">The name of the method on the target object to be disconnected from the signal.</param>
	/// <returns><c>true</c> if the disconnection was successful; otherwise, <c>false</c> if the connection did not exist.</returns>
	/// <exception cref="ArgumentNullException">Thrown when <paramref name="obj"/>, <paramref name="target"/>, <paramref name="signal"/>, or <paramref name="method"/> is null or empty.</exception>
	/// <remarks>
	/// This method checks if the connection exists before attempting to disconnect.
	/// It returns <c>false</c> if the connection does not exist, preventing unnecessary
	/// error messages that would occur with the standard <see cref="Object.Disconnect"/> method.
	/// </remarks>
	public static bool SafeDisconnect(this Object? obj, string? signal, Object? target, string? method) {
		ThrowIfNull(obj, nameof(obj));
		ThrowIfNull(target, nameof(target));
		ThrowIfNullOrEmpty(signal, nameof(signal));
		ThrowIfNullOrEmpty(method, nameof(method));
		if (!obj.IsConnected(signal, target, method)) {
			obj.Disconnect(signal, target, method);
			return true;
		}
		return false;
	}
}