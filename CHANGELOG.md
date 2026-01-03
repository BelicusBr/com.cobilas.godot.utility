# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [7.7.1] - (03/01/2026)

### Changed
- **Method optimization**: Refactored the `Equals(string? other)` method in an unspecified class to use a more concise expression-bodied syntax and modified its comparison logic.
  - **Before**: Sequential null checks and comparisons for `ScenePath`, `Name`, and `NameWithoutExtension` properties
  - **After**: Consolidated logic that requires `ScenePath` to be non-null and then compares `other` against `ScenePath`, `Name`, or `NameWithoutExtension` using logical OR operations

### Technical Notes
- The new implementation returns `false` immediately if `ScenePath` is `null`
- When `ScenePath` is not `null`, the method now checks if `other` equals any of the three properties (`ScenePath`, `Name`, or `NameWithoutExtension`)
- This change may affect behavior in edge cases where `ScenePath` is `null` but `Name` or `NameWithoutExtension` would have matched `other` in the previous implementation
- The refactoring improves code readability and reduces branching complexity

## [7.7.0] - (13/12/2025)

### Added
- **Direct `Godot.Resource` serialization support:** The `BuildSerialization` system now natively supports serializing and deserializing `Godot.Resource` types.
- **New `ResourceCustom` class:** A dedicated property handler (`ResourceCustom`) has been implemented within the `Cobilas.GodotEditor.Utility.Serialization.Properties` namespace. This class handles the conversion between `Resource` instances and their file paths for caching purposes.

### Changed
- **Public API:** The method `BuildSerialization.BuildObjectRender(Godot.Object?)` has been changed from `private static` to **`public static`**, allowing external code to build property manipulation interfaces for Godot objects.
- **Package Version:** Updated all references and documentation from version `7.6.0` to `7.7.0`.
- **Comments:** Added comprehensive XML documentation for the `BuildObjectRender` method and the new `ResourceCustom` class, detailing their purpose and usage.

### Fixed
- **Serialization Test Scene:** Updated the test scene (`Serialization_Test.tscn`) and script to properly test and display serialized `Resource` properties. Resolved a `NullReferenceException` that was previously thrown when accessing the `res` property in the test.

## [7.7.0-rc.3] - (11/12/2025)

### Added
- **Initial Resource Serialization:** Introduced foundational support for serializing `Godot.Resource` objects. The `ResourceCustom` property handler was created and integrated into the `PropertyRender` system.
- **Serialization Test Infrastructure:** Added a comprehensive test scene (`Serialization_Test.tscn`), C# test script (`Serialization_Test.cs`), and a cache file (`cacheList.cache`) to validate the serialization of various custom types including `ObjectRef`, `Rect2D`, vectors, and the new `Resource` type.

## [7.6.2] - (11/12/2025)

### Fixed
- **Culture-Invariant Serialization:** Corrected a critical bug in `Rect2DCustom.ObjectToCacheValue` where the `x` and `y` components of `Vector2` properties within a `Rect2D` were converted to string **without** using `CultureInfo.InvariantCulture`. This mismatch in `IFormatProvider` caused serialization/deserialization failures when the system's culture used a different decimal separator (e.g., a comma) than Godot's internal format (which uses a period).

## [7.6.1] - (11/12/2025)

### Fixed
- **Runtime Serialization in Exported Projects:** Resolved a major issue where `BuildSerialization.SetValue` failed in exported game builds. The problem stemmed from a dependency on cached values that required the object's scene path to generate an ID—information unavailable when `Godot.Object._Set()` is called early in the initialization process.
- **Architecture Simplified:** The workaround class `LastBuildSerialization` (which deferred property setting until the scene was fully loaded) was removed. The fix involved modifying the `PropertyRender.Set` logic to pass values from `_Set()` directly, eliminating the erroneous step of trying to fetch initial values from the cache for fields marked with `IsSaveCache`.
- **Improved Error Messages:** Enhanced the exception messages in `ObjectRef` implicit conversion operators to specify the type of conversion that failed.

### Deprecated
- **Obsolete Code Path:** A comment was added to `BuildSerialization.cs` indicating that certain obsolete objects should be removed in a future version (>= 7.11.0).

## [7.6.0] - (29/11/2025)

### Added
- **Color Conversion**: Added explicit conversion operators from `Color32` and `ColorF` structures to Godot's `Color` structure
- **Integration**: Enhanced interoperability between custom color structures and Godot's native color system

### Features
- **Color32 to Color**: Explicit conversion operator that preserves RGBA values using `Color.Color8` method
- **ColorF to Color**: Explicit conversion operator that uses floating-point RGBA values to create Godot color instances

### Changed
- **Version Update**: Bumped package version from 7.5.1 to 7.6.0
- **Documentation**: Updated README.md to reflect new version 7.6.0 in installation examples

### Technical Notes
- The explicit conversions provide seamless integration between custom color structures and Godot's native color system
- `Color32` conversion uses the `Color.Color8` method which expects 8-bit integer color components (0-255)
- `ColorF` conversion directly uses floating-point color components (0.0-1.0) to construct Godot Color instances

## [7.5.1] - (27/11/2025)

### Fixed
- **Rect2D Point Detection**: Corrected `Rect2D.HasPoint` function that had incorrect pivot calculation leading to inaccurate point detection
- **Camera2D Extensions**: Fixed `ScreenToWorldPoint` and `WorldToScreenPoint` methods to properly account for camera offset in coordinate conversions
- **Control Extensions**: Corrected `GetRect2D`, `SetRect2D`, `GetGlobalRect2D`, and `SetGlobalRect2D` methods to properly handle `RectPivotOffset`
- **Sprite Extensions**: Fixed `GetRect2D` method to correctly calculate pivot when sprite is centered
- **Gizmos System**: Resolved color management issues in `InternalGizmos` by implementing color-targeted drawing system

### Changed
- **Gizmos Architecture**: Refactored `InternalGizmos` to use color-targeted drawing system with `ColorTarget` class for better color management
- **Coordinate Systems**: Improved coordinate transformation logic across all 2D extension methods

### Added
- **Testing**: Added comprehensive `Rect2D_Test` scene and test script for validating rectangle point detection and coordinate transformations
- **Debug Tools**: Enhanced gizmos drawing capabilities with persistent draw functions and improved color handling

### Technical Notes
- The pivot calculation fix ensures accurate point-in-rectangle detection for rotated and scaled rectangles
- Camera coordinate transformations now properly account for both position and offset properties
- Gizmos system now supports multiple colors in the same draw cycle without color conflicts
- Test scene includes interactive controls for validating sprite and control transformations

## [7.5.0] - (24/11/2025)

### Added
- **Sprite Extensions**: Added new extension methods `GetTextureSize` and `SetTextureSize` for `Sprite` class to manipulate sprite size
- **Texture Manipulation**: Implemented methods to get scaled texture size and set texture size by adjusting sprite scale
- **Vector2DInt Support**: Added support for `Vector2DInt` in sprite size manipulation methods

### Features
- **GetTextureSize**: Returns the scaled texture size of a Sprite, accounting for current scale
- **SetTextureSize**: Adjusts sprite scale to achieve desired texture size using `Vector2DInt`

### Technical Notes
- Methods include proper null checking and error handling with `ArgumentNullException`
- Returns `Vector2D.Zero` when texture is null for safe operation
- Uses vector division for precise scale calculation

## [7.4.4] - (24/11/2025)

### Fixed
- **Vector3D Methods**: Fixed `Vector3D.Abs` and `Vector3D.Neg` methods that were incorrectly using `Vector2D` internally, causing `ArgumentOutOfRangeException`
- **Type Consistency**: Corrected return types from `Vector2D` to `Vector3D` in vector operations

### Code Quality
- **Formatting**: Improved code region organization in `Vector2D` class
- **Version Update**: Bumped package version from 7.4.3 to 7.4.4

### Technical Notes
- The bug was causing index out of range exceptions when accessing the Z component
- Methods now properly handle three-dimensional vector operations
- Maintains conditional component modification for flexibility

## [7.4.3] - (23/11/2025)

### Fixed
- **Serialization**: Refactored `PropertyItem.PropertyItemArrayToGDArray` method to correctly convert PropertyItem arrays to Godot Collections.Array by properly serializing each item to dictionary format
- **Type Handling**: Added proper null checking and explicit iteration for array conversion instead of using collection expressions

### Changed
- **Type Aliases**: Added `GDArray` type alias for `Godot.Collections.Array` for cleaner code
- **Version Update**: Bumped package version from 7.4.2 to 7.4.3

## [7.4.2] - (23/11/2025)

### Fixed
- **Error Logging**: Restricted error logs to exceptions within the `try-catch` block only, or when the `ProjectPath` property is null
- **Return Values**: Corrected error logging methods to return appropriate success/failure indicators
- **Project Structure**: Removed redundant solution file and updated project references

### Changed
- **String Constants**: Converted `gameRuntime` and `code` fields from `readonly` to `const` for better performance
- **Build System**: Updated project output paths and assembly references for better organization

## [7.4.1] - (23/11/2025)

### Added
- **Error Handling**: Enhanced `GodotUtilityTask` with improved error logging methods for better build diagnostics
- **Logging Methods**: Added `LogError` and `LogMessage` helper methods for consistent error reporting

### Changed
- **Version Update**: Bumped package version from 7.4.0 to 7.4.1
- **Build System**: Updated build output paths from `lib/runtime` to `build/runtime` for better organization
- **Project Structure**: Reorganized project files and updated solution configurations
- **Message Constants**: Refactored log messages to use string constants for better maintainability

### Fixed
- **Error Reporting**: Fixed error logging in `GodotUtilityTask.Execute` method to properly report missing project files
- **Build Paths**: Corrected assembly reference paths in build targets and project configurations

### Technical Notes
- The improved error handling in `GodotUtilityTask` now properly detects and reports missing Godot project files during build
- Log messages are now centralized in constants, making them easier to maintain and translate
- Build system reorganization provides clearer separation between package content and build artifacts

## [7.4.0] - (21/11/2025)

### Added
- **MSBuild Task**: Added a new MSBuild Task for automatic generation of the `RunTimeInitialization` class in consumer projects
- **Automatic Runtime Setup**: New MSBuild target automatically creates `GameRuntime.cs` and configures Godot project autoload settings
- **Task Infrastructure**: Created separate `com.cobilas.godot.utility.Tasks` project for build-time automation

### Changed
- **Project Structure**: Reorganized project layout, moving License, README, and CHANGELOG files to project root
- **Build System**: Updated build targets and package references to support new MSBuild task architecture
- **File Organization**: Centralized project documentation and license files in root directory
- **Git Integration**: Updated .gitignore to exclude lib directories

### Removed
- **Legacy Build Tasks**: Removed old MSBuild task classes and code generation utilities
- **Test Environment Dependencies**: Eliminated test-specific runtime configurations from main package

### Technical Notes
- The new MSBuild task automatically generates a `GameRuntime.cs` file in the `Godot.Runtime` folder of consumer projects
- Automatic configuration of Godot's autoload system eliminates manual setup steps for runtime initialization
- Package now includes both runtime utilities and build-time automation in a single distribution

## [7.3.3] - (17/11/2025)

### Fixed
- **Path Root Detection**: Fixed `GodotPath.IGetPathRoot` internal method to properly handle non-Windows directory paths and prevent invalid path exceptions
- **Cross-Platform Compatibility**: Enhanced path root detection to work correctly across different operating systems

### Changed
- **Version Update**: Bumped package version from 7.3.2 to 7.3.3
- **Reference Paths**: Updated Godot assembly reference paths in project file

### Technical Notes
- The improved path detection now uses `Path.GetPathRoot` as fallback for non-standard path formats, providing better cross-platform compatibility
- Previously, the method only recognized Windows-style paths (`:\`) and Godot resource paths, causing exceptions on Linux and macOS systems

## [7.3.2] - (03/11/2025)

### Added
- **Serialization System**: Major refactor of the serialization system with new interfaces and classes
- **New Interfaces**: Added IObjectRender and IPropertyRender for property manipulation
- **New Classes**: Introduced NodeRenderer, ResourceRender, PropertyRender for inspector serialization
- **Caching System**: Implemented PropertyRenderCache for caching property values
- **Custom Properties**: Added support for NodePath and Rect2D custom property serialization
- **Project Configuration**: Added .csproj.user file for publish profile settings

### Changed
- **BuildSerialization API**: Refactored BuildSerialization class with new public methods (GetPropertyList, GetValue, SetValue)
- **Property Access**: Improved property access and manipulation with better error handling and standalone mode support
- **Version Update**: Bumped package version from 7.2.3 to 7.3.2

### Technical Notes
- The new serialization system provides a more flexible and robust way to handle property serialization in the Godot editor
- The caching system helps improve performance by storing and retrieving property values efficiently
- Property value changes now automatically propagate through the render hierarchy

## [7.2.3] - (31/10/2025)

### Fixed
- **ObjectRef Safety**: Enhanced ObjectRef<T>.Value property with null safety and proper path validation
- **Generic References**: Improved handling of empty paths in generic object references

### Changed
- **Version Update**: Bumped package version from 7.2.2 to 7.2.3

## [7.2.2] - (31/10/2025)

### Fixed
- **Node Path Resolution**: Corrected path handling in `NodePath_GD_CB_Extension.GetNode` method to properly resolve relative paths with "./" prefix
- **Generic ObjectRef**: Enhanced `ObjectRef<T>.Value` property with proper node resolution and path validation logic

### Changed
- **Version Update**: Bumped package version from 7.2.0 to 7.2.2

## [7.2.0] - (31/10/2025)

### Added
- **Object Reference System**: Introduced `ObjectRef` and `ObjectRef<T>` classes for better manipulation of Node objects in Godot 3 hierarchy
- **Automatic Path Resolution**: Implemented automatic node resolution with path caching and validation
- **Type-Safe References**: Added generic `ObjectRef<T>` for type-safe node references with implicit conversions
- **Serialization Support**: Created `ObjectRefCustom` property custom class for proper editor serialization

### Features
- **Implicit Conversions**: Support for implicit conversions between ObjectRef, NodePath, string, and Node types
- **Path Validation**: Automatic re-resolution when node paths change in the scene hierarchy
- **Editor Integration**: Full integration with Godot editor inspector for path assignment
- **Null Safety**: Proper handling of null nodes and paths with `NullNode` fallback

## [7.1.3] - (31/10/2025)

### Fixed
- **File Timestamps**: Corrected `GetLastWriteTime` and `GetLastWriteTimeUtc` properties in `ArchiveInfo` and `FolderInfo` classes that were incorrectly using access time methods
- **Concurrent File Access**: Modified `ArchiveStream` to use `FileShare.ReadWrite` allowing concurrent file access by different processes
- **Node Path Resolution**: Fixed "Node path not found" exception in `NodePath_GD_CB_Extension.GetNode` extension method
- **Godot Stream Management**: Enhanced `GodotArchiveStream` with improved file handling and resource management

### Technical Improvements
- **Stream Performance**: Optimized GodotArchiveStream to open files on-demand rather than keeping them persistently open
- **Memory Management**: Better resource disposal patterns in stream operations
- **Error Handling**: Improved exception handling and error reporting in file operations

## [7.1.1] - (29/10/2025)

### Changed
- **Build Configuration**: Replaced `GDFeature.HasRelease` with `GDFeature.HasStandalone` and `GDFeature.HasDebug` with `GDFeature.HasEditor` throughout the codebase
- **Cache Location**: Updated `SerializationCache` to use `ResPath` instead of `UserPath` for cache storage
- **Path Handling**: Modified `GodotPath.GlobalizePath` to use `GDFeature.HasEditor` instead of `GDFeature.HasDebug`
- **Screen Configuration**: Updated path handling in `Screen` class to use `GDFeature.HasStandalone`

### Version
- Bumped package version from 7.0.2 to 7.1.1

## [7.0.2] - (29/10/2025)

### Changed
- **Version Update**: Bumped package version from 7.0.1 to 7.0.2
- **Build Configuration**: 
  - Simplified package output path from `C:\local.nuget\$(Configuration)` to `C:\local.nuget`
- **Dependencies**: Updated `Cobilas.Core.Net4x` from version 2.7.1 to 2.7.2
- **Project Structure**: 
  - Updated .gitignore to simplified structure focusing on essential C# project folders and files

## [7.0.1] - 2025-10-29
### Fixed
- **Vector Operations**: Fixed `Vector2DInt.FloorToInt` and `CeilToInt` methods incorrectly using result components instead of input vector components
- **Vector Operations**: Fixed `Vector3DInt.FloorToInt` and `CeilToInt` methods incorrectly using result components instead of input vector components
- **Null Safety**: Added null checks in `Object_CB_GD_Extension.Print` method to prevent null reference exceptions
- **Collection Safety**: Added null-forgiving operator in coroutine managers to handle potential null arrays safely

### Added
- **XML Documentation**: Added comprehensive XML documentation for all IO classes and interfaces including:
  - `Archive` static class with detailed method descriptions
  - `ArchiveAttributes` enum with complete attribute descriptions
  - `ArchiveInfo` class with constructor exceptions and usage notes
  - `ArchiveStream` class with auto-flush capability
  - `DataNull` struct as null object pattern implementation
  - `Folder` static class with folder operation methods
  - `FolderInfo` class with folder management capabilities
  - `GodotArchiveStream` class for Godot-specific file operations
  - All interfaces in `Cobilas.GodotEngine.Utility.IO.Interfaces` namespace
  - `StreamType` enum for stream implementation selection
- **Auto-Flush**: Added `AutoFlush` property to `IStream` interface and implementations
- **Collection Initializers**: Updated `CustonResolutionList` to use modern collection expressions

### Changed
- **Project Configuration**: Updated .csproj to generate documentation, include symbols, and improve package metadata
- **Code Modernization**: Replaced `ToArray()` with spread operator `[.. enumerable]` for better performance
- **Stream Behavior**: Modified stream read operations to preserve position and reset to beginning when reading entire content
- **Enum Values**: Updated `ArchiveAttributes` to use proper hexadecimal values and standard .NET attributes

## [7.0.0] - 2025-10-28
### Added
- **New IO System**: Completely new file system abstraction supporting both Godot virtual file system and system file system
- **Stream Architecture**: New stream-based IO system with `IStream`, `IArchiveStream`, and `IGodotArchiveStream` interfaces
- **Folder Management**: New `IFolderInfo` interface for comprehensive folder operations
- **Archive Management**: New `IArchiveInfo` interface for file operations with copy/move functionality

### Changed
- **Serialization Cache**: Refactored `SerializationCache` to use new IO interfaces instead of legacy Archive/Folder classes
- **Scene Management**: Updated `InternalSceneManager` to use new folder enumeration system
- **Screen Resolution**: Modified `Screen` class to use stream-based configuration file reading
- **Build System**: Updated target frameworks and package references in project file

### Removed
- **Legacy IO Classes**: Removed `DataBase`, `FolderBuilder`, `FolderNode` and related legacy file system abstractions
- **Old Archive/Folder**: Removed previous `Archive` and `Folder` class implementations in favor of new interface-based system

### Fixed
- **File Operations**: Improved error handling and exception management in file operations
- **Path Handling**: Enhanced path normalization and Godot path root detection
- **Memory Management**: Better resource disposal patterns in stream and archive operations

## [6.3.1] (23/10/2025)
### Changed
- Bumped package version to 6.3.1 in the project file (`com.cobilas.godot.utility.csproj`).
- Packaging: enabled generation of symbol package (.snupkg) and portable PDBs (`IncludeSymbols`, `SymbolPackageFormat=snupkg`, `DebugType=portable`). `GeneratePackageOnBuild` now enabled only for Release builds.
- Moved folder-tree construction logic into a dedicated internal `FolderBuilder` class to centralize and simplify `Folder` creation.

### Added
- `ArchiveInfo` and `FolderInfo` structs implementing `IDataInfo` (provide timestamps, `IsInternal` and `IsGodotRoot`).
- `FolderBuilder` internal helper that creates `Folder` and `Archive` trees from both Godot resource paths (`res://`) and filesystem/user paths (`user://`).
- `Folder.SetDataList(Folder, DataBase[]?)` internal accessor to allow the builder to inject constructed children without exposing internals.

### Fixed
- `GodotPath.GetDirectoryName` now correctly preserves Godot special roots (`res://`, `user://`) and maps truncated results (`res:`, `user:`) to the proper prefixes.
- `Archive` buffer refresh logic: added `_LastWriteTime` tracking and checks to avoid unnecessary reloads; respects internal Godot resources and release/debug feature flags.
- `Folder` / `FolderNode`: improved initialization and enumeration of files/folders; added `GetFolders` and `GetFiles` enumerables.
- Extensive XML documentation added across the IO module; several members were marked nullable where appropriate (notably `IDataInfo?`).

### Breaking changes / compatibility notes
- `DataInfo` became nullable (`IDataInfo?`) in core types (`DataBase`, `Archive`, `Folder`, `FolderNode`). Update callers to guard against null (e.g. `if (data.DataInfo?.IsInternal ?? false)`).
- The project `DefineConstants` were adjusted in the csproj: note `RELEASE` is now present in some Debug definitions in this commit — verify if intentional.

### Migration tips
- Replace direct `DataInfo` access with null-aware checks or ensure `DataInfo` is assigned before using.
- Build packages in Release when publishing to ensure optimized assemblies and correct packaging of `.nupkg`/`.snupkg`.
- If you rely on old `GetDirectoryName` truncation behavior, update your code to use `GodotPath` helpers instead.

## [6.2.3] (15/10/2025)
### Fixed
**Identified Issue:**
The `GetDirectoryName` method exhibited incorrect behavior when processing special Godot paths, returning truncated values ​​such as "res:" and "user:" instead of the expected full paths.

**Technical Description:**
- **Affected Method:** `public static string GetDirectoryName(string path)`
- **Defect Scenario:** When receiving paths with Godot prefixes ("res://" and "user://")
- **Previous Behavior:** Returned "res:" and "user:" (incorrect truncation)
- **Expected Behavior:** Maintained the full prefixes of special paths

**Root Cause:**
The method directly used .NET's `Path.GetDirectoryName`, which does not recognize Godot-specific URL schemes, resulting in improper truncation of the prefixes.

**Implemented Solution:**
Replaced the direct implementation with an intelligent substitution system that:
- Preserves default behavior for conventional paths
- Specifically detects and handles "res:" and "user:" cases
- Maps to the appropriate constants (`res_path` and `user_path`)

**Impact of the Fix:**
- ✓ Correctly preserves Godot's special paths
- ✓ Maintains compatibility with conventional system paths
- ✓ Improved interoperability between file systems
- ✓ Prevents errors in subsequent operations that rely on full paths

**Area of ​​Impact:**
- Manipulation of project resources (res://)
- Access to user data (user://)
- File system operations that use Godot paths

## [6.2.2] (15/10/2025)
### Fixed
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

## [6.2.1] (08/10/2025)
### Fixed
- Made the `Gizmos.Color` property public again in the Gizmos class

### Changed
- Added modulus calculation methods to vector structures
- Fixed documentation comments for `Quaternion.Rad2Deg` and `Quaternion.Deg2Rad` constants (previously swapped)
- Marked `Rect2_GD_CB_Extension.Contains` as obsolete since it duplicates `Rect2.HasPoint(Vector2)` functionality

### Added
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

## [6.1.2] (06/10/2025)
### Fixed
The extension method `Camera2D_GD_CB_Extension.WorldToScreenPoint(this Camera2D, Vector2D)` had a convention problem where the method did not consider the zoom of the 2D camera.

## [6.1.1] (03/10/2025)
### Added
The `Node_GD_CB_Extension.SetParent(this Node?, Node?)` extension method was added to the Node class.

### Fixed
The `InputKeyBoard.DoubleClick` property had an issue with remaining true while the mouse button was held down because the property was not reset correctly.

### Changed
The `CoroutineManager` class has been converted to an internal class, and its methods such as `StartCoroutine(IEnumerator?)`, `StopCoroutine(Coroutine?)`, and `StopAllCoroutines()` can now be accessed by the Coroutine class. \
The `SceneManager`, `Gizmos`, `Physics2D`, and `InputKeyBoard` classes no longer inherit from the `Node` class and have become static classes. This was done so that these classes do not inherit methods or fields that derive from the Node class.

### Removed
Deprecated classes such as `GDDirectory`, `GDFile`, `GDFileBase`, and `GDIONull` have been removed. Use classes such as `Archive` and `Folder` to access folders and files.

## [5.1.1] (18/09/2025)
### Fixed
The `NodePath_GD_CB_Extension.GetNode(NodePath?)` extension method could encounter a 'path not found' issue because the extension method does not correctly handle relative paths.

## [5.1.0-ch.2;5.1.0] (24-26/08/2025)
### Changed
This push was focused on resolving possible null references and documenting classes or methods.

## [5.1.0-ch.1] (21/08/2025)
### Added
Two attributes, `ExportRangeAttribute` and `RunInEditorAttribute`, have been added.
Extensions for the `Godot.Object` and `Godot.Vector2` classes.
The methods `Node_GD_CB_Extension.Duplicate<T>(this Node, int) where T : Node`, `Node_GD_CB_Extension.ContainsNode(this Node, Node)`,
`Vector2_CB_GD_Extensions.Neg(this Vector2, bool, bool)`, and `Rect2_GD_CB_Extension.Contains(this Rect2, in Vector2D)` have been added to the extension classes.

#### ExportRangeAttribute
Allows integer or floating-point values ​​to take on a Range element format in the Godot inspector.

#### RunInEditorAttribute
This attribute has the same function as ToolAttribute.

### Changed
The `Node_GD_CB_Extension.Print(this Node, params object[])` method has been moved to the `Object_CB_GD_Extension` extension class.

## [5.0.0] (16/02/2025)
#### Added
- **`Folder` class:** Introduced to replace the `GDDirectory` class. The new `Folder` class offers optimized methods for manipulating directories and files within the Godot project, providing greater efficiency and usability.
- **`Archive` class:** Replaces the `GDFile` class, with improved functionality for file management, including reading and writing data.
- **`GodotPath` class:** Added to make path manipulation in Godot easier. `GodotPath` allows for path merging and verification operations.

#### Deprecate
- **`GDDirectory` class:** Declared obsolete and replaced by the new `Folder` class. All old methods are now implemented more efficiently in the `Folder` class.
- **`GDFile` class:** Marked as obsolete and replaced by the `Archive` class. The new `Archive` class inherits the functionality of `GDFile`, with significant improvements.

#### Removed
- **`GDFileAttributes` enum:** Removed from the package and replaced with the `ArchiveAttributes` enum, which offers a more comprehensive set of attributes and is better aligned with the new `Archive` class.

#### Changed
- **Relocation of serialization classes:** Classes such as `BuildSerialization`, responsible for property serialization, have been moved to the `Cobilas.GodotEditor.Utility.Serialization` namespace. This change better organizes the code and improves the modularity of the project.

## [4.8.1] (13/02/2025)
### Fixed
#### SerializationCache
There was a problem creating cache files due to the use of the `Node.GetPathTo(Node)` method, which was used incorrectly, resulting in the same cache being used in game and editor mode. \
The solution was to create two caches for each mode where the editor cache is copied to the player cache.

## [4.8.0] (13/02/2025)
### Added
#### PlayModeStateChange
The package now provides the `RunTimeInitialization.PlayModeStateChanged` event that allows you to check which mode the editor is in, whether the editor is entering or exiting game or editor mode.

## [4.7.1] (23/01/2025)
### Fixed
#### System.Number.ThrowOverflowOrFormatException
The `PrimitiveTypeCustom.CacheValueToObject(string?, string?)` method had a `System.Number.ThrowOverflowOrFormatException` issue caused by some extensions of the `String_CB_Extension` class.

#### Single cache
Now the cache has been separated for editor mode and player mode to prevent editor and simulation mode from changing the same cache file.

#### Extra cache check
An extra property name comparison check has been added to the `Get` and `Set` methods.

## [4.7.0] (23/01/2025)
### Added
Color structures such as `Color32` and `ColorF` have now been added. \
`Color32` represents an `rgba` value between 0 and 255. \
`ColorF` represents a normalized `rgba` value between 0 and 1f.

## [4.6.1] (23/01/2025)
### Fixed
The formatting of the `Vector4D` structure in the `ToString()` method where the `w` axis was not displayed, instead the `z` axis was displayed.

## [4.6.0] (19/01/2025)
### Added
#### Custom hint
Now `SerializeField` attributes in their parameters can receive `CustomHint`.

## [4.5.1] (19/01/2025)
### Fixed
#### Get members of a type
The `BuildSerialization.Build` method had the problem of searching for the members of the type but did not search for the members of the types inherited by the child class, which caused inherited members not to be serialized in the editor.

#### Converting json values
The `PropertyCustom` class in the `PropertyCustom.CacheValueToObject(string?, string?)` method may throw a `FormatException` because it does not handle an empty json value.

## [4.5.0] (16/01/2025)
### Changed
The `BuildSerialization.Build(Godot.Object)` method now accepts objects of type `Godot.Node` and `Godot.Resource`.

### Added
Now objects of type `Enum` have a `PropertyCustom` class. \
The `PropertyCustomAttribute` attribute allows the use of parent classes for child classes.

## [4.4.1] (13/01/2025)
### Fixed
The `SONull` and `SPCNull` classes had inherited members that returned `System.NotImplementedException` which caused problems when creating an instance of these classes.

## [4.4.0] (08/01/2025)
### Added
#### Custom serialization
Now a class has been added for custom serialization of properties in the Godot inspector. \
With the `HideProperty` and `ShowProperty` attributes you can serialize properties in the Godot inspector.

## [4.3.0] (26/12/2024)
### Added
#### DebugLog
The `DebugLog` class has been added to provide a way to output trace logs to the godot console.

## [4.2.7] (23/12/2024)
### Fixed
#### Invalid file path
The `GDDirectory.GetGDDirectory(string)` method was creating invalid paths for files and directories for representations which resulted in directory or file not found errors.

## [4.2.5] (18/11/2024)
### Fixed
#### Correction of MouseButton enum values
`MouseButton.MouseRight` now has the correct value of 2 (previously 1). \
`MouseButton.MouseLeft` now has the correct value of 1 (previously 2). \
This fix resolves the inversion in mouse trigger detection, ensuring that mouse click events are interpreted correctly.

## [4.2.4] (16/11/2024)
### Changed
#### Dependency update
The `Cobilas.Core.Net4x` dependency of the `Cobilas.Godot.Utility` package has been updated to version `2.0.1`.

## [4.2.3] (14/11/2024)
### Fixed
#### Incorrect resolution
The `Screen.CurrentResolution` property had the problem of returning the full screen resolution without considering other screen modes.
#### Problems with vector and quaternion interpolation
All vector and quaternion structures had a problem of `ArgumentNullException` when inserted into an interpolated string. \
This could occur when the `format` parameter of the `IFormattable.ToString(string, IFormatProvider)` will be null.

## [4.2.1] (14/11/2024)
### Fixed
#### NullReferenceException in Screen class
Private `Screen.AddResolution` function always emitted a `NullReferenceException` because it does not correct the parameter if it is null and void or not.

## [4.2.0] (12/11/2024)
### Added
#### Vector inversion operators
Now the `Vector2D`, `Vector2DInt`, `Vector3D`, `Vector3DInt` and `Vector4D` structures have received values inversion operators.

#### Implicit operator
The `Quaternion` structure received an implicit operator from `Quaternion` to `Vector4D`.

## [4.1.2] (12/11/2024)
### Fixed
#### Deltascroll always positive
The `Inputkeyboard.Deltascroll` property always returned positive values by misunderstanding inputs reeiers

## [4.1.1] (11/11/2024)
### Fixed
#### NullReferenceException in Gizmos._Process(float)
The method uses a private variable to update the drawing method but the `Gizm._Process(float)` method did not verify if variable was null.

## [4.1.0] (29/10/2024 - 04/11/2024)
### Added
#### IYieldCoroutine.IsLastCoroutine property
This property was added to indicate whether the coroutine will be executed after all update methods.

#### LastFixedRunTimeSecond class
This class was added to signal that the coroutine will be executed after all `_PhysicsProcess(float)` update methods.

#### LastRunTimeSecond class
This class was added to signal that the coroutine will be executed after all `_Process(float)` update methods.

#### NullNode class
This class gives a representation of a null node.

#### Vector2D operator
Added addition, subtraction, division and multiplication operators between `Vector2D` and `Godot.Vector2`.

#### Vector3D operator
Added addition, subtraction, division and multiplication operators between `Vector3D` and `Godot.Vector3`.

#### Scene
##### properties
The `Scene.SceneNode` property was added to obtain the scene in node form. \
The `Scene.Empty` property was created to give an empty representation of a scene.

#### SceneManager.BuiltScenes property
This property's function is to reveal all the scenes that were compiled together with the project and that were in the <kbd>res://Scenes/</kbd> folder.

### Changed
All constructors of the `RunTimeInitializationClassAttribute` class have been replaced by the `RunTimeInitializationClassAttribute(string?, [Priority:Priority.StartBefore], [int:0], [bool:false])` constructor

### Removed
#### KeyItem and MouseItem
These structures have been removed as they are being replaced by the `PeripheralItem` structure.

#### RunTimeInitialization constants
The constants `RunTimeInitialization.DeltaTime` and `RunTimeInitialization.FixedDeltaTime` have been removed as they have been replaced by the constants `RunTime.DeltaTime` and `RunTime.FixedDeltaTime`.

## [3.2.0] (17/10/2024 - 27/10/2024)
### Added
#### GDDirectory.GetDirectories()
The `GDDirectory.GetDirectories()` method has been added. \
This method allows you to get the subfolders within the current folder.

#### Overloading methods of the GDFile class.
The `GDFile.Load(string, bool)` and `GDFile.Load<T>(string, bool)` methods have been added to the `GDFile` method.

#### properties for the GDIONull classes.
The `GDIONull.FileNull` and `GDIONull.DirectoryNull` properties have been added to the `GDIONull` class. \
These properties return a null representation of a file or directory.

#### KeyCode enum
The `KeyCode` enum was added to extend the `KeyList` enum by adding mouse buttons.

#### PeripheralItem struct
The `PeripheralItem` struct has been added to hold certain keyboard or mouse event information.

#### InputKeyBoard and method overloading
`InputKeyBoard` has built-in methods `InputKeyBoard.GetKeyDown(KeyCode)`, `InputKeyBoard.GetKeyUp(KeyCode)` and `InputKeyBoard.GetKeyPress(KeyCode)` that take `KeyCode` as a parameter.

### Removed
The `InputKeyBoard._PhysicsProcess(float)` method has been removed as it is no longer needed.

### Changed
Now the `KeyStatus` enum has the `System.Flags` attribute.

### Deprecate
The `MouseItem` and `KeyItem` structures have been replaced by the `PeripheralItem` structure.

## [3.1.1] (13/10/2024)
### Added
#### Vector4D Comparison Operator
Equality and difference operators have been added to the Vector4D structure.

## [3.0.0] (12/10/2024)
### Removed
#### IIntVectorGeneric interface
The interface was removed as there was no need to contain non-static functions used to convert floating point vectors.

#### IIntVector.floorToInt
The property was removed as there was no need for it.

#### IIntVector.ceilToInt
The property was removed as there was no need for it.

#### IIntVector.RoundToInt()
The method was removed as there was no need for it.

## [2.0.0] (06/10/2024-12/10/2024)
### Added
#### Screen.OrphanList
The `Screen.OrphanList` property has been added to display a list of custom resolutions not tied to a screen.

#### RayHit2D.IsValid(Dictionary?)
The `RayHit2D.IsValid(Dictionary?)` method has been added to check whether an object of type `Godot.Collections.Dictionary` is valid for conversion.

#### RayHit2D.IsValid(Dictionary?)
The `Hit2D.IsValid(Dictionary?)` method has been added to check whether an object of type `Godot.Collections.Dictionary` is valid for conversion.

### Changed
#### Screen.Resolutions
The `Screen.Resolutions` property now returns an array of type `Resolution` which makes it incompatible with previous versions of the package.

#### Screen.CurrentResolution
The `Screen.CurrentResolution` property now returns a value of type `Resolution` which makes it incompatible with previous versions of the package.

#### Scene struct
The `Scene` struct is implementing the comparison interfaces `IEquatable<Scene>, IEquatable<int>, IEquatable<string>`. \
The `Scene` struct implements the comparison operators and also implements the explicit conversion operators <kbd>Scene -> int</kbd> and <kbd>Scene -> string</kbd>.

## [1.8.0] (03/10/2024-06/10/2024)
### Added
#### RunTime.TimeScale
The `RunTime.TimeScale` property has been added to track in-game time.

#### RunTime.FrameCount
The `RunTime.FrameCount` property has been added to show how many frames have been displayed in-game.

#### Screen.ScreenRefreshRate
The `Screen.ScreenRefreshRate` property has been added to show the screen refresh rate.

#### Screen.Displays
The `Screen.Displays` property has been added to display a list of connected displays on the PC.

#### Screen.CurrentDisplay
The `Screen.CurrentDisplay` property has been added to display information about the current screen.

#### Screen.DisplayCount
The `Screen.DisplayCount` property has been added to display the number of screens connected to the PC.

#### CustonResolutionList struct
The `CustonResolutionList` struct has been added to store a list of custom resolutions for a screen.

#### DisplayInfo struct
The `DisplayInfo` struct has been added to store screen information.

#### Resolution struct
The `Resolution` struct has been added to store resolution information.

## [1.7.0] (29/09/2024)
### Added
New `RunTime` structure
#### Details
The `RunTime` structure has been added to provide runtime values ​​and functions.

### Changed
Changes in `RunTimeInitialization`.
#### Details
Form changes to the `RunTimeInitialization` class such as placing the `PriorityList` structure outside the `RunTimeInitialization` class, allowing `RunTimeInitialization` to accept negative values ​​as priority. \
The lower the priority value, the higher its execution level will be.

## [1.6.0] (26/09/2024)
### Added
New structures and methods.
#### Details
New vector structures like `Vector2Int` and `Vector3Int` have been added. \
The `Vector2D`, `Vector3D` and `Vector4D` structures received the `Neg` Method to negate the vector axes and the `Round` method.

## [1.5.3] (24/09/2024)
### Fixed
`Randomico.value` is now a property.
#### Details
Since `Randomico.value` was of field type, this made the return of pseudorandom methods static.

## [1.5.2] (17/09/2024)
### Fixed
`StackOverflowException` in `Vector2D.Magnitude(in Vector2D)`
#### Details
The `Vector2D.Magnitude(in Vector2D)` method raised `StackOverflowException` by calling itself.

## [1.5.1] (12/09/2024)
### Add
Vector Structure.
#### Details
Vector structures such as 2d, 3d, 4d, and quaternion vectors have been added.

### Fixed
`ArgumentNullException` in `GDDirectory.GetGDDirectory()` method.
#### Details
The `GDDirectory.GetGDDirectory()` method always raised an `ArgumentNullException` due to the fact that the `GDDirectory.GetGDDirectory()` method uses the private `GDDirectory.GetGDDirectory(string, GDFileBase)` method, which received a null value in the parent parameter, but that, as of version `1.4.0`, the parent parameter of the GDDirectory class constructor is now checked if it is null, which caused the `GDDirectory.GetGDDirectory()` method to break.

## [1.4.0] (04/08/2024)
### Add
The `Randomico` and `GDIONull` classes.
#### Details
The `Randomico` class was added for pseudorandom value generation. \
The `GDIONull` class was created to represent a null `GDFileBase` object.

## [1.3.0] (07/07/2024)
### Add
The static class `GDFeature` was added to address the lack of specific <kbd>preprocessing definitions</kbd>.

## [1.2.2] (06/07/2024)
### Changed
Dependency `Cobilas.Core.Net4x@1.1.0` has been updated to version `1.6.1`.
### Fixed
Method not found
#### Details
The `Method not found` exception was caused by the `Cobilas.Core.Net4x` dependency not being updated.

## [30/12/2023]##1.1.1
### Fixed
Methods that use the private method `Array CreateExclude(CollisionObject2D[]);` had the null reference problem.
This occurred when the `Array CreateExclude(CollisionObject2D[]);` method received an empty list which caused the method to return a null list of type `Godot.Collections.Array`.
## Added
The CHANGELOG.md file has been added.
### Changed
The com.cobilas.godot.utility.csproj file has been changed.
## [31/12/2023]##1.1.2
### Fixed
O An exception of type `InvalidCastexception` in the explicit operator of the `Hit2D` structure where the `Collision` property could receive an object of another type than `CollisionObject2D` which would inevitably cause `InvalidCastexception`.
The explicit operator of the `RayHit2D` structure was also changed to avoid the same problem as the explicit operator of the `Hit2D` structure.
## [31/12/2023]##1.2.0
### Added
The `Gizmos` class has been added.
The `Gizmos` class allows you to use drawing functions statically.
## [01/01/2024]##1.2.1
### Fixed
In the static constructor of the `Screen` class it only obtained the resolutions saved in the <kbd>res://AddResolution.json</kbd> file but with the correction the <kbd>projectrootfolder://AddResolution.json</kbd> file in the root folder of the already compiled project is also obtained.