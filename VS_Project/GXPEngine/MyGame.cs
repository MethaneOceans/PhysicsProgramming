using GXPEngine;
using GXPEngine.Assets;
using GXPEngine.Control;
using GXPEngine.Debugging;
using GXPEngine.Scenes;
using System;
using System.Collections.Generic;
using System.Drawing;
//using GXPEngine.Scenes;

internal class MyGame : Game
{
	private bool showfps;
	private readonly EasyDraw fpsCounter;

	public float SoundVolume = 0.05f;

	public MyGame() : base(1600, 900, pFullScreen: false, pPixelArt: true)
	{
		// Add fps counter for debugging (probably more for checking if update loops take too long)
		showfps = false;
		fpsCounter = new EasyDraw(200, 50);
		fpsCounter.TextAlign(CenterMode.Min, CenterMode.Min);
		AddChild(fpsCounter);

		new WormshockedScene().Load();

		Console.WriteLine("MyGame initialized");

		Console.WriteLine(Assets.GetTexturePath("Dungeon_Tileset.png"));
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