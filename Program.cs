using static Raylib_cs.Raylib;

namespace spaceInvadersRaylib
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InitWindow(Globals.screenWidth, Globals.screenHeight, Globals.windowTitle);
            SetTargetFPS(Globals.targetFPS);

            Game game = new Game();

            while (!WindowShouldClose())
            {
                game.Update();
                game.Draw();
            }

            CloseWindow();
        }
    }
}
