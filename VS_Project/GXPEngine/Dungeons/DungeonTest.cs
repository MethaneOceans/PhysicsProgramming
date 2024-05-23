using GXPEngine.Control;
using System.Drawing;

namespace GXPEngine.Dungeons
{
	internal class DungeonTest : Scene
	{
		private Dungeon dungeon;
		private EasyDraw ed;

		private const int minRoomSize = 7;
		private const int dungeonWidth = 140;
		private const int dungeonHeight = 80;

		public DungeonTest()
		{
			ed = new EasyDraw(game.Width, game.Height);
			AddChild(ed);

			dungeon = new SufficientDungeon(new Size(dungeonWidth, dungeonHeight), 4);
			dungeon.RenderToED(ed, 10);
		}
	}
}
