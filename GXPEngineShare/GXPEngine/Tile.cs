using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Tile : AnimationSprite
{

    private bool _doOnce = true;
    private bool _startAnimation = false;
    private ParticalEffect _particals;

    public Tile(string fileName, float xLoc, float yLoc, int numberOfFrames) : base(fileName, numberOfFrames , 1)
    {
        x = xLoc;
        y = yLoc;
    }

    public void Update()
    {
        if (y > -((MyGame)game).GetScreenY() + ((MyGame)game).GetScreenHeight())
        {
            LateDestroy();
        }

        Animation();
    }

    protected void selfDestroy(int points)
    {
        if (_doOnce)
        {
            ((MyGame)game).AddScore(points);
            _startAnimation = true;
            _doOnce = false;
        }
    }

    private void Animation()
    {
        if (_startAnimation)
        {
            _particals = new ParticalEffect("tileExplosion.png", 8, 1);
            AddChild(_particals);
            SetFrame(1);
            _startAnimation = false;
        }
        if (_particals != null)
        {
            if (_particals.GetDoneState())
            {
                LateDestroy();
            }
        }
    }


    public int GetHeight()
    {
        return height;
    }
}
