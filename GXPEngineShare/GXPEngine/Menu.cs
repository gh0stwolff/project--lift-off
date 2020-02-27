using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Menu : Canvas
{
    #region variables
    //all screens
    private MultiplayerMapGenerator _multiPlayer;
    private SingleplayerMapGenerator _singlePlayer;
    private MainMenu _mainScreen;
    private ReadyScreen _readyScreen;
    private ScoreScreen _scoreScreenMulti;
    private ScoreScreen _scoreScreenSingle;
    private HighScoreScreen _highScore;
    
    //all sounds
    private Sound _backgroundMusic;
    private SoundChannel _backgroundMusicChannel;
    private Sound _boiling;
    private SoundChannel _boilingChannel;
    private Sound _thruster;
    private SoundChannel _thrusterChannel;
    private Sound _diggingSound;
    private SoundChannel _diggingSoundChannel;
    private Sound _distantGrowl;
    private SoundChannel _distantGrowlChannel;

    //values that need to be stored during session
    private int _scoreP1 = 0;
    private int _scoreP2 = 0;
    private int _playersReady = 0;
    private int _randomGrowlTimer = 200;

    //states for the menu to be in
    enum Scene { MainMenu, ReadyScreen, MultiplayerLevel, SinglePlayerLevel, ScoreScreen1, ScoreScreen2, HighScoreScreen}
    Scene SceneState = Scene.MainMenu;
    #endregion

    #region setup & update
    public Menu(int width, int height) : base(width, height)
    {
        ((MyGame)game).SetScreenForMenu();
        _mainScreen = new MainMenu(((MyGame)game).GetScreenWidth(), ((MyGame)game).GetScreenHeight());
        AddChild(_mainScreen);
        setupSounds();
    }

    private void setupSounds()
    {
        _backgroundMusic = new Sound("Music.mp3", true, true);
        _backgroundMusicChannel = new SoundChannel(1);
        _backgroundMusicChannel.Volume = 0.15f;
        _backgroundMusicChannel = _backgroundMusic.Play();
        _boiling = new Sound("lavaSound.wav", true);
        _thruster = new Sound("thruster.wav", true);
        _diggingSound = new Sound("diggingSound.wav", true);
        _distantGrowl = new Sound("wormGrowlFar.wav");
        _distantGrowlChannel = new SoundChannel(7);
    }

    void Update()
    {
        displayScreen();
        screenState();
        playingSound();
    }

    private void playingSound()
    {
        #region lava sound
        if (_multiPlayer != null || _singlePlayer != null)
        {
            if (_boilingChannel == null)
            {
                _boilingChannel = new SoundChannel(2);
                _boilingChannel.IsPaused = false;
                _boilingChannel = _boiling.Play();
            }
        }
        else
        {
            if (_boilingChannel != null)
            {
                _boilingChannel.IsPaused = true;
                _boilingChannel = null;
            }
        }
        #endregion

        #region Thruster
        if (_singlePlayer != null || _multiPlayer != null)
        {
            if (Input.GetKey(Key.W) || Input.GetKey(Key.A) || Input.GetKey(Key.S) || Input.GetKey(Key.D))
            {
                if (_thrusterChannel == null)
                {
                    _thrusterChannel = new SoundChannel(3);
                    _thrusterChannel = _thruster.Play();
                }
            }
            else
            {
                if (_thrusterChannel != null)
                {
                    _thrusterChannel.IsPaused = true;
                    _thrusterChannel = null;
                }
            }
        }
        #endregion

        #region digging sound
        if (_singlePlayer != null || _multiPlayer != null)
        {
            if (Input.GetKey(Key.Q))
            {
                if (_diggingSoundChannel == null)
                {
                    _diggingSoundChannel = new SoundChannel(6);
                    _diggingSoundChannel = _diggingSound.Play();
                }
            }
            else
            {
                if (_diggingSoundChannel != null)
                {
                    _diggingSoundChannel.IsPaused = true;
                    _diggingSoundChannel = null;
                }
            }
        }
        #endregion

        #region random Growl
        if (_singlePlayer != null || _multiPlayer != null)
        {
            _randomGrowlTimer--;
            if (_randomGrowlTimer <= 0)
            {
                _distantGrowlChannel = _distantGrowl.Play();
                _randomGrowlTimer = Utils.Random(600, 2400);
            }
        }
        #endregion
    }
    #endregion

    #region handle screen state
    private void screenState()
    {
        if (_mainScreen != null && Input.GetKeyDown(Key.THREE))
        {
            SceneState = Scene.ReadyScreen;
        }
        if (_readyScreen != null)
        {
            _playersReady = _readyScreen.GetPlayersReady();
            if (_playersReady == 1 && Input.GetKeyDown(Key.THREE))
            {
                SceneState = Scene.SinglePlayerLevel;
            }
            if (_playersReady == 2 && Input.GetKeyDown(Key.THREE))
            {
                SceneState = Scene.MultiplayerLevel;
            }
        }
        if ( _scoreScreenMulti != null)
        {
            if (_scoreP2 == 0 && Input.GetKeyDown(Key.THREE))
            {
                SceneState = Scene.MultiplayerLevel;
                Console.WriteLine("load 2nd level");
            }
            else if (_scoreScreenMulti.IsComparing == true && Input.GetKeyUp(Key.THREE))
            {
                SceneState = Scene.HighScoreScreen;
                Console.WriteLine("highscore");
            }
            else if (_scoreP2 != 0 && Input.GetKeyUp(Key.THREE))
            {
                _scoreScreenMulti.Compare();
                Console.WriteLine("comparing");
            }
        }
        if ( _scoreScreenSingle != null)
        {
            if (Input.GetKeyDown(Key.THREE))
            {
                SceneState = Scene.HighScoreScreen;
            }
        }
        if (_highScore != null)
        {
            if (Input.GetKeyDown(Key.THREE))
            {
                SceneState = Scene.MainMenu;
                _scoreP1 = 0;
                _scoreP2 = 0;
            }
        }
    }

    private void displayScreen()
    {
        switch(SceneState)
        {
            case Scene.MainMenu:
                ((MyGame)game).SetScreenForMenu();
                if (_mainScreen == null)
                {
                    _mainScreen = new MainMenu(((MyGame)game).GetScreenWidth(), ((MyGame)game).GetScreenHeight());
                    destroyInactiveScreens(_mainScreen);
                    AddChild(_mainScreen);
                    if (_readyScreen != null) { _readyScreen = null; }
                    if (_singlePlayer != null) { _singlePlayer = null; }
                    if (_multiPlayer != null) { _multiPlayer = null; }
                    if (_highScore != null) { _highScore = null; }
                    if (_scoreScreenMulti != null) { _scoreScreenMulti = null; }
                    if (_scoreScreenSingle != null) { _scoreScreenSingle = null; }
                }
                break;
            case Scene.ReadyScreen:
                if (_readyScreen == null)
                {
                    _readyScreen = new ReadyScreen(((MyGame)game).GetScreenWidth(), ((MyGame)game).GetScreenHeight());
                    destroyInactiveScreens(_readyScreen);
                    AddChild(_readyScreen);
                    if (_mainScreen != null) { _mainScreen = null; }
                    if (_singlePlayer != null) { _singlePlayer = null; }
                    if (_multiPlayer != null) { _multiPlayer = null; }
                    if (_highScore != null) { _highScore = null; }
                    if (_scoreScreenMulti != null) { _scoreScreenMulti = null; }
                    if (_scoreScreenSingle != null) { _scoreScreenSingle = null; }
                }
                break;
            case Scene.SinglePlayerLevel:
                if (_singlePlayer == null)
                {
                    ((MyGame)game).SetScreenForBeginLevel();
                    _singlePlayer = new SingleplayerMapGenerator();
                    destroyInactiveScreens(_singlePlayer);
                    AddChild(_singlePlayer);
                    if (_mainScreen != null) { _mainScreen = null; }
                    if (_readyScreen != null) { _readyScreen = null; }
                    if (_multiPlayer != null) { _multiPlayer = null; }
                    if (_highScore != null) { _highScore = null; }
                    if (_scoreScreenMulti != null) { _scoreScreenMulti = null; }
                    if (_scoreScreenSingle != null) { _scoreScreenSingle = null; }
                }
                break;
            case Scene.MultiplayerLevel:
                if ( _multiPlayer == null)
                {
                    ((MyGame)game).SetScreenForBeginLevel();
                    _multiPlayer = new MultiplayerMapGenerator();
                    destroyInactiveScreens(_multiPlayer);
                    AddChild(_multiPlayer);
                    if (_mainScreen != null) { _mainScreen = null; }
                    if (_readyScreen != null) { _readyScreen = null; }
                    if (_singlePlayer != null) { _singlePlayer = null; }
                    if (_highScore != null) { _highScore = null; }
                    if (_scoreScreenMulti != null) { _scoreScreenMulti = null; }
                    if (_scoreScreenSingle != null) { _scoreScreenSingle = null; }
                }
                break;
            case Scene.ScoreScreen1:
                if (_scoreScreenMulti == null)
                {
                    ((MyGame)game).SetScreenForMenu();
                    _scoreScreenMulti = new ScoreScreen((int)((MyGame)game).GetScreenWidth(), (int)((MyGame)game).GetScreenHeight(), _scoreP1, _scoreP2);
                    destroyInactiveScreens(_scoreScreenMulti);
                    AddChild(_scoreScreenMulti);
                    if (_mainScreen != null) { _mainScreen = null; }
                    if (_readyScreen != null) { _readyScreen = null; }
                    if (_singlePlayer != null) { _singlePlayer = null; }
                    if (_multiPlayer != null) { _multiPlayer = null; }
                    if (_highScore != null) { _highScore = null; }
                    if (_scoreScreenSingle != null) { _scoreScreenSingle = null; }
                }
                break;
            case Scene.ScoreScreen2:
                if (_scoreScreenSingle == null)
                {
                    ((MyGame)game).SetScreenForMenu();
                    _scoreScreenSingle = new ScoreScreen((int)((MyGame)game).GetScreenWidth(), (int)((MyGame)game).GetScreenHeight(), _scoreP1, _scoreP2);
                    destroyInactiveScreens(_scoreScreenSingle);
                    AddChild(_scoreScreenSingle);
                    if (_mainScreen != null) { _mainScreen = null; }
                    if (_readyScreen != null) { _readyScreen = null; }
                    if (_singlePlayer != null) { _singlePlayer = null; }
                    if (_multiPlayer != null) { _multiPlayer = null; }
                    if (_highScore != null) { _highScore = null; }
                    if (_scoreScreenMulti != null) { _scoreScreenMulti = null; }
                }
                break;
            case Scene.HighScoreScreen:
                ((MyGame)game).SetScreenForMenu();
                if ( _highScore == null)
                {
                    _highScore = new HighScoreScreen((int)((MyGame)game).GetScreenWidth(), (int)((MyGame)game).GetScreenHeight(), _scoreP1, _scoreP2);
                    destroyInactiveScreens(_highScore);
                    AddChild(_highScore);
                    if (_mainScreen != null) { _mainScreen = null; }
                    if (_readyScreen != null) { _readyScreen = null; }
                    if (_singlePlayer != null) { _singlePlayer = null; }
                    if (_multiPlayer != null) { _multiPlayer = null; }
                    if (_scoreScreenMulti != null) { _scoreScreenMulti = null; }
                    if (_scoreScreenSingle != null) { _scoreScreenSingle = null; }

                }
                break;
        }

    }

    private void destroyInactiveScreens(GameObject objType)
    {
        foreach( GameObject other in GetChildren())
        {
            if (other != objType)
            {
                Console.WriteLine(other);
                other.LateDestroy();
            }
        }
    }
    #endregion

    public void GameOver(int score)
    {
        if (_playersReady == 2)
        {
            Console.WriteLine(_scoreP1);
            Console.WriteLine(_scoreP2);
            if (_scoreP1 == 0)
            {
                _scoreP1 = score;
                SceneState = Scene.ScoreScreen1;
            }
            else if (_scoreP2 == 0)
            {
                _scoreP2 = score;
                SceneState = Scene.ScoreScreen1;
            }
        }
        else
        {
            _scoreP1 = score;
            SceneState = Scene.ScoreScreen2;
        }
    }

}
