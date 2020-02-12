using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class TestPlayer : GameObject
{
    private int _lastKeyPressed = 0;

    private float _speed = 4.0f;

    private Sprite _skin;
    public TestPlayer() : base()
    {
        _skin = new Sprite("colors.png");
        AddChild(_skin);
        scale = 0.75f;
        SetXY(((MyGame)game).width / 2, ((MyGame)game).height / 2);
    }

    public void Update()
    {
        checkCollisions();
        movement();
    }

    private void checkCollisions()
    {
        foreach (GameObject other in _skin.GetCollisions())
        {
            if (other is Dirt)
            {
                Dirt dirt = other as Dirt;

                dirt.RemoveTile();
            }
            else if (other is DiamondOre)
            {
                DiamondOre diamond = other as DiamondOre;

                diamond.collect();
            }
        }
    }

    private void movement()
    {
        if ( Input.GetKey(Key.W))
        {
            //MoveUntilCollision(0, -_speed);
            Move(0, -_speed);
            _lastKeyPressed = Key.W;
        }
        if (Input.GetKey(Key.S))
        {
            //MoveUntilCollision(0, _speed);
            Move(0, _speed);
            _lastKeyPressed = Key.S;
        }
        if (Input.GetKey(Key.A))
        {
            //MoveUntilCollision(-_speed, 0);
            Move(-_speed, 0);
            _lastKeyPressed = Key.A;
        }
        if (Input.GetKey(Key.D))
        {
            //MoveUntilCollision(_speed, 0);
            Move(_speed, 0);
            _lastKeyPressed = Key.D;
        }
    }
}
