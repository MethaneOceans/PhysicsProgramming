using GXPEngine.Physics;
using GXPEngine.Scenes;
using System.Drawing;

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

		public bool HandleControls(ref float movementLeft)
		{
			movementLeft = HandleMovement(movementLeft);

			if (Input.GetMouseButtonDown(0))
			{
				Shoot();
			}

			if (Input.GetKeyDown(Key.ENTER)) return true;
			else return false;
		}

		private void Shoot()
		{
			Vector2 mouse = Input.mousePos;

			Vector2 diff = (mouse - Position).Normalized();
			((WormshockedScene)parent).AddPhysicsObject(new Projectile(Position + diff * 50, diff * 5, 5));
		}

		private float HandleMovement(float movementLeft)
		{
			bool keyRight = Input.GetKey(Key.D);
			bool keyLeft = Input.GetKey(Key.A);
			bool keyJump = Input.GetKey(Key.SPACE);

			if (body.CollidedLastFrame && movementLeft > 0f)
			{
				// Character on the ground has more control
				if (keyLeft && !keyRight)
				{
					body.Velocity -= Vector2.Right * 0.05f;
					movementLeft -= 0.1f;

				}
				if (keyRight && !keyLeft)
				{
					body.Velocity += Vector2.Right * 0.05f;
					movementLeft -= 0.1f;
				}

				// Jump
				if (keyJump)
				{
					body.Velocity -= Vector2.Down * 4;
					movementLeft -= 1.0f;
				}
			}
			else if (movementLeft > 0f)
			{
				// Character in the air has less movement control
				if (keyLeft && !keyRight)
				{
					body.Velocity -= Vector2.Right * 0.01f;
					movementLeft -= 0.1f;
				}
				if (keyRight && !keyLeft)
				{
					body.Velocity += Vector2.Right * 0.01f;
					movementLeft -= 0.1f;
				}
			}

			return movementLeft;
		}
	}
}
