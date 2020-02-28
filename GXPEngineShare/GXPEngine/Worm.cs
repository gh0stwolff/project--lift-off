using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;

    class Worm : AnimationSprite
{
    private AnimationSprite _mediumWorm;
    private AnimationSprite _bigWorm;
    private Sound _growl;
    private SoundChannel _growlChannel;
    private AnimationSprite _lavaSmall;
    private AnimationSprite _lavaMedium;
    private AnimationSprite _lavaBig;
    private float _speed = 10.0f;
    private int _state = 0;
    bool _shoot = false;
    private int _timer;
    private int _timer2;
    private int _scale = 1;
    private int frame = 0;
    public Worm(float arrowY) : base("small_worm_sprite_sheet.png", 1, 4)
    {
        y = arrowY;
        SetOrigin(width / 2, height / 2);
        x = +1000;
        y = +1000;

        _mediumWorm = new AnimationSprite("medium_worm_sprite_sheet.png", 1, 4);

        AddChild(_mediumWorm);

        _mediumWorm.SetOrigin(_mediumWorm.width / 2, _mediumWorm.height / 2);

        // _mediumWorm.alpha = 0.0f;

        _bigWorm = new AnimationSprite("big_worm_sprite_sheet.png", 1, 4);

        AddChild(_bigWorm);

        _bigWorm.SetOrigin(_bigWorm.width / 2, _bigWorm.height / 2);

        // _bigWorm.alpha = 0.0f;

        _lavaSmall = new AnimationSprite("lava_small.png", 1, 4);
        AddChild(_lavaSmall);
        _lavaSmall.SetOrigin(width / 2, height / 2);
        _lavaSmall.alpha = 0.0f;
        

        _lavaMedium = new AnimationSprite("lava_medium.png", 1, 4);
        AddChild(_lavaMedium);
        _lavaMedium.SetOrigin(_lavaMedium.width / 2, _lavaMedium.height / 2);
        _lavaMedium.alpha = 0.0f;

        _lavaBig = new AnimationSprite("lava_big.png", 1, 4);
        AddChild(_lavaBig);
        _lavaBig.SetOrigin(_lavaBig.width / 2, _lavaBig.height / 2);
        _lavaBig.alpha = 0.0f;

        _growl = new Sound("wormGrowlPassing.wav");
        _growlChannel = new SoundChannel(2);
    }

    void Update()
    {
        Movement();
        if (_shoot == true)
        {
            Animation();
        }

    }

    void Movement()
    {
        
        if (_shoot == false)
        {

            alpha = 0.0f;
            _mediumWorm.alpha = 0.0f;
            _bigWorm.alpha = 0.0f;
            _lavaBig.alpha = 0.0f;
            _lavaMedium.alpha = 0.0f;
            _lavaSmall.alpha = 0.0f;
            if (Input.GetKeyDown(Key.O))
            {
                x = 0;
                y = ((MyGame)game).GetScreenHeight() / 2 - ((MyGame)game).GetScreenY();
                _timer2 = 0;
            }
            if (Input.GetKey(Key.O))
            {
                _state = 1;
   
                
                Mirror(false, false);
                _mediumWorm.Mirror(false, false);
                _bigWorm.Mirror(false, false);
                _lavaSmall.Mirror(true, false);
                _lavaMedium.Mirror(true, false);
                _lavaBig.Mirror(true, false);
                _timer2++;

                if (_timer2 <= 75)
                {
                    _scale = 1;
                    SetFrame(0);
                }

                if (_timer2 > 150)
                {
                    _scale = 2;
                    _mediumWorm.SetFrame(0);
                }

                if (_timer2 > 225)
                {
                    _scale = 3;
                    _bigWorm.SetFrame(0);
                }
                if (_timer2 > 225)
                {
                    _timer2 = 225;
                }
            }
            if(Input.GetKeyDown(Key.U))
            {
                x = ((MyGame)game).GetScreenWidth(); ;
                y = ((MyGame)game).GetScreenHeight() / 2 - ((MyGame)game).GetScreenY();
                _timer2 = 0;
            }
            if (Input.GetKey(Key.U))
            {

                
                _state = 2;
                Mirror(true, false);
                _mediumWorm.Mirror(true, false);
                _bigWorm.Mirror(true, false);
                _lavaSmall.Mirror(false, false);
                _lavaMedium.Mirror(false, false);
                _lavaBig.Mirror(false, false);

                _timer2++;

                if (_timer2 <= 75)
                {
                    _scale = 1;
                    SetFrame(0);
                }

                if (_timer2 > 150)
                {
                    _scale = 2;
                    _mediumWorm.SetFrame(0);
                }

                if (_timer2 > 225)
                {
                    _scale = 3;
                    _bigWorm.SetFrame(0);
                }
                if (_timer2 > 225)
                {
                    _timer2 = 225;
                }

             
            }

            if (Input.GetKey(Key.I))
            {
                y -= _speed;
            }

            if (Input.GetKey(Key.K))
            {
                y += _speed;
            }

            if (Input.GetKeyUp(Key.U) || Input.GetKeyUp(Key.O))
            {
                _shoot = true;
                ((MyGame)game).ShakeCamera(60);
                _growlChannel = _growl.Play();
            }
        }
        

        if (x < -(width) || x > ((MyGame)game).GetScreenWidth() + width) 
        {
            _shoot = false;
            SetScaleXY(1.0f, 1.0f);
        }

        if(_shoot == true && _state == 1)
        {
            x += _speed;
        }

        if (_shoot == true && _state == 2)
        {
            x -= _speed;
        }

        if (_shoot == true)
        {
            if (_scale == 1)
            {
                SetScaleXY(1.0f, 1.0f);

                alpha = 1.0f;

                _mediumWorm.alpha = 0.0f;

                _bigWorm.alpha = 0.0f;

                _lavaBig.alpha = 0.0f;
                _lavaMedium.alpha = 0.0f;
                _lavaSmall.alpha = -((-((MyGame)game).GetScreenY() - y)) / ((MyGame)game).GetScreenHeight();
            }

            if (_scale == 2)
            {
                SetScaleXY(1.5f, 2.0f);

                alpha = 0.0f;

                _mediumWorm.alpha = 1.0f;

                _bigWorm.alpha = 0.0f;

                _lavaBig.alpha = 0.0f;
                _lavaMedium.alpha = -((-((MyGame)game).GetScreenY() - y)) / ((MyGame)game).GetScreenHeight();
                _lavaSmall.alpha = 0.0f;
            }

            if (_scale == 3)
            {
                SetScaleXY(2.0f, 3.0f);

                alpha = 0.0f;

                _mediumWorm.alpha = 0.0f;

                _bigWorm.alpha = 1.0f;

                _lavaBig.alpha = -((-((MyGame)game).GetScreenY() - y)) / ((MyGame)game).GetScreenHeight();
                _lavaMedium.alpha = 0.0f;
                _lavaSmall.alpha = 0.0f;
            }
        }
        ((MyGame)game).SetShoot(_shoot);
    }
    void Animation()
    {
        if (_scale == 1)
        {
            _timer++;
            if (_timer > 5)
            {
                SetFrame(frame);
                _lavaSmall.SetFrame(frame);
                _timer = 0;
                frame++;
                
            }
            if (frame > 3)
            {
                frame = 0;
            }
        }
        if (_scale == 2)
        {
            _timer++;
            if (_timer > 5)
            {
                _mediumWorm.SetFrame(frame);
                _lavaMedium.SetFrame(frame);
                _timer = 0;
                frame++;
            }
            if (frame > 3)
            {
                frame = 0;
            }
        }
        if (_scale == 3)
        {
            _timer++;
            if (_timer > 5)
            {
                _bigWorm.SetFrame(frame);
                _lavaBig.SetFrame(frame);
                _timer = 0;
                frame++;
            }
            if (frame > 3)
            {
                frame = 0;
            }
        }

    }

    void OnCollision(GameObject other)
    {
        if(other is Player)
        {
            ((MyGame)game).GameOver();

        }

        if(other is Collectable || other is Stone)
        {
            other.LateDestroy();
        }
    }

}
