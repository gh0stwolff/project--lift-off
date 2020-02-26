using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class DiamondOre : Collectable
{
    private const int _pointsOnPickUp = 5;

    public DiamondOre(float x, float y) : base("DiamondOre.png", x, y, 2, _pointsOnPickUp)
    {

    }
}
