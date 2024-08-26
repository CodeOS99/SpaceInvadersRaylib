using System.Diagnostics;
using Raylib_cs;

namespace spaceInvadersRaylib;

public class Bullet
{
    public int x, y, r;
    private int speedY;
    private Color color;
    public int pierce;

    public Bullet(int x, int y, int speedY, Color color, int pierce)
    {
        this.x = x;
        this.y = y;
        this.r = 10;
        this.speedY = speedY;
        this.color = color;
        this.pierce = pierce;
    }

    public void Draw()
    {
        Raylib.DrawCircle(x, y, r, color);
    }

    public void Update()
    {
        y += speedY;
    }

    public bool IsOffScreen()
    {
        return this.y < this.r || this.y > Globals.screenWidth - this.r;
    }
}