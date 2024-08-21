using System.Numerics;
using Raylib_cs;

namespace spaceInvadersRaylib.GUI;

public class Button
{
    public string text;
    public int x;
    public int y;
    int width;
    public int height;
    public Color color;
    public Color TextColor;
    public Action<Button> onHover;
    public Action<Button> onUnHover;
    public Button(string text, int x, int y, int width, int height, Color color, Color TextColor, Action<Button> onHover, Action<Button> onUnHover)
    {
        this.text = text;
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
        this.color = color;
        this.TextColor = TextColor;
        this.onHover = onHover;
        this.onUnHover = onUnHover;
    }

    public void Draw()
    {
        Raylib.DrawRectangle(x, y, width, height, color);
        // Measure the text
        Vector2 textSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, 20, 1);

        // Calculate the centered position and cast to int
        int centerX = (int)(x + width / 2 - textSize.X / 2);
        int centerY = (int)(y + height / 2 - textSize.Y / 2);

        // Draw the text at the center
        Raylib.DrawText(text, centerX, centerY, 20, Color.White);
    }

    public void Update()
    {
        // Check if the mouse is inside the button
        if (Raylib.GetMouseX() >= x && Raylib.GetMouseX() <= x + width &&
            Raylib.GetMouseY() >= y && Raylib.GetMouseY() <= y + height)
        {
            // Call the onHover function
            onHover(this);
        }
        else
        {
            // Call the onUnHover function
            onUnHover(this);
        }
    }

    public bool IsClicked()
    {
        return Raylib.IsMouseButtonPressed(MouseButton.Left) &&
               Raylib.GetMouseX() >= x && Raylib.GetMouseX() <= x + width &&
               Raylib.GetMouseY() >= y && Raylib.GetMouseY() <= y + height;
    }
}