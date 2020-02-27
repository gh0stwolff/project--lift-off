using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Gold : Collectable
{
    private const int _pointsOnPickUp = 5;

    public Gold(float locX, float locY) : base("Gold.png", locX, locY, 2, _pointsOnPickUp)
    {

    }

}
