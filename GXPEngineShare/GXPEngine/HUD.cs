using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class HUD : Canvas
{
    #region variables
    private TextBoard _textScore;
    #endregion

    #region setup & update
    public HUD(float width, float height) : base((int)width, (int)height)
    {
        SetXY(0, 100);
        _textScore = new TextBoard(124, 24);
        _textScore.x = 8;
        _textScore.y = 32;
        AddChild(_textScore);
    }

    public void Update()
    {
        y -= ((MyGame)game).GetScreenSpeed();
        _textScore.SetText("Points: " + ((MyGame)game).GetScore());
    }
    #endregion
}
