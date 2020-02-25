using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class MainMenu : Canvas
{
    private int _time = 0;

    private Sprite _backGround;
    private Sprite _pressButton;

    public MainMenu(float width, float height) : base((int)width, (int)height)
    {
        _backGround = new Sprite("backGround.jpg");
        AddChild(_backGround);
        _pressButton = new Sprite("singleplayerButton.png");
        AddChild(_pressButton);
        _pressButton.SetOrigin(_pressButton.width/2, _pressButton.height/2);
        _pressButton.SetXY(300, 500);
    }

    public void Update()
    {
        _time++;
        _pressButton.scale = (float)getSize(_time);
    }

    private double getSize(int time)
    {
        double shakeOffSet = 0.3;
        double speed = 100;     //lower number increases frequency
        double horizontalOffSet = 0;

        double b = shakeOffSet;
        double T = speed;
        double X = time;
        double d = horizontalOffSet;


        return (b * Math.Sin(((2 * Math.PI) / T) * (X - d))) + shakeOffSet+1;
    }

}
