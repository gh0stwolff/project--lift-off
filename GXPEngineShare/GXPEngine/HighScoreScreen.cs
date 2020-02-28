using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using System.IO;

class HighScoreScreen : Canvas
{
    #region variables
    #region text modifiers
    private int _textSize = 72;
    private int _textX = 420;
    private int _textY = 40;
    private int _textYDist = 100;
    #endregion

    private string _scoreP1;
    private string _scoreP2;

    private List<string> scores = new List<string>();

    private TextBoard[] _scoreBoard = new TextBoard[5];
    private ScoreBoard _board = new ScoreBoard("Score.txt");
    #endregion

    #region setup
    public HighScoreScreen(int width, int height, int scoreP1, int scoreP2) : base(width, height)
    {
        Sprite background = new Sprite("highScore.png");
        AddChild(background);
        checkScores(scoreP1, scoreP2);
        setNewData();
        getData();
        showScores();
    }
    #endregion

    #region checkscores
    /// <summary>
    /// checks if the score is not zero
    /// prevent singleplayer form uploading two scores
    /// </summary>
    /// <param name="scoreP1">score of player 1</param>
    /// <param name="scoreP2">score of player 2</param>
    private void checkScores(int scoreP1, int scoreP2)
    {
        if (scoreP1 != 0) { _scoreP1 = scoreP1.ToString(); }
        else { _scoreP1 = null; }
        if (scoreP2 != 0) { _scoreP2 = scoreP2.ToString(); }
        else { _scoreP2 = null; }
    }
    #endregion

    #region get & set data
    private void getData()
    {
        scores = _board.getHighScores();
    }

    private void setNewData()
    {
        if (_scoreP1 != null) { _board.AddLine(_scoreP1); }
        if (_scoreP2 != null) { _board.AddLine(_scoreP2); }
    }
    #endregion

    #region show scores
    /// <summary>
    /// show the top 5 highscores on the screen
    /// </summary>
    private void showScores()
    {
        int place = 1;
        int score = 0;
        if (scores.Count < _scoreBoard.Length)
        {
            
            for (int i = 0; i < scores.Count; i++)
            {
                _scoreBoard[i] = new TextBoard(500, 300);
                AddChild(_scoreBoard[i]);
                _scoreBoard[i].Size(_textSize);
                _scoreBoard[i].SetXY(_textX, _textY + _textYDist * place);
                Int32.TryParse(scores[i], out score);
                Console.WriteLine("{0}. {1}", place, score);
                _scoreBoard[i].SetText(place + ":" + score);
                place++;
            }
        }
        else
        {
            for (int i = 0; i < _scoreBoard.Length; i++)
            {
                _scoreBoard[i] = new TextBoard(500, 300);
                AddChild(_scoreBoard[i]);
                _scoreBoard[i].Size(_textSize);
                _scoreBoard[i].SetXY(_textX, _textY + _textYDist * place);
                Int32.TryParse(scores[i], out score);
                _scoreBoard[i].SetText(place + ":" + score);
                place++;
            }
        }
        
    }
    #endregion
}