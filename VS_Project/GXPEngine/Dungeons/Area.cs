using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GXPEngine.Dungeons.GeneratorConstants;

namespace GXPEngine.Dungeons
{
	// Areas form a BSP and thus can be split into smaller areas
	internal class Area : BSPNode
	{
		public Rectangle Rectangle;
		public int Width => Size.Width;
		public int Height => Size.Height;
		public Size Size => Rectangle.Size;

		public Area(Rectangle rectangle, Area parent = null)
		{
			Rectangle = rectangle;
			Parent = parent;
		}
		public void Split(Random rng)
		{
			(bool horizontal, bool vertical) = CanSplit();

			Rectangle areaA;
			Rectangle areaB;
			SplitMode splitMode;

			// Sets the splitmode according to how an area can be split
			if (horizontal && vertical)
			{
				// Split along longest dimension or random if they are equal
				if (Width > Height) splitMode = SplitMode.horizontal;
				else if (Height > Width) splitMode = SplitMode.vertical;
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
			else return; // Area cannot be split

			if (splitMode == SplitMode.horizontal)
			{
				// Split horizontally
				int splitVal = rng.Next(MIN_AREA_SIZE, Size.Width - MIN_AREA_SIZE);
				areaA = new Rectangle(Rectangle.Left, Rectangle.Top, splitVal, Rectangle.Height);
				areaB = new Rectangle(Rectangle.Left + splitVal - 1, Rectangle.Top, Rectangle.Width - splitVal + 1, Rectangle.Height);
			}
			else
			{
				// Split vertically
				int splitVal = rng.Next(MIN_AREA_SIZE, Size.Height - MIN_AREA_SIZE);
				areaA = new Rectangle(Rectangle.Left, Rectangle.Top, Rectangle.Width, splitVal);
				areaB = new Rectangle(Rectangle.Left, Rectangle.Top + splitVal - 1, Rectangle.Width, Rectangle.Height - splitVal + 1);
			}

			ChildA = new Area(areaA, this);
			ChildB = new Area(areaB, this);

			return;
		}

		// Split the area until tree has a specific depth
		public void Split(int maxDepth, Random rng)
		{
			int depth = 0;
			while (depth < maxDepth)
			{
				foreach (Area leaf in this[depth].Cast<Area>())
				{
					leaf.Split(rng);
				}
				depth++;
			}
		}

		// Enum for better code readability in splitting methods
		protected enum SplitMode
		{
			vertical,
			horizontal,
		}

		// Checks if an area is big enough to be split in either direction
		protected (bool horizontal, bool vertical) CanSplit()
		{
			int minSize = 2 * Dungeon.MIN_AREA_SIZE + 1;
			return (Size.Width > minSize, Size.Height > minSize);
		}
	}
}
