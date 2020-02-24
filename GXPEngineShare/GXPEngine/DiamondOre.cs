using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class DiamondOre : Tile
{
    private bool _doOnce = true;

    public DiamondOre(float x, float y) : base("DiamondOre.png", x, y, 2)
    {

    }

    public void collect()
    {
        //add points

        //add before LateDestroy a partical effect
        if (_doOnce)
        {
            ((MyGame)game).AddScore(5);
            _doOnce = false;
        }
        selfDestroy();
    }
}
