using SA2DGE.Engine.Graphics.Buffers;
using SA2DGE.Engine.Graphics.Shaders;
using SA2DGE.Engine.Math;
using System.Runtime.InteropServices;

namespace SA2DGE.Engine.Graphics;

public sealed class Direct3D11Renderer2D : Renderer2D
{
    private readonly GraphicsBackend _graphics;

    private VertexBuffer? _rectangleVertexBuffer;
    private IndexBuffer? _rectangleIndexBuffer;
    private VertexArray? _rectangleVertexArray;

    private Shader? _rectangleVertexShader;
    private Shader? _rectangleFragmentShader;
    private ShaderProgram? _rectangleShaderProgram;

    private ConstantBuffer? _rectangleTransformBuffer;
    private ConstantBuffer? _rectangleColorBuffer;

    private bool _rectanglePipelineInitialized;

    private Matrix4 _projectionMatrix;

    public Direct3D11Renderer2D(GraphicsBackend graphics)
    {
        ArgumentNullException.ThrowIfNull(graphics);

        _graphics = graphics;
    }

    public override string Name =>
        "SA2DGE Direct3D11 Renderer2D";

    protected override void OnInitialize()
    {
        if (!_graphics.IsInitialized)
        {
            throw new InvalidOperationException(
                "The graphics backend must be initialized before the 2D renderer.");
        }

        if (_graphics.Resources is not GraphicsResources resources)
        {
            throw new InvalidOperationException(
                "Graphics resources are not available.");
        }

        _projectionMatrix = Matrix4.CreateOrthographicOffCenter(
            0.0f,
            _graphics.Width,
            _graphics.Height,
            0.0f,
            -1.0f,
            1.0f);

        _rectangleTransformBuffer =
            resources.CreateConstantBuffer(64);

        _rectangleColorBuffer =
            resources.CreateConstantBuffer(16);

        CreateRectanglePipeline();

        Console.WriteLine(
            "Stage 3 Renderer2D initialized.");
    }

    protected override void BeginFrame2D()
    {
        if (_graphics.Commands is null)
        {
            throw new InvalidOperationException(
                "Graphics commands are not available.");
        }

        _graphics.Commands.Reset();
    }

    protected override void EndFrame2D()
    {
    }

    protected override void ExecuteCommand(RenderCommand command)
    {
        switch (command.Type)
        {
            case RenderCommandType.Rectangle:
                ExecuteRectangle(command);
                break;

            case RenderCommandType.Sprite:
                ExecuteSprite(command);
                break;

            case RenderCommandType.Circle:
                ExecuteCircle(command);
                break;

            case RenderCommandType.Line:
                ExecuteLine(command);
                break;

            case RenderCommandType.Text:
                ExecuteText(command);
                break;

            default:
                throw new ArgumentOutOfRangeException(
                    nameof(command),
                    command.Type,
                    "Unsupported 2D render command.");
        }
    }

    protected override void OnPresent()
    {
        _graphics.Present();
    }

    protected override void OnShutdown()
    {
        _graphics.Commands?.Reset();

        _rectangleColorBuffer?.Dispose();
        _rectangleColorBuffer = null;

        _rectangleTransformBuffer?.Dispose();
        _rectangleTransformBuffer = null;

        _rectangleShaderProgram?.Dispose();
        _rectangleShaderProgram = null;

        _rectangleVertexShader?.Dispose();
        _rectangleVertexShader = null;

        _rectangleFragmentShader?.Dispose();
        _rectangleFragmentShader = null;

        _rectangleVertexArray?.Dispose();
        _rectangleVertexArray = null;

        _rectangleIndexBuffer?.Dispose();
        _rectangleIndexBuffer = null;

        _rectangleVertexBuffer?.Dispose();
        _rectangleVertexBuffer = null;

        _rectanglePipelineInitialized = false;
    }

    private void CreateRectanglePipeline()
    {
        if (_rectanglePipelineInitialized)
        {
            return;
        }

        if (_graphics.Commands is null)
        {
            throw new InvalidOperationException(
                "Graphics commands are not available.");
        }

        if (_graphics.Resources is null)
        {
            throw new InvalidOperationException(
                "Graphics resources are not available.");
        }

        if (_graphics.Shaders is null)
        {
            throw new InvalidOperationException(
                "Graphics shaders are not available.");
        }

        if (_rectangleTransformBuffer is null)
        {
            throw new InvalidOperationException(
                "The rectangle transform buffer is not initialized.");
        }

        Vertex[] vertices =
        {
            new Vertex(
                -0.5f,
                -0.5f,
                0.0f,
                1.0f,
                1.0f,
                1.0f,
                1.0f),

            new Vertex(
                0.5f,
                -0.5f,
                0.0f,
                1.0f,
                1.0f,
                1.0f,
                1.0f),

            new Vertex(
                -0.5f,
                0.5f,
                0.0f,
                1.0f,
                1.0f,
                1.0f,
                1.0f),

            new Vertex(
                0.5f,
                0.5f,
                0.0f,
                1.0f,
                1.0f,
                1.0f,
                1.0f)
        };

        // The orthographic projection flips Y because the
        // 2D coordinate system starts at the top-left.
        //
        // Therefore the triangle winding must be reversed
        // for Direct3D11's default clockwise front-face rule.
        uint[] indices =
        {
            0, 1, 2,
            2, 1, 3
        };

        _rectangleVertexBuffer =
            _graphics.CreateVertexBuffer(
                vertices.Length,
                28);

        _rectangleVertexBuffer.SetData(vertices);

        _rectangleIndexBuffer =
            _graphics.CreateIndexBuffer(
                indices.Length);

        _rectangleIndexBuffer.SetData(indices);

        _rectangleVertexArray =
            _graphics.CreateVertexArray();

        _rectangleVertexArray.AddVertexBuffer(
            _rectangleVertexBuffer);

        _rectangleVertexArray.SetIndexBuffer(
            _rectangleIndexBuffer);

        VertexLayout layout = new();

        layout.Add(
            new VertexAttribute(
                "POSITION",
                0,
                VertexAttributeType.Float3,
                0));

        layout.Add(
            new VertexAttribute(
                "COLOR",
                0,
                VertexAttributeType.Float4,
                12));

        const string vertexShaderSource = """
            cbuffer TransformBuffer : register(b0)
            {
                float4x4 Transform;
            };

            struct VSInput
            {
                float3 Position : POSITION;
                float4 Color : COLOR;
            };

            struct VSOutput
            {
                float4 Position : SV_POSITION;
                float4 Color : COLOR;
            };

            VSOutput main(VSInput input)
            {
                VSOutput output;

                output.Position =
                    mul(
                        float4(input.Position, 1.0f),
                        Transform);

                output.Color = input.Color;

                return output;
            }
            """;

        const string fragmentShaderSource = """
                                            struct PSInput
                                            {
                                                float4 Position : SV_POSITION;
                                                float4 Color : COLOR;
                                            };

                                            float4 main(PSInput input) : SV_TARGET
                                            {
                                                return float4(1.0f, 0.0f, 0.0f, 1.0f);
                                            }
                                            """;

        _rectangleVertexShader =
            _graphics.CreateVertexShader(
                vertexShaderSource);

        _rectangleFragmentShader =
            _graphics.CreateFragmentShader(
                fragmentShaderSource);

        _rectangleVertexShader.Compile();
        _rectangleFragmentShader.Compile();

        _rectangleVertexArray.SetLayout(
            layout,
            _rectangleVertexShader.Bytecode.Span);

        _rectangleShaderProgram =
            _graphics.CreateShaderProgram();

        _rectangleShaderProgram.Attach(
            _rectangleVertexShader);

        _rectangleShaderProgram.Attach(
            _rectangleFragmentShader);

        _rectangleShaderProgram.Link();

        _rectanglePipelineInitialized = true;
    }

    private void UpdateRectangleTransform(
        RenderCommand command)
    {
        if (_rectangleTransformBuffer is null)
        {
            throw new InvalidOperationException(
                "The rectangle transform buffer is not initialized.");
        }

        Matrix4 transform =
            _projectionMatrix
            * Matrix4.CreateTranslation(
                new Vector3(
                    command.Position.X,
                    command.Position.Y,
                    0.0f))
            * Matrix4.CreateRotationZ(
                command.Rotation)
            * Matrix4.CreateScale(
                new Vector3(
                    command.Size.X,
                    command.Size.Y,
                    1.0f));

        float[] values =
        {
            transform.M11,
            transform.M12,
            transform.M13,
            transform.M14,

            transform.M21,
            transform.M22,
            transform.M23,
            transform.M24,

            transform.M31,
            transform.M32,
            transform.M33,
            transform.M34,

            transform.M41,
            transform.M42,
            transform.M43,
            transform.M44
        };

        ReadOnlySpan<byte> bytes =
            MemoryMarshal.AsBytes<float>(
                values);

        _rectangleTransformBuffer.SetData(
            bytes);
    }

    private void UpdateRectangleColor(
        RenderCommand command)
    {
        if (_rectangleColorBuffer is null)
        {
            throw new InvalidOperationException(
                "The rectangle color buffer is not initialized.");
        }

        float[] values =
        {
            command.Color.R / 255.0f,
            command.Color.G / 255.0f,
            command.Color.B / 255.0f,
            command.Color.A / 255.0f
        };

        ReadOnlySpan<byte> bytes =
            MemoryMarshal.AsBytes<float>(
                values);

        _rectangleColorBuffer.SetData(
            bytes);
    }

    private void ExecuteRectangle(
        RenderCommand command)
    {
        if (!_rectanglePipelineInitialized ||
            _rectangleVertexArray is null ||
            _rectangleShaderProgram is null ||
            _rectangleTransformBuffer is null ||
            _rectangleColorBuffer is null ||
            _graphics.Commands is null)
        {
            throw new InvalidOperationException(
                "The rectangle rendering pipeline is not initialized.");
        }

        Console.WriteLine(
            $"RECTANGLE DRAW: X={command.Position.X}, " +
            $"Y={command.Position.Y}, " +
            $"W={command.Size.X}, " +
            $"H={command.Size.Y}");

        UpdateRectangleTransform(command);
        UpdateRectangleColor(command);

        _graphics.Commands.SetVertexArray(
            _rectangleVertexArray);

        _graphics.Commands.SetShaderProgram(
            _rectangleShaderProgram);

        _graphics.Commands.SetVertexConstantBuffer(
            0,
            _rectangleTransformBuffer);

        _graphics.Commands.SetPixelConstantBuffer(
            1,
            _rectangleColorBuffer);

        _graphics.Commands.DrawIndexed(6);
    }

    private static void ExecuteSprite(
        RenderCommand command)
    {
        throw new NotImplementedException(
            "Sprite rendering is not implemented yet.");
    }

    private static void ExecuteCircle(
        RenderCommand command)
    {
        throw new NotImplementedException(
            "Circle rendering is not implemented yet.");
    }

    private static void ExecuteLine(
        RenderCommand command)
    {
        throw new NotImplementedException(
            "Line rendering is not implemented yet.");
    }

    private static void ExecuteText(
        RenderCommand command)
    {
        throw new NotImplementedException(
            "Text rendering is not implemented yet.");
    }

    private readonly struct Vertex
    {
        public readonly float PositionX;
        public readonly float PositionY;
        public readonly float PositionZ;

        public readonly float ColorR;
        public readonly float ColorG;
        public readonly float ColorB;
        public readonly float ColorA;

        public Vertex(
            float positionX,
            float positionY,
            float positionZ,
            float colorR,
            float colorG,
            float colorB,
            float colorA)
        {
            PositionX = positionX;
            PositionY = positionY;
            PositionZ = positionZ;

            ColorR = colorR;
            ColorG = colorG;
            ColorB = colorB;
            ColorA = colorA;
        }
    }
}