using Godot;
using Cobilas.GodotEngine.Utility.Numerics;

namespace Cobilas.GodotEngine.Utility.Input;
/// <summary>This class has methods and properties that get information from keyboard and mouse inputs.</summary>
public static class InputKeyBoard {
    /// <summary>The mouse trigger index.</summary>
    /// <returns>Returns an <see cref="System.Int32"/> containing the index of the mouse trigger.</returns>
    public static int MouseIndex => InternalInputKeyBoard.MouseIndex;
    /// <summary>Detects a double mouse click.</summary>
    /// <returns>Returns true when a double mouse click is detected.</returns>
    public static bool DoubleClick => InternalInputKeyBoard.DoubleClick;
    /// <summary>Indicates a change in mouse scroll.</summary>
    /// <returns>Returns a floating-point value when the mouse scroll is changed.</returns>
    public static float DeltaScroll => InternalInputKeyBoard.DeltaScroll;
    /// <summary>The current mouse position.</summary>
    /// <returns>Returns the mouse position based on the defined <see cref="Godot.Viewport"/>.</returns>
    public static Vector2D MousePosition => InternalInputKeyBoard.MousePosition;
    /// <summary>The current global mouse position.</summary>
    /// <returns>Returns the mouse position based on a root <see cref="Godot.Viewport"/>.</returns>
    public static Vector2D MouseGlobalPosition => InternalInputKeyBoard.MouseGlobalPosition;
    /// <summary>Determines whether the key was pressed.</summary>
    /// <param name="key">The target key to be verified.</param>
    /// <returns>Returns <c>true</c> if the key was pressed.</returns>
    /// <example>
    /// <code>
    /// using Godot;
    /// using Cobilas.GodotEngine.Utility.Input;
    /// 
    /// public override void _Process(float delta) {
    ///     if (InputKeyBoard.GetKeyDown(KeyCode.A))
    ///         GD.Print("This instruction will only be called each time the key is pressed.");
    /// }
    /// </code>
    /// </example>
    /// <remarks>The method has overloads that allow the use of the <seealso cref="KeyList"/>, <seealso cref="MouseButton"/> and <seealso cref="ButtonList"/> enumerators.</remarks>
    public static bool GetKeyDown(KeyCode key) => InternalInputKeyBoard.GetKeyDown(key);
    /// <summary>Determines whether the key is being pressed.</summary>
    /// <param name="key">The target key to be verified.</param>
    /// <returns>Returns <c>true</c> if the key is being pressed.</returns>
    /// <example>
    /// <code>
    /// using Godot;
    /// using Cobilas.GodotEngine.Utility.Input;
    /// 
    /// public override void _Process(float delta) {
    ///     if (InputKeyBoard.GetKeyPress(KeyCode.A))
    ///         GD.Print("The instruction will be called constantly as long as the key is pressed.");
    /// }
    /// </code>
    /// </example>
    /// <remarks>The method has overloads that allow the use of the <seealso cref="KeyList"/>, <seealso cref="MouseButton"/> and <seealso cref="ButtonList"/> enumerators.</remarks>
    public static bool GetKeyPress(KeyCode key) => InternalInputKeyBoard.GetKeyPress(key);
    /// <summary>Determines whether the key has been released.</summary>
    /// <param name="key">The target key to be verified.</param>
    /// <returns>Returns <c>true</c> if the key was released.</returns>
    /// <example>
    /// <code>
    /// using Godot;
    /// using Cobilas.GodotEngine.Utility.Input;
    /// 
    /// public override void _Process(float delta) {
    ///     if (InputKeyBoard.GetKeyUp(KeyCode.A))
    ///         GD.Print("This instruction will only be called whenever the key is released.");
    /// }
    /// </code>
    /// </example>
    /// <remarks>The method has overloads that allow the use of the <seealso cref="KeyList"/>, <seealso cref="MouseButton"/> and <seealso cref="ButtonList"/> enumerators.</remarks>
    public static bool GetKeyUp(KeyCode key) => InternalInputKeyBoard.GetKeyUp(key);
    /// <inheritdoc cref="GetKeyDown(KeyCode)"/>
    public static bool GetKeyDown(KeyList key) => InternalInputKeyBoard.GetKeyDown(key);
    /// <inheritdoc cref="GetKeyPress(KeyCode)"/>
    public static bool GetKeyPress(KeyList key) => InternalInputKeyBoard.GetKeyPress(key);
    /// <inheritdoc cref="GetKeyUp(KeyCode)"/>
    public static bool GetKeyUp(KeyList key) => InternalInputKeyBoard.GetKeyUp(key);
    /// <inheritdoc cref="GetKeyDown(KeyCode)"/>
    public static bool GetKeyDown(MouseButton key) => InternalInputKeyBoard.GetKeyDown(key);
    /// <inheritdoc cref="GetKeyPress(KeyCode)"/>
    public static bool GetKeyPress(MouseButton key) => InternalInputKeyBoard.GetKeyPress(key);
    /// <inheritdoc cref="GetKeyUp(KeyCode)"/>
    public static bool GetKeyUp(MouseButton key) => InternalInputKeyBoard.GetKeyUp(key);
    /// <summary>Determines whether the mouse trigger was pressed.</summary>
    /// <param name="buttonIndex">The target index to be checked.</param>
    /// <returns>Returns <c>true</c> if the mouse trigger was pressed.</returns>
    public static bool GetMouseDown(int buttonIndex) => InternalInputKeyBoard.GetMouseDown(buttonIndex);
    /// <summary>Determines whether the mouse trigger is being pressed.</summary>
    /// <param name="buttonIndex">The target index to be checked.</param>
    /// <returns>Returns <c>true</c> if the mouse trigger is being pressed.</returns>
    public static bool GetMousePress(int buttonIndex) => InternalInputKeyBoard.GetMousePress(buttonIndex);
    /// <summary>Determines whether the mouse trigger has been drop by the user.</summary>
    /// <param name="buttonIndex">The target index to be checked.</param>
    /// <returns>Returns <c>true</c> if the mouse trigger was released by the user.</returns>
    public static bool GetMouseUp(int buttonIndex) => InternalInputKeyBoard.GetMouseUp(buttonIndex);
    /// <inheritdoc cref="GetMouseDown(int)"/>
    /// <param name="button">The target mouse trigger to be checked.</param>
    public static bool GetMouseDown(MouseButton button) => InternalInputKeyBoard.GetMouseDown(button);
    /// <inheritdoc cref="GetMousePress(int)"/>
    /// <param name="button">The target mouse trigger to be checked.</param>
    public static bool GetMousePress(MouseButton button) => InternalInputKeyBoard.GetMousePress(button);
    /// <inheritdoc cref="GetMouseUp(int)"/>
    /// <param name="button">The target mouse trigger to be checked.</param>
    public static bool GetMouseUp(MouseButton button) => InternalInputKeyBoard.GetMouseUp(button);
}
