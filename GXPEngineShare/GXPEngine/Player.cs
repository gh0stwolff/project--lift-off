using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.Core;

class Player : Sprite
{
    private AnimationSprite _animation;
    private Sprite _drill;

    private float _gravity = 0.5f;
    private float _deceleration = 0.9f;
    private float _vSpeed = 0.0f;
    private float _hSpeed = 0.0f;
    private int _state = 1;
    private int _timer = 0;
    private int _angleUp = 0, _angleDown = 180, _angleLeft = 270, _angleRight = 90;
    public Player() : base("collider.png")
    {
        //SetOrigin(width / 2, height / 2);
        // SetScaleXY(0.5f, 0.5f);
        //alpha = 0.0f;

        _animation = new AnimationSprite("player_sprite_sheet.png", 8, 1, -1, false, false);
        AddChild(_animation);
        //_animation.alpha = 0.2f;
        _animation.SetOrigin(width/2+3, height/2+5);


        _drill = new Sprite("drill.png");
        AddChild(_drill);
        _drill.alpha = 0.0f;
        _drill.SetOrigin(width/8-22, height/3-2);
    }

    void Update()
    {
        Gravity();
        Movement();
        Break();
        Animation();
    }

    void Movement()
    {
        _state = 1;
        if (Input.GetKey(Key.Z))
        {
            _state = 4;
        }
        if (Input.GetKey(Key.LEFT))
        {
            _hSpeed -= 1.0f;
            _state = 2;
            if (Input.GetKey(Key.Z))
            {
                _state = 3;
            }

            _animation.SetOrigin(width / 2 + 60, height / 2 +6);

            _animation.rotation = _angleLeft;
        }
        if (Input.GetKey(Key.RIGHT))
        {
            _hSpeed += 1.0f;
            _state = 2;
            if (Input.GetKey(Key.Z))
            {
                _state = 3;
            }

            _animation.SetOrigin(width/ 2 + 2, height / 2 + 60);

           _animation.rotation = _angleRight;
        }

        DoMove(_hSpeed, 0);
        _hSpeed *= _deceleration;

        if (Input.GetKey(Key.UP))
        {
            _vSpeed -= 1.0f;
            _state = 2;
            if (Input.GetKey(Key.Z))
            {
                _state = 3;
            }

            _animation.SetOrigin(width / 2 + 3, height / 2 + 5);

            _animation.rotation = _angleUp;
        }
        if (Input.GetKey(Key.DOWN))
        {
            _vSpeed += 1.0f;
            _state = 2;
            if (Input.GetKey(Key.Z))
            {
                _state = 3;
            }

            _animation.SetOrigin(width * 2 - 25, height * 2 - 20);

           _animation.rotation = _angleDown;
        }


        DoMove(0, _vSpeed);
        _vSpeed *= _deceleration;

    }

    bool DoMove(float moveX, float moveY)
    {
        x += moveX;
        y += moveY;

        if (HandleCollisions(moveX, moveY) == true)
        {
            return false;
        }

        return true;
    }

    bool HandleCollisions(float moveX, float moveY)
    {
        bool result = false;
        foreach (GameObject other in this.GetCollisions())
        {
            if (other is Sprite)
            {
                result = result || HandleCollision(other as Sprite, moveX, moveY);
            }
        }
        return result;
    }

    bool HandleCollision(Sprite other, float moveX, float moveY)
    {
        if (other is Tile)
        {
            ResolveCollision(other, moveX, moveY);
            return true;
        }


        return false;
    }

    void ResolveCollision(Sprite other, float moveX, float moveY)
    {
        if (moveX > 0)
        {
            x = other.x - width;
        }
        if (moveX < 0)
        {
            x = other.x + other.width;
        }
        if (moveY > 0)
        {
            y = other.y - height;
        }
        if (moveY < 0)
        {
            y = other.y + other.height;
        }
    }

  

    void Animation()
    {
        if(_state == 1)
        {
            _timer++;
            if(_timer > 5) _animation.SetFrame(1);
            if(_timer > 10) _animation.SetFrame(0);
            if (_timer > 15) _timer = 0;
        }
        if(_state == 2)
        {
            _timer++;
            if (_timer > 5) _animation.SetFrame(3);
            if (_timer > 10) _animation.SetFrame(2);
            if (_timer > 15) _timer = 0;
        }

        if (_state == 3)
        {
            _timer++;
            if (_timer > 5) _animation.SetFrame(5);
            if (_timer > 10) _animation.SetFrame(4);
            if (_timer > 15) _timer = 0;
        }
        if (_state == 4)
        {
            _timer++;
            if (_timer > 5) _animation.SetFrame(7);
            if (_timer > 10) _animation.SetFrame(6);
            if (_timer > 15) _timer = 0;
        }
    }
    
    void Break()
    {
        foreach(GameObject other in _drill.GetCollisions())
        {
            if(other is Tile && Input.GetKey(Key.Z))
            {
                
                other.LateDestroy();
            }
        }
    }

    void Gravity()
    {
        y += _gravity;
    }

}

