using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using System.IO;

class ScoreBoard
{
    private string _filePath;

    List<string> Lines = new List<string>();

    public ScoreBoard(string filePath)
    {
        _filePath = filePath;
        readFile();
    }

    private void readFile()
    {
        Lines = File.ReadAllLines(_filePath).ToList();
    }

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

    public List<string> getHighScores()
    {
        Lines.Sort();
        Lines.Reverse();
        return Lines;
    }

}
