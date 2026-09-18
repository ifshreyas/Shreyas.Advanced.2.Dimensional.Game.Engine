namespace SA2DGE.Engine.Platform.Input;

public static class Keyboard
{
    public static bool IsDown(KeyCode key)
    {
        return InputManager.IsKeyDown(key);
    }

    public static bool IsPressed(KeyCode key)
    {
        return InputManager.IsKeyPressed(key);
    }

    public static bool IsReleased(KeyCode key)
    {
        return InputManager.IsKeyReleased(key);
    }
}