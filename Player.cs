<<<<<<< HEAD
﻿using System.Numerics;
using Raylib_cs;
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

    public Player()
    {
        this.x = Globals.screenWidth / 2;
        this.y = Globals.screenHeight - 50;
        this.r = 20;
        this.speed = 5;
        this.bullets = new List<Bullet>();
    }
    public void Draw()
    {
        Raylib.DrawCircle(x, y, r, color);
        foreach (Bullet bullet in this.bullets)
        {
            bullet.Draw();
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

        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            this.Shoot();
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
        if (this.y < this.r) this.y = this.r; // Top wall
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
                    if (!bulletRemovalIdx.Contains(i)) bulletRemovalIdx.Add(i);
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
        Bullet bullet = new Bullet(this.x, this.y, -5, Color.Red);
        this.bullets.Add(bullet);
    }
=======
﻿using Raylib_cs;

namespace spaceInvadersRaylib;

public class Player
{
    public int x;
    public int y;
    public int r;
    public int speed;
    public Color color = Color.Red;
    public List<Bullet> bullets;

    public Player()
    {
        this.x = Globals.screenWidth/2;
        this.y = Globals.screenHeight - 50;
        this.r = 20;
        this.speed = 5;
        this.bullets = new List<Bullet>();
    }
    public void Draw()
    {
        Raylib.DrawCircle(x, y, r, color);
        foreach (Bullet bullet in this.bullets)
        {
            bullet.Draw();
        }
    }

    public void Update()
    {
        if (Raylib.IsKeyDown(KeyboardKey.A) || Raylib.IsKeyDown(KeyboardKey.Left))
        {
            x -= speed;
        } else if (Raylib.IsKeyDown(KeyboardKey.D) || Raylib.IsKeyDown(KeyboardKey.Right))
        {
            x += speed;
        }

        if (Raylib.IsKeyDown(KeyboardKey.W) || Raylib.IsKeyDown(KeyboardKey.Up))
        {
            y -= speed;
        } else if (Raylib.IsKeyDown(KeyboardKey.S) || Raylib.IsKeyDown(KeyboardKey.Down))
        {
            y += speed;
        }

        if (Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            this.Shoot();
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

        if(this.x < this.r) this.x = this.r; // Left wall
        if(this.x > Globals.screenWidth - this.r) this.x = Globals.screenWidth - this.r; // Right wall
        if(this.y < this.r) this.y = this.r; // Top wall
        if(this.y > Globals.screenHeight - this.r) this.y = Globals.screenHeight - this.r; // Bottom wall
    }

    private void Shoot()
    { 
        Bullet bullet = new Bullet(this.x, this.y, -5);
        this.bullets.Add(bullet);
    }
>>>>>>> 418dfdb3ed773db8e702e97e6dce5cdbce3295a8
}