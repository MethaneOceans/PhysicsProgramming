using GXPEngine;                                // GXPEngine contains the engine
using GXPEngine.Control;
using System;                                   // System contains a lot of default C# libraries 
using System.Drawing;
//using GXPEngine.Scenes;                           // System.Drawing contains drawing tools such as Color definitions

/// <summary>
/// This MyGame class only contains the scenemanager and setup for the scenemanager.
/// The only available scenes are test scenes
/// </summary>
internal class MyGame : Game
{
	public readonly SceneManager sceneManager;
	private readonly EasyDraw fpsCounter;

	public float SoundVolume = 0.05f;

	public MyGame() : base(1600, 900, pFullScreen: false, pPixelArt: false)
	{
		sceneManager = new SceneManager(this);
		TargetFps = int.MaxValue;

		// Show the fps
		fpsCounter = new EasyDraw(200, 50);
		fpsCounter.TextAlign(CenterMode.Min, CenterMode.Min);
		AddChild(fpsCounter);

		Console.WriteLine("MyGame initialized");
	}

	void Update()
	{
		HandleInput();

		fpsCounter.ClearTransparent();
		fpsCounter.Fill(Color.Green);
		fpsCounter.Text($"{CurrentFps}");
	}

	private void HandleInput()
	{
		if (Input.GetKeyDown(Key.R)) sceneManager.Reload();

		if (Input.GetKeyDown(Key.THREE)) sceneManager.SwitchScene("");
	}
	static void Main()
	{
		new MyGame().Start();
	}
}