using System;
using System.Threading;
using System.Collections;

namespace Cobilas.GodotEngine.Utility; 

public sealed class Coroutine : IEnumerable, IDisposable {
    private bool disposedValue;
    private IEnumerator enumerator;
    private CancellationTokenSource source = new();

    public string ID { get; private set; }
    public bool IsRunning { get; private set; }
    public bool IsCancellationRequested => source!.IsCancellationRequested;

    public Coroutine(IEnumerator enumerator, string iD) {
        this.enumerator = enumerator;
        ID = iD;
    }

    ~Coroutine()
        => Dispose(disposing: false);

    public void Cancel()
        => this.source.Cancel();

    public void CancelAfter(TimeSpan delay)
        => this.source.CancelAfter(delay);

    public void CancelAfter(int millisecondsDelay)
        => this.source.CancelAfter(millisecondsDelay);

    /// <inheritdoc/>
    public void Dispose() {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    internal void SetStatus(bool status) 
        => IsRunning = status;

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
        => this.enumerator;

    private void Dispose(bool disposing) {
        if (!disposedValue) {
            if (disposing) {
                enumerator = null!;
                ID = null!;
                source.Dispose();
                source = null!;
            }
            disposedValue = true;
        }
    }
}