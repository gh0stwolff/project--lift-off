using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;

class Arrow : AnimationSprite
{
    private AnimationSprite _arrow;
    private bool _shoot = false;
    private int _timer = 0;
    private float _speed = 10.0f;

    public Arrow() : base("arrows.png", 4, 1)
    {

        _arrow = new AnimationSprite("arrows.png", 4, 1);

        AddChild(_arrow);

        _arrow.SetOrigin(_arrow.width / 2, _arrow.height / 2);

        _arrow.SetFrame(0);

        SetOrigin(_arrow.width / 2, _arrow.height / 2);

        SetFrame(0);


        x = width;
        _arrow.x = ((MyGame)game).GetScreenWidth() - _arrow.width * 2;
        _arrow.Mirror(true, false);

        alpha = 0.0f;
        _arrow.alpha = 0.0f;
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        _shoot = ((MyGame)game).GetShoot();
        if (_shoot == false)
        {
            if (Input.GetKeyDown(Key.O) || Input.GetKeyDown(Key.U))
            {
                alpha = 1.0f;
                _arrow.alpha = 1.0f;
                y = ((MyGame)game).GetScreenHeight() / 2 - ((MyGame)game).GetScreenY();
                //_arrow.y = ((MyGame)game).GetScreenHeight() / 2 - ((MyGame)game).GetScreenY();
            }

            if (Input.GetKey(Key.O) || Input.GetKey(Key.U))
            {
                _timer++;
                if (_timer <= 75)
                {
                    SetFrame(1);
                    _arrow.SetFrame(1);
                }

                if (_timer > 150)
                {
                    SetFrame(2);
                    _arrow.SetFrame(2);
                }

                if (_timer > 225)
                {
                    SetFrame(3);
                    _arrow.SetFrame(3);
                }
                if (_timer > 225)
                {
                    _timer = 225;
                }

            }

            if (Input.GetKey(Key.I))
            {
                y -= _speed;
            }

            if(Input.GetKey(Key.K))
            {
                y += _speed;
            }

            if (y < -((MyGame)game).GetScreenY() + width)
            {
                y = -((MyGame)game).GetScreenY() + width;
            }

            if (y > ((MyGame)game).GetScreenHeight() - ((MyGame)game).GetScreenY() - width)
            {
                y = ((MyGame)game).GetScreenHeight() - ((MyGame)game).GetScreenY() - width;
            }
        } 
        
        if(_shoot == true)
        {
            alpha = 0.0f;
            _arrow.alpha = 0.0f;
            _timer = 0;
        }
        
        
    }

    
}

