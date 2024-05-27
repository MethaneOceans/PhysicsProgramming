using GXPEngine.Control;
using GXPEngine.Physics;
using GXPEngine.Wormshocked.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.Scenes
{
	// Needed a title and the idea of the game is like what worms and shellshocked have done.
	internal class WormshockedScene : Scene
	{
		//protected List<PhysicsObject> physicsObjects;
		protected PhysicsManager physicsManager;

		public WormshockedScene()
		{
			physicsManager = new PhysicsManager()
			{
				Gravity = new Vector2(0, 0.1)
			};

			int platformWidth = 32;
			int platformHeight = 4;

			for (int j = 0; j < platformHeight; j++)
			{
				for (int i = 0; i < platformWidth; i++)
				{
					SquareTile sTile = new SquareTile(new Vector2(Width / 2 + 25 + (i - platformWidth / 2) * 50, Height - 25 - j * 50));
					physicsManager.Add(sTile);
					AddChild(sTile);
				}
			}
		}

		protected virtual void GenerateTerrain(int platformWidth = 32, int platformHeight = 4)
		{
			
		}

		public void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Console.WriteLine("Adding projectile");
				Projectile projectile = new Projectile(Input.mousePos, new Vector2(), 10);
				
				physicsManager.Add(projectile);
				AddChild(projectile);
			}

			physicsManager.Step();
		}
	}
}
