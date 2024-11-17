# [Cobilas Godot Utility](https://belicusbr.github.io/com.cobilas.docs/mds/gd-utility-getting-started.html)
### Descripition
The package contains utility classes in csharp for godot engine(Godot3.5)
## RunTimeInitialization
(namespace: Cobilas.GodotEngine.Utility.Runtime) \
The `RunTimeInitialization` class allows you to automate the <kbd>Project&gt;Project Settings&gt;AutoLoad</kbd> option. \
To use the `RunTimeInitialization` class, you must create a class and make it inherit `RunTimeInitialization`.
```c#
using Cobilas.GodotEngine.Utility.Runtime;
//The name of the class is up to you.
public class RunTimeProcess : RunTimeInitialization {}
```
And remember to add the class that inherits `RunTimeInitialization` in <kbd>Project&gt;Project Settings&gt;AutoLoad</kbd>. \
Remembering that the `RunTimeInitialization` class uses the virtual method `_Ready()` to perform the initialization of other classes. \
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
//RunTimeInitializationClassAttribute(string? name, Priority bootPriority = Priority.StartBefore, int subPriority = 0, bool lastBoot = false)
[RunTimeInitializationClassAttribute(string?, [Priority:Priority.StartBefore], [int:0], [bool:false])]
[RunTimeInitializationClass()]
```
## CoroutineManager
The `CoroutineManager` class is responsible for creating and managing coroutines for godot. \
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
### IYield Classes
- RunTimeSecond is a framework that allows you to delay your coroutine in seconds. This class inherits `IYieldUpdate`.
- FixedRunTimeSecond is a framework that allows you to delay your coroutine in seconds. This class inherits `IYieldFixedUpdate`.
- IYieldUpdate is an interface that allows the coroutine to run in the `_Process(float)` function.
- IYieldFixedUpdate is an interface that allows the coroutine to run in the `_PhysicsProcess(float)` function.
- IYieldVolatile is an interface that allows the coroutine to run in the `Process(float)` or `_PhysicsProcess(float)` function.
- IYieldCoroutine is the base interface for Yield interfaces.
### Stop coroutines
Now to stop a coroutine.
```c#
public static void StopCoroutine(Coroutine Coroutine);
public static void StopAllCoroutines();
```
## Other classes
`InputKeyBoard` `Physics2D` `SceneManager` `GDDirectory` `Gizmos`

## The [Cobilas Godot Utility](https://www.nuget.org/packages/Cobilas.Godot.Utility/) is on nuget.org
To include the package, open the `.csproj` file and add it.
```xml
<ItemGroup>
  <PackageReference Include="Cobilas.Godot.Utility" Version="4.2.4" />
</ItemGroup>
```
Or use command line.
```
dotnet add package Cobilas.Godot.Utility --version 4.2.4
```
