using GXPEngine.Physics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.Wormshocked.Objects
{
	internal class PlayerCharacter : PhysicsObject
	{
		private const int PLAYER_WIDTH = 32;
		private const int PLAYER_HEIGHT = 32;

		private Sprite sprite;

		public PlayerCharacter(Vector2 position)
		{
			body = new OBCollider(position, new Vector2(PLAYER_WIDTH, PLAYER_HEIGHT), 0, this)
			{
				Behavior = ACollider.ColliderType.Rigid,
				Bounciness = 0.1f,
				BounceMode = BounceCalcMode.Min,
			};
			Position = position;

			EasyDraw ed = new EasyDraw(PLAYER_WIDTH, PLAYER_HEIGHT);
			ed.Fill(Color.Blue);
			ed.Stroke(Color.DarkBlue);
			ed.ShapeAlign(CenterMode.Min, CenterMode.Min);
			ed.Rect(0, 0, ed.width, ed.height);

			sprite = ed;
			AddChild(sprite);
			sprite.SetOrigin(sprite.width / 2, sprite.height / 2);
			sprite.Position = new Vector2();
		}

		private void Update()
		{

		}
	}
}
