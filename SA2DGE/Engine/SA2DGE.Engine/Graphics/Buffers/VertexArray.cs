namespace SA2DGE.Engine.Graphics.Buffers;

public abstract class VertexArray : IDisposable
{
    private readonly List<VertexBuffer> _vertexBuffers = new();

    private IndexBuffer? _indexBuffer;
    private bool _disposed;

    public IReadOnlyList<VertexBuffer> VertexBuffers =>
        _vertexBuffers;

    public IndexBuffer? IndexBuffer =>
        _indexBuffer;

    public bool IsDisposed =>
        _disposed;

    public void AddVertexBuffer(VertexBuffer buffer)
    {
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(buffer);

        if (_vertexBuffers.Contains(buffer))
            return;

        OnVertexBufferAdded(buffer);

        _vertexBuffers.Add(buffer);
    }

    public void SetIndexBuffer(IndexBuffer buffer)
    {
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(buffer);

        _indexBuffer = buffer;

        OnIndexBufferSet(buffer);
    }

    public void Bind()
    {
        ThrowIfDisposed();

        OnBind();
    }

    public void Unbind()
    {
        ThrowIfDisposed();

        OnUnbind();
    }

    protected virtual void OnVertexBufferAdded(
        VertexBuffer buffer)
    {
    }

    protected virtual void OnIndexBufferSet(
        IndexBuffer buffer)
    {
    }

    protected abstract void OnBind();

    protected abstract void OnUnbind();

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        OnDispose();

        _vertexBuffers.Clear();
        _indexBuffer = null;

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