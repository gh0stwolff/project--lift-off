using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Collectable : Tile
{
    private int _pointsOnPickUp;
    private int _framesBeforeCollect = 15;

    private Sound _pickUpSound;
    private SoundChannel _pickUpSoundChannel;

    public Collectable(string fileName, float locX, float locY, int frames, int pointsOnPickup): base(fileName, locX, locY, frames, pointsOnPickup)
    {
        _pickUpSound = new Sound("pickUpSound.wav");
        _pickUpSoundChannel = new SoundChannel(5);
        _pointsOnPickUp = pointsOnPickup;
    }

    public void Collect()
    {
        if (_pointsOnPickUp > 0)
        {
            if (_framesBeforeCollect <= 0)
            {
                _pickUpSoundChannel = _pickUpSound.Play();
                selfDestroy(_pointsOnPickUp);
            }
        }
        else
        {
            selfDestroy(_pointsOnPickUp);
        }
        _framesBeforeCollect--;
    }

}
