using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class MapGenerator : GameObject
{
    const int DIRT = 0;
    const int DIAMOND = 1;
    const int STONE = 2;

    private int _blockCountWidth;
    private int _framesBetweenLines;
    private int _lineNumb;
    private int _linesTillNarrowing = -5;
    private int _targetLine;
    private int _rockThicknessLeft = 2;
    private int _rockThicknessRight = 2;

    private float _dirtChance = 90.0f;
    private float _diamondChance = 10.0f;
    private float _timer = 0;

    private Tile _tile;

    enum BlockType { Dirt, Diamond}
    BlockType Type = BlockType.Dirt;

    public MapGenerator(int blockCountWidth) : base()
    {
        _blockCountWidth = blockCountWidth;
        _tile = new Tile("Dirt.png", 0, 0);
        _framesBetweenLines = (int)(_tile.GetHeight() / ((MyGame)game).GetScreenSpeed());
        _lineNumb = (((MyGame)game).height / _tile.GetHeight());
        for (int i = 0; i < _lineNumb; i = -1)
        {
            generateNewLine();
        }
        _targetLine = _lineNumb + _linesTillNarrowing;
    }

    public void Update()
    {
        if(Input.GetKey(Key.L))
        {
            generateNewLine();
        }
        timerNewLine();
    }

    private void timerNewLine()
    {
        if (_timer <= 0)
        {
            generateNewLine();
            _timer = _framesBetweenLines;
        }
        _timer--;
    }

    private void generateNewLine()
    {
        int[] newLine;
        newLine = new int[_blockCountWidth];

        for (int i = 0; i < newLine.GetLength(0); i++)
        {

            if (_lineNumb < _targetLine)
            {
                if (_rockThicknessLeft == _rockThicknessRight)
                {
                    _rockThicknessLeft++;
                }
                else
                {
                    _rockThicknessRight++;
                }
                _targetLine = _linesTillNarrowing + _lineNumb;
            }

            if (_rockThicknessLeft > i)
            {
                newLine[i] = STONE;
            }
            else if (i >= _blockCountWidth - _rockThicknessRight)
            {
                newLine[i] = STONE;
            }
            else
            {
                newLine[i] = getRandomNumb();
            }
        }

        for (int i = 0; i < newLine.GetLength(0); i++)
        {
            switch (newLine[i])
            {
                case DIRT:
                    Dirt dirt = new Dirt(getXLocation(i), getYLocation(_lineNumb));
                    AddChild(dirt);
                    break;
                case DIAMOND:
                    DiamondOre diamond = new DiamondOre(getXLocation(i), getYLocation(_lineNumb));
                    AddChild(diamond);
                    break;
                case STONE:
                    Stone stone = new Stone(getXLocation(i), getYLocation(_lineNumb));
                    AddChild(stone);
                    break;
            }
        }

        _lineNumb--;
    }

    private int getRandomNumb()
    {

        float randomNumb = Utils.Random(0, 101);

        Console.WriteLine(randomNumb);

        if (randomNumb < _dirtChance)
        {
            Type = BlockType.Dirt;
        }
        else if (_dirtChance < randomNumb && randomNumb < _dirtChance + _diamondChance)
        {
            Type = BlockType.Diamond;
        }

        switch (Type)
        {
            case BlockType.Dirt:
                return DIRT;
            case BlockType.Diamond:
                return DIAMOND;
        }

        return 0;
    }

    private float getXLocation(int index)
    {
        int xPosBlock = ((MyGame)game).width / _blockCountWidth;
        int x = xPosBlock * index;
        return x;
    }

    private float getYLocation(int lineNumb)
    {
        return _tile.GetHeight() * lineNumb;
    }
}

