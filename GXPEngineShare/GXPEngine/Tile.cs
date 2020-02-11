using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Tile : Sprite
{
    private float _speed = 0.5f;

    public Tile(string fileName, float xLoc) : base(fileName)
    {
        x = xLoc;
    }

    public void Update()
    {
        y = y + _speed;

        if(y > ((MyGame)game).height)
        {
            selfDestroy();
        }
    }

    private void selfDestroy()
    {
        LateDestroy();
    }

    public float GetSpeed()
    {
        return _speed;
    }
    public int GetHeight()
    {
        return width;
    }

}
