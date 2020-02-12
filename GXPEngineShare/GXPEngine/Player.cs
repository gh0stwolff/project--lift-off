using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.Core;

class Player : AnimationSprite
{
    private float _gravity = 0.5f;
    private float _deceleration = 0.9f;
    private float _vSpeed = 0.0f;
    private float _hSpeed = 0.0f;
    private int _state = 1;
    private int _timer = 0;
    private int _angleUp = 0, _angleDown = 180, _angleLeft = 270, _angleRight = 90;

    bool UP = false, DOWN = false, RIGHT = false, LEFT = false;
    public Player() : base("player_sprite_sheet.png", 8, 1)
    {
        SetOrigin(width / 2, height / 2);
    }

    void Update()
    {
        Gravity();
        Movement();
        Animation();
    }


    void Movement()
    {
        _state = 1;
        if (Input.GetKey(Key.UP))
        {
            UP = true;
            _vSpeed = 2.5f;
            _state = 2;
            rotation = _angleUp;
            
        }

        if (Input.GetKey(Key.DOWN))
        {
            DOWN = true;
            _vSpeed = -2.0f;
            _state = 2;
            rotation = _angleDown;
        }

        if (Input.GetKey(Key.LEFT))
        {
            LEFT = true;
            _hSpeed = 2.0f;
            _state = 2;
            rotation = _angleLeft;
        }

        if (Input.GetKey(Key.RIGHT))
        {
            RIGHT = true;
            _hSpeed = -2.0f;
            _state = 2;
            rotation = _angleRight;
        }

        if (Input.GetKeyUp(Key.UP))
        {
            UP = false;
        }

        if (Input.GetKeyUp(Key.DOWN))
        {
            DOWN = false;
        }

        if (Input.GetKeyUp(Key.LEFT))
        {
            LEFT = false;
        }

        if (Input.GetKeyUp(Key.RIGHT))
        {
            RIGHT = false;
        }

            y -= _vSpeed;
            x -= _hSpeed;

            _vSpeed *= _deceleration;
            _hSpeed *= _deceleration;
        if (UP)
        {
            _vSpeed = _vSpeed < 0.1f ? 0.0f : _vSpeed;
        }
        if (DOWN)
        {
            _vSpeed = _vSpeed > -0.1f ? 0.0f : _vSpeed;
        }
        if (LEFT)
        {
            _hSpeed = _hSpeed < 0.1f ? 0.0f : _hSpeed;
        }
        if (RIGHT)
        {
            _hSpeed = _hSpeed > -0.1f ? 0.0f : _hSpeed;
        }


        //Collision collision = MoveUntilCollision(0.0f, _vSpeed);

        //if (collision != null)
        //{
        //    if (collision.other.name == "diamond")
        //    {
        //        LateDestroy();
        //    }
        //}


    }

    void Animation()
    {
        if(_state == 1)
        {
            _timer++;
            if(_timer > 5) SetFrame(1);
            if(_timer > 10) SetFrame(0);
            if (_timer > 15) _timer = 0;
        }
        if(_state == 2)
        {
            _timer++;
            if (_timer > 5) SetFrame(3);
            if (_timer > 10) SetFrame(2);
            if (_timer > 15) _timer = 0;
        }
        if (_state == 3)
        {
            _timer++;
            if (_timer > 5) SetFrame(5);
            if (_timer > 10) SetFrame(4);
            if (_timer > 15) _timer = 0;
        }
        if (_state == 4)
        {
            _timer++;
            if (_timer > 5) SetFrame(7);
            if (_timer > 10) SetFrame(6);
            if (_timer > 15) _timer = 0;
        }
    }
    
    void Break()
    {
        
    }

    void Gravity()
    {
        y += _gravity;
    }

    void OnCollision(GameObject other)
    {
        if(other is DiamondOre)
        {
            other.LateDestroy();
        }
    }
}

