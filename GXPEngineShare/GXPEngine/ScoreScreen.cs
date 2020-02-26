using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using System.IO;

class ScoreScreen : Canvas
{
    private Sprite _backGround;
    private TextBoard _yourScore;
    private TextBoard _score;
    private TextBoard _youWon;

    private int _scoreP1 = 0;
    private int _scoreP2 = 0;

    public bool IsComparing = false;

    public ScoreScreen(int width, int height, int scoreP1, int scoreP2) : base(width, height)
    {
        
        _backGround = new Sprite("backGround.jpg");
        AddChild(_backGround);
        _scoreP1 = scoreP1;
        _scoreP2 = scoreP2;
        setupText();
    }

    private void setupText()
    {
        _yourScore = new TextBoard(400, 200);
        AddChild(_yourScore);
        _yourScore.Size(56);
        _yourScore.SetXY(500, 250);
        _yourScore.SetText("Your Score:");
        _score = new TextBoard(400, 200);
        AddChild(_score);
        _score.Size(56);
        _score.SetXY(550, 350);
        if (_scoreP2 == 0)
        {
            _score.SetText(_scoreP1.ToString());
        }
        else
        {
            _score.SetText(_scoreP2.ToString());
        }
        _youWon = new TextBoard(500, 300);
        _youWon.Size(56);
        _youWon.SetXY(400, 400);
        AddChild(_youWon);
    }

    public void Compare()
    {
        IsComparing = true;
        _score.Clear();

        if (_scoreP1 > _scoreP2)
        {
            showP1Won();
        }
        else
        {
            showP2Won();
        }
    }

    private void showP1Won()
    {
        _youWon.SetText("Player 1 WON!");
    }

    private void showP2Won()
    {
        _youWon.SetText("Player 2 WON!");

    }

}
