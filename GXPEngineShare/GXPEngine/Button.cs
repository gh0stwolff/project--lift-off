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

    public void Update()
    {
        if ( checkIfSelected())
        {
            scale = 1.1f;
        }
        else
        {
            scale = 1;
        }
    }

    private bool checkIfSelected()
    {
        foreach (GameObject other in GetCollisions())
        {
            if (other.x > x && other.x < x + width &&
                other.y > y && other.y < y + height)
            {
                return true;
            }
        }

        if (Input.mouseX > x && Input.mouseX < x + width &&
            Input.mouseY > y && Input.mouseY < y + height)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsPressed()
    {
        if (checkIfSelected() && Input.GetMouseButtonDown(0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
