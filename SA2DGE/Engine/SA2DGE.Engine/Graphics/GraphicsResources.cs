using SA2DGE.Engine.Graphics.Buffers;

namespace SA2DGE.Engine.Graphics;

public abstract class GraphicsResources
{
    public abstract VertexBuffer CreateVertexBuffer(
        int vertexCount,
        int vertexSize);

    public abstract IndexBuffer CreateIndexBuffer(
        int indexCount,
        int indexSizeInBytes = sizeof(uint));

    public abstract VertexArray CreateVertexArray();
}