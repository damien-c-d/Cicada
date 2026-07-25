using System.Diagnostics;
using System.Runtime.InteropServices;

using DeadsetAutoClicker.DeadsetAutoClicker.Core.Interop;

namespace DeadsetAutoClicker.DeadsetAutoClicker.Core.Input;

public static class Clicker
{
    // Gap between the clicks of a double/triple click.
    private const int ClickIntervalMs = 50;

    public static async Task PerformClickAsync(ClickType clickType, ClickAmount clickAmount, CancellationToken cancellationToken = default)
    {
        Input[] inputs = NativeMethods.GetInputs(clickType);

        int clicks = clickAmount switch
        {
            ClickAmount.Single => 1,
            ClickAmount.Double => 2,
            ClickAmount.Triple => 3,
            _ => throw new UnreachableException(),
        };

        for (int i = 0; i < clicks; i++)
        {
            if (i > 0)
            {
                await Task.Delay(ClickIntervalMs, cancellationToken);
            }

            uint sent = NativeMethods.SendInput((uint)inputs.Length, inputs, Marshal.SizeOf<Input>());
            Debug.WriteLineIf(sent != inputs.Length, $"SendInput sent {sent}/{inputs.Length} events (error {Marshal.GetLastWin32Error()}).");
        }
    }
}
