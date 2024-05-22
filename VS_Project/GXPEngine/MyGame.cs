using GXPEngine;
using GXPEngine.Assets;
using GXPEngine.Control;
using GXPEngine.Debugging;
using GXPEngine.Dungeons;
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
		TargetFps = int.MaxValue;

		showfps = true;
		fpsCounter = new EasyDraw(200, 50);
		fpsCounter.TextAlign(CenterMode.Min, CenterMode.Min);
		AddChild(fpsCounter);

		new MapTest().Load();

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

		if (Input.GetKeyDown(Key.F2) && !(Scene.Current is TileIndexViewer))
		{
			if (Scene.Current == null) new TileIndexViewer(Assets.GetTexturePath("Dungeon_Tileset.png"), 10, 10).Load();
			else Scene.Switch(new TileIndexViewer(Assets.GetTexturePath("Dungeon_Tileset.png"), 10, 10));
		}

		if (Input.GetKeyDown(Key.R))
		{
			Console.Clear();
			new DungeonTest().Load();
		}
	}
	static void Main()
	{
		new MyGame().Start();
	}
}