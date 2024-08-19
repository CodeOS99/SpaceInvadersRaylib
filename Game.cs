using spaceInvadersRaylib.Scenes;
using static Raylib_cs.Raylib;
namespace spaceInvadersRaylib;

public class Game
{
    private Dictionary<SceneType, IScene> scenes;
    private SceneType currentScene;
    
    public Game()
    {
        scenes = new Dictionary<SceneType, IScene>
        {
            { SceneType.MainMenu, new MainMenuScene() },
            { SceneType.MainGame, new MainGameScene() },
            { SceneType.GameOver, new GameOver() },
        };

        currentScene = SceneType.MainMenu;
    }

    public void Update()
    {
        SceneType nextSceneType = scenes[currentScene].Update();
        if (nextSceneType != currentScene)
        {
            currentScene = nextSceneType;
        }
    }

    public void Draw()
    {
        // begin draw mode
        BeginDrawing();
        scenes[currentScene].Draw();
        EndDrawing();
    }
}