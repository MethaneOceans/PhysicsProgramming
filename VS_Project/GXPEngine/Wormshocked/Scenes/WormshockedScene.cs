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
		public IReadOnlyList<ACollider> Colliders => physicsManager.Objects;

		public WormshockedScene()
		{
			physicsManager = new PhysicsManager()
			{
				Gravity = new Vector2(0, 0.05)
			};

			int platformWidth = 64;
			int platformHeight = 8;

			for (int j = 0; j < platformHeight; j++)
			{
				for (int i = 0; i < platformWidth; i++)
				{
					int tileSize = 25;
					SquareTile sTile = new SquareTile(new Vector2(
						Width / 2 + (tileSize / 2) + (i - platformWidth / 2) * tileSize, 
						Height - (tileSize / 2) - j * tileSize), tileSize);
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
