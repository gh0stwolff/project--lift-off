using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class ReadyScreen : Canvas
{
    #region variables
    private Sprite _manual;
    private Sprite _backGround;
    private AnimationSprite _player1;
    private AnimationSprite _player2;
    #endregion

    #region setup & update
    public ReadyScreen(float width, float height) : base((int)width, (int)height)
    {
        _backGround = new Sprite("Background.png");
        AddChild(_backGround);
        _manual = new Sprite("Contract.png");
        AddChild(_manual);
        _player1 = new AnimationSprite("playerReady.png", 1, 2);
        AddChild(_player1);
        _player1.SetXY(50, 400);
        _player2 = new AnimationSprite("playerReady.png", 1, 2);
        AddChild(_player2);
        _player2.SetXY(960, 400);
    }

    void Update()
    {
        if (Input.GetKey(Key.ONE)) { _player1.SetFrame(1); }
        if (Input.GetKey(Key.TWO)) { _player2.SetFrame(1); }
    }
    #endregion

    public int GetPlayersReady()
    {
        if (_player1.currentFrame == 1 && _player2.currentFrame == 1) { return 2; }
        else if (_player1.currentFrame == 1) { return 1; }
        else { return 0; }
    }
}
