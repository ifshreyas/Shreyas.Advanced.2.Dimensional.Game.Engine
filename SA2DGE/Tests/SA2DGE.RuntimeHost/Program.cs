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

    public override void Update(
        float deltaTime)
    {
        if (Input.Keyboard.IsPressed(InputKey.Escape))
        {
            EngineRuntime.Stop();
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