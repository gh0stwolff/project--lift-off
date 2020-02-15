using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;

    class Worm : AnimationSprite
{
    private float _speed = 10.0f;
    private int _state = 0;
    bool _shoot = false;
    public Worm() : base("worm_sprite_sheet.png", 1, 8)
    {
        SetOrigin(width / 2, height / 2);
        x = -100;
        y = -100;
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        
        if (_shoot == false)
        {

            if (Input.GetKey(Key.LEFT))
            {
                SetFrame(4);
                x = 0;
                y = ((MyGame)game).GetScreenHeight() / 2 - ((MyGame)game).GetScreenY();
                _state = 1;
                alpha = 0.6f;
            }

            if (Input.GetKey(Key.RIGHT))
            {
                SetFrame(0);
                x = ((MyGame)game).GetScreenWidth(); ;
                y = ((MyGame)game).GetScreenHeight() / 2 - ((MyGame)game).GetScreenY();
                _state = 2;
                alpha = 0.6f;
            }

            if (Input.GetKey(Key.UP))
            {
                y -= _speed;
            }

            if (Input.GetKey(Key.DOWN))
            {
                y += _speed;
            }

        }
        if (Input.GetKey(Key.ENTER))
        {
            _shoot = true;
            alpha = 1f;
        }

        if (x < -(width / 2) || x > ((MyGame)game).GetScreenWidth() + width / 2) 
        {
            _shoot = false;
        }

        if(_shoot == true && _state == 1)
        {
            x += _speed;
        }

        if (_shoot == true && _state == 2)
        {
            x -= _speed;
        }
    }
}
