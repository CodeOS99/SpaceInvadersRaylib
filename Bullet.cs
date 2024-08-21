using System.Diagnostics;
using Raylib_cs;

namespace spaceInvadersRaylib;

public class Bullet
{
    public int x, y, r;
    private int speedY;
    private Color color;

    public Bullet(int x, int y, int speedY, Color color)
    {
        this.x = x;
        this.y = y;
        this.r = 10;
        this.speedY = speedY;
        this.color = color;
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
        return this.x < this.r || this.x > Globals.screenWidth - this.r;
    }
}