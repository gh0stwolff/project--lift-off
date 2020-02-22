using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.Core;

class Player : Sprite
{
    private AnimationSprite _animation;
    private AnimationSprite _mining;
    private Sprite _drill;

    private float _gravity = 0.5f;
    private float _deceleration = 0.9f;
    private float _vSpeed = 0.0f;
    private float _hSpeed = 0.0f;
    private int _state = 1;
    private int _timer = 0;
    private int _timer2 = 0;
    private int _angleUp = 0, _angleDown = 180, _angleLeft = 270, _angleRight = 90;

    bool _miningAnimation = false;
    public Player(float x, float y) : base("collider.png")
    {
        SetXY(x, y);
        //SetOrigin(width / 2, height / 2);
        // SetScaleXY(0.5f, 0.5f);
        alpha = 0.0f;

        _animation = new AnimationSprite("player_sprite_sheet.png", 8, 1, -1, false, false);
        AddChild(_animation);
        //_animation.alpha = 0.2f;
        _animation.SetOrigin(width/2+3, height/2+5);

        _mining = new AnimationSprite("mining_sprite_sheet.png", 4, 1);
        AddChild(_mining);
        _mining.SetScaleXY(2.0f, 2.0f);
        _mining.alpha = 0.0f;
        

        _drill = new Sprite("drill.png");
        AddChild(_drill);
        _drill.alpha = 0.0f;
        _drill.SetOrigin(width/8-22, height/3-2);

        _mining.SetOrigin(width / 4 + 5, height / 4);


    }

    void Update()
    {
        //Gravity();
        Movement();
        Break();
        Animation();
    }

    void Movement()
    {
        _state = 1;

        if (Input.GetKey(Key.SPACE))
        {
            _state = 4;
        }

        if (Input.GetKey(Key.A))
        {
            _hSpeed -= 0.5f;
            _state = 2;
            if (Input.GetKey(Key.SPACE))
            {
                _state = 3;
            }

            _animation.SetOrigin(width / 2 + 60, height / 2 +6);

            _animation.rotation = _angleLeft;

            _drill.SetOrigin(width / 2 + 15, height / 2 - 13);

            _drill.rotation = _angleLeft;

            _mining.SetOrigin(width / 2 + 19, height / 2 - 13);

            _mining.rotation = _angleLeft;
        }

        if (Input.GetKey(Key.D))
        {
            _hSpeed += 0.5f;
            _state = 2;
            if (Input.GetKey(Key.SPACE))
            {
                _state = 3;
            }

            _animation.SetOrigin(width/ 2 + 2, height / 2 + 60);

           _animation.rotation = _angleRight;

            _drill.SetOrigin(width / 8 - 22, height / 3 + 50);

            _drill.rotation = _angleRight;

            _mining.SetOrigin(width / 2 - 10 , height / 3 + 23);

            _mining.rotation = _angleRight;
        }

        DoMove(_hSpeed, 0);
        _hSpeed *= _deceleration;

        if (Input.GetKey(Key.W))
        {
            _vSpeed -= 0.5f;
            _state = 2;
            if (Input.GetKey(Key.SPACE))
            {
                _state = 3;
            }

            _animation.SetOrigin(width / 2 + 3, height / 2 + 5);

            _animation.rotation = _angleUp;

            _drill.SetOrigin(width / 8 - 22, height / 3 - 2);

            _drill.rotation = _angleUp;

            _mining.SetOrigin(width / 4 + 5, height / 4);

            _mining.rotation = _angleUp;
        }

        if (Input.GetKey(Key.S))
        {
            _vSpeed += 0.5f;
            _state = 2;
            if (Input.GetKey(Key.SPACE))
            {
                _state = 3;
            }

            _animation.SetOrigin(width * 2 - 25, height * 2 - 20);

           _animation.rotation = _angleDown;

            _drill.SetOrigin(width / 8 + 28, height / 3 + 60);

            _drill.rotation = _angleDown;

            _mining.SetOrigin(width / 2 + 18, height / 2 +16);

            _mining.rotation = _angleDown;
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
        if (other is Lava || other is Worm)
        {
            Dead();
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

     
        if (_miningAnimation == true)
        {
        _timer2++;
        if (_timer2 > 5) _mining.SetFrame(0);
        if (_timer2 > 10) _mining.SetFrame(1);
        if (_timer2 > 15) _mining.SetFrame(2);
        if (_timer2 > 20) _mining.SetFrame(3);
            if (_timer2 > 25)
            {
                _timer2 = 0;
                _miningAnimation = false;
                _mining.alpha = 0.0f;
            }
        }

    }
    
    void Break()
    {
        foreach(GameObject other in _drill.GetCollisions())
        {
            if(other is DiamondOre && Input.GetKey(Key.SPACE))
            {
                DiamondOre diamond = other as DiamondOre;
                diamond.collect();
                _miningAnimation = true;
                _mining.alpha = 1.0f;
            }
            if (other is Dirt)
            {
                Dirt dirt = other as Dirt;
                dirt.Digged();
                _miningAnimation = true;
                _mining.alpha = 1.0f;
            }
        }
    }

    private void Dead()
    {
        ((MyGame)game).GameOver();
    }

    void Gravity()
    {
        y += _gravity;
    }

}

