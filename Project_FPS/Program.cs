using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Project_FPS;

// Initialize a new instance of GameWindowSettings class
// GameWindowSettings represents the settings for a game window in a graphics application
GameWindowSettings settings = new GameWindowSettings()
{
    // 60 times per second
    RenderFrequency = 60.0, // Number of times per second the game window should be redrawn
    UpdateFrequency = 60.0  // Number of times per second the game logic should be updated
};

// Initialize a new instance of NativeWindowSettings class
// Used to specify settings for the native window in a graphics application
NativeWindowSettings nativeWindowSettings = new NativeWindowSettings()
{
    Size = new Vector2i(1920, 1080), // Specifies the dimensions of the window in pixels
    Title = "FPS Project" // Title of the title bar of the game window
};

// Initialize a new instance og Game class and Run Game class
Game game = new Game(settings, nativeWindowSettings);
game.Run();


#region ClassLibrary Project

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
#endregion