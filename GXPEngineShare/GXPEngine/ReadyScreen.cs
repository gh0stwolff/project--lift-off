using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class ReadyScreen : Canvas
{
    #region variables
    private int timer = 0;

    private bool _P1Ready = false;
    private bool _P2Ready = false;

    private Sprite _manual;
    private Sprite _backGround;
    private AnimationSprite _player1;
    private AnimationSprite _player2;
    private AnimationSprite _controlsMiner;
    private AnimationSprite _controlsWorm;
    #endregion

    #region setup & update
    public ReadyScreen(float width, float height) : base((int)width, (int)height)
    {
        _backGround = new Sprite("Background.png");
        AddChild(_backGround);
        _manual = new Sprite("Contract.png");
        AddChild(_manual);
        _player1 = new AnimationSprite("buttonMiner.png", 4, 1);
        AddChild(_player1);
        _player1.SetXY(90, 400);
        _player2 = new AnimationSprite("buttonWorm.png", 4, 1);
        AddChild(_player2);
        _player2.SetXY(1025, 400);
        _controlsMiner = new AnimationSprite("ControlsMiner.png", 10, 1);
        AddChild(_controlsMiner);
        _controlsMiner.SetXY(90, 75);
        _controlsWorm = new AnimationSprite("ControlsWorm.png", 8, 1);
        AddChild(_controlsWorm);
        _controlsWorm.SetXY(1025, 75);
    }

    void Update()
    {
        if (Input.GetKey(Key.ONE)) { _P1Ready = true; }
        if (Input.GetKey(Key.TWO)) { _P2Ready = true; }
        handleAnimation();
    }

    private void handleAnimation()
    {
        leftControlAnimation();
        rightControlAnimation();
        minerReady();
        wormReady();
    }

    private void minerReady()
    {
        int startFrame = 0;
        if (_P1Ready) { startFrame = 2; }
        int numbOfFrames = 2;
        int timeBetweenFrames = 1000;
        timer += Time.deltaTime;

        int currentFrame = (int)(timer / timeBetweenFrames) % numbOfFrames + startFrame;
        _player1.SetFrame(currentFrame);
    }

    private void wormReady()
    {
        int startFrame = 0;
        if (_P2Ready) { startFrame = 2; }
        int numbOfFrames = 2;
        int timeBetweenFrames = 1000;
        timer += Time.deltaTime;

        int currentFrame = (int)(timer / timeBetweenFrames) % numbOfFrames + startFrame;
        _player2.SetFrame(currentFrame);
    }

    private void leftControlAnimation()
    {
        int startFrame = 0;
        int numbOfFrames = 10;
        int timeBetweenFrames = 250;
        timer += Time.deltaTime;

        int currentFrame = (int)(timer / timeBetweenFrames) % numbOfFrames + startFrame;
        _controlsMiner.SetFrame(currentFrame);
    }

    private void rightControlAnimation()
    {
        int startFrame = 0;
        int numbOfFrames = 8;
        int timeBetweenFrames = 250;
        timer += Time.deltaTime;

        int currentFrame = (int)(timer / timeBetweenFrames) % numbOfFrames + startFrame;
        _controlsWorm.SetFrame(currentFrame);
    }
    #endregion

    public int GetPlayersReady()
    {
        if (_player1.currentFrame == 1 && _player2.currentFrame == 1) { return 2; }
        else if (_player1.currentFrame == 1) { return 1; }
        else { return 0; }
    }
}
