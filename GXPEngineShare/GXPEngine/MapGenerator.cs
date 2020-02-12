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

    //chance in percentages, for each type
    //NOTE: All chances together need to be 100
    private float _dirtChance = 90.0f;
    private float _diamondChance = 5.0f;
    private float _stoneChance = 5.0f;

    private bool _isGoingOutWards = false;

    private Tile _tile;

    enum BlockType { Dirt, Stone, Diamond}
    BlockType Type = BlockType.Dirt;

    public MapGenerator() : base()
    {
        _tile = new Tile("Dirt.png", 0, 0);
        _blockCountWidth = ((MyGame)game).width / _tile.width;
        _framesBetweenLines = (int)(_tile.GetHeight() / ((MyGame)game).GetScreenSpeed());
        _lineNumb = (((MyGame)game).height / _tile.GetHeight());
        for (int i = 0; i < _lineNumb; i = -1)
        {
            generateNewLine();
        }
        TestPlayer player = new TestPlayer();
        AddChild(player);
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
                handleWallStat();
                ChangeWallThickness();

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

        float randomNumb = Utils.Random(0, getDirtSpawnChance(index) + getDiamondSpawnChance(index) + getStoneSpawnChance(index) + 1);

        Console.WriteLine("dirt: {0}, Diamond: {1}, Stone: {2}", getDirtSpawnChance(index), getDiamondSpawnChance(index), getStoneSpawnChance(index));

        if (randomNumb < getDirtSpawnChance(index))
        {
            Type = BlockType.Dirt;
        }
        else if (randomNumb < getDirtSpawnChance(index) + getDiamondSpawnChance(index))
        {
            Type = BlockType.Diamond;
        }
        else if (randomNumb < getDirtSpawnChance(index) + getDiamondSpawnChance(index) + getStoneSpawnChance(index))
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
        // y = a(x*x) - bx + c ,chance lowers in the middle
        // y = -a(x*x) - bx + c, chance is higher in the middle
        // y = answer
        // x = index location
        // a = postive is peak on top, negative is peak on bottom
        // a = < 1 makes the parabool less steep
        // b = moves the graph on the x-axis
        // c = moves the graph on the y-axis

        float maxChance = _dirtChance;
        float minChance = 20f;
        float chanceChangeRate = 1.0f;
        bool isChanceHigherInMiddle = true;

        float a = chanceChangeRate;
        float b = maxChance;
        float c = _blockCountWidth;

        if ( isChanceHigherInMiddle)
        {
            a = a * -1;
            b = minChance;
        }

        return a * (index * index) - b * index + c;
    }

    private float getDiamondSpawnChance(int index)
    {
        float maxChance = _diamondChance;
        float minChance = 1.0f;
        float chanceChangeRate = 1.0f;
        bool isChanceHigherInMiddle = false;

        float a = chanceChangeRate;
        float b = maxChance;
        float c = _blockCountWidth / 2;

        if ( isChanceHigherInMiddle)
        {
            a = a * -1;
            c = minChance;
        }

        return a * (index * index) - b * index + c;
    }

    private float getStoneSpawnChance(int index)
    {
        float maxChance = _stoneChance;
        float minChance = 2.0f;
        float chanceChangeRate = 1.0f;
        bool isChanceHigherInMiddle = true;

        float a = chanceChangeRate;
        float b = maxChance;
        float c = _blockCountWidth / 2;

        if ( isChanceHigherInMiddle)
        {
            a = a * -1;
            c = minChance;
        }

        return a * (index * index) - b * index + c;
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

