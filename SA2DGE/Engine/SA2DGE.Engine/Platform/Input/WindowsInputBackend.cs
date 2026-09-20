using System.Runtime.InteropServices;

namespace SA2DGE.Engine.Platform.Input;

public sealed class WindowsInputBackend : IInputBackend
{
    private bool _initialized;
    private bool _disposed;

    public void Initialize()
    {
        ThrowIfDisposed();

        if (_initialized)
            return;

        if (!OperatingSystem.IsWindows())
        {
            throw new PlatformNotSupportedException(
                "WindowsInputBackend requires Windows.");
        }

        _initialized = true;
    }

    public void Update(Input input)
    {
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(input);

        if (!_initialized)
        {
            throw new InvalidOperationException(
                "The Windows input backend has not been initialized.");
        }

        UpdateKeyboard(input);
    }

    public void Shutdown()
    {
        if (_disposed)
            return;

        if (!_initialized)
            return;

        _initialized = false;
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        Shutdown();

        _disposed = true;
    }

    private static void UpdateKeyboard(Input input)
    {
        UpdateKey(input, InputKey.A, 0x41);
        UpdateKey(input, InputKey.B, 0x42);
        UpdateKey(input, InputKey.C, 0x43);
        UpdateKey(input, InputKey.D, 0x44);
        UpdateKey(input, InputKey.E, 0x45);
        UpdateKey(input, InputKey.F, 0x46);
        UpdateKey(input, InputKey.G, 0x47);
        UpdateKey(input, InputKey.H, 0x48);
        UpdateKey(input, InputKey.I, 0x49);
        UpdateKey(input, InputKey.J, 0x4A);
        UpdateKey(input, InputKey.K, 0x4B);
        UpdateKey(input, InputKey.L, 0x4C);
        UpdateKey(input, InputKey.M, 0x4D);
        UpdateKey(input, InputKey.N, 0x4E);
        UpdateKey(input, InputKey.O, 0x4F);
        UpdateKey(input, InputKey.P, 0x50);
        UpdateKey(input, InputKey.Q, 0x51);
        UpdateKey(input, InputKey.R, 0x52);
        UpdateKey(input, InputKey.S, 0x53);
        UpdateKey(input, InputKey.T, 0x54);
        UpdateKey(input, InputKey.U, 0x55);
        UpdateKey(input, InputKey.V, 0x56);
        UpdateKey(input, InputKey.W, 0x57);
        UpdateKey(input, InputKey.X, 0x58);
        UpdateKey(input, InputKey.Y, 0x59);
        UpdateKey(input, InputKey.Z, 0x5A);

        UpdateKey(input, InputKey.Number0, 0x30);
        UpdateKey(input, InputKey.Number1, 0x31);
        UpdateKey(input, InputKey.Number2, 0x32);
        UpdateKey(input, InputKey.Number3, 0x33);
        UpdateKey(input, InputKey.Number4, 0x34);
        UpdateKey(input, InputKey.Number5, 0x35);
        UpdateKey(input, InputKey.Number6, 0x36);
        UpdateKey(input, InputKey.Number7, 0x37);
        UpdateKey(input, InputKey.Number8, 0x38);
        UpdateKey(input, InputKey.Number9, 0x39);

        UpdateKey(input, InputKey.Escape, 0x1B);
        UpdateKey(input, InputKey.Enter, 0x0D);
        UpdateKey(input, InputKey.Tab, 0x09);
        UpdateKey(input, InputKey.Backspace, 0x08);
        UpdateKey(input, InputKey.Space, 0x20);

        UpdateKey(input, InputKey.Left, 0x25);
        UpdateKey(input, InputKey.Up, 0x26);
        UpdateKey(input, InputKey.Right, 0x27);
        UpdateKey(input, InputKey.Down, 0x28);

        UpdateKey(input, InputKey.LeftShift, 0xA0);
        UpdateKey(input, InputKey.RightShift, 0xA1);
        UpdateKey(input, InputKey.LeftControl, 0xA2);
        UpdateKey(input, InputKey.RightControl, 0xA3);
        UpdateKey(input, InputKey.LeftAlt, 0xA4);
        UpdateKey(input, InputKey.RightAlt, 0xA5);

        UpdateKey(input, InputKey.Insert, 0x2D);
        UpdateKey(input, InputKey.Delete, 0x2E);
        UpdateKey(input, InputKey.Home, 0x24);
        UpdateKey(input, InputKey.End, 0x23);
        UpdateKey(input, InputKey.PageUp, 0x21);
        UpdateKey(input, InputKey.PageDown, 0x22);

        UpdateKey(input, InputKey.CapsLock, 0x14);
        UpdateKey(input, InputKey.NumLock, 0x90);
        UpdateKey(input, InputKey.ScrollLock, 0x91);

        UpdateKey(input, InputKey.F1, 0x70);
        UpdateKey(input, InputKey.F2, 0x71);
        UpdateKey(input, InputKey.F3, 0x72);
        UpdateKey(input, InputKey.F4, 0x73);
        UpdateKey(input, InputKey.F5, 0x74);
        UpdateKey(input, InputKey.F6, 0x75);
        UpdateKey(input, InputKey.F7, 0x76);
        UpdateKey(input, InputKey.F8, 0x77);
        UpdateKey(input, InputKey.F9, 0x78);
        UpdateKey(input, InputKey.F10, 0x79);
        UpdateKey(input, InputKey.F11, 0x7A);
        UpdateKey(input, InputKey.F12, 0x7B);
    }

    private static void UpdateKey(
        Input input,
        InputKey key,
        int virtualKey)
    {
        short state = GetAsyncKeyState(virtualKey);

        bool isDown = (state & 0x8000) != 0;

        input.SetKeyState(
            key,
            isDown);
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(
            _disposed,
            this);
    }

    [DllImport(
        "user32.dll",
        SetLastError = false)]
    private static extern short GetAsyncKeyState(
        int virtualKey);
}