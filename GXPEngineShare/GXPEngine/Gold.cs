using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Gold : Tile
{
    private int PointsOnPickUp = 5;

    public Gold(float locX, float locY) : base("Gold.png", locX, locY, 2)
    {

    }

    public void Collect()
    {
        selfDestroy(PointsOnPickUp);
    }

}
