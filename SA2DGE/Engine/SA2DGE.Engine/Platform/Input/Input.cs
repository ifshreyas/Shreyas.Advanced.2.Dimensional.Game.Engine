namespace SA2DGE.Engine.Platform.Input;

public sealed class Input
{
    public Keyboard Keyboard { get; }

    internal Input()
    {
        Keyboard = new Keyboard();
    }

    internal void BeginFrame()
    {
        Keyboard.BeginFrame();
    }

    internal void SetKeyState(
        InputKey key,
        bool isDown)
    {
        Keyboard.SetKeyState(
            key,
            isDown);
    }

    internal void Clear()
    {
        Keyboard.Clear();
    }
}