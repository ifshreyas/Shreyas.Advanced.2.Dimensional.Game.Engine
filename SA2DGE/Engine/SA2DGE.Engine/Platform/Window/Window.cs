namespace SA2DGE.Engine.Platform.Window;

public sealed class Window : IDisposable
{
    private bool _isOpen;

    public string Title { get; private set; }

    public int Width { get; private set; }

    public int Height { get; private set; }

    public bool IsOpen => _isOpen;

    public bool VSync { get; private set; }

    public bool Resizable { get; private set; }

    public bool Fullscreen { get; private set; }

    public bool Borderless { get; private set; }

    public Window(WindowConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        Title = config.Title;
        Width = config.Width;
        Height = config.Height;
        VSync = config.VSync;
        Resizable = config.Resizable;
        Fullscreen = config.Fullscreen;
        Borderless = config.Borderless;

        _isOpen = false;
    }

    public void Open()
    {
        if (_isOpen)
        {
            return;
        }

        _isOpen = true;
    }

    public void Close()
    {
        if (!_isOpen)
        {
            return;
        }

        _isOpen = false;
    }

    public void Resize(int width, int height)
    {
        if (width <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(width));
        }

        if (height <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(height));
        }

        Width = width;
        Height = height;
    }

    public void SetTitle(string title)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);

        Title = title;
    }

    public void Dispose()
    {
        Close();
    }
}