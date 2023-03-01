using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opgave_1___OpenTK.RendererStuff
{
    public class GameObject
    {
        public Transform transform;
        public Renderer renderer;
        protected GameWindow gameWindow;
        public Model model;

        public GameObject(Renderer renderer, GameWindow gameWindow)
        {
            this.renderer = renderer;
            this.gameWindow = gameWindow;
            transform = new Transform();

            Console.WriteLine("renderer " + renderer.ToString());
        }

        public GameObject(Model model, GameWindow gameWindow)
        {
            //renderer = new ModelRenderer(model);
            this.model = model;
            this.gameWindow = gameWindow;
            transform = new Transform();

            Console.WriteLine("model " + model);
        }

        public void Update(FrameEventArgs args)
        {

        }
        public void Draw(Matrix4 vp)
        {
            if (renderer.mesh != null)
            {
                //Console.WriteLine("Is an object");

                renderer.Draw(transform.CalculateModel() * vp);
  
            }

            else if(renderer.mesh == null)
            {
                //Console.WriteLine("Is a model");
                renderer.DrawModel(transform.CalculateModel() *vp);
            }


            else
            {
                Console.WriteLine("Neither an object or a model");
            }
        }

    }
}
