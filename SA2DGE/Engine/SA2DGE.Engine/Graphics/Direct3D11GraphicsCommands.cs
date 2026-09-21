using SA2DGE.Engine.Graphics.Buffers;
using SA2DGE.Engine.Graphics.Shaders;
using Vortice.Direct3D11;

namespace SA2DGE.Engine.Graphics;

internal sealed class Direct3D11GraphicsCommands : GraphicsCommands
{
    private readonly ID3D11DeviceContext _context;

    private VertexArray? _vertexArray;
    private ShaderProgram? _shaderProgram;

    public Direct3D11GraphicsCommands(
        ID3D11DeviceContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _context = context;
    }

    protected override void SetVertexArrayBackend(
        VertexArray vertexArray)
    {
        _vertexArray = vertexArray;
    }

    protected override void SetShaderProgramBackend(
        ShaderProgram shaderProgram)
    {
        _shaderProgram = shaderProgram;
    }

    protected override void DrawIndexedBackend(
        int indexCount,
        int startIndex)
    {
        EnsurePipelineState();

        _vertexArray!.Bind();
        _shaderProgram!.Use();

        _context.DrawIndexed(
            (uint)indexCount,
            (uint)startIndex,
            0);
    }

    protected override void DrawBackend(
        int vertexCount,
        int startVertex)
    {
        EnsurePipelineState();

        _vertexArray!.Bind();
        _shaderProgram!.Use();

        _context.Draw(
            (uint)vertexCount,
            (uint)startVertex);
    }

    protected override void ResetBackend()
    {
        _shaderProgram?.StopUsing();
        _vertexArray?.Unbind();

        _shaderProgram = null;
        _vertexArray = null;
    }

    protected override void DisposeBackend()
    {
        ResetBackend();
    }

    private void EnsurePipelineState()
    {
        if (_vertexArray is null)
        {
            throw new InvalidOperationException(
                "No vertex array has been selected.");
        }

        if (_shaderProgram is null)
        {
            throw new InvalidOperationException(
                "No shader program has been selected.");
        }

        if (!_shaderProgram.IsLinked)
        {
            throw new InvalidOperationException(
                "The selected shader program has not been linked.");
        }
    }
}