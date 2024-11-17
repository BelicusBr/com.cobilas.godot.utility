using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections;
using Cobilas.Collections;
using System.Collections.Generic;
using Cobilas.IO.Serialization.Json;

namespace Cobilas.GodotEngine.Utility;
/// <summary>Stores custom resolutions.</summary>
/// <remarks>Stores all custom resolutions defined in the application to be used.</remarks>
[Serializable]
public readonly struct CustonResolutionList : IEnumerable<Resolution> {

    private readonly int hash;
    private readonly Resolution[] list;

    /// <summary>Starts a new instance of the object.</summary>
    public CustonResolutionList(in int hash, in Resolution[] list) {
        this.hash = hash;
        this.list = list;
    }

    /// <summary>Starts a new instance of the object.</summary>
    public CustonResolutionList(in int hash, in IEnumerable<Resolution> list) :
        this(hash, list.ToArray()) {}
    /// <summary>Serializes a list of objects to a specified file.</summary>
    /// <param name="list">The list to be serialized.</param>
    /// <param name="stream">The file that will receive the list.</param>
    /// <exception cref="ArgumentNullException">An exception will be thrown if the parameters are null.</exception>
    public static void Serialize(in CustonResolutionList[] list, Stream? stream) {
        if (stream is null)
            throw new ArgumentNullException("stream");
        else if (stream is null)
            throw new ArgumentNullException("list");
        stream.Write(Json.Serialize(list, true));
    }
    /// <summary>Deserializes a list of objects from a specified file.</summary>
    /// <param name="stream">The file where the list is.</param>
    /// <returns>Returns a custom resolution list.</returns>
    /// <exception cref="ArgumentNullException">An exception will be thrown if the parameters are null.</exception>
    public static CustonResolutionList[] Deserialize(Stream? stream) {
        if (stream is null)
            throw new ArgumentNullException("stream");
        return Json.Deserialize<CustonResolutionList[]>(Encoding.UTF8.GetString(stream.Read()))!;
    }

    /// <inheritdoc/>
    public readonly IEnumerator<Resolution> GetEnumerator() => new ArrayToIEnumerator<Resolution>(list);

    readonly IEnumerator IEnumerable.GetEnumerator() => new ArrayToIEnumerator<Resolution>(list);
    /// <summary>Explicit conversion operator.(<seealso cref="CustonResolutionList"/> to <seealso cref="Int32"/>)</summary>
    /// <param name="R">Object to be converted.</param>
    public static explicit operator int(CustonResolutionList R) => R.hash;
}
