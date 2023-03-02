// See https://aka.ms/new-console-template for more information
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using Opgave_1___OpenTK;

Console.WriteLine("Hello, World!");

GameWindowSettings settings = new GameWindowSettings()
{
    RenderFrequency = 60.0,
    UpdateFrequency = 60.0
};

NativeWindowSettings nativeWindowSettings = new NativeWindowSettings()
{
    Size = new Vector2i(1280, 720),
    Title = "Bazinga"
};

Game game = new Game(settings, nativeWindowSettings);
game.Run();