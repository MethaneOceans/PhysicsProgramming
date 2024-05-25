using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.Dungeons
{
	internal class Room
	{
		public Rectangle Area;

		public Point Center => new Point(Area.X + Area.Width / 2, Area.Y + Area.Height / 2);

		public Room
	}
}
