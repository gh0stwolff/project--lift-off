using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class HUD : Canvas
{
    private TextBoard _textPoints;

    public HUD(float width, float height) : base((int)width, (int)height)
    {
        SetXY(0, 100);
        _textPoints = new TextBoard(132, 32);
        _textPoints.x += 100;
        _textPoints.y -= 32;
        AddChild(_textPoints);
    }

    public void Update()
    {
        y -= ((MyGame)game).GetScreenSpeed();
        _textPoints.SetText("Points: " + ((MyGame)game).GetScore());
    }
}
