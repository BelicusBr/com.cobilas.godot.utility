# ChangeLog
## [30/12/2023]#1.1.1
- ### Fixed
Methods that use the private method `Array CreateExclude(CollisionObject2D[]);` had the null reference problem.
This occurred when the `Array CreateExclude(CollisionObject2D[]);` method received an empty list which caused the method to return a null list of type `Godot.Collections.Array`.
- ### Added
The CHANGELOG.md file has been added.
- ### Changed
The com.cobilas.godot.utility.csproj file has been changed.