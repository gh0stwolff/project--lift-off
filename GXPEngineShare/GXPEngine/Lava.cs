using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Lava : AnimationSprite
{
    #region variables
    private float _boilingLevel = 0;
    private float _height = 0;
    private float timer = 0;

    private AnimationSprite _secondLava;
    #endregion

    #region setup & update
    public Lava() : base("Lava.png", 1, 4)
    {
        SetXY(0, ((MyGame)game).height - height/2);
        _secondLava = new AnimationSprite("Lava.png", 1, 4);
        _secondLava.SetXY(0, 50);
        Sprite glow = new Sprite("lavaGlow.png");
        AddChild(glow);
        AddChild(_secondLava);
        glow.y = -720;
        glow.alpha = 0.5f;
    }

    public void Update()
    {
        _height++;
        y -= ((MyGame)game).GetScreenSpeed() - _boilingLevel;
        boiling();
        animation();
    }

    private void animation()
    {
        int startFrame = 0;
        int numbOfFrames = 4;
        int timeBetweenFrames = 500;
        timer += Time.deltaTime;

        int currentFrame = (int)(timer / timeBetweenFrames) % numbOfFrames + startFrame;
        SetFrame(currentFrame);
        _secondLava.SetFrame(currentFrame);
    }
    #endregion

    #region boiling effect
    private void boiling()
    {
        _boilingLevel = (float)GetLevel();
    }

    private double GetLevel()
    {
        double waveAmplitude = 0.8;
        double waveSpeed = 250; //lower number increases frequency
        double horizontalOffset = 100;

        double b = waveAmplitude;
        double T = waveSpeed;
        double X = _height;
        double d = horizontalOffset;

        return (b * Math.Sin(((2 * Math.PI) / T) * (X - d)));
    }
    #endregion
}
