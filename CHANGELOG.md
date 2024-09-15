# [1.5.1] (12/09/2024)
## Add
Vector Structure.
### Detalhes
Vector structures such as 2d, 3d, 4d, and quaternion vectors have been added.

## Fixed
`ArgumentNullException` in `GDDirectory.GetGDDirectory()` method.
### Detalhes
The `GDDirectory.GetGDDirectory()` method always raised an `ArgumentNullException` due to the fact that the `GDDirectory.GetGDDirectory()` method uses the private `GDDirectory.GetGDDirectory(string, GDFileBase)` method, which received a null value in the parent parameter, but that, as of version `1.4.0`, the parent parameter of the GDDirectory class constructor is now checked if it is null, which caused the `GDDirectory.GetGDDirectory()` method to break.

# [1.4.0] (04/08/2024)
## Add
The `Randomico` and `GDIONull` classes.
### Detalhes
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
### Detalhes
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