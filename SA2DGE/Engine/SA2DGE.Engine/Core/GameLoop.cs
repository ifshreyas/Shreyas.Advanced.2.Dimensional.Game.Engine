namespace SA2DGE.Engine.Core;

public sealed class GameLoop
{
    private readonly Game _game;

    public GameLoop(Game game)
    {
        _game = game ?? throw new ArgumentNullException(nameof(game));
    }

    public void Run()
    {
        _game.Initialize();

        try
        {
            while (true)
            {
                _game.Update(0.0f);
                _game.Render();
            }
        }
        finally
        {
            _game.Shutdown();
        }
    }
}