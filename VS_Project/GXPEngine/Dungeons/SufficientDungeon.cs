using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static GXPEngine.Mathf;

namespace GXPEngine.Dungeons
{
	internal class SufficientDungeon : Dungeon
	{
		public SufficientDungeon(Size size, int maxDepth = 2, int? seed = null) : base(size, seed)
		{
			rootArea = new Area(new Rectangle(0, 0, size.Width, size.Height));

			rootArea.Split(maxDepth, rng);
			//SplitRectNode(rootArea, maxDepth);

			foreach (Area leaf in rootArea.Leaves)
			{
				const int padding = 1;
				FillRect(new Rectangle(
					leaf.Rectangle.X + padding,
					leaf.Rectangle.Y + padding,
					leaf.Rectangle.Width - 2 * padding,
					leaf.Rectangle.Height - 2 * padding));
			}
		}

		private bool SplitRectNode(Area node, int maxDepth = 2, int depth = 0)
		{
			//if (areaA.Left == areaB.Left)
			//{
			//	//	  A
			//	//	  |
			//	//	  B

			//	Point doorPosition = new Point(areaA.Left + rng.Next(areaA.Width - 2) + 1, areaA.Bottom - 1);
			//	tiles[doorPosition.X, doorPosition.Y] = Tile.Empty;
			//}
			//else
			//{
			//	//
			//	//	A - B
			//	//

			//	Point doorPosition = new Point(areaA.Right - 1, areaA.Top + rng.Next(areaA.Height - 2) + 1);
			//	tiles[doorPosition.X, doorPosition.Y] = Tile.Empty;
			//}

			return true;
		}
	}
}