namespace SA2DGE.Engine.Graphics.Textures;

public abstract class Texture : IDisposable
{
    private bool _disposed;

    public int Width { get; }

    public int Height { get; }

    public bool IsDisposed =>
        _disposed;

    protected Texture(
        int width,
        int height)
    {
        if (width <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(width));
        }

        if (height <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(height));
        }

        Width = width;
        Height = height;
    }

    public void Bind(int slot = 0)
    {
        ThrowIfDisposed();

        if (slot < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(slot));
        }

        OnBind(slot);
    }

    public void Unbind()
    {
        ThrowIfDisposed();

        OnUnbind();
    }

    public void SetData<T>(
        ReadOnlySpan<T> data)
        where T : unmanaged
    {
        ThrowIfDisposed();

        OnSetData(data);
    }

    protected abstract void OnBind(int slot);

    protected abstract void OnUnbind();

    protected virtual void OnSetData<T>(
        ReadOnlySpan<T> data)
        where T : unmanaged
    {
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        OnDispose();

        _disposed = true;
    }

    protected virtual void OnDispose()
    {
    }

    protected void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(
            _disposed,
            this);
    }
}