using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using System.IO;
using System.Drawing;
using System.Drawing.Text;

class ScoreScreen : Canvas
{
    #region variables
    private Sprite _backGround;
    
    private TextBoard _score;
    private TextBoard _youWon;
    private Sound _gameOverSound;
    private SoundChannel _gameOverSoundChannel;

    private int _scoreP1 = 0;
    private int _scoreP2 = 0;

    public bool IsComparing = false;
    #endregion

    #region setup
    public ScoreScreen(int width, int height, int scoreP1, int scoreP2) : base(width, height)
    {
        
        _backGround = new Sprite("scoreScreen.png");
        AddChild(_backGround);
        _scoreP1 = scoreP1;
        _scoreP2 = scoreP2;
        setupText();
        setTextProps();
        
        _gameOverSound = new Sound("gameOver.wav");
        _gameOverSoundChannel = new SoundChannel(8);
        _gameOverSoundChannel = _gameOverSound.Play();
    }

    private void setupText()
    {
        _score = new TextBoard(600, 200);
        AddChild(_score);
        _youWon = new TextBoard(500, 300);
        AddChild(_youWon);
    }

    private void setTextProps()
    {
        _score.Size(128);
        _score.SetXY(450, 350);
        if (_scoreP2 == 0) { _score.SetText(_scoreP1.ToString()); }
        else { _score.SetText(_scoreP2.ToString()); }

        _youWon.Size(56);
        _youWon.SetXY(400, 400);
    }
    #endregion

    #region show who won
    public void Compare()
    {
        IsComparing = true;
        _score.Clear();

        if (_scoreP1 > _scoreP2) { showP1Won(); }
        else { showP2Won(); }
    }

    private void showP1Won()
    {
        _youWon.SetText("Player 1 WON!");
    }

    private void showP2Won()
    {
        _youWon.SetText("Player 2 WON!");
    }
    #endregion
}
