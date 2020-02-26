using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Iron : Tile
{
    private int PointsOnPickUp = 5;

    public Iron(float locX, float locY) : base("Iron.png", locX, locY, 2)
    {

    }

    public void Collect()
    {
        selfDestroy(PointsOnPickUp);
    }

}