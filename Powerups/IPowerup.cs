namespace spaceInvadersRaylib.Powerups;

public interface IPowerup
{
    public void Visit(Player player);
    public void Draw();
    public void Update();
    public bool IsOffScreen();
    public bool CollidesWithPlayer(Player player);
}