using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;



namespace Opgave_1___OpenTK
{
    internal class Game : GameWindow
    {
        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            GL.ClearColor(0.5f, 0.2f, 0.7f, 1.0f);
        }



        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.BindVertexArray(vertexArrayObject);
            // GL.DrawArrays(PrimitiveType.TriangleFan, 0, vertices.Length/3);
            GL.DrawElements(PrimitiveType.TriangleFan, indices.Length, DrawElementsType.UnsignedInt, 0);
            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            KeyboardState input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
        }

        uint[] indices =
        {
            0, 1, 3,// first triangle
            1, 2, 3 // second triangle
        };

        //float[] vertices =
        //{
        //    0.5f, 0.5f, 0.0f,// top right
        //    0.5f, -0.5f, 0.0f,// bottom right
        //    -0.5f, -0.5f, 0.0f,// bottom left
        //    -0.5f, 0.5f, 0.0f // top left
        //};

        float[] vertices =
        {
            0.5f, 0.5f, 0.0f,   1.0f, 1.0f,     // top right
            0.5f, -0.5f, 0.0f,  1.0f, 0.0f,     // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f,     // bottom left
            -0.5f, 0.5f, 0.0f,   0.0f, 1.0f     // top left
        };


        //private readonly float[] vertices/*TriangleWithCol*/ =
        //{
        //    // positions            // colors
        //    0.5f, -0.5f, 0.0f,   1.0f, 0.0f, 0.0f, // bottom right
        //    -0.5f, -0.5f, 0.0f,  0.0f, 1.0f, 0.0f, // bottom left
        //    0.0f, 0.5f, 0.0f,    0.0f, 0.0f, 1.0f, // top
        //};

        //uint[] indices/*Triangle*/ =
        //{
        //    0, 1, 2, // first triangle
        //};


        private int vertexArrayObject;
        int VertexBufferObject;
        int elementBufferObject;
        Shader shader;
        Texture texture;
        protected override void OnLoad()
        {
            base.OnLoad();

            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            //GL.EnableVertexAttribArray(0);

            shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
            shader.Use();
            Console.WriteLine("shader used");

            GL.EnableVertexAttribArray(shader.GetAttribLocation("aPosition"));

            var greenValue = 1.0f;
            int vertexColorLocation = GL.GetUniformLocation(shader.Handle, "ourColor");
            GL.Uniform4(vertexColorLocation, 0.0f, greenValue, 0.0f, 1.0f);

            

            elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            texture = new Texture("Textures/wall.jpg");
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
        }


    }



}

