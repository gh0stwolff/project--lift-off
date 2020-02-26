using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Air : Collectable
{
    private const int _pointsOnPickUp = 0;

    public Air(float locX, float locY) : base("air.png", locX, locY, 2, _pointsOnPickUp)
    {

    }

}
