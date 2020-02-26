﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Dirt : Tile
{
    private int PointsOnPickUp = 0;

    public Dirt(float x, float y) : base("Dirt.png", x, y, 2)
    {

    }

    public void Digged()
    {
        selfDestroy(PointsOnPickUp);
    }

}
