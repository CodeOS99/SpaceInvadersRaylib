using System.Numerics;
using Raylib_cs;

namespace spaceInvadersRaylib.Powerups;

public class IncreasedNumberOfBullets : IPowerup
{
    public int x, y, speedY, r;
    public Color color;
    public IncreasedNumberOfBullets(int x, int y, int speedY, Color color)
    {
        this.x = x;
        this.y = y;
        this.speedY = speedY;
        this.color = color;
        this.r = 10;
    }

    public void Visit(Player player) // We need the parameter because interface
    {
        Globals.maxPlayerBullets++;
    }

    public bool CollidesWithPlayer(Player player)
    {
        return Raylib.CheckCollisionCircles(new Vector2(x, y), r, new Vector2(player.x, player.y), player.r);
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
        return y < r || y > Globals.screenHeight - r;
    }
}