<<<<<<< HEAD
﻿using Raylib_cs;
using spaceInvadersRaylib.GUI;

namespace spaceInvadersRaylib.Scenes;

public class MainMenuScene : IScene
{
    Button startButton;

    public MainMenuScene()
    {
        startButton = new Button("Start", 20, 100, 200, 50, Color.Black, Color.White,
            (Button button) => { button.color = Color.DarkGray; }, (Button button) => { button.color = Color.Black; });
    }

    public SceneType Update()
    {
        startButton.Update();

        if(startButton.IsClicked()) return SceneType.MainGame;

        return SceneType.MainMenu;
    }
    public void Draw()
    {
        // Draw a background
        Raylib.ClearBackground(Color.Gray);

        // Draw the title
        Raylib.DrawText("Space Invaders", 20, 20, 40, Color.White);

        // Draw the start button
        startButton.Draw();
    }
=======
﻿using Raylib_cs;
using spaceInvadersRaylib.GUI;

namespace spaceInvadersRaylib.Scenes;

public class MainMenuScene : IScene
{
    Button startButton;

    public MainMenuScene()
    {
        startButton = new Button("Start", 20, 100, 200, 50, Color.Black, Color.White,
            (Button button) => { button.color = Color.DarkGray; }, (Button button) => { button.color = Color.Black; });
    }

    public SceneType Update()
    {
        startButton.Update();

        if(startButton.IsClicked()) return SceneType.MainGame;

        return SceneType.MainMenu;
    }
    public void Draw()
    {
        // Draw a background
        Raylib.ClearBackground(Color.Gray);

        // Draw the title
        Raylib.DrawText("Space Invaders", 20, 20, 40, Color.White);

        // Draw the start button
        startButton.Draw();
    }
>>>>>>> 418dfdb3ed773db8e702e97e6dce5cdbce3295a8
}