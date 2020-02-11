using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class MapGenerator : GameObject
{
    private int _blockCountWidth;

    public MapGenerator(int blockCountWidth) : base()
    {
        _blockCountWidth = blockCountWidth;
    }

    public void Update()
    {
        if(Input.GetKey(Key.L))
        {
            generateNewLine();
        }
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
                    Console.WriteLine("dirt");
                    break;
                case 1:
                    DiamondOre diamond = new DiamondOre(getScreenLocation(i));
                    AddChild(diamond);
                    Console.WriteLine("diamond");
                    break;
            }
        }
        Console.WriteLine("-----------------------");

        //foreach (int numb in newLine)
        //{

        //}

        //array[,] tiles;

        //for (int i = 0; i < tiles.GetLength(0); i++)
        //{
        //    for (int j = 0; j < tiles.GetLength(1); j++)
        //    {
        //        AnimationSprite tile = new AnimationSprite(fileName, 2, 1);
        //        AddChild(tile);
        //        tile.x = blockWidth * i;
        //        tile.y = blockHeight * j;

        //        tiles[i, j] = tile;
        //    }
        //}
    }

    private float getScreenLocation(int index)
    {
        int xPosBlock = ((MyGame)game).width / _blockCountWidth;
        int x = xPosBlock * index;
        return x;
    }
}

