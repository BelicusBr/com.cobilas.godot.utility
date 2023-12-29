# Cobilas Godot Utility
### Descripition
The package contains utility classes in csharp for godot engine(Godot3.5)
## RunTimeInitialization
(namespace: Cobilas.GodotEngine.Utility.Runtime)<br>
The `RunTimeInitialization` class allows you to automate the <kbd>Project&gt;Project Settings&gt;AutoLoad</kbd> option.<br>
To use the `RunTimeInitialization` class, you must create a class and make it inherit `RunTimeInitialization`.
```c#
using Cobilas.GodotEngine.Utility.Runtime;
//The name of the class is up to you.
public class RunTimeProcess : RunTimeInitialization {}
```
And remember to add the class that inherits `RunTimeInitialization` in <kbd>Project&gt;Project Settings&gt;AutoLoad</kbd>.<br>
Remembering that the `RunTimeInitialization` class uses the virtual method `_Ready()` to perform the initialization of other classes.<br>
And to initialize other classes along with the `RunTimeInitialization` class, the class must inherit the `Godot.Node` class or some class that inherits `Godot.Node` and use the `RunTimeInitializationClassAttribute` attribute.
```c#
using Godot;
using Cobilas.GodotEngine.Utility.Runtime;
[RunTimeInitializationClass]
public class ClassTest : Node {}
```
### RunTimeInitializationClass
```c#
/*
bootPriority: Represents the boot order
{ (enum Priority)values
        StartBefore,
        StartLater
}
name:The name of the object
subPriority: And the execution priority order.
*/
[RunTimeInitializationClass(Priority bootPriority, string name, int subPriority)]
[RunTimeInitializationClass(Priority bootPriority)]
[RunTimeInitializationClass(Priority bootPriority, string name)]
[RunTimeInitializationClass(string name, int subPriority)]
[RunTimeInitializationClass(string name)]
[RunTimeInitializationClass()]
```
## CoroutineManager
The `CoroutineManager` class is responsible for creating and managing coroutines for godot.<br>
How to create a coroutine?
```c#
using Godot;
using System.Collections;
using Cobilas.GodotEngine.Utility;

public class ClassTest : Node {
  private Coroutine coroutine;
  public override void _Ready() {
    coroutine = CoroutineManager.StartCoroutine(Corroutine1());
    coroutine = CoroutineManager.StartCoroutine(Corroutine2());
    coroutine = CoroutineManager.StartCoroutine(Corroutine3());
  }

  private IEnumerator Corroutine1() {
    GD.Print("Zé da manga");
    //When the return is null, by default the coroutine is executed as _Process().
    yield return null;
  }

  private IEnumerator Corroutine2() {
    GD.Print("Zé da manga");
    //When the return is RunTimeSecond the coroutine is executed as _Process() with a pre-defined delay.
    yield return new RunTimeSecond(3);
  }

  private IEnumerator Corroutine3() {
    GD.Print("Zé da manga");
    When the return is RunTimeSecond the coroutine is executed as _PhysicProcess() with a pre-defined delay.
    yield return new FixedRunTimeSecond(3);
  }
}
```
With the `IYieldVolatile` interface you can switch coroutine execution between `_Process(float)` and `_PhysicsProcess(float)`.
### Stop coroutines
Now to stop a coroutine.
```c#
public static void StopCoroutine(Coroutine Coroutine);
public static void StopAllCoroutines();
```
## Other classes
`InputKeyBoard` `Physics2D` `SceneManager` `GDDirectory`

## The [Cobilas Godot Utility](https://www.nuget.org/packages/Cobilas.Godot.Utility/) is on nuget.org
To include the package, open the `.csproj` file and add it.
```xml
<ItemGroup>
  <PackageReference Include="Cobilas.Godot.Utility" Version="1.1.0" />
</ItemGroup>
```
Or use command line.
```
dotnet add package Cobilas.Godot.Utility --version 1.1.0
```
