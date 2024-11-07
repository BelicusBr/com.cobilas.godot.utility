using System;
using System.Threading;
using System.Collections;

namespace Cobilas.GodotEngine.Utility; 
/// <summary>This class represents a corrotine process.</summary>
public sealed class Coroutine : IEnumerable, IDisposable {
    private bool disposedValue;
    private IEnumerator enumerator;
    private CancellationTokenSource source = new();
    /// <summary>The process id.</summary>
    /// <returns>Returns a <seealso cref="string"/> with process ID.</returns>
    public string ID { get; private set; }
    /// <summary>Indicates if the process is running.</summary>
    /// <returns>
    /// <para>Returns <c>true</c> if the process is ongoing.</para>
    /// <para>Returns <c>false</c> when the process is completed.</para>
    /// </returns>
    public bool IsRunning { get; private set; }
    /// <inheritdoc cref="CancellationTokenSource.IsCancellationRequested"/>
    public bool IsCancellationRequested => source!.IsCancellationRequested;
    /// <summary>Creates a new instance of this object.</summary>
    /// <param name="enumerator">The <seealso cref="IEnumerator"/> object that will be used.</param>
    /// <param name="iD">The ID of this <seealso cref="Coroutine"/> object.</param>
    /// <exception cref="ArgumentNullException">It occurs when the enumerator parameter is null.</exception>
    /// <exception cref="ArgumentNullException">It occurs when the ID parameter is null.</exception>
    public Coroutine(IEnumerator? enumerator, string? iD) {
        if (enumerator is null) throw new ArgumentNullException(nameof(enumerator));
        if (iD is null) throw new ArgumentNullException(nameof(iD));
        this.enumerator = enumerator;
        ID = iD;
    }
    /// <summary>The destructor is responsible for discarding unmanaged resources.</summary>
    ~Coroutine()
        => Dispose(disposing: false);
    /// <summary>Emits the cancellation signal for the <seealso cref="Coroutine"/> process.</summary>
    /// <inheritdoc cref="CancellationTokenSource.Cancel()"/>
    public void Cancel()
        => this.source.Cancel();
    /// <summary>Emits a delayed cancellation signal for the <seealso cref="Coroutine"/> process.</summary>
    /// <param name="delay">Defines the delay where the cancellation signal will be issued.</param>
    /// <inheritdoc cref="CancellationTokenSource.CancelAfter(TimeSpan)"/>
    public void CancelAfter(TimeSpan delay)
        => this.source.CancelAfter(delay);
    /// <inheritdoc cref="CancelAfter(TimeSpan)"/>
    /// <param name="millisecondsDelay">Defines the delay in milliseconds where the cancellation signal will be issued.</param>
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
            source?.Dispose();
            if (disposing) {
                enumerator = null!;
                ID = string.Empty;
                source = null!;
            }
            disposedValue = true;
        }
    }
}