using Raylib_cs;
using spaceInvadersRaylib.GUI;

namespace spaceInvadersRaylib.Scenes;

public class GameOver : IScene
{
    private Button button = new Button("Restart", 20, 100, 200, 50, Color.Black, Color.White,
        (Button button) => { button.color = Color.DarkGray; }, (Button button) => { button.color = Color.Black; });

    public void Draw()
    {
        Raylib.ClearBackground(Color.Gray);
        Raylib.DrawText("Game Over", 20, 20, 40, Color.White);
        button.Draw();
    }

    public SceneType Update()
    {
        button.Update();
        if (button.IsClicked())
        {
            return SceneType.MainGame;
        }
        return SceneType.GameOver;
    }
}