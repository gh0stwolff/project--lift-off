using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class HUD : Canvas
{
    private TextBoard _textScore;
    private EasyDraw _score;

    public HUD(float width, float height) : base((int)width, (int)height)
    {
        SetXY(0, 100);
        _textScore = new TextBoard(124, 24);
        _textScore.x += 8;
        _textScore.y -= 32;
        AddChild(_textScore);

        _score = new EasyDraw(350, 200);
        AddChild(_score);
        _score.TextAlign(CenterMode.Min, CenterMode.Min);
    }

    public void Update()
    {
        y -= ((MyGame)game).GetScreenSpeed();
        _textScore.SetText("Points: " + ((MyGame)game).GetScore());
    }
}
