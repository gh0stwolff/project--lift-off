using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Button : Sprite
{

    public Button(string fileName, float x, float y, float sizeX, float sizeY) : base(fileName)
    {
        SetXY(x, y);
        SetScaleXY(sizeX, sizeY);
    }

    public bool IsPressed()
    {
        if (Input.mouseX > x && Input.mouseX < x + width &&
            Input.mouseY > y && Input.mouseY < y + height && Input.GetMouseButtonDown(0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
