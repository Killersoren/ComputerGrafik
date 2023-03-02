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
using OpenTK.Input;
using OpenTK.Windowing.Common.Input;
using Opgave_1___OpenTK.RendererStuff;
using ClassLibrary;
//using Mesh3d = ClassLibrary.Mesh3d;
using System.Diagnostics;

namespace Opgave_1___OpenTK
{
    public class Game : GameWindow
    {
        private Texture texture0;
        private Texture texture1;

        private Model3d enemyModel;
        private Shader3d enemyShader;
        private Texture3d enemyTexture;

        //float rotation = 45;

        //float rotationX3D = -55;

        //float rotationY3D = -55;

        //float rotationZ3D = -55;


        //float scale3D = 1;
        //Stopwatch stopwatch = new Stopwatch();

        //float position3D;

        private Camera camera;
        private bool firstMove = true;
        private Vector2 lastPosition; // The camera's last position
        const float mouseSensitivity = 0.2f;


        private float fieldOfView = 60.0f; // Determines how much of the scene i visible through the camera lens.
        private float nearClipPlane = 0.3f; // The distance from the camera to the near clipping plane. It's the closest distance from the camera at which objects will be rendered.
        private float farClipPlane = 1000.0f; // The distance from the camera to the far clipping plane. The farthest distance from the camera at which objects will be rendered

        List<GameObject> gameObjects = new List<GameObject>();

        //private int vaoHandle, vboHandle, eboHandle;
        //private int numElements;


        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            GL.ClearColor(0.5f, 0.2f, 0.7f, 1.0f);
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            // In this state, the cursor is hidden and its
            // position is locked to the center of the window
            CursorState = CursorState.Grabbed;

            // Load cube and triangle, textures and shaders
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
            triangle.transform.Position = (-2.0f, 0.5f, 0.0f);
            GameObject cube = new GameObject(rend2, this);
            //cube.AddComponent<MoveLeftRightBehaviour>();
            //cube.AddComponent<MoveUpDownBehaviour>();
            gameObjects.Add(cube);
            cube.transform.Position = (2.0f, 0.5f, 0.0f);
            GL.Enable(EnableCap.DepthTest);
            // watch.Start();

            // Load 3D model, texture and shader
            enemyModel = new Model3d("Models/Enemy/EnemyBug.fbx");
            enemyTexture = Texture3d.LoadFromFile("Models/Enemy/EnemyBug_color.png");
            enemyShader = new Shader3d("Shaders/shader2.vert", "Shaders/shader2.frag");

            //Renderer rendModel2 = new Renderer(mat, enemyModel);

            //GameObject cactusGameObject = new GameObject(enemyModel, this);


            // Creates a camera GameObject by passing the current GameWindow object (this)
            GameObject cam = new GameObject(this);
            // AddComponent method adds a new Camera component to a GameObject
            // The Camera's constructor takes 4 parameters: fieldOfView, aspectRatio(x,y), nearClipPlane, farClipPlane
            // The aspect ratio here is the Size of the GameWindow's window size.
            // The aspect ratio is the ratio of the width and height of the camera's view (Size.X, Size.Y).
            cam.AddComponent<Camera>(fieldOfView, (float)Size.X, (float)Size.Y, nearClipPlane, farClipPlane);
            // The Camera class is set equal to the camera GameObject
            camera = cam.GetComponent<Camera>();
            // The camera is added to the list of GameObjects
            gameObjects.Add(cam);


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


            // Load shaders
            //shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
            //shader.Use();
            enemyShader.Use();
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            // Binds an empty buffer to the ArrayBuffer target, which unbinds any previous bound buffer.
            // This is good practice. It ensures that any resources bound to the graphics pipeline are
            // released when the game window is closed.
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //Matrix4 view = Matrix4.CreateTranslation(0.0f, 0, -3f);
            //Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(60.0f),
            //(float)Size.X / (float)Size.Y, 0.3f, 1000.0f);

            // ForEach-loop which draws every camera in the list of gameObjects (with the camera's view matrix)
            gameObjects.ForEach(x => x.Draw(camera.GetViewMatrix()));



            ////3D model test stuff
            //GL.BindVertexArray(vaoHandle);
            //GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboHandle);
            //GL.DrawElements(PrimitiveType.Triangles, numElements, DrawElementsType.UnsignedInt, 0);

            // Sets the uniform variable model to the identity matrix.
            // Translate, scale and rotate is set to the enemy objects default position
            enemyShader.SetMatrix4("model", Matrix4.Identity);
            // Sets the uniform variable view to the view matrix.
            // Transforms the vertices of the object from world space to camera space.
            enemyShader.SetMatrix4("view", camera.GetViewMatrix2());
            // Sets the uniform variable projection to the projection matrix. 
            // Transforms the vertices from camera space to clip space (the visible part of the scene).
            enemyShader.SetMatrix4("projection", camera.GetProjectionMatrix());
            // Binds enemy texture to texture unit 0.
            enemyTexture.Use(TextureUnit.Texture0);
            // Sets the shader to sample texture unit 0.
            enemyShader.SetInt("texture0", 0);

            foreach (Mesh3d mesh in enemyModel.meshes)
            {
                // Binds Vertex Array Object (VAO) with the enemy mesh
                GL.BindVertexArray(mesh.VAO);
                // Draws the mesh on the screen.
                GL.DrawElements(PrimitiveType.Triangles, mesh.indicesCount, DrawElementsType.UnsignedInt, 0);
                // Specifies we draw the first index.
                GL.BindVertexArray(0);
            }



            // Swaps the front and back buffers of the game window
            // to display the rendered frame on the screen
            SwapBuffers();
        }



        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            const float sensitivity = 0.2f;

            // ForEach-loop which updates every game object in the list of gameObjects
            gameObjects.ForEach(x => x.Update(args));

            KeyboardState input = KeyboardState;
            MouseState mouse = MouseState;
            Vector2 currentPosition = new Vector2(mouse.X, mouse.Y);


            // Press Escape to close the game window
            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            // Indicates if this is the first time the mouse has been moved.
            if (firstMove)
            {
                lastPosition = currentPosition;
                firstMove = false;
            }
            else
            {
                // Calculates the change in the mouse position since the last frame.
                var deltaX = currentPosition.X - lastPosition.X;
                var deltaY = currentPosition.Y - lastPosition.Y;
                // lastPosition is set to the current mouse position.
                lastPosition = currentPosition;

                // Updates the camera's orientation on the x- and y-axis and is multiplied by mouse sensitivity
                camera.Yaw += deltaX * mouseSensitivity;
                camera.Pitch -= deltaY * mouseSensitivity;
            }
        }

        //protected override void OnResize(ResizeEventArgs e)
        //{
        //    base.OnResize(e);

        //    GL.Viewport(0, 0, e.Width, e.Height);
        //}


        //float[] vertices =
        //    {
        //        -0.5f, -0.5f, -0.5f, 0.0f, 0.0f,
        //        0.5f, -0.5f, -0.5f, 1.0f, 0.0f,
        //        0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
        //        0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
        //        -0.5f, 0.5f, -0.5f, 0.0f, 1.0f,
        //        -0.5f, -0.5f, -0.5f, 0.0f, 0.0f,
        //        -0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
        //        0.5f, -0.5f, 0.5f, 1.0f, 0.0f,
        //        0.5f, 0.5f, 0.5f, 1.0f, 1.0f,
        //        0.5f, 0.5f, 0.5f, 1.0f, 1.0f,
        //        -0.5f, 0.5f, 0.5f, 0.0f, 1.0f,
        //        -0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
        //        -0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
        //        -0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
        //        -0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
        //        -0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
        //        -0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
        //        -0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
        //        0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
        //        0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
        //        0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
        //        0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
        //        0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
        //        0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
        //        -0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
        //        0.5f, -0.5f, -0.5f, 1.0f, 1.0f,
        //        0.5f, -0.5f, 0.5f, 1.0f, 0.0f,
        //        0.5f, -0.5f, 0.5f, 1.0f, 0.0f,
        //        -0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
        //        -0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
        //        -0.5f, 0.5f, -0.5f, 0.0f, 1.0f,
        //        0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
        //        0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
        //        0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
        //        -0.5f, 0.5f, 0.5f, 0.0f, 0.0f,
        //        -0.5f, 0.5f, -0.5f, 0.0f, 1.0f
        //    };


        //private int vertexArrayObject;
        //int VertexBufferObject;
        //int elementBufferObject;

        protected override void OnMouseWheel(MouseWheelEventArgs args)
        {
            base.OnMouseWheel(args);

            float maxFov = fieldOfView;
            float minFov = 10.0f;

            // Checks if the Y offset is less than 0 and Fov is less than max Fov
            if (args.OffsetY <= 0f && camera.FOV <= maxFov)
            {
                // Sets Fov equal to max Fov to prevent zooming further out than max Fov
                camera.FOV = maxFov;
            }
            // Checks if Y offset is bigger than 0 and Fov is bigger than min Fov
            else if (args.OffsetY >= 0f && camera.FOV >= minFov)
            {
                // Sets Fov equal to min Fov to prevent zooming further in than min Fov
                camera.FOV += (float)args.OffsetY;
                camera.FOV = minFov;
            }
        }


        //void CalculateAndSetTransform()
        //{
        //    Matrix4 trs = Matrix4.Identity;
        //    Matrix4 srt = Matrix4.Identity;


        //    Matrix4 t = Matrix4.CreateTranslation(new Vector3(0.5f, 0.5f, 0));
        //    Matrix4 r = Matrix4.CreateRotationZ(rotation);
        //    Matrix4 s = Matrix4.CreateScale(new Vector3(0.5f, 0.5f, 1));

        //    trs = s * r * t;

        //    srt = t * r * s;
        //    shader.SetMatrix("transform", trs);

        //    Matrix4 model = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotationX3D));

        //    Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotationY3D));
        //    Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotationZ3D));


        //    Matrix4 view = Matrix4.CreateTranslation(0.0f, 0.0f, -3f);
        //    Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), (float)Size.X / (float)Size.Y, 0.1f, 100.0f);
        //    //shader.SetMatrix("model", model);
        //    //shader.SetMatrix("view", view);
        //    //shader.SetMatrix("projection", projection);

        //    shader.SetMatrix("mvp", model * view * projection);
        //}

    }



}

