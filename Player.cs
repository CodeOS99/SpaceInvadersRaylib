using System.Numerics;
using Raylib_cs;
using spaceInvadersRaylib.Powerups;
using spaceInvadersRaylib.Scenes;

namespace spaceInvadersRaylib;

public class Player
{
    public int x;
    public int y;
    public int r;
    public int speed;
    public Color color = Color.Red;
    public List<Bullet> bullets;
    private int bulletSpeed;
    private Random random;
    private List<IPowerup> powerups;
    private int MAX_FORWARD; // The max the player can go forward
    private int bulletPierce;
    private int numOfBulletsInOneGo;

    public Player()
    {
        this.x = Globals.screenWidth / 2;
        this.y = Globals.screenHeight - 50;
        this.MAX_FORWARD = Globals.screenHeight - 250;
        this.r = 20;
        this.speed = 5;
        this.bullets = new List<Bullet>();
        this.powerups = new List<IPowerup>();
        this.random = new Random();
        this.bulletPierce = 1;
        this.bulletSpeed = -5;
        this.numOfBulletsInOneGo = 1;
    }
    public void Draw()
    {
        Raylib.DrawCircle(x, y, r, color);
        foreach (Bullet bullet in this.bullets)
        {
            bullet.Draw();
        }

        foreach (IPowerup powerup in this.powerups)
        {
            powerup.Draw();
        }
    }

    public SceneType Update(List<Enemy> enemies)
    {
        if (Raylib.IsKeyDown(KeyboardKey.A) || Raylib.IsKeyDown(KeyboardKey.Left))
        {
            x -= speed;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.D) || Raylib.IsKeyDown(KeyboardKey.Right))
        {
            x += speed;
        }

        if (Raylib.IsKeyDown(KeyboardKey.W) || Raylib.IsKeyDown(KeyboardKey.Up))
        {
            y -= speed;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.S) || Raylib.IsKeyDown(KeyboardKey.Down))
        {
            y += speed;
        }

        if (Raylib.IsMouseButtonPressed(MouseButton.Left) && bullets.Count < Globals.maxPlayerBullets)
        {
            this.Shoot();
        }

        for (int i = 0; i < this.powerups.Count; i++)
        {
            powerups[i].Update();
            if (powerups[i].IsOffScreen())
            {
                powerups.RemoveAt(i);
                i--;
            }
            else if (powerups[i].CollidesWithPlayer(this))
            {
                powerups[i].Visit(this);
                powerups.RemoveAt(i);
                i--;
            }
        }

        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].Update();
            if (bullets[i].IsOffScreen())
            {
                bullets.RemoveAt(i);
                i--;
            }
        }

        if (this.x < this.r) this.x = this.r; // Left wall
        if (this.x > Globals.screenWidth - this.r) this.x = Globals.screenWidth - this.r; // Right wall
        if (this.y < this.MAX_FORWARD - this.r) this.y = this.MAX_FORWARD - this.r; // Top wall
        if (this.y > Globals.screenHeight - this.r) this.y = Globals.screenHeight - this.r; // Bottom wall
        return CheckCollisionWithEnemies(enemies);
    }

    private SceneType CheckCollisionWithEnemies(List<Enemy> enemies)
    {
        CheckBulletEnemyCollision(enemies);
        return CheckPlayerEnemyCollision(enemies);
    }

    private void CheckBulletEnemyCollision(List<Enemy> enemies)
    {
        List<int> bulletRemovalIdx = new List<int>();
        List<int> enemyRemovalIdx = new List<int>();

        for (int i = 0; i < bullets.Count; i++)
        {
            for (int j = 0; j < enemies.Count; j++)
            {
                if (Raylib.CheckCollisionCircles(new Vector2(bullets[i].x, bullets[i].y), bullets[i].r,
                        new Vector2(enemies[j].x, enemies[j].y), enemies[j].r))
                {
                    bullets[i].pierce--;
                    if (!bulletRemovalIdx.Contains(i) && bullets[i].pierce == 0) bulletRemovalIdx.Add(i);
                    this.SpawnPowerup(enemies[j]);
                    if (!enemyRemovalIdx.Contains(j)) enemyRemovalIdx.Add(j);
                }
            }
        }

        // Remove bullets and enemies after iteration to avoid index issues
        for (int i = bulletRemovalIdx.Count - 1; i >= 0; i--)
        {
            bullets.RemoveAt(bulletRemovalIdx[i]);
        }

        for (int j = enemyRemovalIdx.Count - 1; j >= 0; j--)
        {
            enemies.RemoveAt(enemyRemovalIdx[j]);
        }
    }

    private SceneType CheckPlayerEnemyCollision(List<Enemy> enemies)
    {
        // Check if player collides with enemies
        for (int i = 0; i < enemies.Count; i++)
        {
            if (Raylib.CheckCollisionCircles(new Vector2(enemies[i].x, enemies[i].y), enemies[i].r,
                    new Vector2(this.x, this.y), this.r))
            {
                return SceneType.GameOver;
            }
        }

        // Check if player collides with enemy bullets
        foreach (Enemy enemy in enemies)
        {
            foreach (Bullet bullet in enemy.bullets)
            {
                if (Raylib.CheckCollisionCircles(new Vector2(bullet.x, bullet.y), bullet.r,
                        new Vector2(this.x, this.y), this.r))
                {
                    return SceneType.GameOver;
                }
            }
        }

        return SceneType.MainGame;
    }

    private void Shoot()
    {
        for (int _ = 0; _ < this.numOfBulletsInOneGo; _++)
        {
            Bullet bullet = new Bullet(this.x, this.y, this.bulletSpeed, Color.Red, this.bulletPierce);
            this.bullets.Add(bullet);
        }
    }

    public void IncreasePierce(int n)
    {
        this.bulletPierce += n;
    }

    public void IncreaseSpeed(int n)
    {
        this.speed += n;
    }

    private void SpawnPowerup(Enemy targetEnemy)
    {
        if (random.Next(0, 100) <= 10)
        {
            int powerUp = random.Next(0, 5);
            switch (powerUp)
            {
                case 0:
                    powerups.Add(new IncreasedNumberOfBullets(targetEnemy.x, targetEnemy.y, 5, Color.DarkPurple));
                    break;
                case 1:
                    powerups.Add(new IncreasePiercePowerup(targetEnemy.x, targetEnemy.y, 5, Color.DarkGreen));
                    break;
                case 2:
                    powerups.Add(new IncreaseSpeedPowerup(targetEnemy.x, targetEnemy.y, 5, Color.DarkBlue));
                    break;
                case 3:
                    powerups.Add(new IncreaseBulletSpeedPowerup(targetEnemy.x, targetEnemy.y, 5, Color.White));
                    break;
            }
        }

    }

    public void IncreaseUpSpeedOfBullets(int n)
    {
        this.bulletSpeed -= n;
    }

    public void IncreaseNumberOfBulletsInOneGo(int n)
    {
        this.numOfBulletsInOneGo += n;
    }
}