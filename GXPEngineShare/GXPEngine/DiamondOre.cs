﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class DiamondOre : Tile
{

    public DiamondOre(float x, float y) : base("DiamondOre.png", x, y)
    {

    }

    public void collect()
    {
        //add points

        //add before LateDestroy a partical effect
        LateDestroy();
    }
}
