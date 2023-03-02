using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Opgave_1___OpenTK.RendererStuff;

namespace Opgave_1___OpenTK
{
    public abstract class Behaviour
    {
        #region Fields
        protected GameObject gameObject;
        protected Game window;
        #endregion

        #region Constructor
        public Behaviour(GameObject gameObject, Game window)
        {
            this.gameObject = gameObject;
            this.window = window;
        }
        #endregion

        #region Method
        public abstract void Update(FrameEventArgs args);
        #endregion
    }
}