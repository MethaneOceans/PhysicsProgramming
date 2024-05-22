using System;
using GXPEngine.Control;

namespace GXPEngine
{
	internal class MapTest : Scene
	{
		Map map;

		public MapTest()
		{
			map = new Map(64, 64);
			AddChild(map);
		}
	}
}
