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
    private AnimationSprite _glow;
    private Sprite _drill;
    private Sprite _fog;
    private int frame = 0;
    private float _deceleration = 0.9f;
    private float _vSpeed = 0.0f;
    private float _hSpeed = 0.0f;
    private float _speed = 0.5f;
    private int _state = 1;
    private int _timer = 0;
    private int _timer2 = 0;
    private int _angleUp = 0, _angleDown = 180, _angleLeft = 270, _angleRight = 90;
    private int _boost = 0;

    bool _miningAnimation = false;
    private bool _doOnce = true;

    public Player(float x, float y) : base("collider.png")
    {
        SetXY(x, y);
        //SetOrigin(width / 2, height / 2);
        // SetScaleXY(0.5f, 0.5f);
        alpha = 0.0f;

        

        _drill = new Sprite("drill.png");
        AddChild(_drill);
        _drill.alpha = 0.0f;
        _drill.SetOrigin(_drill.width / 2 - width / 2, _drill.height / 2);

        _fog = new Sprite("fog.png");
        AddChild(_fog);
        _fog.SetOrigin(_fog.width / 2 - width / 2, _fog.height / 2 - height / 2);

        _animation = new AnimationSprite("player_sprite_sheet.png", 8, 1, -1, false, false);
        AddChild(_animation);
        //_animation.alpha = 0.2f;
        _animation.SetOrigin(_animation.width / 2 - width / 2, _animation.height / 2 - height / 2);

         _mining = new AnimationSprite("mining_sprite_sheet.png", 4, 1, - 1, false, false);
        AddChild(_mining);
        _mining.SetScaleXY(2.0f, 2.0f);
        _mining.alpha = 0.0f;
        _mining.SetOrigin(_mining.width/2 - width + 5, _mining.height/ 2 - height);

        _glow = new AnimationSprite("lavaPlayer.png", 4, 1, -1, false, false);
        AddChild(_glow);
        _glow.SetOrigin(_animation.width / 2 - width / 2 - 12, _animation.height / 2 - height / 2);
        _glow.SetFrame(3);
    }

    void Update()
    {
        Movement();
        Break();
        Animation();
        Boost();

    }

    void Movement()
    {
        _glow.alpha = -((-((MyGame)game).GetScreenY() - y)) / ((MyGame)game).GetScreenHeight();
        _state = 1;

        if (Input.GetKey(Key.Q))
        {
            _state = 4;
        }
        
        if (Input.GetKey(Key.A))
        {
            _hSpeed -= _speed;
            _state = 2;
            if (Input.GetKey(Key.Q))
            {
                _state = 3;
            }
            _animation.SetOrigin(_animation.width / 2 + width / 2, _animation.height / 2 - height / 2);

            _animation.rotation = _angleLeft;

            _drill.SetOrigin(_drill.width / 2 + width / 2, _drill.height / 2);

            _drill.rotation = _angleLeft;

            _mining.SetOrigin(_mining.width / 2 - 20 , _mining.height / 2 - height);

            _mining.rotation = _angleLeft;

            _glow.SetOrigin(_animation.width / 2 + width / 2 - 12, _animation.height / 2 - height / 2);

            _glow.rotation = _angleLeft;

            _glow.SetFrame(1);
        }

        if (Input.GetKey(Key.D))
        {
            _hSpeed += _speed;
            _state = 2;
            if (Input.GetKey(Key.Q))
            {
                _state = 3;
            }
            _animation.SetOrigin(_animation.width / 2 - width / 2, _animation.height / 2 + height / 2);

            _animation.rotation = _angleRight;

            _drill.SetOrigin(_drill.width / 2 - width / 2, _drill.height / 2 + height);

            _drill.rotation = _angleRight;

            _mining.SetOrigin(_mining.width / 2 - width + 5, _mining.height / 2 - height / 2);

            _mining.rotation = _angleRight;

            _glow.SetOrigin(_animation.width / 2 - width / 2 - 12, _animation.height / 2 + height / 2);

            _glow.rotation = _angleRight;

            _glow.SetFrame(0);
        }

        

        if (Input.GetKey(Key.W))
        {
            _vSpeed -= _speed;
            _state = 2;
            if (Input.GetKey(Key.Q))
            {
                _state = 3;
            }
            _animation.SetOrigin(_animation.width / 2 - width / 2, _animation.height / 2 - height / 2);

            _animation.rotation = _angleUp;

            _drill.SetOrigin(_drill.width / 2 - width / 2, _drill.height / 2);

            _drill.rotation = _angleUp;

            _mining.SetOrigin(_mining.width / 2 - width + 5, _mining.height / 2 - height);

            _mining.rotation = _angleUp;

            _glow.SetOrigin(_animation.width / 2 - width / 2 - 12 , _animation.height / 2 - height / 2);

            _glow.rotation = _angleUp;

            _glow.SetFrame(3);
        }

        if (Input.GetKey(Key.S))
        {
            _vSpeed += _speed;
            _state = 2;
            if (Input.GetKey(Key.Q))
            {
                _state = 3;
            }
            _animation.SetOrigin(_animation.width / 2 + width / 2, _animation.height / 2 + height / 2);

            _animation.rotation = _angleDown;

            _drill.SetOrigin(_drill.width / 2 + width / 2, _drill.height / 2 + height);

            _drill.rotation = _angleDown;

            _mining.SetOrigin(_mining.width / 2 - width /2 + 5, _mining.height / 2 - height / 2);

            _mining.rotation = _angleDown;

            _glow.SetOrigin(_animation.width / 2 + width / 2 - 12, _animation.height / 2 + height / 2);

            _glow.rotation = _angleDown;

            _glow.SetFrame(2);
        }


        DoMove(0, _vSpeed);
        _vSpeed *= _deceleration;

        DoMove(_hSpeed, 0);
        _hSpeed *= _deceleration;


        if (y < -((MyGame)game).GetScreenY()) y += 4;

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
            if(_timer > 5)
            {
                _animation.SetFrame(frame);
                frame++;
                _timer = 0;
            }
            if(frame > 1)
            {
                frame = 0;
            }
        }
        if(_state == 2)
        {
            if (frame == 0)
            {
                frame = 2;
            }
            _timer++;
            if (_timer > 5)
            {
                _animation.SetFrame(frame);
                frame++;
                _timer = 0;
            }
            if (frame > 3)
            {
                frame = 2;
            }
        }

        if (_state == 3)
        {
            if (frame == 0)
            {
                frame = 4;
            }
            _timer++;
            if (_timer > 5)
            {
                _animation.SetFrame(frame);
                frame++;
                _timer = 0;
            }
            if (frame > 5)
            {
                frame = 4;
            }
        }
        if (_state == 4)
        {
            if (frame == 0)
            {
                frame = 6;
            }
            _timer++;
            if (_timer > 5)
            {
                _animation.SetFrame(frame);
                frame++;
                _timer = 0;
            }
            if (frame > 7)
            {
                frame = 6;
            }
        }


        if (_miningAnimation == true)
        {
            _timer2++;
            if (_timer2 > 3) _mining.SetFrame(0);
            if (_timer2 > 6) _mining.SetFrame(1);
            if (_timer2 > 9) _mining.SetFrame(2);
            if (_timer2 > 12) _mining.SetFrame(3);
            if (_timer2 > 15)
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
            if(other is Collectable && Input.GetKey(Key.Q))
            {
                Collectable coll = other as Collectable;
                coll.Collect();
                startMiningAnimation();
            }
        }
    }

    private void startMiningAnimation()
    {
        _miningAnimation = true;
        _mining.alpha = 1.0f;
    }

    private void Dead()
    {
        if (_doOnce)
        {
            ((MyGame)game).GameOver();
            _doOnce = false;
        }
    }

    void Boost()
    {
        if (Input.GetKey(Key.E) && _boost < 540)
        {
            _speed = 2.0f;
            _boost += 5;           
        }
        else
        {
            _speed = 0.4f;
            _boost--;
        }

        if (_boost < 0)
        {
            _boost = 0;
        }

        if(_boost > 600)
        {
            _boost = 600;
        }

        ((MyGame)game).SetBooster(_boost);
    }

}

