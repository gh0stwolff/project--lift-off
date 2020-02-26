using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using System.IO;

class HighScoreScreen : Canvas
{
    private string _scoreP1;
    private string _scoreP2;

    private List<string> scores = new List<string>();
    private TextBoard[] _scoreBoard = new TextBoard[5];
    private ScoreBoard _board = new ScoreBoard("Score.txt");

    public HighScoreScreen(int width, int height, int scoreP1, int scoreP2) : base(width, height)
    {
        checkScores(scoreP1, scoreP2);
        setNewData();
        getData();
        setupScoreBoard();
    }

    private void checkScores(int scoreP1, int scoreP2)
    {
        if (scoreP1 != 0) { _scoreP1 = scoreP1.ToString(); }
        else { _scoreP1 = null; }
        if (scoreP2 != 0) { _scoreP2 = scoreP2.ToString(); }
        else { _scoreP2 = null; }
    }

    private void getData()
    {
        scores = _board.getHighScores();
    }

    private void setNewData()
    {
        if (_scoreP1 != null) { _board.AddLine(_scoreP1); }
        if (_scoreP2 != null) { _board.AddLine(_scoreP2); }
    }

    private void setupScoreBoard()
    {
        showScores();
    }

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
                _scoreBoard[i].Size(56);
                _scoreBoard[i].SetXY(400, 50 + 100 * place);
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
                _scoreBoard[i].Size(56);
                _scoreBoard[i].SetXY(400, 50 + 100 * place);
                Int32.TryParse(scores[i], out score);
                _scoreBoard[i].SetText(place + ":" + score);
                place++;
            }
        }
        
    }

}