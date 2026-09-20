using EngineColor = SA2DGE.Engine.Math.Color;
using SA2DGE.Engine.Platform.Window;
using Vortice;
using Vortice.Direct3D;
using Vortice.Direct3D11;
using Vortice.DXGI;
using Vortice.Mathematics;

namespace SA2DGE.Engine.Graphics;

public sealed class Direct3D11GraphicsBackend : GraphicsBackend
{
    private ID3D11Device? _device;
    private ID3D11DeviceContext? _context;

    private IDXGIFactory2? _factory;
    private IDXGISwapChain1? _swapChain;

    private ID3D11RenderTargetView? _renderTargetView;

    private bool _vsync;

    protected override void InitializeBackend(
        Window window)
    {
        ArgumentNullException.ThrowIfNull(window);

        if (!OperatingSystem.IsWindows())
        {
            throw new PlatformNotSupportedException(
                "Direct3D 11 is only supported on Windows.");
        }

        _vsync = window.VSync;

        

        _factory =
            DXGI.CreateDXGIFactory2<IDXGIFactory2>(
                false);

        var deviceResult =
            D3D11.D3D11CreateDevice(
                null,
                DriverType.Hardware,
                DeviceCreationFlags.BgraSupport,
                new[]
                {
                    FeatureLevel.Level_11_0,
                    FeatureLevel.Level_10_0
                },
                out _device,
                out _,
                out _context);

        if (deviceResult.Failure)
        {
            Console.WriteLine(
                $"Hardware D3D11 device creation failed: {deviceResult}");

            DisposeNativeObjects();

            deviceResult =
                D3D11.D3D11CreateDevice(
                    null,
                    DriverType.Warp,
                    DeviceCreationFlags.BgraSupport,
                    new[]
                    {
                        FeatureLevel.Level_11_0,
                        FeatureLevel.Level_10_0
                    },
                    out _device,
                    out _,
                    out _context);
        }

        deviceResult.CheckError();

        if (_device is null ||
            _context is null ||
            _factory is null)
        {
            throw new InvalidOperationException(
                "D3D11 device, context, or DXGI factory was not created.");
        }

        Console.WriteLine(
            "D3D11 device initialized.");

        CreateSwapChain(window);
        CreateRenderTarget();
    }

    protected override void ResizeBackend(
        int width,
        int height)
    {
        if (_swapChain is null ||
            _context is null)
        {
            throw new InvalidOperationException(
                "The Direct3D 11 backend is not initialized.");
        }

        ReleaseRenderTarget();

        _context.Flush();

        var result =
            _swapChain.ResizeBuffers(
                2,
                checked((uint)width),
                checked((uint)height),
                Format.R8G8B8A8_UNorm,
                SwapChainFlags.None);

        result.CheckError();

        CreateRenderTarget();
    }

    protected override void ClearBackend(
        EngineColor color)
    {
        if (_context is null ||
            _renderTargetView is null)
        {
            throw new InvalidOperationException(
                "The Direct3D 11 render target is not initialized.");
        }

        _context.OMSetRenderTargets(
            _renderTargetView,
            null);

        

        _context.ClearRenderTargetView(
            _renderTargetView,
            new Color4(
                color.R,
                color.G,
                color.B,
                color.A));
    }

    protected override void PresentBackend()
    {
        if (_swapChain is null)
        {
            throw new InvalidOperationException(
                "The Direct3D 11 swap chain is not initialized.");
        }

        uint syncInterval =
            _vsync ? 1u : 0u;

        var result =
            _swapChain.Present(
                syncInterval,
                PresentFlags.None);

        if (result.Failure)
        {
            Console.WriteLine(
                $"D3D11 Present failed: {result}");
        }
    }

    protected override void ShutdownBackend()
    {
        DisposeNativeObjects();
    }

    private void CreateSwapChain(
        Window window)
    {
        if (_device is null ||
            _factory is null)
        {
            throw new InvalidOperationException(
                "The D3D11 device or DXGI factory is not initialized.");
        }

        SwapChainDescription1 description = new()
        {
            Width =
                checked((uint)Width),

            Height =
                checked((uint)Height),

            Format =
                Format.R8G8B8A8_UNorm,

            Stereo =
                false,

            SampleDescription =
                new SampleDescription(1, 0),

            BufferUsage =
                Usage.RenderTargetOutput,

            BufferCount =
                2,

            Scaling =
                Scaling.Stretch,

            SwapEffect =
                SwapEffect.FlipDiscard,

            AlphaMode =
                AlphaMode.Ignore,

            Flags =
                SwapChainFlags.None
        };

        using IDXGIDevice dxgiDevice =
            _device.QueryInterface<IDXGIDevice>();

        _swapChain =
            _factory.CreateSwapChainForHwnd(
                dxgiDevice,
                window.NativeHandle,
                description);

        _factory.MakeWindowAssociation(
            window.NativeHandle,
            WindowAssociationFlags.IgnoreAltEnter);

        Console.WriteLine(
            $"D3D11 flip swap chain created: {Width}x{Height}");
    }

    private void CreateRenderTarget()
    {
        if (_device is null ||
            _context is null ||
            _swapChain is null)
        {
            throw new InvalidOperationException(
                "The D3D11 device, context, or swap chain is not initialized.");
        }

        using ID3D11Texture2D backBuffer =
            _swapChain.GetBuffer<ID3D11Texture2D>(0);

        _renderTargetView =
            _device.CreateRenderTargetView(
                backBuffer);

        _context.OMSetRenderTargets(
            _renderTargetView,
            null);

        _context.RSSetViewport(
            new Viewport(
                0.0f,
                0.0f,
                Width,
                Height,
                0.0f,
                1.0f));

        
    }

    private void ReleaseRenderTarget()
    {
        if (_context is not null)
        {
            _context.OMSetRenderTargets(
                _renderTargetView!,
                null);
        }

        _renderTargetView?.Dispose();
        _renderTargetView = null;
    }

    private void DisposeNativeObjects()
    {
        ReleaseRenderTarget();

        _swapChain?.Dispose();
        _swapChain = null;

        _factory?.Dispose();
        _factory = null;

        _context?.Dispose();
        _context = null;

        _device?.Dispose();
        _device = null;
    }
}