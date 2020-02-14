using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

public class MyGame : Game
{
	private float _speed = 1.0f;
    //private MapGenerator _map;
    private Menu _menu;
    private int _shakeTime = 0;

	public MyGame() : base(1408, 720, false)		// Create a window that's 800x600 and NOT fullscreen
	{
        //_map = new MapGenerator();
        //AddChild(_map);
        _menu = new Menu(width, height);
        AddChild(_menu);
    }

    public void Update()
	{
		y = y + _speed;

        if (Input.GetKeyDown(Key.O))
        {
            ShakeCamera(100);
        }
        shake();
        //if (Input.GetKeyDown(Key.P))
        //{
        //    ParticalEffect partical = new ParticalEffect("tileExplosion.png", 8, 1);
        //    Add
        //}
	}

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}

	public float GetScreenSpeed()
	{
		return _speed;
	}

    public void SetScreenSpeed(float speed)
    {
        _speed = speed;
    }

    public void ShakeCamera(int timeShaking)
    {
        _shakeTime = timeShaking;
    }

    private void shake()
    {
        if (_shakeTime > 0)
        {
            if (_menu._level != null)
            {
                _menu._level.x = getShaking(_shakeTime);
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
}