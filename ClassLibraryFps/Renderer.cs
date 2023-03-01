using OpenTK.Mathematics;

namespace ClassLibraryFps
{
    public class Renderer
    {
        #region Fields
        public Material material;
        Mesh mesh;
        #endregion

        #region Constructor
        public Renderer(Material material, Mesh mesh)
        {
            this.material = material;
            this.mesh = mesh;
        }
        #endregion

        #region Method
        public void Draw(Matrix4 mvp)
        {
            material.UseShader();
            material.SetUniform("mvp", mvp);
            mesh.Draw();
        }
        #endregion
    }
}