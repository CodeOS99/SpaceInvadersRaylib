using Raylib_cs;

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
}