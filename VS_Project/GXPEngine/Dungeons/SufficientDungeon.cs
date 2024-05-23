using System;
using System.Collections.Generic;
using System.Drawing;
using static GXPEngine.Mathf;

namespace GXPEngine.Dungeons
{
	internal class SufficientDungeon : Dungeon
	{
		BinaryTreeNode<Rectangle> areaTopNode;

		public SufficientDungeon(Size size, int maxDepth = 2, int? seed = null) : base(size)
		{
			if (seed == null) rng = new Random();
			else rng = new Random((int)seed);

			Rectangle baseArea = new Rectangle(0, 0, size.Width, size.Height);
			areaTopNode = new BinaryTreeNode<Rectangle>(baseArea);

			SplitRectNode(areaTopNode, maxDepth);

			List<Rectangle> leafAreas = areaTopNode.Leaves;
			foreach (Rectangle leaf in leafAreas)
			{
				const int padding = 2;
				FillRect(new Rectangle(leaf.X + padding, leaf.Y + padding, leaf.Width - 2 * padding, leaf.Height - 2 * padding));
			}
		}

		private bool SplitRect(Rectangle area, out (Rectangle a, Rectangle b)? subAreas)
		{
			subAreas = null;

			(bool horizontal, bool vertical) canSplit = CanSplit(area);
			Rectangle areaA;
			Rectangle areaB;
			SplitMode splitMode;

			// Sets the splitmode according to how an area can be split
			if (canSplit.horizontal && canSplit.vertical)
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
			else if (canSplit.horizontal) splitMode = SplitMode.horizontal;
			else if (canSplit.vertical) splitMode = SplitMode.vertical;
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
		private bool SplitRectNode(BinaryTreeNode<Rectangle> node, int maxDepth = 2, int depth = 0)
		{
			bool splitSucceeded = SplitRect(node.Self, out (Rectangle a, Rectangle b)? areas);
			if (!splitSucceeded) return false;

			Rectangle areaA = areas.Value.a;
			Rectangle areaB = areas.Value.b;

			node.ChildA = new BinaryTreeNode<Rectangle>(areaA, node);
			node.ChildB = new BinaryTreeNode<Rectangle>(areaB, node);

			if (depth < maxDepth)
			{
				SplitRectNode(node.ChildA, maxDepth, depth + 1);
				SplitRectNode(node.ChildB, maxDepth, depth + 1);
			}

			if (areaA.Left == areaB.Left)
			{
				// Horizontally aligned
				//	  A
				//	  |
				//	  B

				Point doorPosition = new Point(areaA.Left + rng.Next(areaA.Width - 2) + 1, areaA.Bottom - 1);
				//tiles[doorPosition.X, doorPosition.Y] = Tile.Empty;

				for (int i = (areaA.Top + areaA.Bottom) / 2; i < (areaB.Top + areaB.Bottom) / 2; i++)
				{
					tiles[doorPosition.X, i] = Tile.Empty;
				}
			}
			else
			{
				// Vertically aligned
				//
				//	A - B
				//

				Point doorPosition = new Point(areaA.Right - 1, areaA.Top + rng.Next(areaA.Height - 2) + 1);
				//tiles[doorPosition.X, doorPosition.Y] = Tile.Empty;
				
				for (int i = (areaA.Left + areaA.Right) / 2; i < (areaB.Left + areaB.Right) / 2; i++)
				{
					tiles[i, doorPosition.Y] = Tile.Empty;
				}
			}

			return true;
		}

		// Enum for better readability of BSP generation
		private enum SplitMode
		{
			vertical,
			horizontal,
		}
		// Checks if an area is big enough to be split in either direction
		private (bool horizontal, bool vertical) CanSplit(Rectangle area)
		{
			int minSize = 2 * MIN_AREA_SIZE + 1;
			return (area.Size.Width > minSize, area.Size.Height > minSize);
		}
	}
}