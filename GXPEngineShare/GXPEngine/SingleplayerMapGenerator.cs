﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using TiledMapParser;
class SingleplayerMapGenerator : GameObject
{
    #region variables
    #region constants
    //types of tiles
    const int AIR = 1;
    const int DIRT = 2;
    const int STONE = 3;
    const int DIAMOND = 4;
    const int EDGESTONE = 5;
    const int DARKNESS = 6;
    const int IRON = 7;
    const int GOLD = 8;
    const int COAL = 9;
    const int EMERALD = 10;
    #endregion

    #region lineGeneration
    //for generating new lines
    private int _blockCountWidth;
    private int _minWidth = 6;
    private int _maxWidth = 18;
    private float _framesBetweenLines;
    private int _lineNumb;
    private int _linesTillNarrowing = -5;
    private float _timer = 0;
    private int _targetLine;
    //how big the outerborder is
    private int _rockThicknessLeft = 2;
    private int _rockThicknessRight = 2;

    private bool _isGoingOutWards = false;
    #endregion

    private Tile _tile;
    private Lava _lava;
    private Sprite _background;

    private ScreenLayer[] layers = new ScreenLayer[8];
    #endregion

    #region setup & update
    public SingleplayerMapGenerator() : base()
    {
        setupVariables();
        addObject();
        //spawn the first area from tiled
        setupSpawn();
        //generate the first line after the tiled map
        generateNewLine();
    }

    #region setup methods
    private void setupVariables()
    {
        //giving tile a block
        _tile = new Tile("Dirt.png", 0, 0, 2, 0);
        //calc how many tiles fit in the width
        _blockCountWidth = ((MyGame)game).width / _tile.width;
        //how low it takes before a new line needs to be drawn
        _framesBetweenLines = (int)(_tile.GetHeight() / ((MyGame)game).GetScreenSpeed());
        //reset the score in case it is the 2nd round
        ((MyGame)game).ResetScore();
        //next line number before the wall becomes one block smaller/wider
        _targetLine = _lineNumb + _linesTillNarrowing;
    }

    private void addObject()
    {
        //initialize layers
        for (int i = 0; i < layers.Length; i++)
        {
            layers[i] = new ScreenLayer();
            AddChild(layers[i]);
        }
        //adding lava
        _lava = new Lava();
        layers[2].AddChild(_lava);
        //adding a hud
        HUD hud = new HUD(((MyGame)game).GetScreenWidth(), ((MyGame)game).GetScreenHeight());
        layers[3].AddChild(hud);
        BoostBar boostbar = new BoostBar();
        layers[7].AddChild(boostbar);
        //adding a background
        _background = new Sprite("inGameBackground.png");
        layers[0].AddChild(_background);
    }
    #endregion

    public void Update()
    {
        timerNewLine();
        if (((MyGame)game).GetScreenSpeed() < ((MyGame)game).GetMaxScreenSpeed())
        {
            ((MyGame)game).IncreaseSpeed();
        }
        _framesBetweenLines = (int)(_tile.GetHeight() / ((MyGame)game).GetScreenSpeed());
    }
    #endregion

    #region lineGenerator
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
            handleWallState();
            changeWallThickness();
            _targetLine = _linesTillNarrowing + _lineNumb;
        }

        for (int i = 0; i < newLine.GetLength(0); i++)
        {
            if (_rockThicknessLeft == i + 1) { newLine[i] = EDGESTONE; }
            else if (_rockThicknessLeft > i) { newLine[i] = DARKNESS; }
            else if (i == _blockCountWidth - _rockThicknessRight) { newLine[i] = EDGESTONE; }
            else if (i > _blockCountWidth - _rockThicknessRight) { newLine[i] = DARKNESS; }
            else { newLine[i] = getRandomNumb(i); }
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
                    layers[0].AddChild(stone);
                    break;
                case EDGESTONE:
                    EdgeStone edgeStone = new EdgeStone(getXLocation(i), getYLocation(_lineNumb), true);
                    layers[2].AddChild(edgeStone);
                    break;
                case DARKNESS:
                    EdgeStone darkness = new EdgeStone(getXLocation(i), getYLocation(_lineNumb), false);
                    layers[2].AddChild(darkness);
                    break;
                case IRON:
                    Iron iron = new Iron(getXLocation(i), getYLocation(_lineNumb));
                    layers[0].AddChild(iron);
                    break;
                case GOLD:
                    Gold gold = new Gold(getXLocation(i), getYLocation(_lineNumb));
                    layers[0].AddChild(gold);
                    break;
                case COAL:
                    Coal coal = new Coal(getXLocation(i), getYLocation(_lineNumb));
                    layers[0].AddChild(coal);
                    break;
                case EMERALD:
                    Emerald emerald = new Emerald(getXLocation(i), getYLocation(_lineNumb));
                    layers[0].AddChild(emerald);
                    break;
            }
        }
        _background.y = -((MyGame)game).GetScreenY() - _tile.height;
        _lineNumb--;
        ((MyGame)game).AddScore(1);
    }

    #region handle sides
    private void changeWallThickness()
    {
        if (!_isGoingOutWards)
        {
            if (_rockThicknessLeft == _rockThicknessRight) { _rockThicknessLeft++; }
            else { _rockThicknessRight++; }
        }
        else
        {
            if (_rockThicknessLeft == _rockThicknessRight)
            { _rockThicknessLeft--; }
            else { _rockThicknessRight--; }
        }
    }

    private void handleWallState()
    {
        if (_blockCountWidth - (_rockThicknessRight + _rockThicknessLeft) <= _minWidth) { _isGoingOutWards = true; }
        else if (_blockCountWidth - (_rockThicknessRight + _rockThicknessLeft) >= _maxWidth) { _isGoingOutWards = false; }
    }
    #endregion

    private int getRandomNumb(int index)
    {
        #region get all chances
        float dirtChance = getDirtSpawnChance(index);
        float diamondChance = getDiamondSpawnChance(index);
        float stoneChance = getStoneSpawnChance(index);
        float airChance = getAirSpawnChance(index);
        float coalChance = getCoalSpawnChance(index);
        float ironChance = getIronSpawnChance(index);
        float goldChance = getGoldSpawnChance(index);
        float emeraldChance = getEmeraldSpawnChance(index);
        #endregion

        float randomNumb = Utils.Random(0, dirtChance + diamondChance + stoneChance + airChance + coalChance + ironChance + goldChance + emeraldChance + 1);

        #region check which tile
        if (randomNumb < dirtChance)
        { return DIRT; }
        else if (randomNumb < dirtChance + diamondChance)
        { return DIAMOND; }
        else if (randomNumb < dirtChance + diamondChance + stoneChance)
        { return STONE; }
        else if (randomNumb < dirtChance + diamondChance + stoneChance + airChance)
        { return AIR; }
        else if (randomNumb < dirtChance + diamondChance + stoneChance + airChance + coalChance)
        { return COAL; }
        else if (randomNumb < dirtChance + diamondChance + stoneChance + airChance + coalChance + ironChance)
        { return IRON; }
        else if (randomNumb < dirtChance + diamondChance + stoneChance + airChance + coalChance + ironChance + goldChance)
        { return GOLD; }
        else if (randomNumb < dirtChance + diamondChance + stoneChance + airChance + coalChance + ironChance + goldChance + emeraldChance)
        { return EMERALD; }
        else { return 0; }
        #endregion
    }

    #region spawnChances
    private float getDirtSpawnChance(int index)
    {
        float maxChance = 80.0f;
        float minChance = 50.0f;
        bool isChanceHigherInMiddle = true;

        return calculateChance(maxChance, minChance, isChanceHigherInMiddle, index);
    }

    private float getDiamondSpawnChance(int index)
    {
        float maxChance = 5.0f;
        float minChance = 2.0f;
        bool isChanceHigherInMiddle = false;

        return calculateChance(maxChance, minChance, isChanceHigherInMiddle, index);
    }

    private float getStoneSpawnChance(int index)
    {
        float maxChance = 35.0f;
        float minChance = 30.0f;
        bool isChanceHigherInMiddle = false;

        return calculateChance(maxChance, minChance, isChanceHigherInMiddle, index);
    }

    private float getAirSpawnChance(int index)
    {
        float maxChance = 5.0f;
        float minChance = 2.0f;
        bool isChanceHigherInMiddle = true;

        return calculateChance(maxChance, minChance, isChanceHigherInMiddle, index);
    }

    private float getCoalSpawnChance(int index)
    {
        float maxChance = 5.0f;
        float minChance = 5.0f;
        bool isChanceHigherInMiddle = true;

        return calculateChance(maxChance, minChance, isChanceHigherInMiddle, index);
    }

    private float getEmeraldSpawnChance(int index)
    {
        float maxChance = 5.0f;
        float minChance = 2.0f;
        bool isChanceHigherInMiddle = false;

        return calculateChance(maxChance, minChance, isChanceHigherInMiddle, index);
    }

    private float getGoldSpawnChance(int index)
    {
        float maxChance = 5.0f;
        float minChance = 2.0f;
        bool isChanceHigherInMiddle = false;

        return calculateChance(maxChance, minChance, isChanceHigherInMiddle, index);
    }

    private float getIronSpawnChance(int index)
    {
        float maxChance = 5.0f;
        float minChance = 4.0f;
        bool isChanceHigherInMiddle = false;

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
    #endregion

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
    #endregion

    #region load tiled map
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
                switch (tileNumber)
                {
                    case AIR:
                        break;
                    case DIRT:
                        Dirt dirt = new Dirt(getXLocation(column), getYLocation(row));
                        layers[0].AddChild(dirt);
                        break;
                    case STONE:
                        Stone stone = new Stone(getXLocation(column), getYLocation(row));
                        layers[0].AddChild(stone);
                        break;
                    case DIAMOND:
                        DiamondOre diamond = new DiamondOre(getXLocation(column), getYLocation(row));
                        layers[0].AddChild(diamond);
                        break;
                    case EDGESTONE:
                        EdgeStone edge = new EdgeStone(getXLocation(column), getYLocation(row), true);
                        layers[3].AddChild(edge);
                        break;
                    case DARKNESS:
                        EdgeStone dark = new EdgeStone(getXLocation(column), getYLocation(row), false);
                        layers[3].AddChild(dark);
                        break;
                    case IRON:
                        Iron iron = new Iron(getXLocation(column), getYLocation(row));
                        layers[0].AddChild(iron);
                        break;
                    case GOLD:
                        Gold gold = new Gold(getXLocation(column), getYLocation(row));
                        layers[0].AddChild(gold);
                        break;
                    case COAL:
                        Coal coal = new Coal(getXLocation(column), getYLocation(row));
                        layers[0].AddChild(coal);
                        break;
                    case EMERALD:
                        Emerald emerald = new Emerald(getXLocation(column), getYLocation(row));
                        layers[0].AddChild(emerald);
                        break;
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
                    layers[1].AddChild(player);
                    break;
            }
        }
    }
    #endregion
}