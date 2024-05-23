using System;
using System.Collections.Generic;
using System.Drawing;

namespace GXPEngine.Dungeons
{
	internal class SufficientDungeon : Dungeon
	{
		BinaryTreeNode<Rectangle> areaTopNode;

		public SufficientDungeon(Size size, int maxDepth = 2, int? seed = null) : base(size)
		{
			if (seed == null) rng = new Random();
			else rng = new Random((int)seed);

			Rectangle baseArea = new Rectangle(1, 1, size.Width, size.Height);
			areaTopNode = new BinaryTreeNode<Rectangle>(baseArea);

			SplitRectNode(areaTopNode, maxDepth);

			List<Rectangle> leafAreas = areaTopNode.Leaves;
			foreach (Rectangle leaf in leafAreas)
			{
				FillRect(new Rectangle(leaf.X + 1, leaf.Y + 1, leaf.Width - 2, leaf.Height - 2));
			}
		}

		private (bool horizontal, bool vertical) CanSplit(Rectangle area)
		{
			int minSize = 2 * MIN_ROOM_SIZE + 1;
			return (area.Size.Width > minSize, area.Size.Height > minSize);
		}
		private bool SplitRectNode(BinaryTreeNode<Rectangle> node, int maxDepth = 2, int depth = 0)
		{
			Rectangle root = node.Self;

			// Check if node can be split
			(bool vertical, bool horizontal) canSplit = CanSplit(node.Self);
			Rectangle areaA;
			Rectangle areaB;

			if (canSplit.horizontal && canSplit.vertical)
			{
				// Pick random axis to split
				int axis = rng.Next() % 2;
				if (axis == 0)
				{
					int splitVal = SplitValue(root.Left, root.Right);
					areaA = new Rectangle(root.Left, root.Top, splitVal, root.Height);
					areaB = new Rectangle(root.Right, root.Top, root.Width - splitVal, root.Height);
				}
				else
				{
					int splitVal = SplitValue(root.Top, root.Bottom);
					areaA = new Rectangle(root.Left, root.Top, root.Width, splitVal);
					areaB = new Rectangle(root.Left, root.Bottom, root.Width, root.Height - splitVal);
				}
			}
			else if (canSplit.horizontal)
			{
				// Split horizontally
				int splitVal = SplitValue(root.Left, root.Right);
				areaA = new Rectangle(root.Left, root.Top, splitVal, root.Height);
				areaB = new Rectangle(root.Right, root.Top, root.Width - splitVal, root.Height);
			}
			else if (canSplit.vertical)
			{
				// Split vertically
				int splitVal = SplitValue(root.Top, root.Bottom);
				areaA = new Rectangle(root.Left, root.Top, root.Width, splitVal);
				areaB = new Rectangle(root.Left, root.Bottom, root.Width, root.Height - splitVal);
			}
			else return false;

			node.ChildA = new BinaryTreeNode<Rectangle>(areaA);
			node.ChildB = new BinaryTreeNode<Rectangle>(areaB);

			if (depth < maxDepth)
			{
				SplitRectNode(node.ChildA, depth + 1, maxDepth);
				SplitRectNode(node.ChildB, depth + 1, maxDepth);
			}

			return true;
		}

		private int SplitValue(int roomMinBound, int roomMaxBound) => rng.Next(roomMinBound + MIN_ROOM_SIZE, roomMaxBound - MIN_ROOM_SIZE);
	}
}