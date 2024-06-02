using GXPEngine.Physics;
using GXPEngine.Scenes;
using System.Collections.Generic;

namespace GXPEngine.Wormshocked.Objects
{
	internal class Projectile : PhysicsObject
	{
		protected Sprite Sprite;

		public Projectile(Vector2 position, Vector2 velocity, int radius, Sprite sprite = null)
		{
			body = new CircleCollider(position, 10, this)
			{
				Velocity = velocity,
				Behavior = ACollider.ColliderType.Rigid
			};
			body.OnCollision += OnCollision;
			body.OnDestroy += (sender, args) => Destroy();

			EasyDraw ed = new EasyDraw(radius * 2, radius * 2);
			ed.ShapeAlign(CenterMode.Min, CenterMode.Min);
			ed.Ellipse(0, 0, ed.width - 0.5f, ed.height - 0.5f);
			Sprite = ed;
			AddChild(Sprite);
			Sprite.SetOrigin(radius, radius);
			Sprite.Position = new Vector2();

			Position = position;
		}

		private void OnCollision(object sender, (ACollider thisCol, ACollider otherCol) args)
		{
			if (!(parent is WormshockedScene scene && args.otherCol != null && !body.ShouldRemove)) return;

			if (args.otherCol.Owner is SquareTile)
			{
				IReadOnlyList<ACollider> colliders = scene.Colliders;

				CircleCollider circle = new CircleCollider(body.Position, 100, this);
				foreach (ACollider collider in colliders)
				{
					if (collider.Owner is SquareTile tile && circle.Overlapping(collider)) tile.Health--;
				}

				body.ShouldRemove = true;
			}
		}
		private void OnCollisionTile(object sender, (ACollider thisCol, ACollider otherCol) args)
		{

		}
	}
}
