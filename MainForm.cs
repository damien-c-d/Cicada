using System.Diagnostics;
using System.Runtime.InteropServices;
using DeadsetAutoClicker.DeadsetAutoClicker.Core.Input;
using DeadsetAutoClicker.DeadsetAutoClicker.Core.Interop;

namespace DeadsetAutoClicker;
public partial class MainForm : Form
{
    private bool _Started;
    private CancellationTokenSource _CancellationTokenSource;


    // Define hotkey IDs
    private const int CTRL_F12_HOTKEY = 1;
    private const int ALT_F12_HOTKEY = 2;

    public bool Started
    {
        get => _Started; private set
        {
            _Started = value;
            SetButtonsEnabled();
        }
    }

    public ClickType ClickType
    {
        get
        {
            if (LeftClickRadioButton.Checked)
            {
                return ClickType.Left;
            }
            else if (RightClickRadioButton.Checked)
            {
                return ClickType.Right;
            }
            else
            {
                return ClickType.Middle;
            }
        }
    }

    public RepeatType RepeatType
    {
        get
        {
            if (InfinitelyRadioButton.Checked)
            {
                return RepeatType.Infinite;
            }
            else
            {
                return RepeatType.SetCount;
            }
        }
    }

    public ClickAmount ClickAmount
    {
        get
        {
            if (SingleClickRadioButton.Checked)
            {
                return ClickAmount.Single;
            }
            else if (DoubleClickRadioButton.Checked)
            {
                return ClickAmount.Double;
            }
            else
            {
                return ClickAmount.Triple;
            }
        }
    }

    public int RepeatCount
    {
        get => (int)RepeatCountUpDown.Value;
    }

    public int Delay
    {
        get => (int)DelayUpDown.Value;
    }

    private void SetButtonsEnabled()
    {
        StartButton.Enabled = !Started;
        StopButton.Enabled = Started;
    }

    public MainForm()
    {
        InitializeComponent();
        SetDefaults();
        SetRepeatCountEnabled();

        // Register your hotkeys
        NativeMethods.RegisterHotKey(this.Handle, CTRL_F12_HOTKEY, 0x0002, (uint)Keys.F12); //Ctrl + F12
        NativeMethods.RegisterHotKey(this.Handle, ALT_F12_HOTKEY, 0x0001, (uint)Keys.F12); //Alt + F12

    }

    private void SetDefaults()
    {
        Started = false;
        LeftClickRadioButton.Checked = true;
        InfinitelyRadioButton.Checked = true;
        SingleClickRadioButton.Checked = true;
    }

    private void RepeatSetCountRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        SetRepeatCountEnabled();
    }

    private void SetRepeatCountEnabled()
    {
        RepeatCountUpDown.Enabled = RepeatSetCountRadioButton.Checked;
    }

    public static void PerformClick(ClickType clickType, ClickAmount clickAmount)
    {
        // Get the inputs
        Input[] inputs = GetInputs(clickType);

        uint result;

        // Send the mouse single click event
        if (clickAmount == ClickAmount.Single)
        {
            result = NativeMethods.SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
            Debug.WriteLine($"Single-Click Events Successful: {result}");
        }
        // Send the mouse double click event
        else if (clickAmount == ClickAmount.Double)
        {
            result = NativeMethods.SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
            Thread.Sleep(50);
            result += NativeMethods.SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
            Debug.WriteLine($"Double-Click Events Successful: {result}");
        }
        // Send the mouse triple click event
        else if (clickAmount == ClickAmount.Triple)
        {
            result = NativeMethods.SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
            Thread.Sleep(50);
            result += NativeMethods.SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
            Thread.Sleep(50);
            result += NativeMethods.SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
            Debug.WriteLine($"Triple-Click Events Successful: {result}");
        }
    }



    private async void AutoClickBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
    {
        int executeCount = 0;
        _CancellationTokenSource = new CancellationTokenSource();
        do
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(Delay), _CancellationTokenSource.Token);

                PerformClick(ClickType, ClickAmount);

                executeCount++;
            }
            catch (TaskCanceledException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        while ((RepeatType != RepeatType.SetCount || executeCount < RepeatCount) && !AutoClickBackgroundWorker.CancellationPending);
    }

    // Override the WndProc method to listen for hotkey messages
    protected override void WndProc(ref Message m)
    {
        if (m.Msg == 0x0312)
        {
            int id = m.WParam.ToInt32();
            if (id == CTRL_F12_HOTKEY)
            {
                // Start Background Worker
                StartAutoClicking();
            }
            else if (id == ALT_F12_HOTKEY)
            {
                // Stop BackgroundWorker
                StopAutoClicking();
            }
        }
        base.WndProc(ref m);
    }

    private void StopAutoClicking()
    {
        if (!Started)
        {
            return;
        }

        Started = false;
        AutoClickBackgroundWorker.CancelAsync();
        WindowState = FormWindowState.Normal;
        _CancellationTokenSource.Cancel();
    }

    private void StartAutoClicking()
    {
        if (Started)
        {
            return;
        }

        Started = true;
        AutoClickBackgroundWorker.RunWorkerAsync();
        WindowState = FormWindowState.Minimized;
    }

    private void StopButton_Click(object sender, EventArgs e)
    {
        StopAutoClicking();
    }

    private void StartButton_Click(object sender, EventArgs e)
    {
        StartAutoClicking();
    }

    // Unregister your hotkeys in your form's destructor
    ~MainForm()
    {
        NativeMethods.UnregisterHotKey(this.Handle, CTRL_F12_HOTKEY);
        NativeMethods.UnregisterHotKey(this.Handle, ALT_F12_HOTKEY);
    }
}
