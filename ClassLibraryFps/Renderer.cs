using OpenTK.Mathematics;

namespace ClassLibraryFps
{
    public class Renderer
    {
        public Material material;
        Mesh mesh;
        public Renderer(Material material, Mesh mesh)
        {
            this.material = material;
            this.mesh = mesh;
        }

        public void Draw(Matrix4 mvp)
        {
            material.UseShader();
            material.SetUniform("mvp", mvp);
            mesh.Draw();
        }
    }
}