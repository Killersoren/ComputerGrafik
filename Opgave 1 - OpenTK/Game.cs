using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using Opgave_1___OpenTK.RendererStuff;
using ClassLibrary;

namespace Opgave_1___OpenTK
{
    public class Game : GameWindow
    {
        #region Fields
        private Texture texture0;
        private Texture texture1;

        private Model3d enemyModel;
        private Shader3d enemyShader;
        private Texture3d enemyTexture;

        private Camera camera;
        private bool firstMove = true;
        private Vector2 lastPosition; // The camera's last position
        const float mouseSensitivity = 0.2f;


        private float fieldOfView = 60.0f; // Determines how much of the scene i visible through the camera lens.
        private float nearClipPlane = 0.3f; // The distance from the camera to the near clipping plane. It's the closest distance from the camera at which objects will be rendered.
        private float farClipPlane = 1000.0f; // The distance from the camera to the far clipping plane. The farthest distance from the camera at which objects will be rendered

        List<GameObject> gameObjects = new List<GameObject>();
        #endregion

        #region Constructor
        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            GL.ClearColor(0.5f, 0.2f, 0.7f, 1.0f);
        }
        #endregion

        #region Methods
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
            gameObjects.Add(cube);
            cube.transform.Position = (2.0f, 0.5f, 0.0f);
            GL.Enable(EnableCap.DepthTest);

            // Load 3D model, texture and shader
            enemyModel = new Model3d("Models/Enemy/EnemyBug.fbx");
            enemyTexture = Texture3d.LoadFromFile("Models/Enemy/EnemyBug_color.png");
            enemyShader = new Shader3d("Shaders/shader2.vert", "Shaders/shader2.frag");

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

            // ForEach-loop which draws every camera in the list of gameObjects (with the camera's view matrix)
            gameObjects.ForEach(x => x.Draw(camera.GetViewMatrix()));

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
        #endregion
    }
}

