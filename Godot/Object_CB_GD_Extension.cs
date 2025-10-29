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
}