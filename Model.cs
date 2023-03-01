using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Opgave_1___OpenTK
{
    public class Model
    {
        public Transform transform;
        public Vector3[] Vertices { get; private set; }
        public int[] Indices { get; private set; }
        public int VaoHandle { get; internal set; }
        public int NumElements { get; internal set; }

        protected int vertexArrayObject;
        protected int elementBufferObject;
        protected int vertexBufferObject;

        private int vaoHandle, vboHandle, eboHandle;
        private int numElements;

        public Model(Vector3[] vertices, int[] indices)
        {
            Vertices = vertices;
            Indices = indices;
            transform = new Transform();

            GenerateBuffers(); //setup based on vertices, indices, etc
        }

        public Model()
        {

            GenerateBuffers(); //setup based on vertices, indices, etc
        }

        public virtual void Draw()
        {
            GL.BindVertexArray(vertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, Indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        protected virtual void GenerateBuffers()
        {
            // Create vertex buffer
            GL.GenBuffers(1, out vboHandle);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * Vector3.SizeInBytes, Vertices, BufferUsageHint.StaticDraw);

            // Create element buffer
            GL.GenBuffers(1, out eboHandle);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboHandle);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Indices.Length * sizeof(int), Indices, BufferUsageHint.StaticDraw);
            numElements = Indices.Length;

            // Create vertex array object
            GL.GenVertexArrays(1, out vaoHandle);
            GL.BindVertexArray(vaoHandle);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vector3.SizeInBytes, 0);

            return;
        }

    }
}
