using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class ParticalEffect : AnimationSprite
{
    #region variables
    private int _timer;
    private bool _done = false;
    #endregion

    #region setup & update
    public ParticalEffect(string fileName, int columns, int rows) : base(fileName, columns, rows, -1, false)
    {
        _timer = columns * rows - 1;
    }

    public void Update()
    {
        if ( currentFrame < _timer)
        {
            SetFrame(currentFrame);
            currentFrame++;
        }
        else { _done = true; }
    }
    #endregion

    public bool GetDoneState()
    {
        return _done;
    }
}
