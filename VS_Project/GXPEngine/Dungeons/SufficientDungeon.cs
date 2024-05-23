using System;
using System.Collections.Generic;
using System.Drawing;
using static GXPEngine.Mathf;

namespace GXPEngine.Dungeons
{
	internal class SufficientDungeon : Dungeon
	{
		public SufficientDungeon(Size size, int maxDepth = 2, int? seed = null) : base(size, seed)
		{
			Rectangle baseArea = new Rectangle(0, 0, size.Width, size.Height);
			BSPRoot = new BSPNode<Rectangle>(baseArea);

			SplitRectNode(BSPRoot, maxDepth);

			List<Rectangle> leafAreas = BSPRoot.Leaves;
			foreach (Rectangle leaf in leafAreas)
			{
				const int padding = 1;
				FillRect(new Rectangle(leaf.X + padding, leaf.Y + padding, leaf.Width - 2 * padding, leaf.Height - 2 * padding));
			}
		}

		private bool SplitRectNode(BSPNode<Rectangle> node, int maxDepth = 2, int depth = 0)
		{
			bool splitSucceeded = SplitArea(node.Self, out (Rectangle a, Rectangle b)? areas);
			if (!splitSucceeded) return false;

			Rectangle areaA = areas.Value.a;
			Rectangle areaB = areas.Value.b;

			node.ChildA = new BSPNode<Rectangle>(areaA, node);
			node.ChildB = new BSPNode<Rectangle>(areaB, node);

			if (depth < maxDepth)
			{
				SplitRectNode(node.ChildA, maxDepth, depth + 1);
				SplitRectNode(node.ChildB, maxDepth, depth + 1);
			}

			if (areaA.Left == areaB.Left)
			{
				//	  A
				//	  |
				//	  B

				Point doorPosition = new Point(areaA.Left + rng.Next(areaA.Width - 2) + 1, areaA.Bottom - 1);
				tiles[doorPosition.X, doorPosition.Y] = Tile.Empty;
			}
			else
			{
				//
				//	A - B
				//

				Point doorPosition = new Point(areaA.Right - 1, areaA.Top + rng.Next(areaA.Height - 2) + 1);
				tiles[doorPosition.X, doorPosition.Y] = Tile.Empty;
			}

			return true;
		}
	}
}