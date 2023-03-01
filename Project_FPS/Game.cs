using ClassLibrary;
using ClassLibraryFps;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Input;
using System.Diagnostics;

namespace Project_FPS
{
    public class Game : GameWindow
    {
        #region Fields
        NativeWindowSettings settings;

        Color4 clearColor = new Color4(0.2f, 0.3f, 0.3f, 1.0f); // Background color

        private bool firstMove = true;
        private Vector2 lastPosition; // The camera's last position

        private float fieldOfView = 60.0f; // Determines how much of the scene i visible through the camera lens.
        private float nearClipPlane = 0.3f; // The distance from the camera to the near clipping plane. It's the closest distance from the camera at which objects will be rendered.
        private float farClipPlane = 1000.0f; // The distance from the camera to the far clipping plane. The farthest distance from the camera at which objects will be rendered

        Stopwatch stopwatch = new Stopwatch();

        Texture texture0, texture1;

        List<GameObject> gameObjects = new List<GameObject>();

        Camera camera;
        #endregion

        #region Constructor
        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            // Set the color of the background
            GL.ClearColor(clearColor);
            // Used to access the settings for the game window
            settings = nativeWindowSettings;
            // The game window is centered in the center of the screen
            this.CenterWindow();
        }
        #endregion

        #region Methods
        protected override void OnLoad()
        {
            base.OnLoad();

            // Creates two textures for the CubeMesh
            texture0 = new Texture("Textures/box_side.png");
            texture1 = new Texture("Textures/box_x2.png");
            // Creates a Dictionary
            Dictionary<string, object> uniforms = new Dictionary<string, object>();
            uniforms.Add("texture0", texture0); // Add texture0 to the dictionary
            uniforms.Add("texture1", texture1); // Add texture1 to the dictionary

            // Creates a new Material object by passing the two shader files and the uniforms dictionary as parameters
            Material material = new Material("Shaders/shader2.vert", "Shaders/shader2.frag", uniforms);

            // Creates a Renderer object by passing the Material object and a CubeMesh object as parameters
            Renderer renderCube = new Renderer(material, new CubeMesh());
            
            // Creates a cube GameObject by passing the Renderer object and the current GameWindow object (this)
            GameObject cube = new GameObject(renderCube, this);
            gameObjects.Add(cube); // Add the cube to the List of GameObjects

            // Creates a camera GameObject by passing null and the current GameWindow object (this)
            GameObject cam = new GameObject(null, this);
            // AddComponent method adds a new Camera component to a GameObject
            // The Camera's constructor takes 4 parameters: fieldOfView, aspectRatio(x,y), nearClipPlane, farClipPlane
            // The aspectRatio here is the Size of the GameWindow's window size. It is the ratio of the width and height of the camera's view.
            cam.AddComponent<Camera>(fieldOfView, (float)Size.X, (float)Size.Y, nearClipPlane, farClipPlane);
            // The Camera class is set equal to the camera GameObject
            camera = cam.GetComponent<Camera>();
            // The camera is added to the list of GameObjects
            gameObjects.Add(cam);

            // DepthTest is a technique used to ensure objects are rendered in the correct order
            GL.Enable(EnableCap.DepthTest);

            stopwatch.Start(); // Start stopwatch. Used to measure elapsed time in the game
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GameObject gameObject;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            gameObjects.ForEach(x => x.Draw(camera.GetViewMatrix()));

            SwapBuffers();
        }

        //protected override void OnResize(ResizeEventArgs e)
        //{
        //    base.OnResize(e);

        //    GL.Viewport(0, 0, Size.X, Size.Y);
        //}

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            const float sensitivity = 0.2f;

            gameObjects.ForEach(x => x.Update(args));

            KeyboardState input = KeyboardState;
            MouseState mouse = MouseState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            if (firstMove)
            {
                lastPosition = new Vector2(mouse.X, mouse.Y);
                firstMove = false;
            }
            else
            {
                var deltaX = mouse.X - lastPosition.X;
                var deltaY = mouse.Y - lastPosition.Y;
                lastPosition = new Vector2(mouse.X, mouse.Y);

                camera.Yaw += deltaX * sensitivity;
                camera.Pitch -= deltaY * sensitivity;
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs args)
        {
            base.OnMouseWheel(args);

            camera.Fov -= args.OffsetY;
        }

        #endregion



        #region ClassLibrary Project
        //private Model crateModel;
        //private Shader crateShader;
        //private Texture crateTexture;

        //private Camera camera;

        //private bool firstMove = true;

        //private Vector2 lastPosition;

        //public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        //    : base(gameWindowSettings, nativeWindowSettings)
        //{
        //}

        //protected override void OnLoad()
        //{
        //    base.OnLoad();

        //    GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

        //    GL.Enable(EnableCap.DepthTest);

        //    // Here we import the model object, texture and define our Shader.
        //    // For now we are just using a simple shader which just samples the texture.
        //    crateModel = new Model("Resources/Crate/woodencrate_dir.fbx");
        //    crateTexture = Texture.LoadFromFile("Resources/Crate/boxtexture_dir.png");
        //    crateShader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");

        //    camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);

        //    CursorState = CursorState.Grabbed;
        //}

        //protected override void OnRenderFrame(FrameEventArgs args)
        //{
        //    base.OnRenderFrame(args);

        //    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        //    // First we setup the shader, including the texture uniform and then call the Draw() method on the imported model to draw all the contained meshes
        //    crateShader.Use();
        //    crateShader.SetMatrix4("model", Matrix4.Identity);
        //    crateShader.SetMatrix4("view", camera.GetViewMatrix());
        //    crateShader.SetMatrix4("projection", camera.GetProjectionMatrix());
        //    crateTexture.Use(TextureUnit.Texture0);
        //    crateShader.SetInt("texture0", 0);

        //    // For each mesh in the model, bind the VAO (that contains all of the vertices and indices) and draw the triangles
        //    foreach (Mesh mesh in crateModel.meshes)
        //    {
        //        GL.BindVertexArray(mesh.VAO);
        //        GL.DrawElements(PrimitiveType.Triangles, mesh.indicesCount, DrawElementsType.UnsignedInt, 0);
        //        GL.BindVertexArray(0);
        //    }

        //    SwapBuffers();
        //}

        //protected override void OnUpdateFrame(FrameEventArgs args)
        //{
        //    base.OnUpdateFrame(args);

        //    if (!IsFocused)
        //    {
        //        return;
        //    }

        //    KeyboardState input = KeyboardState;

        //    if (input.IsKeyDown(Keys.Escape))
        //    {
        //        Close();
        //    }

        //    const float cameraSpeed = 1.5f;
        //    const float sensitivity = 0.2f;

        //    if (input.IsKeyDown(Keys.W))
        //    {
        //        camera.Position += camera.Front * cameraSpeed * (float)args.Time; // Forward
        //    }
        //    if (input.IsKeyDown(Keys.S))
        //    {
        //        camera.Position -= camera.Front * cameraSpeed * (float)args.Time; // Backwards
        //    }
        //    if (input.IsKeyDown(Keys.A))
        //    {
        //        camera.Position -= camera.Right * cameraSpeed * (float)args.Time; // Left
        //    }
        //    if (input.IsKeyDown(Keys.D))
        //    {
        //        camera.Position += camera.Right * cameraSpeed * (float)args.Time; // Right
        //    }
        //    if (input.IsKeyDown(Keys.Space))
        //    {
        //        camera.Position += camera.Up * cameraSpeed * (float)args.Time; // Up
        //    }
        //    if (input.IsKeyDown(Keys.LeftShift))
        //    {
        //        camera.Position -= camera.Up * cameraSpeed * (float)args.Time; // Down
        //    }

        //    var mouse = MouseState;

        //    if (firstMove)
        //    {
        //        lastPosition = new Vector2(mouse.X, mouse.Y);
        //        firstMove = false;
        //    }
        //    else
        //    {
        //        var deltaX = mouse.X - lastPosition.X;
        //        var deltaY = mouse.Y - lastPosition.Y;
        //        lastPosition = new Vector2(mouse.X, mouse.Y);

        //        camera.Yaw += deltaX * sensitivity;
        //        camera.Pitch -= deltaY * sensitivity;
        //    }
        //}

        //protected override void OnMouseWheel(MouseWheelEventArgs args)
        //{
        //    base.OnMouseWheel(args);

        //    camera.Fov -= args.OffsetY;
        //}

        //protected override void OnResize(ResizeEventArgs args)
        //{
        //    base.OnResize(args);

        //    GL.Viewport(0, 0, Size.X, Size.Y);
        //    camera.AspectRatio = Size.X / (float)Size.Y;
        //}
        #endregion
    }
}
