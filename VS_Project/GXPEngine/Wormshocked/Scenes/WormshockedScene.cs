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

		List<PlayerCharacter> playerCharacters;
		PlayerCharacter currentPlayer;
		int currentCharIndex = 0;

		public WormshockedScene()
		{
			physicsManager = new PhysicsManager()
			{
				Gravity = new Vector2(0, 0.05)
			};

			int platformWidth = 64;
			int platformHeight = 8;
			int tileSize = 25;

			// Create floor out of tiles with specified size and count of tiles
			for (int j = 0; j < platformHeight; j++)
			{
				for (int i = 0; i < platformWidth; i++)
				{
					SquareTile sTile = new SquareTile(new Vector2(
						Width / 2 + (tileSize / 2) + (i - platformWidth / 2) * tileSize,
						Height - (tileSize / 2) - j * tileSize), tileSize);
					physicsManager.Add(sTile);
					AddChild(sTile);
				}
			}

			playerCharacters = new List<PlayerCharacter>
			{
				new PlayerCharacter(new Vector2(Width / 4, Height / 2)),
				new PlayerCharacter(new Vector2(Width - Width / 4, Height / 2)),
			};
			foreach (PlayerCharacter character in playerCharacters)
			{
				physicsManager.Add(character);
				AddChild(character);
			}
			currentPlayer = playerCharacters[currentCharIndex];
		}

		protected virtual void GenerateTerrain(int platformWidth = 32, int platformHeight = 4)
		{
			
		}

		public void Update()
		{
			currentPlayer.HandleControls();

			physicsManager.Step();
		}
	}
}
