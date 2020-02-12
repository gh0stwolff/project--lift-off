using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Tile : Sprite
{

    public Tile(string fileName, float xLoc, float yLoc) : base(fileName, false , true)
    {
        x = xLoc;
        y = yLoc;
    }

    public void Update()
    {
        if(y > ((MyGame)game).height)
        {
            selfDestroy();
        }
    }

    private void selfDestroy()
    {
        LateDestroy();
    }

    public int GetHeight()
    {
        return width;
    }

}
