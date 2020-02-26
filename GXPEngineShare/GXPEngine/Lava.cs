using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Lava : AnimationSprite
{
    private float _boilingLevel = 0;
    private float _height = 0;
    private Sprite _outOfBounds;

    public Lava() : base("Lava.png", 1, 1)
    {
        SetXY(0, ((MyGame)game).height - height/2);
        Sprite glow = new Sprite("lavaGlow.png");
        AddChild(glow);
        glow.y = -720;
        glow.alpha = 0.3f;
        _outOfBounds = new Sprite("lavaGlow.png");
        AddChild(_outOfBounds);
        _outOfBounds.y = 100;
    }

    public void Update()
    {
        _height++;
        y -= ((MyGame)game).GetScreenSpeed() - _boilingLevel;
        boiling();
        destroyTiles();
    }

    private void boiling()
    {
        _boilingLevel = (float)GetLevel();
    }

    private void destroyTiles()
    {
        foreach (GameObject other in _outOfBounds.GetCollisions())
        {
            if (other is Tile)
            {
                other.LateDestroy();
            }
        }
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
