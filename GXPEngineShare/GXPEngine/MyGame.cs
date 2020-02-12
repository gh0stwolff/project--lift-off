using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

public class MyGame : Game
{

	private float _speed = 1.0f;

	public MyGame() : base(1408, 720, false)		// Create a window that's 800x600 and NOT fullscreen
	{
        MapGenerator map = new MapGenerator(width/64);
        AddChild(map);
        player = new Player();
        AddChild(player);
    }

    void Update()
	{
		y = y + _speed;
	}

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it

	}

	public float GetScreenSpeed()
	{
		return _speed;
	}
}