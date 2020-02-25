using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GXPEngine;

public class TextBoard : GameObject
{
    private EasyDraw _text;

    public TextBoard(int width, int height) : base()
    {
        _text = new EasyDraw(width, height);
        _text.TextAlign(CenterMode.Min, CenterMode.Min);
        AddChild(_text);
    }

    public void SetText(string text)
    {
        _text.Clear(0);
        _text.Text(text, 0, 0);
    }

    public void Clear()
    {
        _text.Clear(0);
    }

    public void Size(int fontSize)
    {
        _text.TextSize(fontSize);
    }
}
