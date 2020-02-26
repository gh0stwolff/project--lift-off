using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

public class MyGame : Game
{
    private float _startScreenSpeed = 1.0f;
    private float _speed;
    private Menu _menu;
    private int _shakeTime = 0;
    private int _score = 0;

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

        if (Input.GetKeyDown(Key.O))
        {
            ShakeCamera(100);
        }
        shake();
    }

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}

    public void GameOver()
    {
        _menu.GameOver(_score);
    }

	public float GetScreenSpeed()
	{
		return _speed;
	}

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
    public int GetScore()
    {
        return _score;
    }
    public void AddScore(int amount)
    {
        _score += amount;
    }
    public void ResetScore()
    {
        _score = 0;
    }
    public void IncreaseSpeed()
    {
        _speed += 0.0001f;
    }
}