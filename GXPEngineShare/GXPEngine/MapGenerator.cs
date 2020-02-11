using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class MapGenerator : GameObject
{
    private int _blockCountWidth;
    private float _lineSpeed = 0;
    private int _lineHeight = 0;
    private int _framesBetweenLines;
    private float _timer = 0;

    private Tile tile;

    public MapGenerator(int blockCountWidth) : base()
    {
        _blockCountWidth = blockCountWidth;
        tile = new Tile("Dirt.png", 0);
        _lineSpeed = tile.GetSpeed();
        _lineHeight = tile.GetHeight();
        _framesBetweenLines = (int)(tile.GetHeight() / tile.GetSpeed());
    }

    public void Update()
    {
        if(Input.GetKey(Key.L))
        {
            generateNewLine();
        }
        timerNewLine();
        Console.WriteLine(Time.deltaTime);
    }

    private void timerNewLine()
    {
        if (_timer <= 0)
        {
            generateNewLine();
            _timer = _framesBetweenLines;
        }

        _timer--;

        //count frame met int counter


    }

    private void generateNewLine()
    {
        int[] newLine;
        newLine = new int[_blockCountWidth];

        for (int i = 0; i < newLine.GetLength(0); i++)
        {
            newLine[i] = (int)Utils.Random(0, 2);
        }

        for (int i = 0; i < newLine.GetLength(0); i++)
        {
            switch (newLine[i])
            {
                case 0:
                    Dirt dirt = new Dirt(getScreenLocation(i));
                    AddChild(dirt);
                    //Console.WriteLine("dirt");
                    break;
                case 1:
                    DiamondOre diamond = new DiamondOre(getScreenLocation(i));
                    AddChild(diamond);
                    //Console.WriteLine("diamond");
                    break;
            }
        }
        //Console.WriteLine("-----------------------");
    }

    private float getScreenLocation(int index)
    {
        int xPosBlock = ((MyGame)game).width / _blockCountWidth;
        int x = xPosBlock * index;
        return x;
    }
}

