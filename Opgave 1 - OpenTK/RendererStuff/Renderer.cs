using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opgave_1___OpenTK.RendererStuff
{
    public class Renderer
    {
        public Material material;
        public Model model;
        public Mesh mesh;
        public Renderer(Material material, Mesh mesh)
        {
            this.material = material;
            this.mesh = mesh;
          //  Console.WriteLine("mesh " + mesh.ToString());
        }

        public Renderer(Material material, Model model)
        {
            this.material = material;
            this.model = model;
            Console.WriteLine("model elements number " + model.NumElements);
        }
        
        public virtual void Draw(Matrix4 mvp)
        {
            material.UseShader();
            material.SetUniform("mvp", mvp);
            mesh.Draw();

        }

        public virtual void DrawModel(Matrix4 mvp)
        {
            material.UseShader();
            material.SetUniform("mvp", mvp);
            model.Draw();
        }


    }
}
