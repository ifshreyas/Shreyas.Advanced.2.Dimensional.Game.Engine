using SA2DGE.Engine.Platform.Window;
using SA2DGE.Engine.Graphics;
namespace SA2DGE.Engine.Core;

public abstract class Game : IDisposable
{
    private bool _initialized;
    private bool _shutdown;
    private bool _disposed;

    public Window Window { get; }
    
    public GraphicsBackend? Graphics { get; private set; }

    public bool IsInitialized =>
        _initialized;

    public bool IsShutdown =>
        _shutdown;

    protected Game(
        WindowConfig? windowConfig = null)
    {
        Window =
            new Window(
                windowConfig ??
                new WindowConfig());
    }

    public virtual void Initialize()
    {
    }

    public virtual void Update(
        float deltaTime)
    {
    }

    public virtual void Render()
    {
    }

    public virtual void Shutdown()
    {
    }

    internal void InitializeRuntime()
    {
        ThrowIfDisposed();

        if (_initialized)
        {
            return;
        }

        Window.Open();
        
        Graphics =
            new Direct3D11GraphicsBackend();

        Graphics.Initialize(
            Window);

        try
        {
            Initialize();

            _initialized = true;
            _shutdown = false;
        }
        catch
        {
            Window.Close();
            throw;
        }
    }

    internal void ShutdownRuntime()
    {
        if (_shutdown)
        {
            return;
        }

        try
        {
            Shutdown();
        }
        finally
        {
            Graphics?.Dispose();
            Graphics = null;

            Window.Close();

            _initialized = false;
            _shutdown = true;
        }
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        ShutdownRuntime();

        Window.Dispose();

        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(
            _disposed,
            this);
    }
}