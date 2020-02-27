using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;

    class Worm : AnimationSprite
{
    private AnimationSprite _mediumWorm;
    private AnimationSprite _bigWorm;
    private AnimationSprite[] _arrow = new AnimationSprite[2];
    private Sound _growl;
    private SoundChannel _growlChannel;
    private float _speed = 10.0f;
    private int _state = 0;
    bool _shoot = false;
    private int _timer;
    private int _timer2;
    private int _scale = 1;
    public Worm() : base("small_worm_sprite_sheet.png", 1, 4)
    {
        SetOrigin(width / 2, height / 2);
        x = -100;
        y = -100;

        _mediumWorm = new AnimationSprite("medium_worm_sprite_sheet.png", 1, 4);

        AddChild(_mediumWorm);

        _mediumWorm.SetOrigin(_mediumWorm.width / 2, _mediumWorm.height / 2);

        // _mediumWorm.alpha = 0.0f;

        _bigWorm = new AnimationSprite("big_worm_sprite_sheet.png", 1, 4);

        AddChild(_bigWorm);

        _bigWorm.SetOrigin(_bigWorm.width / 2, _bigWorm.height / 2);

        // _bigWorm.alpha = 0.0f;
        for (int i = 0; i < 2; i++)
        {
            _arrow[i] = new AnimationSprite("arrow.png", 3, 1);

            AddChild(_arrow[i]);

            _arrow[i].SetOrigin(_arrow[i].width / 2, _arrow[i].height / 2);

            _arrow[i].SetFrame(0);

            _arrow[i].alpha = 0.0f;
        }
        _arrow[0].x = ((MyGame)game).GetScreenX();
        _arrow[1].x = ((MyGame)game).GetScreenWidth() - _arrow[1].width;
        _arrow[1].Mirror(true, false);

        _growl = new Sound("wormGrowlPassing.wav");
        _growlChannel = new SoundChannel(2);
    }

    void Update()
    {
        Movement();
        if (_shoot == true) Animation();
    }

    void Movement()
    {
        
        if (_shoot == false)
        {

            Console.WriteLine(_scale);
            alpha = 0.0f;
            _mediumWorm.alpha = 0.0f;
            _bigWorm.alpha = 0.0f;
            _arrow[0].x = _arrow[0].width;
            _arrow[1].x = ((MyGame)game).GetScreenWidth() - _arrow[1].width;
            if (Input.GetKey(Key.U))
            {
                _state = 1;
              
                x = 0;
                y = ((MyGame)game).GetScreenHeight() / 2 - ((MyGame)game).GetScreenY();
                Mirror(false, false);
                _mediumWorm.Mirror(false, false);
                _bigWorm.Mirror(false, false);
                _timer2++;

                if (_timer2 <= 50)
                {
                    _scale = 1;
                    SetFrame(0);
                    _arrow[0].SetFrame(0);
                    _arrow[1].SetFrame(0);
                }

                if (_timer2 > 50)
                {
                    _scale = 2;
                    _mediumWorm.SetFrame(0);
                    _arrow[0].SetFrame(1);
                    _arrow[1].SetFrame(1);
                }

                if (_timer2 > 100)
                {
                    _scale = 3;
                    _bigWorm.SetFrame(0);
                    _arrow[0].SetFrame(2);
                    _arrow[1].SetFrame(2);
                }

                _arrow[0].alpha = 0.5f;
                _arrow[1].alpha = 0.5f;

                _arrow[0].Mirror(false, false);
                _arrow[1].Mirror(true, false);
            }

            if (Input.GetKey(Key.O))
            {

                x = ((MyGame)game).GetScreenWidth(); ;
                y = ((MyGame)game).GetScreenHeight() / 2 - ((MyGame)game).GetScreenY();
                _state = 2;
                Mirror(true, false);
                _mediumWorm.Mirror(true, false);
                _bigWorm.Mirror(true, false);

                _timer2++;

                if (_timer2 <= 50)
                {
                    _scale = 1;
                    SetFrame(0);
                    _arrow[0].SetFrame(0);
                    _arrow[1].SetFrame(0);
                }

                if (_timer2 > 50)
                {
                    _scale = 2;
                    _mediumWorm.SetFrame(0);
                    _arrow[0].SetFrame(1);
                    _arrow[1].SetFrame(1);
                }

                if (_timer2 > 100)
                {
                    _scale = 3;
                    _bigWorm.SetFrame(0);
                    _arrow[0].SetFrame(2);
                    _arrow[1].SetFrame(2);
                }

                _arrow[0].alpha = 0.5f;
                _arrow[1].alpha = 0.5f;

                

                _arrow[0].Mirror(true, false);
                _arrow[1].Mirror(false, false);

            }

            if(_state == 2)
            {
                _arrow[0].x = -(_arrow[0].width);
                _arrow[1].x = -(((MyGame)game).GetScreenWidth() - _arrow[1].width);
            }

            if (Input.GetKey(Key.I))
            {
                y -= _speed;
            }

            if (Input.GetKey(Key.K))
            {
                y += _speed;
            }

            if (Input.GetKeyUp(Key.LEFT) || Input.GetKeyUp(Key.RIGHT))
            {
                _timer2 = 0;
            }


        }
        if (Input.GetKey(Key.THREE))
        {
            _shoot = true;
            ((MyGame)game).ShakeCamera(60);
            _growlChannel = _growl.Play();
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
            _arrow[0].alpha = 0.0f;
            _arrow[1].alpha = 0.0f;
            if (_scale == 1)
            {
                SetScaleXY(1.0f, 1.0f);

                alpha = 1.0f;

                _mediumWorm.alpha = 0.0f;

                _bigWorm.alpha = 0.0f;
            }

            if (_scale == 2)
            {
                SetScaleXY(1.5f, 2.0f);

                alpha = 0.0f;

                _mediumWorm.alpha = 1.0f;

                _bigWorm.alpha = 0.0f;
            }

            if (_scale == 3)
            {
                SetScaleXY(2.0f, 3.0f);

                alpha = 0.0f;

                _mediumWorm.alpha = 0.0f;

                _bigWorm.alpha = 1.0f;
            }
        }
    }
    void Animation()
    {
        if (_scale == 1)
        {
            _timer++;
            if (_timer > 5) SetFrame(0);
            if (_timer > 10) SetFrame(1);
            if (_timer > 15) SetFrame(2);
            if (_timer > 20) SetFrame(3);
            if (_timer > 25) _timer = 0;
        }
        if (_scale == 2)
        {
            _timer++;
            if (_timer > 5) _mediumWorm.SetFrame(0);
            if (_timer > 10) _mediumWorm.SetFrame(1);
            if (_timer > 15) _mediumWorm.SetFrame(2);
            if (_timer > 20) _mediumWorm.SetFrame(3);
            if (_timer > 25) _timer = 0;
        }
        if (_scale == 3)
        {
            _timer++;
            if (_timer > 5) _bigWorm.SetFrame(0);
            if (_timer > 10) _bigWorm.SetFrame(1);
            if (_timer > 15) _bigWorm.SetFrame(2);
            if (_timer > 20) _bigWorm.SetFrame(3);
            if (_timer > 25) _timer = 0;
        }

    }

    void OnCollision(GameObject other)
    {
        if(other is Player)
        {
            other.LateDestroy();

        }

        if(other is Tile)
        {
            other.LateDestroy();
        }
    }
}
