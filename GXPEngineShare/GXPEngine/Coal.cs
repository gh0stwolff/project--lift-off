using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Coal : Tile
{
    private int PointsOnPickUp = 5;

    public Coal(float locX, float locY) : base("Coal.png", locX, locY, 2)
    {

    }

    public void Collect()
    {
        selfDestroy(PointsOnPickUp);
    }

}