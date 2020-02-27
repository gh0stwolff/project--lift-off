using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Dirt : Collectable
{
    private const int _pointsOnPickUp = 0;

    public Dirt(float x, float y) : base("Dirt.png", x, y, 2, _pointsOnPickUp)
    {

    }

    public void Digged()
    {
        selfDestroy(_pointsOnPickUp);
    }

}
