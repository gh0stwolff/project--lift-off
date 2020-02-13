using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class MapGenerator : GameObject
{
    //types of tiles
    const int DIRT = 0;
    const int DIAMOND = 1;
    const int STONE = 2;

    //for generating new lines
    private int _blockCountWidth;
    private int _minWidth = 6;
    private int _maxWidth = 18;
    private int _framesBetweenLines;
    private int _lineNumb;
    private int _linesTillNarrowing = -5;
    private int _timer = 0;
    private int _targetLine;
    //how big the outerborder is
    private int _rockThicknessLeft = 2;
    private int _rockThicknessRight = 2;


    private bool _isGoingOutWards = false;

    private Tile _tile;

    enum BlockType { Dirt, Stone, Diamond}
    BlockType Type = BlockType.Dirt;

    public MapGenerator() : base()
    {
        _tile = new Tile("Dirt.png", 0, 0);
        _blockCountWidth = ((MyGame)game).width / _tile.width;
        _framesBetweenLines = (int)(_tile.GetHeight() / ((MyGame)game).GetScreenSpeed());
        _lineNumb = -1;

        Level spawn = new Level("startPoint.tmx", _blockCountWidth, 13);
        AddChild(spawn);

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

        if (_lineNumb < _targetLine)
        {
            handleWallStat();
            ChangeWallThickness();

            _targetLine = _linesTillNarrowing + _lineNumb;
        }

        for (int i = 0; i < newLine.GetLength(0); i++)
        {
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
                newLine[i] = getRandomNumb(i);
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

    private void ChangeWallThickness()
    {
        if (!_isGoingOutWards)
        {
            if (_rockThicknessLeft == _rockThicknessRight)
            {
                _rockThicknessLeft++;
            }
            else
            {
                _rockThicknessRight++;
            }
        }
        else
        {
            if (_rockThicknessLeft == _rockThicknessRight)
            {
                _rockThicknessLeft--;
            }
            else
            {
                _rockThicknessRight--;
            }
        }
    }

    private void handleWallStat()
    {
        if (_blockCountWidth - (_rockThicknessRight + _rockThicknessLeft) <= _minWidth)
        {
            _isGoingOutWards = true;
        }
        else if (_blockCountWidth - (_rockThicknessRight + _rockThicknessLeft) >= _maxWidth)
        {
            _isGoingOutWards = false;
        }
    }

    private int getRandomNumb(int index)
    {
        float dirtChance = getDirtSpawnChance(index);
        float diamondChance = getDiamondSpawnChance(index);
        float stoneChance = getStoneSpawnChance(index);

        float randomNumb = Utils.Random(0, dirtChance + diamondChance + stoneChance + 1);

        if (randomNumb < dirtChance)
        {
            Type = BlockType.Dirt;
        }
        else if (randomNumb < dirtChance + diamondChance)
        {
            Type = BlockType.Diamond;
        }
        else if (randomNumb < dirtChance + diamondChance + stoneChance)
        {
            Type = BlockType.Stone;
        }

        switch (Type)
        {
            case BlockType.Dirt:
                return DIRT;
            case BlockType.Diamond:
                return DIAMOND;
            case BlockType.Stone:
                return STONE;
        }

        return 0;
    }

    private float getDirtSpawnChance(int index)
    {
        float maxChance = 80.0f;
        float minChance = 50.0f;
        bool isChanceHigherInMiddle = true;

        return calculateChance(maxChance, minChance, isChanceHigherInMiddle, index);
    }

    private float getDiamondSpawnChance(int index)
    {
        float maxChance = 10.0f;
        float minChance = 5.0f;
        bool isChanceHigherInMiddle = false;

        return calculateChance(maxChance, minChance, isChanceHigherInMiddle, index);
    }

    private float getStoneSpawnChance(int index)
    {
        float maxChance = 15.0f;
        float minChance = 10.0f;
        bool isChanceHigherInMiddle = true;

        return calculateChance(maxChance, minChance, isChanceHigherInMiddle, index);
    }

    private float calculateChance(float maxChance, float minChance, bool isChanceHigherInMiddle, int index)
    {
        if (isChanceHigherInMiddle)
        {
            float y = minChance;
            float x = index;
            float p = _blockCountWidth / 2;
            float q = maxChance;

            double a = (y - q) / (p * p);

            y = (float)(a * ((x - p) * (x - p)) + q);
            return y;
        }
        else
        {
            float y = maxChance;
            float x = index;
            float p = _blockCountWidth / 2;
            float q = minChance;

            double a = (y - q) / (p * p);

            y = (float)(a * ((x - p) * (x - p)) + q);
            return y;

        }
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

