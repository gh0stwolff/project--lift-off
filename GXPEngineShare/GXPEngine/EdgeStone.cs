using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class EdgeStone : Tile
{

    public EdgeStone(float x, float y, bool isEdge) : base( "EdgeStone.png", x, y, 3)
    {
        checkSpawnLocation(x, isEdge);
    }

    private void checkSpawnLocation(float x, bool isEdge)
    {
        if (isEdge)
        {
            if (x < ((MyGame)game).GetScreenWidth() / 2) { SetFrame(2); }
            else { SetFrame(0); }
        }
        else { SetFrame(1); }
    }

}
