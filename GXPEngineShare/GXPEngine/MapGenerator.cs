using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using TiledMapParser;
class MapGenerator : GameObject
{
    //types of tiles
    const int AIR = 1;
    const int DIRT = 2;
    const int STONE = 3;
    const int DIAMOND = 4;


    //for generating new lines
    private int _blockCountWidth;
    private int _minWidth = 6;
    private int _maxWidth = 18;
    private int _framesBetweenLines;
    private int _lineNumb;
    //private int _amountOfStartingLinesWithRockRestirction = 10;
    private int _linesTillNarrowing = -5;
    private int _timer = 0;
    private int _targetLine;
    //how big the outerborder is
    private int _rockThicknessLeft = 2;
    private int _rockThicknessRight = 2;


    private bool _isGoingOutWards = false;

    private Tile _tile;
    private Lava _lava;

    enum BlockType { Dirt, Stone, Diamond}
    BlockType Type = BlockType.Dirt;
    private ScreenLayer[] layers = new ScreenLayer[4];


    public MapGenerator() : base()
    {
        _tile = new Tile("Dirt.png", 0, 0);
        _blockCountWidth = ((MyGame)game).width / _tile.width;
        _framesBetweenLines = (int)(_tile.GetHeight() / ((MyGame)game).GetScreenSpeed());
        _lineNumb = -1; //((MyGame)game).height / _tile.GetHeight();
        //_amountOfStartingLinesWithRockRestirction += _lineNumb - 21;

        for (int i = 0; i < layers.Length; i++)
        {
            layers[i] = new ScreenLayer();
            AddChild(layers[i]);
        }
        setupSpawn();

        //for (int i = 0; i < _lineNumb; i = -1)
        //{
        //    generateNewLine();
        //}


        //Player player = new Player(((MyGame)game).width / 2, ((MyGame)game).height / 2);
        //AddChild(player);
        Worm worm = new Worm();
        AddChild(worm);
        //Level spawn = new Level("startPoint.tmx", _blockCountWidth, 13);
        //layers[2].AddChild(spawn);

        _lava = new Lava();
        layers[1].AddChild(_lava);
 
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
                    layers[0].AddChild(dirt);
                    break;
                case DIAMOND:
                    DiamondOre diamond = new DiamondOre(getXLocation(i), getYLocation(_lineNumb));
                    layers[0].AddChild(diamond);
                    break;
                case STONE:
                    Stone stone = new Stone(getXLocation(i), getYLocation(_lineNumb));
                    layers[2].AddChild(stone);
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
        //if (_amountOfStartingLinesWithRockRestirction < _lineNumb)
        //{
        //    stoneChance = 0;
        //}


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

    private void setupSpawn()
    {
        Map levelData = MapParser.ReadMap("startPoint.tmx");
        spawnTiles(levelData);
        spawnObjects(levelData);
    }

    private void spawnTiles(Map levelData)
    {
        if (levelData.Layers == null || levelData.Layers.Length == 0)
            return;

        Layer mainLayer = levelData.Layers[0];
        short[,] tileNumbers = mainLayer.GetTileArray();
        for (int row = 0; row < mainLayer.Height; row++)
        {
            for (int column = 0; column < mainLayer.Width; column++)
            {
                int tileNumber = tileNumbers[column, row];
                if (tileNumber == STONE)
                {
                    Stone stone = new Stone(getXLocation(column), getYLocation(row));
                    layers[2].AddChild(stone);
                }
                if (tileNumber == DIRT)
                {
                    Dirt dirt = new Dirt(getXLocation(column), getYLocation(row));
                    layers[0].AddChild(dirt);
                }
                if (tileNumber == DIAMOND)
                {
                    DiamondOre diamond = new DiamondOre(getXLocation(column), getYLocation(row));
                    layers[0].AddChild(diamond);
                }
            }
        }
    }

    private void spawnObjects(Map levelData)
    {
        if (levelData.ObjectGroups == null || levelData.ObjectGroups.Length == 0)
            return;

        ObjectGroup objectGroup = levelData.ObjectGroups[0];
        if (objectGroup.Objects == null || objectGroup.Objects.Length == 0)
            return;

        foreach (TiledObject obj in objectGroup.Objects)
        {
            switch (obj.Name)
            {
                case "Player":
                    Player player = new Player(obj.X, obj.Y);
                    layers[3].AddChild(player);
                    break;
            }
        }
    }
}