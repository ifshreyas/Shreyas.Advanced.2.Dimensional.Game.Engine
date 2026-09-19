using SA2DGE.Engine.Math;
using SA2DGE.Engine.Platform.Window;

namespace SA2DGE.Engine.Graphics;

public abstract class GraphicsBackend : IDisposable
{
    private bool _disposed;

    public bool IsInitialized { get; private set; }

    public int Width { get; private set; }

    public int Height { get; private set; }

    protected GraphicsBackend()
    {
    }

    public void Initialize(
        Window window)
    {
        ThrowIfDisposed();

        ArgumentNullException.ThrowIfNull(window);

        if (IsInitialized)
        {
            throw new InvalidOperationException(
                "The graphics backend is already initialized.");
        }

        Width = window.Width;
        Height = window.Height;

        InitializeBackend(
            window);

        IsInitialized = true;
    }

    public void Resize(
        int width,
        int height)
    {
        ThrowIfDisposed();

        if (!IsInitialized)
        {
            throw new InvalidOperationException(
                "The graphics backend has not been initialized.");
        }

        ValidateSize(
            width,
            height);

        if (width == Width &&
            height == Height)
        {
            return;
        }

        ResizeBackend(
            width,
            height);

        Width = width;
        Height = height;
    }

    public void Clear(
        Color color)
    {
        ThrowIfDisposed();
        EnsureInitialized();

        ClearBackend(
            color);
    }

    public void Present()
    {
        ThrowIfDisposed();
        EnsureInitialized();

        PresentBackend();
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (IsInitialized)
        {
            try
            {
                ShutdownBackend();
            }
            finally
            {
                IsInitialized = false;
            }
        }

        _disposed = true;
    }

    protected abstract void InitializeBackend(
        Window window);

    protected abstract void ResizeBackend(
        int width,
        int height);

    protected abstract void ClearBackend(
        Color color);

    protected abstract void PresentBackend();

    protected abstract void ShutdownBackend();

    protected void EnsureInitialized()
    {
        if (!IsInitialized)
        {
            throw new InvalidOperationException(
                "The graphics backend has not been initialized.");
        }
    }

    private static void ValidateSize(
        int width,
        int height)
    {
        if (width <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(width),
                "Width must be greater than zero.");
        }

        if (height <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(height),
                "Height must be greater than zero.");
        }
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(
            _disposed,
            this);
    }
}