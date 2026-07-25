using System;
using System.Threading;
using System.Threading.Tasks;

using DeadsetAutoClicker.DeadsetAutoClicker.Core.Input;

namespace DeadsetAutoClicker.App.Services;

public sealed class AutoClickingService
{
    private CancellationTokenSource? _CancellationTokenSource;

    public bool IsRunning => _CancellationTokenSource is not null;

    /// <summary>
    /// Clicks until <paramref name="repeatCount"/> is reached (or forever when
    /// <paramref name="repeatInfinitely"/>), waiting <paramref name="delay"/> before each click.
    /// Completes when the run finishes or <see cref="Stop"/> is called.
    /// </summary>
    public async Task StartAsync(ClickType clickType, ClickAmount clickAmount, TimeSpan delay, bool repeatInfinitely, int repeatCount)
    {
        Stop();

        CancellationTokenSource cancellationTokenSource = new();
        _CancellationTokenSource = cancellationTokenSource;
        CancellationToken cancellationToken = cancellationTokenSource.Token;

        try
        {
            for (int executed = 0; repeatInfinitely || executed < repeatCount; executed++)
            {
                await Task.Delay(delay, cancellationToken);
                await Clicker.PerformClickAsync(clickType, clickAmount, cancellationToken);
            }
        }
        catch (OperationCanceledException)
        {
            // Stop() was called - a normal way to end a run.
        }
        finally
        {
            if (_CancellationTokenSource == cancellationTokenSource)
            {
                _CancellationTokenSource = null;
            }

            cancellationTokenSource.Dispose();
        }
    }

    public void Stop() => _CancellationTokenSource?.Cancel();
}
