using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opgave_1___OpenTK.RendererStuff
{
    public class Mesh
    {
        protected int vertexArrayObject;
        protected int elementBufferObject;
        protected int vertexBufferObject;

        //protected virtual float[] vertices =
        //{
        ////Position Texture coordinates
        //0.5f, 0.5f, 0.0f, 1.0f, 1.0f, // top right
        //0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
        //-0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
        //-0.5f, 0.5f, 0.0f, 0.0f, 1.0f // top left
        //};

        //protected virtual uint[] indices =
        //{
        //    0, 1, 3, // first triangle
        //    1, 2, 3 // second triangle
        //};

        protected virtual float[] Vertices { get;}
        protected virtual float[] Indices { get; }


        float[] vertices =
{
            //Position          Texture coordinates
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };
        uint[] indices =
        {
            0, 1, 3,   // first triangle
            1, 2, 3    // second triangle
        };

        //Position Texture coordinates
        //0.5f, 0.5f, 0.0f, 1.0f, 1.0f, // top right
        //0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
        //-0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
        //-0.5f, 0.5f, 0.0f, 0.0f, 1.0f // top left

        //0, 1, 3, // first triangle
        //1, 2, 3 // second triangle



        public Mesh()
        {
            GenerateBuffers(); //setup based on vertices, indices, etc


        }
        public virtual void Draw()
        {
            GL.BindVertexArray(vertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        protected virtual void GenerateBuffers()
        {
            //vertexBufferObject = GL.GenBuffer();
            //GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            //GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            //vertexArrayObject = GL.GenVertexArray();
            //GL.BindVertexArray(vertexArrayObject);
            //GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            //GL.EnableVertexAttribArray(0);
            //GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            //GL.EnableVertexAttribArray(1);

            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, (int)vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float),
                vertices, BufferUsageHint.StaticDraw);
            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, (int)elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            Console.WriteLine("heo2");
        }
    }
}
