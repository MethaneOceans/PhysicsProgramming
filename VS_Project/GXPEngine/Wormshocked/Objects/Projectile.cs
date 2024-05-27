using GXPEngine.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			Sprite.Destroy();
			body.ShouldRemove = true;

			if (args.otherCol != null && args.otherCol.Owner is SquareTile squareTile)
			{
				squareTile.Health -= 1;
			}
		}
	}
}
