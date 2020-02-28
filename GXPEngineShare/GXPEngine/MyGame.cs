using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

public class MyGame : Game
{
    #region variables
    private Menu _menu;

    private float _startScreenSpeed = 1.0f;
    private float _maxScreenSpeed = 3.5f;
    private float _speed;

    private int _shakeTime = 0;
    private int _score = 0;
    private int _boost = 0;
    private bool _shoot = false;
    #endregion

    #region setup & update
    //arcade screen res: 1366x768
    public MyGame() : base(1408, 720, false, true, 1366, 768)		// Create a window that's 800x600 and NOT fullscreen
	{
        _menu = new Menu(width, height);
        AddChild(_menu);
        _speed = _startScreenSpeed;
        //ScoreBoard score = new ScoreBoard("Score.txt");
    }

    public void Update()
	{
		y = y + _speed;

        shake();
    }
    #endregion

    static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}

    public void GameOver()
    {
        _menu.GameOver(_score);
    }

    #region Get...
    public float GetScreenSpeed()
	{
		return _speed;
	}

    private float getShaking(double time)
    {
        double shakeOffSet = 15;
        double speed = 7.5f;     //lower number increases frequency
        double horizontalOffSet = 0;

        double b = shakeOffSet;
        double T = speed;
        double X = time;
        double d = horizontalOffSet;


        return (float)(b * Math.Sin(((2 * Math.PI) / T) * (X - d)));
    }

    public float GetScreenWidth()
    {
        return width;
    }

    public float GetScreenHeight()
    {
        return height;
    }

    public float GetScreenY()
    {
        return y;
    }

    public float GetScreenX()
    {
        return x;
    }

    public int GetScore()
    {
        return _score;
    }

    public float GetMaxScreenSpeed()
    {
        return _maxScreenSpeed;
    }
    #endregion

    #region set...
    public void SetScreenForMenu()
    {
        _speed = 0.0f;
        y = 0;
    }

    public void SetScreenForBeginLevel()
    {
        _speed = _startScreenSpeed;
        y = 0;
    }
    #endregion

    #region camera shake
    public void ShakeCamera(int timeShaking)
    {
        _shakeTime = timeShaking;
    }

    private void shake()
    {
        if (_shakeTime > 0)
        {
            if (_menu != null)
            {
                _menu.x = getShaking(_shakeTime);
                _shakeTime--;
            }
        }
        else
        {
            _menu.x = 0;
        }
    }
    #endregion

    #region score
    public void AddScore(int amount)
    {
        _score += amount;
    }
    public void ResetScore()
    {
        _score = 0;
    }
    #endregion

    public void IncreaseSpeed()
    {
        _speed += 0.0001f;
    }

    public void SetBooster(int value)
    {
        _boost = value;
    }

    public int GetBooster()
    {
        return _boost;
    }

    public void SetShoot(bool boolean)
    {
        _shoot = boolean;
    }

    public bool GetShoot()
    {
        return _shoot;
    }
}