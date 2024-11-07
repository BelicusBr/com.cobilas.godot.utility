using Cobilas.GodotEngine.Utility.Numerics;

namespace Cobilas.GodotEngine.Utility.Input;

internal record struct MouseInfo(
    int mouseIndex,
    bool doubleClick,
    float deltaScroll,
    Vector2D mousePosition,
    Vector2D mouseGlobalPosition,
    ChangeState changeState
    );
