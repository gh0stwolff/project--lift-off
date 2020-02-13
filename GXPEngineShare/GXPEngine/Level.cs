using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using TiledMapParser;

public class Level : GameObject
{

    public Level(String fileName, int width, int height) : base()
    {
        Map levelData = MapParser.ReadMap(fileName);
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
                Console.WriteLine(tileNumber);
                if (tileNumber == 1)
                {
                    Stone stone = new Stone(0, 0);
                    stone.x = stone.width * column;
                    stone.y = stone.height * row;
                    AddChild(stone);
                }
                else if (tileNumber == 2)
                {
                    Dirt dirt = new Dirt(0, 0);
                    dirt.x = dirt.width * column;
                    dirt.y = dirt.height * row;
                    AddChild(dirt);
                }
                else if (tileNumber == 3)
                {
                    DiamondOre diamond = new DiamondOre(0, 0);
                    diamond.x = diamond.width * column;
                    diamond.y = diamond.height * row;
                    AddChild(diamond);
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
                case "TestPlayer":
                    TestPlayer player = new TestPlayer();
                    AddChild(player);
                    break;
            }
        }
    }
}
