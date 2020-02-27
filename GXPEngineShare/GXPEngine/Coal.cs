using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Coal : Collectable
{
    private const int _pointsOnPickUp = 5;

    public Coal(float locX, float locY) : base("Coal.png", locX, locY, 2, _pointsOnPickUp)
    {

    }

}