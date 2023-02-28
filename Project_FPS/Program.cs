using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Project_FPS;

GameWindowSettings settings = new GameWindowSettings()
{
    RenderFrequency = 60.0,
    UpdateFrequency = 60.0
};

NativeWindowSettings nativeWindowSettings = new NativeWindowSettings()
{
    
    Size = new Vector2i(1920, 1080),/*(1280, 720),*/
    Title = "FPS Project"
};

Game game = new Game(settings, nativeWindowSettings);
game.Run();

//namespace Project_FPS
//{
//    public static class Program
//    {
//        private static void Main()
//        {
//            var nativeWindowSettings = new NativeWindowSettings()
//            {
//                Size = new Vector2i(800, 600),
//                Title = "FPS project",
//                // This is needed to run on macos
//                //Flags = ContextFlags.ForwardCompatible,
//            };

//            using (var game = new Game(GameWindowSettings.Default, nativeWindowSettings))
//            {
//                game.Run();
//            }
//        }
//    }
//}