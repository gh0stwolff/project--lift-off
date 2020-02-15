using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Lava : AnimationSprite
{
    private float _boilingLevel = 0;
    private float _height = 0;

    public Lava() : base("Lava.png", 1, 1)
    {
        SetXY(0, ((MyGame)game).height - height/2);
    }

    public void Update()
    {
        _height++;
        y -= ((MyGame)game).GetScreenSpeed() - _boilingLevel;
        boiling();
    }

    private void boiling()
    {
        _boilingLevel = (float)GetLevel();
    }

    private double GetLevel()
    {
        double waveAmplitude = 0.5;
        double waveSpeed = 100; //lower number increases frequency
        double horizontalOffset = 0;

        double b = waveAmplitude;
        double T = waveSpeed;
        double X = _height;
        double d = horizontalOffset;

        return (b * Math.Sin(((2 * Math.PI) / T) * (X - d)));
    }

}
