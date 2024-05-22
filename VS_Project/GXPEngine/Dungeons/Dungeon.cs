using GXPEngine.Control;
using System;
using System.Drawing;
using System.Drawing.Design;

namespace GXPEngine.Dungeons
{
	internal class Dungeon : GameObject
	{
		private Size size;
		private Tile[,] tiles;

		public Dungeon(Size size)
		{
			this.size = size;

			// Fill the dungeon with walls
			tiles = new Tile[size.Width, size.Height];
			for (int y = 0; y < size.Height; y++)
			{
				for (int x = 0; x < size.Width; x++)
				{
					tiles[x, y] = Tile.Wall;
				}
			}

			FillRect(new Rectangle(1, 1, size.Width - 2, size.Height - 2), Tile.Empty);
		}

		private void FillRect(Rectangle rect, Tile tile)
		{
			for (int j = 0; j < rect.Height; j++)
			{
				for (int i = 0; i < rect.Width; i++)
				{
					int x = rect.X + i;
					int y = rect.Y + j;
					tiles[x, y] = tile;
				}
			}
		}

		public void RenderToConsole()
		{
			for (int y = 0; y < size.Height; y++)
			{
				for (int x = 0; x < size.Width; x++)
				{
					Tile tile = tiles[x, y];
					if (tile == Tile.Wall) Console.Write("#");
					if (tile == Tile.Empty) Console.Write(".");
					else Console.Write(" ");
				}
				Console.WriteLine();
			}
		}
		public void RenderToED(EasyDraw ed, int tileSize)
		{
			ed.ClearTransparent();
			ed.ShapeAlign(CenterMode.Min, CenterMode.Min);
			ed.Stroke(Color.Black);
			
			for (int y = 0; y < size.Height; ++y)
			{
				for (int x = 0; x < size.Width; x++)
				{
					Tile tile = tiles[x, y];
					if (tile == Tile.Empty)
					{
						ed.Fill(Color.White);
					}
					if (tile == Tile.Wall)
					{
						ed.Fill(Color.Gray);
					}
					ed.Rect(x * tileSize, y * tileSize, tileSize, tileSize);
				}
			}
		}
	}

	internal enum Tile
	{
		Empty,
		Wall,
	}

	internal class DungeonTest : Scene
	{
		private Dungeon dungeon;
		private EasyDraw ed;

		private const int minRoomSize = 7;
		private const int dungeonWidth = 40;
		private const int dungeonHeight = 22;

		public DungeonTest()
		{
			ed = new EasyDraw(game.Width, game.Height);
			AddChild(ed);

			GenerateDungeon();
		}

		private Dungeon GenerateDungeon()
		{
			dungeon = new Dungeon(new Size(dungeonWidth, dungeonHeight));

			dungeon.RenderToED(ed, 40);
			return dungeon;
		}
	}
}
