using ClassLibrary;
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
        List<Behaviour> behaviours = new List<Behaviour>();


        public GameObject( Game gameWindow)
        {
            this.gameWindow = gameWindow;
            transform = new Transform();
        }

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
            foreach (Behaviour behaviour in behaviours)
            {
                behaviour.Update(args);
            }
        }
        public void Draw(Matrix4 vp)
        {

            if (renderer == null)
            {
                // Is a camera. Do nothing.
            }

            else if (renderer.mesh != null)
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

        public T GetComponent<T>() where T : Behaviour
        {
            foreach (Behaviour component in behaviours)
            {
                T componentAsT = component as T;
                if (componentAsT != null) return componentAsT;
            }
            return null;
        }

        public void AddComponent<T>(params object?[]? args) where T : Behaviour
        {
            if (args == null)
            {
                behaviours.Add(Activator.CreateInstance(typeof(T), this, gameWindow) as T);
            }
            else
            {
                int initialParameters = 2;
                int totalParams = args.Length + initialParameters;
                object?[]? objects = new object[totalParams];
                objects[0] = this;
                objects[1] = gameWindow;
                for (int i = initialParameters; i < totalParams; i++)
                {
                    objects[i] = args[i - 2];
                }

                behaviours.Add(Activator.CreateInstance(typeof(T), objects) as T);
            }
        }

    }
}
