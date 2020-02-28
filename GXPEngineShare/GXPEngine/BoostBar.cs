using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class BoostBar : AnimationSprite
{
    private int _boost;
    public BoostBar() : base("boostbar.png", 11, 1)
    {

    }

    void Update()
    {
        _boost = ((MyGame)game).GetBooster();
        y = - ((MyGame)game).GetScreenY();
        Animation();
    }

    void Animation()
    {
        if (_boost % 60 == 0)
        {
            SetFrame(_boost / 60);
        }
    }
}