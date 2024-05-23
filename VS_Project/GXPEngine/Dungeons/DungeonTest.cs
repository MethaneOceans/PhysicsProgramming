using GXPEngine.Control;
using System.Drawing;

namespace GXPEngine.Dungeons
{
	internal class DungeonTest : Scene
	{
		private Dungeon dungeon;
		private EasyDraw ed;

		private const int minRoomSize = 7;
		private const int dungeonWidth = 40;
		private const int dungeonHeight = 22;

		public DungeonTest()
		{
			ed = new EasyDraw(game.Width, game.Height);
			AddChild(ed);

			dungeon = new SufficientDungeon(new Size(dungeonWidth, dungeonHeight), 2, 1);
		}
	}
}
