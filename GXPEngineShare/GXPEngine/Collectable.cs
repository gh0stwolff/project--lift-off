using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Collectable : Tile
{
    private int _pointsOnPickUp;

    public Collectable(string fileName, float locX, float locY, int frames, int pointsOnPickup): base(fileName, locX, locY, frames)
    {
        _pointsOnPickUp = pointsOnPickup;
    }

    public void Collect()
    {
        selfDestroy(_pointsOnPickUp);
    }

}
