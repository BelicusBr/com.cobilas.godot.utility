using System;
using System.Collections;

namespace Cobilas.GodotEngine.Utility;

internal sealed class CoroutineItem {
    private bool init;
    private DateTime time;
    private readonly Coroutine coroutine;
    public string ID => coroutine.ID;
    public bool IsPhysicsProcess {
        get {
            object obj = (coroutine as IEnumerable).GetEnumerator().Current;
            return obj is IYieldFixedUpdate || (obj is IYieldVolatile @volatile && @volatile.IsPhysicsProcess);
        }
    }

    public CoroutineItem(Coroutine coroutine) {
        this.coroutine = coroutine;
        time = DateTime.Now;
    }

    public void Cancel()
        => coroutine.Cancel();
        
    public bool Run() {
        if (coroutine.IsCancellationRequested) {
            coroutine.SetStatus(false);
            return coroutine.IsRunning;
        }
        bool res = true;
        IEnumerator enumerator = (coroutine as IEnumerable).GetEnumerator();
        TimeSpan delay = enumerator.Current is not IYieldCoroutine wait ? TimeSpan.Zero : wait.Delay;
        if (!init) {
            res = enumerator.MoveNext();
            init = true;
        } else if (DateTime.Now > time + delay)
            if (res = enumerator.MoveNext())
                time = DateTime.Now;
        coroutine.SetStatus(res);
        return res;
    }
}