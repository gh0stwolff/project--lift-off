using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Emerald : Tile
{
    private int PointsOnPickUp = 5;

    public Emerald(float locX, float locY) : base("Emerald.png", locX, locY, 2)
    {

    }

    public void Collect()
    {
        selfDestroy(PointsOnPickUp);
    }

}