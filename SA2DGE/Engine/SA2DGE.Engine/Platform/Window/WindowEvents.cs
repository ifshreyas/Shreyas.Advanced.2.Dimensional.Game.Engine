namespace SA2DGE.Engine.Platform.Window;

public sealed class WindowEvents
{
    public event Action? Closed;

    public event Action<int, int>? Resized;

    public event Action? FocusGained;

    public event Action? FocusLost;

    public event Action? Minimized;

    public event Action? Restored;

    internal void RaiseClosed()
    {
        Closed?.Invoke();
    }

    internal void RaiseResized(int width, int height)
    {
        Resized?.Invoke(width, height);
    }

    internal void RaiseFocusGained()
    {
        FocusGained?.Invoke();
    }

    internal void RaiseFocusLost()
    {
        FocusLost?.Invoke();
    }

    internal void RaiseMinimized()
    {
        Minimized?.Invoke();
    }

    internal void RaiseRestored()
    {
        Restored?.Invoke();
    }
}