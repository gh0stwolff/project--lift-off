using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Tile : Sprite
{
    private float _speed = 2.0f;

    public Tile(string fileName, float xLoc) : base(fileName)
    {
        x = xLoc;
    }

    public void Update()
    {
        y = y + _speed;
    }

}
