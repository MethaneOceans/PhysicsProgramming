

namespace GXPEngine.Control.Scenes
{
	internal class TestScene0 : Scene
	{
		public TestScene0()
		{
			EasyDraw ed = new EasyDraw(game.Width, game.Height);
			AddChild(ed);
			ed.TextAlign(CenterMode.Center, CenterMode.Center);
			ed.Text("Scene 0");
		}
		private void Update()
		{
			if (Input.GetKeyDown(Key.RIGHT))
			{
				Unload();
				new TestScene1().Load();
			}
		}
	}
}
