namespace Godot;
/// <summary>Provide methods for <seealso cref="string"/> class.</summary>
public static class String_GD_CB_Extension {
    /// <summary>Hash the <seealso cref="string"/> and return a 32 bits unsigned integer.</summary>
    /// <param name="str"><seealso cref="string"/> to be calculated.</param>
    /// <returns>The calculated hash of the <seealso cref="string"/>.</returns>
    /// <exception cref="System.ArgumentNullException">Occurs when the np parameter is null!</exception>
    public static string StringHash(this string str) => str.Hash().ToString();
}
