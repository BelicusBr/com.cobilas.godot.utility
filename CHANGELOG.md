# [6.2.2] (15/10/2025)
## Fixed
**Problem:** The `Vector2D` properties (`Pivot`, `Scale`, `MinSize`, `Size` and `Position`) were throwing `ArgumentOutOfRangeException` due to an error in initializing the `Vector2D` structure.

**Root Cause:**
- The `Vector2D` structure was being initialized incorrectly in the internal `InitVector2D` method
- The code was assigning the value of the X axis (`x`) to both index 0 and index 2, leaving the Y axis (`y`) uninitialized
- This affected all properties that depended on these vectors and any methods that used them

**Implemented Solution:**
- Fixed the `InitVector2D` method to correctly initialize both axes:
  - Index 0: X axis (`x`)
  - Index 1: Y axis (`y`)
- Eliminated the incorrect assignment to index 2 (nonexistent in `Vector2D`)

**Impact:**
- Fixed `ArgumentOutOfRangeException` in all operations involving vector properties
- Restored correct operation of methods that depend on these properties
- Improved overall stability of 2D geometry manipulation

# [6.2.1] (08/10/2025)
## Fixed
- Made the `Gizmos.Color` property public again in the Gizmos class

## Changed
- Added modulus calculation methods to vector structures
- Fixed documentation comments for `Quaternion.Rad2Deg` and `Quaternion.Deg2Rad` constants (previously swapped)
- Marked `Rect2_GD_CB_Extension.Contains` as obsolete since it duplicates `Rect2.HasPoint(Vector2)` functionality

## Added
- **Extension Methods**:
  - `Rect2_GD_CB_Extension.Width(this Rect2)` - Gets the width of a Rect2
  - `Rect2_GD_CB_Extension.Height(this Rect2)` - Gets the height of a Rect2
  - **Quaternion Direction Generation**:
    - `GenerateDirection(Vector3D)` - Generates a direction based on a Vector3D
    - `GenerateDirectionRight()` - Generates right direction
    - `GenerateDirectionUp()` - Generates up direction  
    - `GenerateDirectionForward()` - Generates forward direction
    - `GenerateDirectionLeft()` - Generates left direction
    - `GenerateDirectionDown()` - Generates down direction
    - `GenerateDirectionBack()` - Generates back direction

- **Vector Modulus Operators**:
  - `Vector3D % float`, `Vector3D % Vector3`, `Vector3D % Vector3D`
  - `Vector2D % float`, `Vector2D % Vector2`, `Vector2D % Vector2D`
  - `Vector2DInt % int`, `Vector2DInt % Vector2DInt`
  - `Vector3DInt % int`, `Vector3DInt % Vector3DInt`

- **UI Extension Methods**:
  - Comprehensive text manipulation extensions for `Label`, `Control`, `Sprite`, and `TextEdit` classes
  - Includes Append, Insert, Remove, Replace, ClearText, and formatting methods
  - Uses shared StringBuilder for performance in frequent text operations

- **Enhanced Rectangle Structure**:
  - Added `Rect2D` structure as an enhanced version of Godot's Rect2
  - Provides additional properties for Control-derived classes and Sprite
  - Includes rotation, scale, pivot, and minimum size support
  - Advanced point detection considering transformations

- **Color System Enhancements**:
  - Added 140+ predefined color constants to `ColorF` structure
  - Added comprehensive arithmetic operators (+, -, *, /) for ColorF
  - Support for operations with Godot's Color type
  - Enhanced color manipulation capabilities

The update focuses on expanding mathematical utilities, improving UI component functionality, and providing more comprehensive color and transformation handling for Godot Engine development.

# [6.1.2] (06/10/2025)
## Fixed
The extension method `Camera2D_GD_CB_Extension.WorldToScreenPoint(this Camera2D, Vector2D)` had a convention problem where the method did not consider the zoom of the 2D camera.

# [6.1.1] (03/10/2025)
## Added
The `Node_GD_CB_Extension.SetParent(this Node?, Node?)` extension method was added to the Node class.

## Fixed
The `InputKeyBoard.DoubleClick` property had an issue with remaining true while the mouse button was held down because the property was not reset correctly.

## Changed
The `CoroutineManager` class has been converted to an internal class, and its methods such as `StartCoroutine(IEnumerator?)`, `StopCoroutine(Coroutine?)`, and `StopAllCoroutines()` can now be accessed by the Coroutine class. \
The `SceneManager`, `Gizmos`, `Physics2D`, and `InputKeyBoard` classes no longer inherit from the `Node` class and have become static classes. This was done so that these classes do not inherit methods or fields that derive from the Node class.

## Removed
Deprecated classes such as `GDDirectory`, `GDFile`, `GDFileBase`, and `GDIONull` have been removed. Use classes such as `Archive` and `Folder` to access folders and files.

# [5.1.1] (18/09/2025)
## Fixed
The `NodePath_GD_CB_Extension.GetNode(NodePath?)` extension method could encounter a 'path not found' issue because the extension method does not correctly handle relative paths.

# [5.1.0-ch.2;5.1.0] (24-26/08/2025)
## Changed
This push was focused on resolving possible null references and documenting classes or methods.

# [5.1.0-ch.1] (21/08/2025)
## Added
Two attributes, `ExportRangeAttribute` and `RunInEditorAttribute`, have been added.
Extensions for the `Godot.Object` and `Godot.Vector2` classes.
The methods `Node_GD_CB_Extension.Duplicate<T>(this Node, int) where T : Node`, `Node_GD_CB_Extension.ContainsNode(this Node, Node)`,
`Vector2_CB_GD_Extensions.Neg(this Vector2, bool, bool)`, and `Rect2_GD_CB_Extension.Contains(this Rect2, in Vector2D)` have been added to the extension classes.

### ExportRangeAttribute
Allows integer or floating-point values ​​to take on a Range element format in the Godot inspector.

### RunInEditorAttribute
This attribute has the same function as ToolAttribute.

## Changed
The `Node_GD_CB_Extension.Print(this Node, params object[])` method has been moved to the `Object_CB_GD_Extension` extension class.

# [5.0.0] (16/02/2025)
### Added
- **`Folder` class:** Introduced to replace the `GDDirectory` class. The new `Folder` class offers optimized methods for manipulating directories and files within the Godot project, providing greater efficiency and usability.
- **`Archive` class:** Replaces the `GDFile` class, with improved functionality for file management, including reading and writing data.
- **`GodotPath` class:** Added to make path manipulation in Godot easier. `GodotPath` allows for path merging and verification operations.

### Deprecate
- **`GDDirectory` class:** Declared obsolete and replaced by the new `Folder` class. All old methods are now implemented more efficiently in the `Folder` class.
- **`GDFile` class:** Marked as obsolete and replaced by the `Archive` class. The new `Archive` class inherits the functionality of `GDFile`, with significant improvements.

### Removed
- **`GDFileAttributes` enum:** Removed from the package and replaced with the `ArchiveAttributes` enum, which offers a more comprehensive set of attributes and is better aligned with the new `Archive` class.

### Changed
- **Relocation of serialization classes:** Classes such as `BuildSerialization`, responsible for property serialization, have been moved to the `Cobilas.GodotEditor.Utility.Serialization` namespace. This change better organizes the code and improves the modularity of the project.

# [4.8.1] (13/02/2025)
## Fixed
### SerializationCache
There was a problem creating cache files due to the use of the `Node.GetPathTo(Node)` method, which was used incorrectly, resulting in the same cache being used in game and editor mode. \
The solution was to create two caches for each mode where the editor cache is copied to the player cache.

# [4.8.0] (13/02/2025)
## Added
### PlayModeStateChange
The package now provides the `RunTimeInitialization.PlayModeStateChanged` event that allows you to check which mode the editor is in, whether the editor is entering or exiting game or editor mode.

# [4.7.1] (23/01/2025)
## Fixed
### System.Number.ThrowOverflowOrFormatException
The `PrimitiveTypeCustom.CacheValueToObject(string?, string?)` method had a `System.Number.ThrowOverflowOrFormatException` issue caused by some extensions of the `String_CB_Extension` class.

### Single cache
Now the cache has been separated for editor mode and player mode to prevent editor and simulation mode from changing the same cache file.

### Extra cache check
An extra property name comparison check has been added to the `Get` and `Set` methods.

# [4.7.0] (23/01/2025)
## Added
Color structures such as `Color32` and `ColorF` have now been added. \
`Color32` represents an `rgba` value between 0 and 255. \
`ColorF` represents a normalized `rgba` value between 0 and 1f.

# [4.6.1] (23/01/2025)
## Fixed
The formatting of the `Vector4D` structure in the `ToString()` method where the `w` axis was not displayed, instead the `z` axis was displayed.

# [4.6.0] (19/01/2025)
## Added
### Custom hint
Now `SerializeField` attributes in their parameters can receive `CustomHint`.

# [4.5.1] (19/01/2025)
## Fixed
### Get members of a type
The `BuildSerialization.Build` method had the problem of searching for the members of the type but did not search for the members of the types inherited by the child class, which caused inherited members not to be serialized in the editor.

### Converting json values
The `PropertyCustom` class in the `PropertyCustom.CacheValueToObject(string?, string?)` method may throw a `FormatException` because it does not handle an empty json value.

# [4.5.0] (16/01/2025)
## Changed
The `BuildSerialization.Build(Godot.Object)` method now accepts objects of type `Godot.Node` and `Godot.Resource`.

## Added
Now objects of type `Enum` have a `PropertyCustom` class. \
The `PropertyCustomAttribute` attribute allows the use of parent classes for child classes.

# [4.4.1] (13/01/2025)
## Fixed
The `SONull` and `SPCNull` classes had inherited members that returned `System.NotImplementedException` which caused problems when creating an instance of these classes.

# [4.4.0] (08/01/2025)
## Added
### Custom serialization
Now a class has been added for custom serialization of properties in the Godot inspector. \
With the `HideProperty` and `ShowProperty` attributes you can serialize properties in the Godot inspector.

# [4.3.0] (26/12/2024)
## Added
### DebugLog
The `DebugLog` class has been added to provide a way to output trace logs to the godot console.

# [4.2.7] (23/12/2024)
## Fixed
### Invalid file path
The `GDDirectory.GetGDDirectory(string)` method was creating invalid paths for files and directories for representations which resulted in directory or file not found errors.

# [4.2.5] (18/11/2024)
## Fixed
### Correction of MouseButton enum values
`MouseButton.MouseRight` now has the correct value of 2 (previously 1). \
`MouseButton.MouseLeft` now has the correct value of 1 (previously 2). \
This fix resolves the inversion in mouse trigger detection, ensuring that mouse click events are interpreted correctly.

# [4.2.4] (16/11/2024)
## Changed
### Dependency update
The `Cobilas.Core.Net4x` dependency of the `Cobilas.Godot.Utility` package has been updated to version `2.0.1`.

# [4.2.3] (14/11/2024)
## Fixed
### Incorrect resolution
The `Screen.CurrentResolution` property had the problem of returning the full screen resolution without considering other screen modes.
### Problems with vector and quaternion interpolation
All vector and quaternion structures had a problem of `ArgumentNullException` when inserted into an interpolated string. \
This could occur when the `format` parameter of the `IFormattable.ToString(string, IFormatProvider)` will be null.

# [4.2.1] (14/11/2024)
## Fixed
### NullReferenceException in Screen class
Private `Screen.AddResolution` function always emitted a `NullReferenceException` because it does not correct the parameter if it is null and void or not.

# [4.2.0] (12/11/2024)
## Added
### Vector inversion operators
Now the `Vector2D`, `Vector2DInt`, `Vector3D`, `Vector3DInt` and `Vector4D` structures have received values inversion operators.

### Implicit operator
The `Quaternion` structure received an implicit operator from `Quaternion` to `Vector4D`.

# [4.1.2] (12/11/2024)
## Fixed
### Deltascroll always positive
The `Inputkeyboard.Deltascroll` property always returned positive values by misunderstanding inputs reeiers

# [4.1.1] (11/11/2024)
## Fixed
### NullReferenceException in Gizmos._Process(float)
The method uses a private variable to update the drawing method but the `Gizm._Process(float)` method did not verify if variable was null.

# [4.1.0] (29/10/2024 - 04/11/2024)
## Added
### IYieldCoroutine.IsLastCoroutine property
This property was added to indicate whether the coroutine will be executed after all update methods.

### LastFixedRunTimeSecond class
This class was added to signal that the coroutine will be executed after all `_PhysicsProcess(float)` update methods.

### LastRunTimeSecond class
This class was added to signal that the coroutine will be executed after all `_Process(float)` update methods.

### NullNode class
This class gives a representation of a null node.

### Vector2D operator
Added addition, subtraction, division and multiplication operators between `Vector2D` and `Godot.Vector2`.

### Vector3D operator
Added addition, subtraction, division and multiplication operators between `Vector3D` and `Godot.Vector3`.

### Scene
#### properties
The `Scene.SceneNode` property was added to obtain the scene in node form. \
The `Scene.Empty` property was created to give an empty representation of a scene.

### SceneManager.BuiltScenes property
This property's function is to reveal all the scenes that were compiled together with the project and that were in the <kbd>res://Scenes/</kbd> folder.

## Changed
All constructors of the `RunTimeInitializationClassAttribute` class have been replaced by the `RunTimeInitializationClassAttribute(string?, [Priority:Priority.StartBefore], [int:0], [bool:false])` constructor

## Removed
### KeyItem and MouseItem
These structures have been removed as they are being replaced by the `PeripheralItem` structure.

### RunTimeInitialization constants
The constants `RunTimeInitialization.DeltaTime` and `RunTimeInitialization.FixedDeltaTime` have been removed as they have been replaced by the constants `RunTime.DeltaTime` and `RunTime.FixedDeltaTime`.

# [3.2.0] (17/10/2024 - 27/10/2024)
## Added
### GDDirectory.GetDirectories()
The `GDDirectory.GetDirectories()` method has been added. \
This method allows you to get the subfolders within the current folder.

### Overloading methods of the GDFile class.
The `GDFile.Load(string, bool)` and `GDFile.Load<T>(string, bool)` methods have been added to the `GDFile` method.

### properties for the GDIONull classes.
The `GDIONull.FileNull` and `GDIONull.DirectoryNull` properties have been added to the `GDIONull` class. \
These properties return a null representation of a file or directory.

### KeyCode enum
The `KeyCode` enum was added to extend the `KeyList` enum by adding mouse buttons.

### PeripheralItem struct
The `PeripheralItem` struct has been added to hold certain keyboard or mouse event information.

### InputKeyBoard and method overloading
`InputKeyBoard` has built-in methods `InputKeyBoard.GetKeyDown(KeyCode)`, `InputKeyBoard.GetKeyUp(KeyCode)` and `InputKeyBoard.GetKeyPress(KeyCode)` that take `KeyCode` as a parameter.

## Removed
The `InputKeyBoard._PhysicsProcess(float)` method has been removed as it is no longer needed.

## Changed
Now the `KeyStatus` enum has the `System.Flags` attribute.

## Deprecate
The `MouseItem` and `KeyItem` structures have been replaced by the `PeripheralItem` structure.

# [3.1.1] (13/10/2024)
## Added
### Vector4D Comparison Operator
Equality and difference operators have been added to the Vector4D structure.

# [3.0.0] (12/10/2024)
## Removed
### IIntVectorGeneric interface
The interface was removed as there was no need to contain non-static functions used to convert floating point vectors.

### IIntVector.floorToInt
The property was removed as there was no need for it.

### IIntVector.ceilToInt
The property was removed as there was no need for it.

### IIntVector.RoundToInt()
The method was removed as there was no need for it.

# [2.0.0] (06/10/2024-12/10/2024)
## Added
### Screen.OrphanList
The `Screen.OrphanList` property has been added to display a list of custom resolutions not tied to a screen.

### RayHit2D.IsValid(Dictionary?)
The `RayHit2D.IsValid(Dictionary?)` method has been added to check whether an object of type `Godot.Collections.Dictionary` is valid for conversion.

### RayHit2D.IsValid(Dictionary?)
The `Hit2D.IsValid(Dictionary?)` method has been added to check whether an object of type `Godot.Collections.Dictionary` is valid for conversion.

## Changed
### Screen.Resolutions
The `Screen.Resolutions` property now returns an array of type `Resolution` which makes it incompatible with previous versions of the package.

### Screen.CurrentResolution
The `Screen.CurrentResolution` property now returns a value of type `Resolution` which makes it incompatible with previous versions of the package.

### Scene struct
The `Scene` struct is implementing the comparison interfaces `IEquatable<Scene>, IEquatable<int>, IEquatable<string>`. \
The `Scene` struct implements the comparison operators and also implements the explicit conversion operators <kbd>Scene -> int</kbd> and <kbd>Scene -> string</kbd>.

# [1.8.0] (03/10/2024-06/10/2024)
## Added
### RunTime.TimeScale
The `RunTime.TimeScale` property has been added to track in-game time.

### RunTime.FrameCount
The `RunTime.FrameCount` property has been added to show how many frames have been displayed in-game.

### Screen.ScreenRefreshRate
The `Screen.ScreenRefreshRate` property has been added to show the screen refresh rate.

### Screen.Displays
The `Screen.Displays` property has been added to display a list of connected displays on the PC.

### Screen.CurrentDisplay
The `Screen.CurrentDisplay` property has been added to display information about the current screen.

### Screen.DisplayCount
The `Screen.DisplayCount` property has been added to display the number of screens connected to the PC.

### CustonResolutionList struct
The `CustonResolutionList` struct has been added to store a list of custom resolutions for a screen.

### DisplayInfo struct
The `DisplayInfo` struct has been added to store screen information.

### Resolution struct
The `Resolution` struct has been added to store resolution information.

# [1.7.0] (29/09/2024)
## Added
New `RunTime` structure
### Details
The `RunTime` structure has been added to provide runtime values ​​and functions.

## Changed
Changes in `RunTimeInitialization`.
### Details
Form changes to the `RunTimeInitialization` class such as placing the `PriorityList` structure outside the `RunTimeInitialization` class, allowing `RunTimeInitialization` to accept negative values ​​as priority. \
The lower the priority value, the higher its execution level will be.

# [1.6.0] (26/09/2024)
## Added
New structures and methods.
### Details
New vector structures like `Vector2Int` and `Vector3Int` have been added. \
The `Vector2D`, `Vector3D` and `Vector4D` structures received the `Neg` Method to negate the vector axes and the `Round` method.

# [1.5.3] (24/09/2024)
## Fixed
`Randomico.value` is now a property.
### Details
Since `Randomico.value` was of field type, this made the return of pseudorandom methods static.

# [1.5.2] (17/09/2024)
## Fixed
`StackOverflowException` in `Vector2D.Magnitude(in Vector2D)`
### Details
The `Vector2D.Magnitude(in Vector2D)` method raised `StackOverflowException` by calling itself.

# [1.5.1] (12/09/2024)
## Add
Vector Structure.
### Details
Vector structures such as 2d, 3d, 4d, and quaternion vectors have been added.

## Fixed
`ArgumentNullException` in `GDDirectory.GetGDDirectory()` method.
### Details
The `GDDirectory.GetGDDirectory()` method always raised an `ArgumentNullException` due to the fact that the `GDDirectory.GetGDDirectory()` method uses the private `GDDirectory.GetGDDirectory(string, GDFileBase)` method, which received a null value in the parent parameter, but that, as of version `1.4.0`, the parent parameter of the GDDirectory class constructor is now checked if it is null, which caused the `GDDirectory.GetGDDirectory()` method to break.

# [1.4.0] (04/08/2024)
## Add
The `Randomico` and `GDIONull` classes.
### Details
The `Randomico` class was added for pseudorandom value generation. \
The `GDIONull` class was created to represent a null `GDFileBase` object.

# [1.3.0] (07/07/2024)
## Add
The static class `GDFeature` was added to address the lack of specific <kbd>preprocessing definitions</kbd>.

# [1.2.2] (06/07/2024)
## Changed
Dependency `Cobilas.Core.Net4x@1.1.0` has been updated to version `1.6.1`.
## Fixed
Method not found
### Details
The `Method not found` exception was caused by the `Cobilas.Core.Net4x` dependency not being updated.

# [30/12/2023]#1.1.1
## Fixed
Methods that use the private method `Array CreateExclude(CollisionObject2D[]);` had the null reference problem.
This occurred when the `Array CreateExclude(CollisionObject2D[]);` method received an empty list which caused the method to return a null list of type `Godot.Collections.Array`.
# Added
The CHANGELOG.md file has been added.
## Changed
The com.cobilas.godot.utility.csproj file has been changed.
# [31/12/2023]#1.1.2
## Fixed
O An exception of type `InvalidCastexception` in the explicit operator of the `Hit2D` structure where the `Collision` property could receive an object of another type than `CollisionObject2D` which would inevitably cause `InvalidCastexception`.
The explicit operator of the `RayHit2D` structure was also changed to avoid the same problem as the explicit operator of the `Hit2D` structure.
# [31/12/2023]#1.2.0
## Added
The `Gizmos` class has been added.
The `Gizmos` class allows you to use drawing functions statically.
# [01/01/2024]#1.2.1
## Fixed
In the static constructor of the `Screen` class it only obtained the resolutions saved in the <kbd>res://AddResolution.json</kbd> file but with the correction the <kbd>projectrootfolder://AddResolution.json</kbd> file in the root folder of the already compiled project is also obtained.