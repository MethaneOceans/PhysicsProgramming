using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.Control.Scenes
{
	internal class TestScene1 : Scene
	{
		public TestScene1()
		{
			EasyDraw ed = new EasyDraw(game.Width, game.Height);
			AddChild(ed);
			ed.TextAlign(CenterMode.Center, CenterMode.Center);
			ed.Text("Scene 1");
		}

		private void Update()
		{
			if (Input.GetKeyDown(Key.RIGHT))
			{
				Unload();
				new TestScene0().Load();
			}
		}
	}
}
