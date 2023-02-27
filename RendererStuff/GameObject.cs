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

        public GameObject(Renderer renderer, GameWindow gameWindow)
        {
            this.renderer = renderer;
            this.gameWindow = gameWindow;
            transform = new Transform();
        }
        public void Update(FrameEventArgs args)
        {

        }
        public void Draw(Matrix4 vp)
        {
            if (renderer != null)
            {
                renderer.Draw(transform.CalculateModel() * vp);

            }
        }
    }
}
