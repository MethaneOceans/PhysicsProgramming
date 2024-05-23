using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.Dungeons
{
	internal class NavNode
	{
		public Point Position;
		public List<NavNode> Connections;

		public NavNode(Point position)
		{
			Position = position;
		}
	}
}
