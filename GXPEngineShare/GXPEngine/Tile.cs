using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Tile : AnimationSprite
{
    #region variables
    protected int _pointsOnPickup;

    private bool _doOnce = true;
    private bool _startAnimation = false;

    private TextBoard _points;
    private ParticalEffect _particals;
    #endregion

    #region setup & update
    public Tile(string fileName, float xLoc, float yLoc, int numberOfFrames, int pointsOnPickup) : base(fileName, numberOfFrames , 1)
    {
        x = xLoc;
        y = yLoc;
        _pointsOnPickup = pointsOnPickup;
    }

    public void Update()
    {
        if (y > -((MyGame)game).GetScreenY() + ((MyGame)game).GetScreenHeight()) { LateDestroy(); }
        Animation();
    }
    #endregion

    #region play partical on destrution
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
            Console.WriteLine(_pointsOnPickup);
            if (_pointsOnPickup > 0)
            {
                _points = new TextBoard(50, 25);
                AddChild(_points);
                _points.SetXY(width / 2, height / 2);
                _points.Size(16);
                _points.SetText("+" + _pointsOnPickup.ToString());
            }
            _startAnimation = false;
        }
        if (_particals != null)
        {
            if (_particals.GetDoneState()) { LateDestroy(); }
        }
        if (_points != null)
        {
            _points.y--;
        }
    }
    #endregion

    public int GetHeight()
    {
        return height;
    }
}
