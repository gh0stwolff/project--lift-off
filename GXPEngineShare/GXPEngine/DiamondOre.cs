using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class DiamondOre : Tile
{
    private int PointsOnPickUp = 5;

    public DiamondOre(float x, float y) : base("DiamondOre.png", x, y, 2)
    {

    }

    public void Collect()
    {
        selfDestroy(PointsOnPickUp);
    }
}
