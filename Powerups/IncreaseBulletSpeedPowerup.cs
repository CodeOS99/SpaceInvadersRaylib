using System.Numerics;
using Raylib_cs;

namespace spaceInvadersRaylib.Powerups;

public class IncreaseBulletSpeedPowerup : IPowerup
{
    private int x, y, speedY, r;
    private Color color;

    public IncreaseBulletSpeedPowerup(int x, int y, int speedY, Color color)
    {
        this.x = x;
        this.y = y;
        this.speedY = speedY;
        this.color = color;
        this.r = 10;
    }

    public void Visit(Player player) // We need the parameter because interface
    {
        player.IncreaseUpSpeedOfBullets(2);
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

    public bool CollidesWithPlayer(Player player)
    {
        return Raylib.CheckCollisionCircles(new Vector2(x, y), r, new Vector2(player.x, player.y), player.r);
    }
}