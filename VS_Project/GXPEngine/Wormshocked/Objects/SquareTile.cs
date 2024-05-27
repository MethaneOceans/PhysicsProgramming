﻿using GXPEngine.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.Wormshocked.Objects
{
	internal class SquareTile : PhysicsObject
	{
		private Sprite Sprite;
		public int Health;

		public SquareTile(Vector2 position, int health = 3)
		{
			body = new OBCollider(position, new Vector2(50, 50), 0, this);
			body.OnDestroy += (caller, args) => Destroy();

			EasyDraw ed = new EasyDraw(50, 50);
			ed.ShapeAlign(CenterMode.Min, CenterMode.Min);
			ed.Stroke(128);
			ed.Fill(200);
			ed.Rect(0, 0, 50, 50);
			Sprite = ed;
			AddChild(Sprite);
			Sprite.SetOrigin(Sprite.width / 2, Sprite.height / 2);
			Sprite.Position = new Vector2();

			Position = position;

			Health = health;
		}

		public void Update()
		{
			if (Health <= 0) body.ShouldRemove = true;
		}
	}
}
