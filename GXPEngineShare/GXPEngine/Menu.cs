using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Menu : Canvas
{
    //private bool _gameStarted = false;

    public MapGenerator _level;
    private Button _startButton;
    private Button _returnToMenuButton;
    enum Scene { Menu, Level, GameOver}
    Scene SceneState = Scene.Menu;


    //don't forget to reset MyGame frame back to 0 and speed to 0 as long as you are in a menu screen
    public Menu(int width, int height) : base(width, height)
    {
        ((MyGame)game).SetScreenForMenu();
    }

    void Update()
    {
        checkButtonPresses();
        handleSceneState();
    }

    private void checkButtonPresses()
    {
        if (_startButton != null)
        {
            if (_startButton.IsPressed())
            {
                SceneState = Scene.Level;
            }
        }
        if (_returnToMenuButton != null)
        {
            if (_returnToMenuButton.IsPressed())
            {
                SceneState = Scene.Menu;
            }
        }
    }

    private void handleSceneState()
    {
        switch (SceneState)
        {
            case Scene.Menu:
                if (_level != null) { _level.LateDestroy(); _level = null; }
                if ( _returnToMenuButton != null) { _returnToMenuButton.LateDestroy(); _returnToMenuButton = null; }
                if (_startButton == null)
                {
                    _startButton = new Button("startButton.png", 400, 300, 0.5f, 0.5f);
                    AddChild(_startButton);
                }
                ((MyGame)game).SetScreenForMenu();
                break;
            case Scene.Level:
                if (_startButton != null) { _startButton.LateDestroy(); _startButton = null; }
                if ( _returnToMenuButton != null) { _returnToMenuButton.LateDestroy(); _returnToMenuButton = null; }
                if (_level == null)
                {
                    ((MyGame)game).SetScreenForStartLevel();
                    _level = new MapGenerator();
                    AddChild(_level);
                }
                break;
            case Scene.GameOver:
                if (_startButton != null) { _startButton.LateDestroy(); _startButton = null; }
                if (_level != null) { _level.LateDestroy(); _level = null; }
                if (_returnToMenuButton == null)
                {
                    _returnToMenuButton = new Button("returnButton.png", 400, 300, 1, 1);
                    AddChild(_returnToMenuButton);
                }
                ((MyGame)game).SetScreenForMenu();
                break;
        }
    }

    public void GameOver()
    {
        SceneState = Scene.GameOver;
    }

}
