using System;
using System.Drawing;
using System.Drawing.Design;

namespace GXPEngine.Dungeons
{
	internal class Dungeon : GameObject
	{
		public const int MIN_ROOM_SIZE = 7;
		public const int MIN_AREA_SIZE = MIN_ROOM_SIZE + 2;

		protected Size size;
		protected Tile[,] tiles;
		protected Random rng;
		protected Area rootArea;

		//public List<Room> rooms { get; protected set; }

		public Dungeon(Size size, int? seed = null)
		{
			if (seed == null) rng = new Random();
			else rng = new Random((int)seed);

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

		protected bool SplitArea(Rectangle area, out (Rectangle a, Rectangle b)? subAreas)
		{
			subAreas = null;

			(bool horizontal, bool vertical) = CanSplit(area);
			Rectangle areaA;
			Rectangle areaB;
			SplitMode splitMode;

			// Sets the splitmode according to how an area can be split
			if (horizontal && vertical)
			{
				// Split along longest dimension or random if they are equal
				if (area.Width > area.Height)
				{
					splitMode = SplitMode.horizontal;
				}
				else if (area.Height > area.Width)
				{
					splitMode = SplitMode.vertical;
				}
				else
				{
					// Pick random axis to split
					int axis = rng.Next() % 2;
					if (axis == 0) splitMode = SplitMode.horizontal;
					else splitMode = SplitMode.vertical;
				}
			}
			else if (horizontal) splitMode = SplitMode.horizontal;
			else if (vertical) splitMode = SplitMode.vertical;
			// Area cannot be split, return false indicating that the split operation failed
			else return false;

			if (splitMode == SplitMode.horizontal)
			{
				// Split horizontally
				int splitVal = rng.Next(MIN_AREA_SIZE, area.Width - MIN_AREA_SIZE);
				areaA = new Rectangle(area.Left, area.Top, splitVal, area.Height);
				areaB = new Rectangle(area.Left + splitVal - 1, area.Top, area.Width - splitVal + 1, area.Height);
			}
			else
			{
				// Split vertically
				int splitVal = rng.Next(MIN_AREA_SIZE, area.Height - MIN_AREA_SIZE);
				areaA = new Rectangle(area.Left, area.Top, area.Width, splitVal);
				areaB = new Rectangle(area.Left, area.Top + splitVal - 1, area.Width, area.Height - splitVal + 1);
			}

			subAreas = (areaA, areaB);
			return true;
		}

		// Enum for better readability of BSP generation
		protected enum SplitMode
		{
			vertical,
			horizontal,
		}
		// Checks if an area is big enough to be split in either direction
		protected (bool horizontal, bool vertical) CanSplit(Rectangle area)
		{
			int minSize = 2 * MIN_AREA_SIZE + 1;
			return (area.Size.Width > minSize, area.Size.Height > minSize);
		}
	}
}