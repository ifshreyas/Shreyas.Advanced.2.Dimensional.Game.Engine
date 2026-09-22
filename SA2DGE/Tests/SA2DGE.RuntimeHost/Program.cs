using EngineRuntime = SA2DGE.Engine.Core.Engine;
using SA2DGE.Engine.Core;
using SA2DGE.Engine.Platform.Window;
using SA2DGE.Engine.Platform.Input;
using SA2DGE.Engine.Graphics.Buffers;
using SA2DGE.Engine.Graphics.Shaders;

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
        Console.WriteLine("SA2DGE initialized.");

        VertexBuffer vertexBuffer = Graphics!.CreateVertexBuffer(
            vertexCount: 3,
            vertexSize: 20);

        IndexBuffer indexBuffer = Graphics.CreateIndexBuffer(
            indexCount: 3);

        VertexArray vertexArray = Graphics.CreateVertexArray();

        Console.WriteLine("Graphics resources created successfully.");

        vertexArray.Dispose();
        indexBuffer.Dispose();
        vertexBuffer.Dispose();

        Console.WriteLine("Graphics resources disposed successfully.");
        
        const string vertexShaderSource = """
                                          struct VSInput
                                          {
                                              float3 Position : POSITION;
                                          };

                                          struct VSOutput
                                          {
                                              float4 Position : SV_POSITION;
                                          };

                                          VSOutput main(VSInput input)
                                          {
                                              VSOutput output;

                                              output.Position = float4(
                                                  input.Position,
                                                  1.0f);

                                              return output;
                                          }
                                          """;

        const string fragmentShaderSource = """
                                            struct PSInput
                                            {
                                                float4 Position : SV_POSITION;
                                            };

                                            float4 main(PSInput input) : SV_TARGET
                                            {
                                                return float4(
                                                    1.0f,
                                                    0.0f,
                                                    0.0f,
                                                    1.0f);
                                            }
                                            """;

        Shader vertexShader =
            Graphics!.CreateVertexShader(vertexShaderSource);

        Shader fragmentShader =
            Graphics.CreateFragmentShader(fragmentShaderSource);

        vertexShader.Compile();
        fragmentShader.Compile();

        ShaderProgram shaderProgram =
            Graphics.CreateShaderProgram();

        shaderProgram.Attach(vertexShader);
        shaderProgram.Attach(fragmentShader);
        shaderProgram.Link();

        Console.WriteLine(
            "D3D11 shaders compiled and linked successfully.");

        shaderProgram.Dispose();
        vertexShader.Dispose();
        fragmentShader.Dispose();
    }

    public override void Update(float deltaTime)
    {
        if (Input.Mouse.DeltaX != 0.0f ||
            Input.Mouse.DeltaY != 0.0f)
        {
            Console.WriteLine(
                $"Mouse Move: X={Input.Mouse.X}, Y={Input.Mouse.Y}, " +
                $"DeltaX={Input.Mouse.DeltaX}, DeltaY={Input.Mouse.DeltaY}");
        }

        if (Input.Mouse.IsPressed(MouseButton.Left))
        {
            Console.WriteLine("LEFT MOUSE PRESSED");
        }

        if (Input.Mouse.IsDown(MouseButton.Left))
        {
            Console.WriteLine("LEFT MOUSE HELD");
        }

        if (Input.Mouse.IsReleased(MouseButton.Left))
        {
            Console.WriteLine("LEFT MOUSE RELEASED");
        }

        if (Input.Keyboard.IsPressed(InputKey.Escape))
        {
            Engine.Stop();
        }
        
        if (Input.Mouse.WheelDelta != 0.0f)
        {
            Console.WriteLine(
                $"Mouse Wheel: {Input.Mouse.WheelDelta}");
        }
        
        if (Gamepad.IsConnected)
        {
            Console.WriteLine(
                $"Gamepad: " +
                $"LX={Gamepad.LeftStickX:F2} " +
                $"LY={Gamepad.LeftStickY:F2} " +
                $"RX={Gamepad.RightStickX:F2} " +
                $"RY={Gamepad.RightStickY:F2} " +
                $"LT={Gamepad.LeftTrigger:F2} " +
                $"RT={Gamepad.RightTrigger:F2}");

            if (Gamepad.IsDown(GamepadButton.A))
                Console.WriteLine("GAMEPAD A");

            if (Gamepad.IsDown(GamepadButton.B))
                Console.WriteLine("GAMEPAD B");
        }
    }
    
    public override void OnWindowEvent(WindowEvent windowEvent)
    {
        base.OnWindowEvent(windowEvent);

        Console.WriteLine(
            $"Window Event: {windowEvent.Type} ({windowEvent.Width}x{windowEvent.Height})");
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