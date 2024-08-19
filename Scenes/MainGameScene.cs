using Raylib_cs;

namespace spaceInvadersRaylib.Scenes;

public class MainGameScene : IScene
{
    private Player player;

    public MainGameScene()
    {
        player = new Player();
    }
    public void Draw()
    {
        Raylib.ClearBackground(Color.Black);
        player.Draw();
    }

    public SceneType Update()
    {
        player.Update();
        return SceneType.MainGame;
    }
}