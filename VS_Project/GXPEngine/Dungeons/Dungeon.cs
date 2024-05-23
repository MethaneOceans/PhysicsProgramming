using System;
using System.Drawing;
using System.Drawing.Design;

namespace GXPEngine.Dungeons
{
	internal class Dungeon : GameObject
	{
		protected const int MIN_ROOM_SIZE = 7;

		protected Size size;
		protected Tile[,] tiles;
		protected Random rng;

		public Dungeon(Size size)
		{
			this.size = size;

			// Fill the dungeon with walls
			tiles = new Tile[size.Width, size.Height];
			Clear();
		}

		// Fill the spaces contained in the rectangle with a tile
		protected void FillRect(Rectangle rect, Tile tile = Tile.Empty)
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
		// Clears the dungeon with a specific tile type (default is wall)
		protected void Clear(Tile clearTile = Tile.Wall)
		{
			FillRect(new Rectangle(0, 0, size.Width, size.Height), clearTile);
		}

		// Writes walls as '#' and empty spaces as '.'
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
		// Colors walls gray and empty spaces white
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
}