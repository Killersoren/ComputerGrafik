using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Mathematics;
using ClassLibraryFps;

namespace Project_FPS
{
    public class GameObject
    {
        #region Fields
        public Transform transform;
        public Renderer renderer;
        protected GameWindow gameWindow;

        List<Behaviour> behaviours = new List<Behaviour>();
        #endregion

        #region Generic Methods
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
        #endregion

        #region Constructor
        public GameObject(Renderer renderer, Project_FPS.Game gameWindow)
        {
            this.renderer = renderer;
            this.gameWindow = gameWindow;
            transform = new Transform();
        }
        #endregion

        #region Methods
        public void Update(FrameEventArgs args)
        {
            foreach (Behaviour behaviour in behaviours)
            {
                behaviour.Update(args);
            }
        }

        public void Draw(Matrix4 vp)
        {
            if (renderer != null)
            {
                renderer.Draw(transform.CalculateModel() * vp);

            }
        }
        #endregion
    }
}