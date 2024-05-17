using GXPEngine.Control;
using System.Collections.Generic;
using System.Drawing;
using static GXPEngine.Mathf;

namespace GXPEngine.Scenes
{
	internal class RayTest : Scene
	{
		float playerRadius;
		Circle player;
		Vector2 playerPos
		{
			get => new Vector2(player.x, player.y);
			set
			{
				player.x = value.x;
				player.y = value.y;
			}
		}
		List<(RayDrawn ray, float angleOffset)> rays;

		public override void Initialize()
		{
			base.Initialize();

			playerRadius = 25;
			player = new Circle(new Vector2(width / 2, height / 2), playerRadius);
			AddChild(player);

			float minAngle = -50;
			float maxAngle = -minAngle;
			int numRays = 50;
			float angleStep = Abs(minAngle - maxAngle) / numRays;

			rays = new List<(RayDrawn ray, float angleOffset)>();
			for (int i = 0; i < numRays; i++)
			{
				float angle = minAngle + angleStep / 2 + angleStep * i;
				Vector2 dir = Vector2.GetUnitVectorDeg(angle);
				RayDrawn ray = new RayDrawn(playerPos + playerRadius * dir, dir);
				rays.Add((ray, angle));
				AddChild(ray);
			}
			
		}

		public void Update()
		{
			Vector2 direction = new Vector2();
			float oldRotation = player.rotation;
			if (Input.GetKey(Key.W)) direction.y -= 1;
			if (Input.GetKey(Key.S)) direction.y += 1;
			if (Input.GetKey(Key.A)) direction.x -= 1;
			if (Input.GetKey(Key.D)) direction.x += 1;
			
			player.rotation = Vector2.Rad2Deg(Atan2(Input.mouseY - player.y, Input.mouseX - player.x));

			direction.Normalize();
			if (direction.LengthSquared() != 0 || player.rotation != oldRotation) MovePlayer(direction * 5);
		}

		private void MovePlayer(Vector2 displacement)
		{
			Vector2 newPosition = new Vector2(player.x, player.y) + displacement;
			player.SetXY(newPosition.x, newPosition.y);

			foreach ((RayDrawn ray, float angleOffset) in rays)
			{
				Vector2 rotationVec = Vector2.GetUnitVectorDeg(player.rotation + angleOffset);
				ray.Origin = playerPos + playerRadius * rotationVec;
				ray.Direction = rotationVec;
			}
		}
	}
}
