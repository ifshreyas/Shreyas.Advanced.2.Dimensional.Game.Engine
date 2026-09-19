using EngineRuntime = SA2DGE.Engine.Core.Engine;
using SA2DGE.Engine.Core;

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
        if (Console.KeyAvailable)
        {
            ConsoleKey key =
                Console.ReadKey(
                    true).Key;

            if (key == ConsoleKey.Escape)
            {
                EngineRuntime.Stop();
            }
        }
    }

    public override void Render()
    {
        Console.WriteLine("RENDER");

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