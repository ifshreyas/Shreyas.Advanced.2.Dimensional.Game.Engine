using EngineRuntime = SA2DGE.Engine.Core.Engine;
using SA2DGE.Engine.Core;
using SA2DGE.Engine.Platform.Window;
using SA2DGE.Engine.Platform.Input;

internal sealed class RuntimeGame : Game
{
    public RuntimeGame()
        : base(
            new SA2DGE.Engine.Platform.Window.WindowConfig
            {
                Title = "SA2DGE Runtime Test",
                Width = 1280,
                Height = 720,
                VSync = true,
                Resizable = true,
                Fullscreen = false,
                Borderless = false
            })
    
    
    {
        
    }

    public override void Initialize()
    {
        Console.WriteLine(
            "SA2DGE initialized.");
    }

    public override void Update(
        float deltaTime)
    {
        if (Input.Keyboard.IsPressed(InputKey.Escape))
        {
            EngineRuntime.Stop();
        }
    }

    public override void OnWindowEvent(
        WindowEvent windowEvent)
    {
        Console.WriteLine(
            $"Window Event: {windowEvent.Type} " +
            $"({windowEvent.Width}x{windowEvent.Height})");
    }
    
    public override void Render()
    {
        

        Graphics!.Clear(
            new SA2DGE.Engine.Math.Color(
                255,
                0,
                0,
                255));

        Graphics.Present();
    }

    public override void Shutdown()
    {
        Console.WriteLine(
            "SA2DGE shutdown.");
    }
}

internal static class Program
{
    private static void Main()
    {
        using RuntimeGame game =
            new();

        EngineRuntime.Run(game);
    }
}