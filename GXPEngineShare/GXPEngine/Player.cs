using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.Core;

class Player : Sprite
{
    private float gravity = 0.5f;
    private float deceleration = 0.98f;
    private float vSpeed = 0.0f;
    private float hSpeed = 0.0f;

    bool UP = false, DOWN = false, RIGHT = false, LEFT = false;
    public Player() : base("player.png")
    {

    }

    void Update()
    {
        Gravity();
        Movement();
    }


    void Movement()
    {

        if (Input.GetKey(Key.UP))
        {
            UP = true;
            vSpeed = 2.0f;
        }

        if (Input.GetKey(Key.DOWN))
        {
            DOWN = true;
            vSpeed = -2.0f;
        }

        if (Input.GetKey(Key.LEFT))
        {
            LEFT = true;
            hSpeed = 2.0f;
        }

        if (Input.GetKey(Key.RIGHT))
        {
            RIGHT = true;
            hSpeed = -2.0f;
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

            y -= vSpeed;
            x -= hSpeed;

            vSpeed *= 0.9f;
            hSpeed *= 0.9f;
        if (UP)
        {
            vSpeed = vSpeed < 0.1f ? 0.0f : vSpeed;
        }
        if (DOWN)
        {
            vSpeed = vSpeed > -0.1f ? 0.0f : vSpeed;
        }
        if (LEFT)
        {
            hSpeed = hSpeed < 0.1f ? 0.0f : hSpeed;
        }
        if (RIGHT)
        {
            hSpeed = hSpeed > -0.1f ? 0.0f : hSpeed;
        }


        //Collision collision = MoveUntilCollision(0.0f, -upT);
        //if(collision != null)
        //{
        //    Console.Write(collision.other.name);
        //    if(collision.other.name == "coin")
        //    {
        //       
        //    }
        //}

    }


    void Gravity()
    {
        y += gravity;
    }

}

