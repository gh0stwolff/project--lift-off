using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Menu : Canvas
{
    private MultiplayerMapGenerator _multiPlayer;
    private SingleplayerMapGenerator _singlePlayer;
    //private Button _multiplayerButton;
    //private Button _singlePlayerButton;
    //private Button _returnToMenuButton;
    private MainMenu _mainScreen;
    private ReadyScreen _readyScreen;

    enum Scene { MainMenu, ReadyScreen, MultiplayerLevel, SinglePlayerLevel, GameOver}
    Scene SceneState = Scene.MainMenu;

    public Menu(int width, int height) : base(width, height)
    {
        ((MyGame)game).SetScreenForMenu();
        _mainScreen = new MainMenu(((MyGame)game).GetScreenWidth(), ((MyGame)game).GetScreenHeight());
        AddChild(_mainScreen);
    }

    void Update()
    {
        displayScreen();
        screenState();

        //checkButtonPresses();
        //handleSceneState();
    }

    private void screenState()
    {
        if (_mainScreen != null && Input.GetKeyDown(Key.ENTER))
        {
            SceneState = Scene.ReadyScreen;
        }
        if (_readyScreen != null)
        {
            if (_readyScreen.GetPlayersReady() == 1 && Input.GetKeyDown(Key.ENTER))
            {
                SceneState = Scene.SinglePlayerLevel;
            }
            if (_readyScreen.GetPlayersReady() == 2 && Input.GetKeyDown(Key.ENTER))
            {
                SceneState = Scene.MultiplayerLevel;
            }
        }
    }

    private void displayScreen()
    {
        switch(SceneState)
        {
            case Scene.MainMenu:
                ((MyGame)game).SetScreenForMenu();
                if (_readyScreen != null) { _readyScreen = null; }
                if (_singlePlayer != null) { _singlePlayer = null; }
                if ( _multiPlayer != null) { _multiPlayer = null; }
                if (_mainScreen == null)
                {
                    _mainScreen = new MainMenu(((MyGame)game).GetScreenWidth(), ((MyGame)game).GetScreenHeight());
                    destroyInactiveScreens(_mainScreen);
                    AddChild(_mainScreen);
                }
                break;
            case Scene.ReadyScreen:
                if (_mainScreen != null) { _mainScreen = null; }
                if (_readyScreen == null)
                {
                    _readyScreen = new ReadyScreen(((MyGame)game).GetScreenWidth(), ((MyGame)game).GetScreenHeight());
                    destroyInactiveScreens(_readyScreen);
                    AddChild(_readyScreen);
                }
                break;
            case Scene.SinglePlayerLevel:
                if (_mainScreen != null) { _mainScreen = null; }
                if (_readyScreen != null) { _readyScreen = null; }
                if (_singlePlayer == null)
                {
                    ((MyGame)game).SetScreenForBeginLevel();
                    _singlePlayer = new SingleplayerMapGenerator();
                    destroyInactiveScreens(_singlePlayer);
                    AddChild(_singlePlayer);
                }
                break;
            case Scene.MultiplayerLevel:
                if (_mainScreen != null) { _mainScreen = null; }
                if ( _readyScreen != null) { _readyScreen = null; }
                if ( _multiPlayer == null)
                {
                    ((MyGame)game).SetScreenForBeginLevel();
                    _multiPlayer = new MultiplayerMapGenerator();
                    destroyInactiveScreens(_multiPlayer);
                    AddChild(_multiPlayer);
                }
                break;
            case Scene.GameOver:
                if (_multiPlayer != null) { _multiPlayer = null; }
                if ( _singlePlayer != null) { _singlePlayer = null; }
                
                break;
        }

    }

    private void destroyInactiveScreens(GameObject objType)
    {
        foreach( GameObject other in GetChildren())
        {
            if (other != objType)
            {
                other.LateDestroy();
            }
        }
    }

    //private void checkButtonPresses()
    //{
    //    if (_multiplayerButton != null)
    //    {
    //        if (_multiplayerButton.IsPressed())
    //        {
    //            SceneState = Scene.MultiplayerLevel;
    //        }
    //    }
    //    if ( _singlePlayerButton != null)
    //    {
    //        if (_singlePlayerButton.IsPressed())
    //        {
    //            SceneState = Scene.SinglePlayerLevel;
    //        }
    //    }
    //    if (_returnToMenuButton != null)
    //    {
    //        if (_returnToMenuButton.IsPressed())
    //        {
    //            SceneState = Scene.Menu;
    //        }
    //    }
    //}

    //private void handleSceneState()
    //{
    //    switch (SceneState)
    //    {
    //        case Scene.Menu:
    //            if (_multiPlayer != null) { _multiPlayer.LateDestroy(); _multiPlayer = null; }
    //            if (_singlePlayer != null) { _singlePlayer.LateDestroy(); _singlePlayer = null; }
    //            if ( _returnToMenuButton != null) { _returnToMenuButton.LateDestroy(); _returnToMenuButton = null; }
    //            if (_multiplayerButton == null)
    //            {
    //                _multiplayerButton = new Button("multiplayerButton.png", 400, 300, 1, 1);
    //                AddChild(_multiplayerButton);
    //                _singlePlayerButton = new Button("singleplayerButton.png", 400, 500, 1, 1);
    //                AddChild(_singlePlayerButton);
    //            }
    //            ((MyGame)game).SetScreenForMenu();
    //            break;
    //        case Scene.MultiplayerLevel:
    //            if (_multiplayerButton != null) { _multiplayerButton.LateDestroy(); _multiplayerButton = null; }
    //            if ( _returnToMenuButton != null) { _returnToMenuButton.LateDestroy(); _returnToMenuButton = null; }
    //            if (_singlePlayerButton != null) { _singlePlayerButton.LateDestroy(); _singlePlayerButton = null; }
    //            if (_singlePlayer != null) { _singlePlayer.LateDestroy(); _singlePlayer = null; }
    //            if (_multiPlayer == null)
    //            {
    //                ((MyGame)game).SetScreenForBeginLevel();
    //                _multiPlayer = new MultiplayerMapGenerator();
    //                AddChild(_multiPlayer);
    //            }
    //            break;
    //        case Scene.SinglePlayerLevel:
    //            if (_multiplayerButton != null) { _multiplayerButton.LateDestroy(); _multiplayerButton = null; }
    //            if (_returnToMenuButton != null) { _returnToMenuButton.LateDestroy(); _returnToMenuButton = null; }
    //            if (_singlePlayerButton != null) { _singlePlayerButton.LateDestroy(); _singlePlayerButton = null; }
    //            if (_multiPlayer != null) { _multiPlayer.LateDestroy(); _multiPlayer = null; }
    //            if (_singlePlayer == null)
    //            {
    //                ((MyGame)game).SetScreenForBeginLevel();
    //                _singlePlayer = new SingleplayerMapGenerator();
    //                AddChild(_singlePlayer);
    //            }
    //            break;
    //        case Scene.GameOver:
    //            if (_multiplayerButton != null) { _multiplayerButton.LateDestroy(); _multiplayerButton = null; }
    //            if (_multiPlayer != null) { _multiPlayer.LateDestroy(); _multiPlayer = null; }
    //            if (_singlePlayer != null) { _singlePlayer.LateDestroy(); _singlePlayer = null; }
    //            if (_returnToMenuButton == null)
    //            {
    //                _returnToMenuButton = new Button("returnButton.png", 400, 300, 1, 1);
    //                AddChild(_returnToMenuButton);
    //            }
    //            ((MyGame)game).SetScreenForMenu();
    //            break;
    //    }
    //}

    public void GameOver()
    {
        SceneState = Scene.MainMenu;
    }

}
