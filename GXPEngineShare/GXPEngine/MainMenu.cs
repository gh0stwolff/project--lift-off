using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class MainMenu : Canvas
{
    #region variables
    private int _time = 0;

    private Sprite _pressStart;
    #endregion

    #region setup & update
    public MainMenu(float width, float height) : base((int)width, (int)height)
    {
        Sprite backGround = new Sprite("Background.png");
        AddChild(backGround);
        _pressStart = new Sprite("pressStart.png");
        AddChild(_pressStart);
        _pressStart.SetOrigin(_pressStart.width/2, _pressStart.height/2);
        _pressStart.SetXY(704, 500);
    }

    public void Update()
    {
        _time++;
        _pressStart.scale = (float)getSize(_time);
    }
    #endregion

    private double getSize(int time)
    {
        double shakeOffSet = 0.1;
        double speed = 80;     //lower number increases frequency
        double horizontalOffSet = 0;

        double b = shakeOffSet;
        double T = speed;
        double X = time;
        double d = horizontalOffSet;


        return (b * Math.Sin(((2 * Math.PI) / T) * (X - d))) + shakeOffSet+1;
    }
}
