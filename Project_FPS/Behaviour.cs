using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Project_FPS
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