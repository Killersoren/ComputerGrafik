using OpenTK.Mathematics;

namespace ClassLibraryFps
{
    public class Transform
    {
        #region Fields
        public Vector3 Position;

        public Vector3 Rotation;

        public Vector3 Scale = new Vector3(1, 1, 1);
        #endregion

        #region Method
        public Matrix4 CalculateModel()
        {
            Matrix4 t = Matrix4.CreateTranslation(Position);
            Matrix4 rX = Matrix4.CreateRotationX(Rotation.X);
            Matrix4 rY = Matrix4.CreateRotationY(Rotation.Y);
            Matrix4 rZ = Matrix4.CreateRotationZ(Rotation.Z);
            Matrix4 s = Matrix4.CreateScale(Scale);

            return s * rX * rY * rZ * t;
        }
        #endregion
    }
}