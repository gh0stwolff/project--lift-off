using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Lava : AnimationSprite
{

    public Lava() : base("Lava.png", 1, 1)
    {
        SetXY(0, ((MyGame)game).height - height);
    }

    public void Update()
    {
        y -= ((MyGame)game).GetScreenSpeed();
    }

}
