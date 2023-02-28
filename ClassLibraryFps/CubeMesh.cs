using OpenTK.Graphics.OpenGL4;

namespace ClassLibraryFps
{
    public class CubeMesh : Mesh
    {
        static int vertexArrayObject;
        static int vertexBufferObject;
        private static bool buffersCreated;
        protected override float[] Vertices => new float[]
        {
            // Back face
            -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,

            // Front face
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f, // modified texture coordinate (from "0.0f, 0.0f," to "0.0f, 1.0f")
             0.5f, -0.5f,  0.5f,  1.0f, 0.0f, // modified texture coordinate (from "1.0f, 0.0f," to "1.0f, 1.0f")
             0.5f,  0.5f,  0.5f,  1.0f, 1.0f, // modified texture coordinate (from "1.0f, 1.0f," to "1.0f, 0.0f")
             0.5f,  0.5f,  0.5f,  1.0f, 1.0f, // modified texture coordinate (from "1.0f, 1.0f," to "1.0f, 0.0f")
            -0.5f,  0.5f,  0.5f,  0.0f, 1.0f, // modified texture coordinate (from "0.0f, 1.0f," to "0.0f, 0.0f")
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f, // modified texture coordinate (from "0.0f, 0.0f," to "0.0f, 1.0f")


            //// Left face
            //-0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
            //-0.5f,  0.5f, -0.5f,  1.0f, 1.0f, // modified texture coordinate (from "1.0f, 1.0f," to "0.0f, 0.0f")
            //-0.5f, -0.5f, -0.5f,  0.0f, 1.0f, 
            //-0.5f, -0.5f, -0.5f,  0.0f, 1.0f, 
            //-0.5f, -0.5f,  0.5f,  0.0f, 0.0f, // modified texture coordinate (from "0.0f, 0.0f," to "1.0f, 1.0f")
            //-0.5f,  0.5f,  0.5f,  1.0f, 0.0f, // modified texture coordinate (from "1.0f, 0.0f," to "1.0f, 1.0f")

            //// Left face (rotated 90 degrees clockwise)
            //-0.5f,  0.5f,  0.5f,  0.0f, 0.0f,
            //-0.5f,  0.5f, -0.5f,  1.0f, 0.0f,
            //-0.5f, -0.5f, -0.5f,  1.0f, 1.0f,
            //-0.5f, -0.5f, -0.5f,  1.0f, 1.0f,
            //-0.5f, -0.5f,  0.5f,  0.0f, 1.0f,
            //-0.5f,  0.5f,  0.5f,  0.0f, 0.0f,

            // Left face (rotated 90 degrees counter clockwise)
            -0.5f,  0.5f,  0.5f,  0.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f, 1.0f,


             //// Right face
             //0.5f,  0.5f,  0.5f,  1.0f, 0.0f, // modified texture coordinate (from "1.0f, 0.0f," to "0.0f, 0.0f")
             //0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
             //0.5f, -0.5f, -0.5f,  0.0f, 1.0f, // modified texture coordinate (from "0.0f, 1.0f," to "1.0f, 0.0f")
             //0.5f, -0.5f, -0.5f,  0.0f, 1.0f, // modified texture coordinate (from "0.0f, 1.0f," to "1.0f, 1.0f")
             //0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
             //0.5f,  0.5f,  0.5f,  1.0f, 0.0f, // modified texture coordinate (from "1.0f, 0.0f," to "0.0f, 1.0f")

             //// Right face (rotated 90 degrees clockwise)
             //0.5f,  0.5f,  0.5f,  0.0f, 0.0f,
             //0.5f,  0.5f, -0.5f,  1.0f, 0.0f,
             //0.5f, -0.5f, -0.5f,  1.0f, 1.0f,
             //0.5f, -0.5f, -0.5f,  1.0f, 1.0f,
             //0.5f, -0.5f,  0.5f,  0.0f, 1.0f,
             //0.5f,  0.5f,  0.5f,  0.0f, 0.0f,

             // Right face (rotated 90 degrees counter clockwise)
             0.5f,  0.5f,  0.5f,  0.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f, 1.0f,


             // Bottom face
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,

            // Top face
            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f, // modified texture coordinate (from "0.0f, 1.0f," to "1.0f, 1.0f")
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f, // modified texture coordinate (from "1.0f, 1.0f," to "0.0f, 1.0f")
             0.5f,  0.5f,  0.5f,  1.0f, 0.0f, // modified texture coordinate (from "1.0f, 0.0f," to "0.0f, 0.0f")
             0.5f,  0.5f,  0.5f,  1.0f, 0.0f, // modified texture coordinate (from "1.0f, 0.0f," to "0.0f, 0.0f")
            -0.5f,  0.5f,  0.5f,  0.0f, 0.0f, // modified texture coordinate (from "0.0f, 0.0f," to "1.0f, 0.0f")
            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f  // modified texture coordinate (from "0.0f, 1.0f," to "1.0f, 1.0f")
        };

        protected override void GenerateBuffers()
        {
            if (buffersCreated)
            {
                return;
            }
            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float),
                Vertices, BufferUsageHint.StaticDraw);
            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            buffersCreated = true;
        }
        public override void Draw()
        {
            GL.BindVertexArray(vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

        }
    }
}