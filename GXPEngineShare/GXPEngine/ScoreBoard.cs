using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using System.IO;

class ScoreBoard
{
    #region variables
    private string _filePath;

    List<string> Lines = new List<string>();
    #endregion

    #region setup
    public ScoreBoard(string filePath)
    {
        _filePath = filePath;
        readFile();
        getHighScores();
    }
    #endregion


    #region add a new line
    public void AddLine(string score)
    {
        addLine(score);
        StoreData();
        readFile();
    }
    private void addLine(string score)
    {
        Lines.Add(score);
    }

    private void StoreData()
    {
        File.WriteAllLines(_filePath, Lines);
    }

    private void readFile()
    {
        Lines = File.ReadAllLines(_filePath).ToList();
    }
    #endregion

    #region sort and sent array
    public List<string> getHighScores()
    {
        SortList(Lines);
        return Lines;
    }

    private void SortList(List<string> list)
    {
        for (int i = 0; i <= list.Count - 2; i++)
        {
            for (int j = 0; j <= list.Count - 2; j++)
            {
                Int32.TryParse(list[j], out int score1);
                Int32.TryParse(list[j + 1], out int score2);

                if (score1 < score2)
                {
                    string t = list[j + 1];
                    list[j + 1] = list[j];
                    list[j] = t;
                }
            }
        }
    }
    #endregion
}
