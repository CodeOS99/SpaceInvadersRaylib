using System.Diagnostics.Metrics;
using Raylib_cs;

namespace spaceInvadersRaylib;

public class Enemy
{
    public int x, y;
    public bool canShoot;
    public Color color;
    public int r;
    public List<Bullet> bullets;
    private int bulletCounter;
    private Random random;
    private int counter;
    private int bulletPierce;

    public Enemy(int x, int y, int r, bool canShoot, int bulletPierce)
    {
        this.x = x;
        this.y = y;
        this.r = r;
        this.canShoot = canShoot;
        this.bulletPierce = bulletPierce;

        this.bullets = new List<Bullet>();
        this.color = new Color(Raylib.GetRandomValue(0, 255), Raylib.GetRandomValue(0, 255), Raylib.GetRandomValue(0, 255), 255);
        this.random = new Random();
        this.bulletCounter = this.random.Next(0, 100);
        this.counter = 0;
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
        if (canShoot && this.bulletCounter%300==0 && this.random.Next(100) < 99)
        {
            Shoot();
        }

        for(int i = 0; i < bullets.Count; i++)
        {
            bullets[i].Update();
            if (bullets[i].IsOffScreen())
            {
                bullets.RemoveAt(i);
                i--;
            }
        }

        // Right Screen Wrap
        if (this.x > Globals.screenWidth + this.r) this.x = -this.r;
        // Left Screen Wrap
        if (this.x < -this.r) this.x = Globals.screenWidth + this.r;

        if (this.counter%50 == 0)this.x += Globals.enemyDx;
        if(bulletCounter > 300) bulletCounter = 0;
        if(this.counter > 50) this.counter = 0;
        bulletCounter++;
        this.counter++;
    }

    private void Shoot()
    {
        this.bullets.Add(new Bullet(this.x, this.y, 5, Color.Purple, this.bulletPierce));
    }
}