namespace SA2DGE.Engine.Platform.Input;

public interface IInputBackend : IDisposable
{
    void Initialize();

    void Update(Input input);

    void Shutdown();
}