namespace SA2DGE.Engine.Platform.Input;

public static class Mouse
{
    public static float X => InputManager.MouseX;

    public static float Y => InputManager.MouseY;

    public static float DeltaX => InputManager.MouseDeltaX;

    public static float DeltaY => InputManager.MouseDeltaY;

    public static bool IsDown(MouseButton button)
    {
        return InputManager.IsMouseButtonDown(button);
    }

    public static bool IsPressed(MouseButton button)
    {
        return InputManager.IsMouseButtonPressed(button);
    }

    public static bool IsReleased(MouseButton button)
    {
        return InputManager.IsMouseButtonReleased(button);
    }

    internal static void SetPosition(float x, float y)
    {
        InputManager.SetMousePosition(x, y);
    }

    internal static void SetButtonState(
        MouseButton button,
        bool pressed)
    {
        InputManager.SetMouseButtonState(button, pressed);
    }
}