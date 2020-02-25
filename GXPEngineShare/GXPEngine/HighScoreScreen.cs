using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using System.IO;

class HighScoreScreen : Canvas
{
    private string _line;
    private List<int> scores = new List<int>();
    private TextBoard[] _scoreBoard = new TextBoard[5];
    private StreamReader _reader;

    public HighScoreScreen(int width, int height) : base(width, height)
    {
        _reader = new StreamReader("Score.txt");
        _line = _reader.ReadLine();
        setupScoreBoard();
    }

    private void setupScoreBoard()
    {
        Console.WriteLine("start");
        readLines();
        _reader.Close();
        Console.WriteLine("done");
        sortScores();
        showScores();
    }

    private void readLines()
    {
        while (_line != null)
        {
            int score;

            Int32.TryParse(_line, out score);
            scores.Add(score);
            Console.WriteLine(score);
            _line = _reader.ReadLine();
        }
    }

    private void sortScores()
    {
        scores.Sort();
        scores.Reverse();
        for (int i = 0; i < scores.Count; i++)
        {
            Console.WriteLine(scores[i]);
        }
    }

    private void showScores()
    {
        int place = 1;
        if (scores.Count < _scoreBoard.Length)
        {
            for (int i = 0; i < scores.Count; i++)
            {
                _scoreBoard[i] = new TextBoard(500, 300);
                AddChild(_scoreBoard[i]);
                _scoreBoard[i].Size(72);
                _scoreBoard[i].SetXY(400, 200 + 100 * place);
                int score = scores[i];
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
                _scoreBoard[i].Size(72);
                _scoreBoard[i].SetXY(400, 200 + 100 * place);
                int score = scores[i];
                Console.WriteLine("{0}. {1}", place, score);
                _scoreBoard[i].SetText(place + ":" + score);
                place++;
            }
        }
        
    }

}