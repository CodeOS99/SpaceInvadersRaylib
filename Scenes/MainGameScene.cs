using Raylib_cs;

namespace spaceInvadersRaylib.Scenes;

public class MainGameScene : IScene
{
    private Player player;
    private List<Enemy> enemies;
    private int numOfEnemies;
    private int numOfRows;
    private const int TILE_SIZE = 50;
    private int increaseLevelCounter;
    private const int MAX_LVL_COUNTER = 100;

    public MainGameScene()
    {
        this.player = new Player();
        this.numOfEnemies = 10;
        this.increaseLevelCounter = MAX_LVL_COUNTER;
        numOfRows = 1;
        // Add enemies
        this.enemies = new List<Enemy>();
        this.SpawnEnemies();
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

        if (enemies.Count == 0)
        {
            this.increaseLevelCounter--;
            if (this.increaseLevelCounter <= 0)
            {
                numOfRows++;
                this.SpawnEnemies();
                this.increaseLevelCounter = MAX_LVL_COUNTER;
            }
        }
        return player.Update(enemies);
    }

    private void SpawnEnemies()
    {
        // !! Start indexing from 1
        for (int i = 1; i <= this.numOfRows; i++)
        {
            for (int j = 1; j <= this.numOfEnemies; j++)
            {
                this.enemies.Add(new Enemy(TILE_SIZE * j, TILE_SIZE * i, 20, true));
            }
        }
    }
}