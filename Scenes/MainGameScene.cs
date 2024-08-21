using Raylib_cs;

namespace spaceInvadersRaylib.Scenes;

public class MainGameScene : IScene
{
    private Player player;
    private List<Enemy> enemies;
    private int numOfEnemies;
    private const int TILE_SIZE = 50;

    public MainGameScene()
    {
        this.player = new Player();
        this.numOfEnemies = 10;
        // Add enemies
        this.enemies = new List<Enemy>();
        for (int i = 1; i <= this.numOfEnemies; i++)
        {
            this.enemies.Add(new Enemy(TILE_SIZE * i, 50, 20, true));
        }
    }
    public void Draw()
    {
        Raylib.ClearBackground(Color.Black);
        player.Draw();
        foreach (Enemy enemy in enemies)
        {
            enemy.Draw();
        }
    }

    public SceneType Update()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.Update();
        }
        return player.Update(enemies);
    }
}