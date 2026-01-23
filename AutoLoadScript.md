# AutoLoadScript Plugin for Godot

**AutoLoadScript** is a powerful C# plugin for the Godot engine that enables dynamic script loading through code using the `AutoLoadScript` attribute. \
This plugin automates the process of converting C# classes into Godot's auto-load singletons, streamlining project organization and initialization workflows.

## Key Features

- **Attribute-Based Auto-Loading**: Automatically registers classes marked with `[AutoLoadScript]` as Godot auto-load nodes
- **Configurable Naming**: Generates project files with the `Auto_` prefix or a custom name specified in the attribute
- **Priority Management**: Control load order through configurable priority values
- **Non-Conflict Design**: Prevents naming collisions by enforcing distinct auto-load names

## Usage Example

```csharp
using Godot;

namespace godot.test;
[AutoLoadScript(priority: 0, autoLoadName: "AutoTest")]
public class AutoLoadTest : Node {
    // All code resides here
}
```

## Pre-Configured Auto-Load Scripts

The package includes several pre-configured auto-load scripts with optimized load orders:

| Script Name | Priority | Auto-Load Name |
|-------------|----------|----------------|
| InternalInputKeyBoard | 0 | (class name) |
| InternalCoroutineManager | 1 | (class name) |
| InternalPhysics2D | 2 | (class name) |
| RunTimeInitialization | 3 | GameRuntime |
| InternalSceneManager | 4 | (class name) |
| InternalGizmos | 5 | (class name) |
| LastRunTimeInitialization | 6 | (class name) |
| LastCoroutineManager | 7 | (class name) |
| GCInputKeyBoard | 8 | (class name) |

# AutoLoadOrderReplace: Priority Override System

The **AutoLoadOrderReplace** class provides advanced control over auto-load priorities, \
allowing developers to override the default load order of specific classes.

## Key Benefits

- **Custom Priority Mapping**: Redefine load orders for specific auto-load scripts
- **Backward Compatibility**: Maintains original priorities for classes not explicitly overridden
- **Type-Safe Configuration**: Utilizes strongly-typed KeyValuePair collections

## Configuration Example

```csharp
public sealed class NewPriority : AutoLoadOrderReplace {
    public override KeyValuePair<OrderValue, Type>[] AutoLoadList => new KeyValuePair<OrderValue, Type>[] {
        new(0, typeof(InternalCoroutineManager)),
        new(1, typeof(InternalPhysics2D)),
        new(2, typeof(InternalInputKeyBoard)),
    };
}
```

## Technical Advantages

- **Seamless Integration**: Works natively with Godot's auto-load system
- **Code-First Approach**: Manage auto-load configuration directly in C# code
- **Runtime Flexibility**: Modify load orders without restructuring project files
- **Debug-Friendly**: Clear priority assignment facilitates troubleshooting load-order dependencies

This plugin is ideal for Godot developers seeking to implement scalable, \
maintainable initialization systems for complex projects, particularly those requiring precise \
control over component load order and dependency management.