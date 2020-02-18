using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class DiamondOre : Tile
{
    private bool _doOnce = true;
    private int _pointsUponDestruction = 5;

    public DiamondOre(float x, float y) : base("DiamondOre.png", x, y)
    {

    }

    public void collect()
    {
        //add points

        //add before LateDestroy a partical effect
        selfDestroy();
        if (_doOnce)
        {
            ((MyGame)game).AddScore(_pointsUponDestruction);
            _doOnce = false;
        }
    }
}
