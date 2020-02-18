using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Menu : Canvas
{
    //private bool _gameStarted = false;

    private MultiplayerMapGenerator _multiPlayer;
    private SingleplayerMapGenerator _singlePlayer;
    private Button _multiplayerButton;
    private Button _singlePlayerButton;
    private Button _returnToMenuButton;
    enum Scene { Menu, MultiplayerLevel, SinglePlayerLevel, GameOver}
    Scene SceneState = Scene.Menu;

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
        if (_multiplayerButton != null)
        {
            if (_multiplayerButton.IsPressed())
            {
                SceneState = Scene.MultiplayerLevel;
            }
        }
        if ( _singlePlayerButton != null)
        {
            if (_singlePlayerButton.IsPressed())
            {
                SceneState = Scene.SinglePlayerLevel;
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
                if (_multiPlayer != null) { _multiPlayer.LateDestroy(); _multiPlayer = null; }
                if (_singlePlayer != null) { _singlePlayer.LateDestroy(); _singlePlayer = null; }
                if ( _returnToMenuButton != null) { _returnToMenuButton.LateDestroy(); _returnToMenuButton = null; }
                if (_multiplayerButton == null)
                {
                    _multiplayerButton = new Button("multiplayerButton.png", 400, 300, 1, 1);
                    AddChild(_multiplayerButton);
                    _singlePlayerButton = new Button("singleplayerButton.png", 400, 500, 1, 1);
                    AddChild(_singlePlayerButton);
                }
                ((MyGame)game).SetScreenForMenu();
                break;
            case Scene.MultiplayerLevel:
                if (_multiplayerButton != null) { _multiplayerButton.LateDestroy(); _multiplayerButton = null; }
                if ( _returnToMenuButton != null) { _returnToMenuButton.LateDestroy(); _returnToMenuButton = null; }
                if (_singlePlayerButton != null) { _singlePlayerButton.LateDestroy(); _singlePlayerButton = null; }
                if (_singlePlayer != null) { _singlePlayer.LateDestroy(); _singlePlayer = null; }
                if (_multiPlayer == null)
                {
                    ((MyGame)game).SetScreenForBeginLevel();
                    _multiPlayer = new MultiplayerMapGenerator();
                    AddChild(_multiPlayer);
                }
                break;
            case Scene.SinglePlayerLevel:
                if (_multiplayerButton != null) { _multiplayerButton.LateDestroy(); _multiplayerButton = null; }
                if (_returnToMenuButton != null) { _returnToMenuButton.LateDestroy(); _returnToMenuButton = null; }
                if (_singlePlayerButton != null) { _singlePlayerButton.LateDestroy(); _singlePlayerButton = null; }
                if (_multiPlayer != null) { _multiPlayer.LateDestroy(); _multiPlayer = null; }
                if (_singlePlayer == null)
                {
                    ((MyGame)game).SetScreenForBeginLevel();
                    _singlePlayer = new SingleplayerMapGenerator();
                    AddChild(_singlePlayer);
                }
                break;
            case Scene.GameOver:
                if (_multiplayerButton != null) { _multiplayerButton.LateDestroy(); _multiplayerButton = null; }
                if (_multiPlayer != null) { _multiPlayer.LateDestroy(); _multiPlayer = null; }
                if (_singlePlayer != null) { _singlePlayer.LateDestroy(); _singlePlayer = null; }
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
