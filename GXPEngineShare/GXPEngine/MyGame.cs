using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

public class MyGame : Game
{
	public MyGame() : base(3200, 6400, false, true, 600, 1200)		// Create a window that's 800x600 and NOT fullscreen
	{
        MapGenerator map = new MapGenerator(8);
        AddChild(map);
	}

	void Update()
	{
		// Empty
	}

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
        //test123
	}
}