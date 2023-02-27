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
using Opgave_1___OpenTK.RendererStuff;

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

        List<GameObject> gameObjects = new List<GameObject> ();

        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            GL.ClearColor(0.5f, 0.2f, 0.7f, 1.0f);
        }



        //protected override void OnRenderFrame(FrameEventArgs args)
        //{
        //    base.OnRenderFrame(args);
        //    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        //   // GL.Clear(ClearBufferMask.ColorBufferBit);
        //    GL.BindVertexArray(vertexArrayObject);
        //    // GL.DrawArrays(PrimitiveType.TriangleFan, 0, vertices.Length/3);
        //    texture0.Use(TextureUnit.Texture0);
        //    texture1.Use(TextureUnit.Texture1);
        //    shader.Use();
        //    //GL.DrawElements(PrimitiveType.TriangleFan, indices.Length, DrawElementsType.UnsignedInt, 0);
        //    GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
        //    SwapBuffers();
        //}

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Matrix4 view = Matrix4.CreateTranslation(0.0f, 0, -3f);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(60.0f),
            (float)Size.X / (float)Size.Y, 0.3f, 1000.0f);
            gameObjects.ForEach(x => x.Draw(view * projection));
            SwapBuffers();
        }



        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            gameObjects.ForEach(x => x.Update(args));
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
        }


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
            texture0 = new Texture("Textures/wall.jpg");
            texture1 = new Texture("Textures/TorbenHD.png");
            Dictionary<string, object> uniforms = new Dictionary<string, object>();
            uniforms.Add("texture0", texture0);
            uniforms.Add("texture1", texture1);
            Material mat = new Material("Shaders/shader.vert", "Shaders/shader.frag", uniforms);
            Renderer rend = new Renderer(mat, new TriangleMesh());
            Renderer rend2 = new Renderer(mat, new CubeMesh());
            GameObject triangle = new GameObject(rend, this);
            gameObjects.Add(triangle);
            GameObject cube = new GameObject(rend2, this);
            //cube.AddComponent<MoveLeftRightBehaviour>();
            //cube.AddComponent<MoveUpDownBehaviour>();
            gameObjects.Add(cube);
            cube.transform.Position = (1,0,0);


            GameObject cam = new GameObject(null, this);
            //cam.AddComponent<Camera>(60.0f, (float)Size.X, (float)Size.Y, 0.3f, 1000.0f);
            //camera = cam.GetComponent<Camera>();
            gameObjects.Add(cam);
            GL.Enable(EnableCap.DepthTest);
           // watch.Start();
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

