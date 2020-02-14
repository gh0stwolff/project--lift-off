using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Menu : Canvas
{
    private float _startScreenSpeed = 1.0f;

    private bool _gameStarted = false;

    public MapGenerator _level;
    private Button _startButton;


    //don't forget to reset MyGame frame back to 0 and speed to 0 as long as you are in a menu screen
    public Menu(int width, int height) : base(width, height)
    {
        ((MyGame)game).SetScreenSpeed(0.0f);
        _startButton = new Button("startButton.png", 400, 300, 0.5f, 0.5f);
        AddChild(_startButton);
    }

    void Update()
    {
        checkButtonPresses();
        handleVisibilityState();
    }

    private void checkButtonPresses()
    {
        if (_startButton.IsPressed())
        {
            ((MyGame)game).SetScreenSpeed(_startScreenSpeed);
            if (_level == null)
            {
                _gameStarted = true;
                _level = new MapGenerator();
                AddChild(_level);
            }
        }
    }

    private void handleVisibilityState()
    {
        if (_gameStarted)
        {
            _startButton.alpha = 0.0f;
        }
        else
        {
            _startButton.alpha = 1.0f;
        }
    }

}
