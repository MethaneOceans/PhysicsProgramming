using GXPEngine;                                // GXPEngine contains the engine
using GXPEngine.Control;
using GXPEngine.Control.Scenes;
using System;                                   // System contains a lot of default C# libraries 
using System.Drawing;
//using GXPEngine.Scenes;                           // System.Drawing contains drawing tools such as Color definitions

/// <summary>
/// This MyGame class only contains the scenemanager and setup for the scenemanager.
/// The only available scenes are test scenes
/// </summary>
internal class MyGame : Game
{
	private bool showfps;
	private readonly EasyDraw fpsCounter;

	public float SoundVolume = 0.05f;
	public MyGame() : base(1600, 900, pFullScreen: false, pPixelArt: false)
	{
		TargetFps = int.MaxValue;

		showfps = true;
		fpsCounter = new EasyDraw(200, 50);
		fpsCounter.TextAlign(CenterMode.Min, CenterMode.Min);
		AddChild(fpsCounter);

		Console.WriteLine("MyGame initialized");
	}

	private void Update()
	{
		HandleInput();

		if (showfps)
		{
			fpsCounter.ClearTransparent();
			fpsCounter.Fill(Color.Green);
			fpsCounter.Text($"{CurrentFps}");
		}
	}

	private void HandleInput()
	{
		if (Input.GetKeyDown(Key.F1))
		{
			showfps = !showfps;
			fpsCounter.visible = showfps;
		}
	}
	static void Main()
	{
		new MyGame().Start();
	}
}