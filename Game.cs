using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;

namespace Opgave_1___OpenTK
{
    internal class Game : GameWindow

    {
        float rotation = 45;

        float rotationX3D = -55;

        float rotationY3D = -55;

        float rotationZ3D = -55;


        float scale3D = 1;

        float position3D;

        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            GL.ClearColor(0.5f, 0.2f, 0.7f, 1.0f);
        }



        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
           // GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.BindVertexArray(vertexArrayObject);
            // GL.DrawArrays(PrimitiveType.TriangleFan, 0, vertices.Length/3);
            texture0.Use(TextureUnit.Texture0);
            texture1.Use(TextureUnit.Texture1);
            shader.Use();
            //GL.DrawElements(PrimitiveType.TriangleFan, indices.Length, DrawElementsType.UnsignedInt, 0);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
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

            //if (input.IsKeyDown(Keys.E))
            //{
            //    rotation -= 0.1f;
            //}

            //else if (input.IsKeyDown(Keys.Q))
            //{
            //    rotation += 0.1f;
            //}

            if (input.IsKeyDown(Keys.E))
            {
                rotationX3D -= 1f;
            }

            else if (input.IsKeyDown(Keys.Q))
            {
                rotationX3D += 1f;
            }

            CalculateAndSetTransform();

        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
        }

        //uint[] indices =
        //{
        //    0, 1, 3,// first triangle
        //    1, 2, 3 // second triangle
        //};

        //float[] vertices =
        //{
        //    0.5f, 0.5f, 0.0f,// top right
        //    0.5f, -0.5f, 0.0f,// bottom right
        //    -0.5f, -0.5f, 0.0f,// bottom left
        //    -0.5f, 0.5f, 0.0f // top left
        //};

        //float[] vertices =
        //{
        //    0.5f, 0.5f, 0.0f,   1.0f, 1.0f,     // top right
        //    0.5f, -0.5f, 0.0f,  1.0f, 0.0f,     // bottom right
        //    -0.5f, -0.5f, 0.0f, 0.0f, 0.0f,     // bottom left
        //    -0.5f, 0.5f, 0.0f,   0.0f, 1.0f     // top left
        //};


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


        float[] vertices =
            {
                -0.5f, -0.5f, -0.5f, 0.0f, 0.0f,
                0.5f, -0.5f, -0.5f, 1.0f, 0.0f,
                0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
                0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
                -0.5f, 0.5f, -0.5f, 0.0f, 1.0f,
                -0.5f, -0.5f, -0.5f, 0.0f, 0.0f,
                -0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
                0.5f, -0.5f, 0.5f, 1.0f, 0.0f,
                0.5f, 0.5f, 0.5f, 1.0f, 1.0f,
                0.5f, 0.5f, 0.5f, 1.0f, 1.0f,
                -0.5f, 0.5f, 0.5f, 0.0f, 1.0f,
                -0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
                -0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
                -0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
                -0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
                -0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
                -0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
                -0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
                0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
                0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
                0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
                0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
                0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
                0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
                -0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
                0.5f, -0.5f, -0.5f, 1.0f, 1.0f,
                0.5f, -0.5f, 0.5f, 1.0f, 0.0f,
                0.5f, -0.5f, 0.5f, 1.0f, 0.0f,
                -0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
                -0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
                -0.5f, 0.5f, -0.5f, 0.0f, 1.0f,
                0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
                0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
                0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
                -0.5f, 0.5f, 0.5f, 0.0f, 0.0f,
                -0.5f, 0.5f, -0.5f, 0.0f, 1.0f
            };


        private int vertexArrayObject;
        int VertexBufferObject;
        int elementBufferObject;
        Shader shader;
        Texture texture0;
        Texture texture1;
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

            texture0 = new Texture("Textures/wall.jpg");
            texture1 = new Texture("Textures/TorbenHD.png");

            shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
            shader.Use();

            shader.SetInt("texture0", 0);
            shader.SetInt("texture1", 1);

            Console.WriteLine("shader used");

            GL.EnableVertexAttribArray(shader.GetAttribLocation("aPosition"));

            var greenValue = 1.0f;
            int vertexColorLocation = GL.GetUniformLocation(shader.Handle, "ourColor");
            GL.Uniform4(vertexColorLocation, 0.0f, greenValue, 0.0f, 1.0f);

            

            //elementBufferObject = GL.GenBuffer();
            //GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            //GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            GL.Enable(EnableCap.DepthTest);

        }

        protected override void OnUnload()
        {
            base.OnUnload();
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
        }

         void CalculateAndSetTransform()
        {
            Matrix4 trs = Matrix4.Identity;
            Matrix4 srt = Matrix4.Identity;


            Matrix4 t = Matrix4.CreateTranslation(new Vector3(0.5f, 0.5f, 0));
            Matrix4 r = Matrix4.CreateRotationZ(rotation);
            Matrix4 s = Matrix4.CreateScale(new Vector3(0.5f, 0.5f, 1));

            trs = s * r * t;

            srt = t * r * s;
            shader.SetMatrix("transform", trs);


            //Matrix4 model = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation3D));

            Matrix4 model = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotationX3D));

            Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotationY3D));
            Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotationZ3D));


            Matrix4 view = Matrix4.CreateTranslation(0.0f, 0.0f, -3f);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), (float)Size.X / (float)Size.Y, 0.1f, 100.0f);
            //shader.SetMatrix("model", model);
            //shader.SetMatrix("view", view);
            //shader.SetMatrix("projection", projection);

            shader.SetMatrix("mvp", model * view * projection);
        }

    }



}

