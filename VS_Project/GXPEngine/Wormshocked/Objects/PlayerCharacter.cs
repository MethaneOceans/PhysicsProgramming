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

		public readonly Sprite sprite;

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
			ed.Fill(Color.White);
			ed.Stroke(Color.Gray);
			ed.ShapeAlign(CenterMode.Min, CenterMode.Min);
			ed.Rect(0, 0, ed.width, ed.height);

			sprite = ed;
			AddChild(sprite);
			sprite.SetOrigin(sprite.width / 2, sprite.height / 2);
			sprite.Position = new Vector2();
		}

		public float HandleControls(float movementLeft)
		{
			if (body.CollidedLastFrame)
			{
				if (Input.GetKey(Key.A))
				{
					body.Velocity -= Vector2.Right * 0.05f;
					movementLeft -= 0.1f;

				}
				if (Input.GetKey(Key.D))
				{
					body.Velocity += Vector2.Right * 0.05f;
					movementLeft -= 0.1f;
				}
			}
			else
			{
				if (Input.GetKey(Key.A))
				{
					body.Velocity -= Vector2.Right * 0.01f;
					movementLeft -= 0.1f;
				}
				if (Input.GetKey(Key.D))
				{
					body.Velocity += Vector2.Right * 0.01f;
					movementLeft -= 0.1f;
				}
			}

			if (Input.GetKeyDown(Key.SPACE))
			{
				body.Velocity -= Vector2.Down * 4;
				movementLeft -= 0.5f;
			}

			if (Input.GetMouseButtonDown(0))
			{
				
			}

			return movementLeft;
		}
	}
}
