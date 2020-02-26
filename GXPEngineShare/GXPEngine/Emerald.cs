using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Emerald : Collectable
{
    private const int _pointsOnPickUp = 5;

    public Emerald(float locX, float locY) : base("Emerald.png", locX, locY, 2, _pointsOnPickUp)
    {

    }

}