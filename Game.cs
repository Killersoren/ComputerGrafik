﻿using System;
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

        private int vaoHandle, vboHandle, eboHandle;
        private int numElements;


        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            GL.ClearColor(0.5f, 0.2f, 0.7f, 1.0f);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Matrix4 view = Matrix4.CreateTranslation(0.0f, 0, -3f);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(60.0f),
            (float)Size.X / (float)Size.Y, 0.3f, 1000.0f);
            gameObjects.ForEach(x => x.Draw(view * projection));


            ////3D model test stuff
            //GL.BindVertexArray(vaoHandle);
            //GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboHandle);
            //GL.DrawElements(PrimitiveType.Triangles, numElements, DrawElementsType.UnsignedInt, 0);


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


           // GameObject cam = new GameObject(null, this);
            //cam.AddComponent<Camera>(60.0f, (float)Size.X, (float)Size.Y, 0.3f, 1000.0f);
            //camera = cam.GetComponent<Camera>();
          //  gameObjects.Add(cam);
            GL.Enable(EnableCap.DepthTest);
            // watch.Start();



            //3D model test stuff
            var loader = new ObjLoader();

            var model = loader.Load("Models/cactus.obj");
            //var model = loader.Load("Models/Cup.obj");

            Renderer rendModel = new Renderer(mat, model);

        //    GameObject modelGameObject = new GameObject(model, this);

            GameObject modelGameObject = new GameObject(rendModel, this);


            gameObjects.Add(modelGameObject);

            //// Create vertex buffer
            //GL.GenBuffers(1, out vboHandle);
            //GL.BindBuffer(BufferTarget.ArrayBuffer, vboHandle);
            //GL.BufferData(BufferTarget.ArrayBuffer, model.Vertices.Length * Vector3.SizeInBytes, model.Vertices, BufferUsageHint.StaticDraw);

            //// Create element buffer
            //GL.GenBuffers(1, out eboHandle);
            //GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboHandle);
            //GL.BufferData(BufferTarget.ElementArrayBuffer, model.Indices.Length * sizeof(int), model.Indices, BufferUsageHint.StaticDraw);
            //numElements = model.Indices.Length;

            //// Create vertex array object
            //GL.GenVertexArrays(1, out vaoHandle);
            //GL.BindVertexArray(vaoHandle);
            //GL.EnableVertexAttribArray(0);
            //GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vector3.SizeInBytes, 0);

            model.transform.Position = (-1, 0, 0);
            //Console.WriteLine(model.transform.Position);

            Console.WriteLine("elements count " + model.NumElements);


            // Load shaders
            shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
            shader.Use();
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
