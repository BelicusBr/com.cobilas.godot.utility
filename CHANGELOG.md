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