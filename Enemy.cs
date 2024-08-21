using Raylib_cs;

namespace spaceInvadersRaylib;

public class Enemy
{
    public int x, y;
    public bool canShoot;
    public Color color;
    public int r;
    public List<Bullet> bullets;
    private int counter;
    private int bulletCounter;
    private Random random;

    public Enemy(int x, int y, int r, bool canShoot)
    {
        this.x = x;
        this.y = y;
        this.r = r;
        this.canShoot = canShoot;

        this.bullets = new List<Bullet>();
        this.color = new Color(Raylib.GetRandomValue(0, 255), Raylib.GetRandomValue(0, 255), Raylib.GetRandomValue(0, 255), 255);
        this.counter = 0;
        this.random = new Random();
        this.bulletCounter = this.random.Next(0, 100);
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
        if (counter % 100 == 0) this.x += 5;
        else if(counter%100 == 25) this.x += 10;
        else if (counter % 100 == 50) this.x -= 5; // Simple pivot
        else if(counter%100 == 75) this.x -= 10;
        if (counter > 100) counter = 0;
        if(bulletCounter > 300) bulletCounter = 0;
        counter++;
        bulletCounter++;
    }

    private void Shoot()
    {
        this.bullets.Add(new Bullet(this.x, this.y, 5, Color.Purple));
    }
}