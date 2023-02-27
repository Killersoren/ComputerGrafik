using ClassLibrary;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Project_FPS
{
    public class Game : GameWindow
    {
        private Model crateModel;
        private Shader crateShader;
        private Texture crateTexture;

        private Camera camera;

        private bool firstMove = true;

        private Vector2 lastPosition;

        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            GL.Enable(EnableCap.DepthTest);

            // Here we import the model object, texture and define our Shader.
            // For now we are just using a simple shader which just samples the texture.
            crateModel = new Model("Resources/Crate/woodencrate_dir.fbx");
            crateTexture = Texture.LoadFromFile("Resources/Crate/boxtexture_dir.png");
            crateShader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");

            camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);

            CursorState = CursorState.Grabbed;
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // First we setup the shader, including the texture uniform and then call the Draw() method on the imported model to draw all the contained meshes
            crateShader.Use();
            crateShader.SetMatrix4("model", Matrix4.Identity);
            crateShader.SetMatrix4("view", camera.GetViewMatrix());
            crateShader.SetMatrix4("projection", camera.GetProjectionMatrix());
            crateTexture.Use(TextureUnit.Texture0);
            crateShader.SetInt("texture0", 0);

            // For each mesh in the model, bind the VAO (that contains all of the vertices and indices) and draw the triangles
            foreach (Mesh mesh in crateModel.meshes)
            {
                GL.BindVertexArray(mesh.VAO);
                GL.DrawElements(PrimitiveType.Triangles, mesh.indicesCount, DrawElementsType.UnsignedInt, 0);
                GL.BindVertexArray(0);
            }

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (!IsFocused)
            {
                return;
            }

            KeyboardState input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            const float cameraSpeed = 1.5f;
            const float sensitivity = 0.2f;

            if (input.IsKeyDown(Keys.W))
            {
                camera.Position += camera.Front * cameraSpeed * (float)args.Time; // Forward
            }
            if (input.IsKeyDown(Keys.S))
            {
                camera.Position -= camera.Front * cameraSpeed * (float)args.Time; // Backwards
            }
            if (input.IsKeyDown(Keys.A))
            {
                camera.Position -= camera.Right * cameraSpeed * (float)args.Time; // Left
            }
            if (input.IsKeyDown(Keys.D))
            {
                camera.Position += camera.Right * cameraSpeed * (float)args.Time; // Right
            }
            if (input.IsKeyDown(Keys.Space))
            {
                camera.Position += camera.Up * cameraSpeed * (float)args.Time; // Up
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                camera.Position -= camera.Up * cameraSpeed * (float)args.Time; // Down
            }

            var mouse = MouseState;

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

        protected override void OnResize(ResizeEventArgs args)
        {
            base.OnResize(args);

            GL.Viewport(0, 0, Size.X, Size.Y);
            camera.AspectRatio = Size.X / (float)Size.Y;
        }
    }
}
