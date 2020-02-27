using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Iron : Collectable
{
    private const int _pointsOnPickUp = 5;

    public Iron(float locX, float locY) : base("Iron.png", locX, locY, 2, _pointsOnPickUp)
    {
    }
}