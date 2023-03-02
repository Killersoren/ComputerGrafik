using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Opgave_1___OpenTK;
using Opgave_1___OpenTK.RendererStuff;

namespace Opgave_1___OpenTK
{
    public class Camera : Behaviour
    {
        #region Fields
        Vector3 front = new Vector3(0.0f, 0.0f, -1.0f);
        Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
        Vector3 right = new Vector3(1.0f, 0.0f, 0.0f);

        private float speed;
        // Jump variables
        private float jumpHeight = 2.0f;
        private float fallSpeed = 1.5f;
        private bool isJumping = false;
        // Crouch variables
        private float standingHeight = 0.0f;
        private float crouchHeight = 0.25f;
        private float heightOffset = 1.0f;
        private bool isCrouching = false;

        // Rotation around the Y axis (radians)
        public float FOV = MathHelper.PiOver2;
        private float aspectX;
        private float aspectY;
        public float near = 0.01f;
        public float far = 100;

        // Rotation around the X axis (radians)
        private float pitch;

        // Rotation around the Y axis (radians)
        private float yaw = -MathHelper.PiOver2; // Without this, you would be started rotated 90 degrees right.
        #endregion

        #region Properties

        // Up-down rotation of the camera
        public float Pitch
        {
            // Converts from radians to degrees(improves performance)
            get => MathHelper.RadiansToDegrees(pitch);
            set
            {
                // Clamps the pitch value between -89 and 89 to prevent the camera from reversing,
                // and some other weird "bugs" when using euler angles for rotation.
                var angle = MathHelper.Clamp(value, -89f, 89f);
                // Converts from degrees to radians (improves performance)
                pitch = MathHelper.DegreesToRadians(angle);
                // Updates the camera's direction based on the new pitch value
                UpdateVectors();
            }
        }

        // Right-left rotation of the camera
        public float Yaw
        {
            // Converts from radians to degrees(improves performance)
            get => MathHelper.RadiansToDegrees(yaw);
            set
            {
                // Converts from degrees to radians (improves performance)
                yaw = MathHelper.DegreesToRadians(value);
                // Updates the camera's direction based on the new yaw value
                UpdateVectors();
            }
        }

        // The field of view (FOV) is the vertical angle of the camera view.
        public float Fov
        {
            // Converts from radians to degrees(improves performance)
            get => MathHelper.RadiansToDegrees(FOV);
            set
            {
                // Clamp the input angle between 1 and 90 degrees.
                var angle = MathHelper.Clamp(value, 1f, 90f);
                // Converts from degrees to radians (improves performance)
                FOV = MathHelper.DegreesToRadians(angle);
            }
        }
        #endregion

        #region Constructor
        public Camera(GameObject gameObject, Game window, float FOV, float aspectX, float aspectY, float near, float far) : base(gameObject, window)
        {
            gameObject.transform.Position = new Vector3(0.0f, 0.0f, 3.0f);
            this.FOV = FOV;
            this.aspectX = aspectX;
            this.aspectY = aspectY;
            this.near = near;
            this.far = far;
        }
        #endregion

        #region Methods
        // Get the view matrix using the LookAt function
        public Matrix4 GetViewMatrix()
        {
            Matrix4 view = Matrix4.LookAt(gameObject.transform.Position, gameObject.transform.Position + front, up);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(FOV), aspectX / aspectY, near, far);
            return view * projection;
        }
        public Matrix4 GetViewMatrix2()
        {
            Matrix4 view = Matrix4.LookAt(gameObject.transform.Position, gameObject.transform.Position + front, up);
            return view;
        }

        // Get the projection matrix using the CreatePerspectiveFieldOfView function
        public Matrix4 GetProjectionMatrix()
        {
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(FOV), aspectX / aspectY, near, far);
            return projection;
        }

        // Uupdate the direction vertices.
        private void UpdateVectors()
        {
            // The front matrix is calculated using some basic trigonometry.
            front.X = MathF.Cos(pitch) * MathF.Cos(yaw);
            front.Y = MathF.Sin(pitch);
            front.Z = MathF.Cos(pitch) * MathF.Sin(yaw);

            // Make sure that the vectors are all normalized.
            front = Vector3.Normalize(front);

            // Calculate both the right and the up vector using cross product.
            right = Vector3.Normalize(Vector3.Cross(front, Vector3.UnitY));
            up = Vector3.Normalize(Vector3.Cross(right, front));
        }

        public override void Update(FrameEventArgs args)
        {
            KeyboardState input = window.KeyboardState;

            // Update crouch state
            if (input.IsKeyDown(Keys.LeftControl))
            {
                isCrouching = true;
            }
            else
            {
                isCrouching = false;
            }

            if (isCrouching)
            {
                heightOffset = crouchHeight - standingHeight;
                speed = 1.0f;
            }
            else
            {
                heightOffset = 1.0f;
                speed = 3.0f;
            }

            // Apply new height offset to camera's position
            gameObject.transform.Position = new Vector3(
                    gameObject.transform.Position.X,
                    standingHeight + heightOffset,
                    gameObject.transform.Position.Z
                );

            // Check for jump key
            if (input.IsKeyPressed(Keys.Space) && !isJumping)
            {
                isJumping = true;
            }

            // jump
            if (isJumping)
            {
                gameObject.transform.Position = new Vector3(
                    gameObject.transform.Position.X,
                    jumpHeight,
                    gameObject.transform.Position.Z
                );

                jumpHeight -= fallSpeed * ((float)args.Time);

                if (jumpHeight <= 1.0f)
                {
                    jumpHeight = 2.0f;
                    isJumping = false;
                }
            }

            // movement with camera orientation but cannot move on the y-axis
            Vector3 forward = new Vector3(front.X, 0, front.Z);
            Vector3 rightDir = new Vector3(right.X, 0, right.Z);

            if (input.IsKeyDown(Keys.W))
            {
                gameObject.transform.Position += Vector3.Normalize(forward) * speed * (float)args.Time; // Forward 
            }
            if (input.IsKeyDown(Keys.S))
            {
                gameObject.transform.Position -= Vector3.Normalize(forward) * speed * (float)args.Time; // Backwards
            }
            if (input.IsKeyDown(Keys.A))
            {
                gameObject.transform.Position -= Vector3.Normalize(rightDir) * speed * (float)args.Time; // Left
            }
            if (input.IsKeyDown(Keys.D))
            {
                gameObject.transform.Position += Vector3.Normalize(rightDir) * speed * (float)args.Time; // Right
            }
        }
        #endregion
    }
}
