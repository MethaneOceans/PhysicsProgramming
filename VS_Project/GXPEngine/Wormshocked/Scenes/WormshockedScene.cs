using GXPEngine.Control;
using GXPEngine.Physics;
using GXPEngine.Wormshocked.Objects;
using System.Collections.Generic;
using System.Drawing;
using static GXPEngine.Mathf;

namespace GXPEngine.Scenes
{
	// Needed a title and the idea of the game is like what worms and shellshocked have done.
	internal class WormshockedScene : Scene
	{
		private const int MAX_MOVEMENT_PER_TURN = 100;

		//protected List<PhysicsObject> physicsObjects;
		protected PhysicsManager physicsManager;
		public IReadOnlyList<ACollider> Colliders => physicsManager.Objects;

		List<PlayerCharacter> playerCharacters;
		PlayerCharacter currentPlayer;
		float movementLeft;
		int currentCharIndex = 0;

		// TODO: Add UI showing movement left and current player
		EasyDraw movementTracker;

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

			// Add a list of player characters
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
			NextPlayer();

			movementTracker = new EasyDraw(1, 75);
			movementTracker.Clear(Color.Yellow);
			movementTracker.Position = new Vector2(25, 25);
			AddChild(movementTracker);
		}

		public void Update()
		{
			// Control handling
			if (currentPlayer.HandleControls(ref movementLeft)) NextPlayer();

			// Movement bar scaling
			float normalizedMovementLeft = movementLeft / MAX_MOVEMENT_PER_TURN;
			float moveTrackerScale = Clamp((Width - 50) * normalizedMovementLeft, 0, Width);
			movementTracker.scaleX = moveTrackerScale;

			physicsManager.Step();
		}

		// Advance the turn
		private void NextPlayer()
		{
			currentCharIndex++;
			currentCharIndex %= playerCharacters.Count;
			currentPlayer = playerCharacters[currentCharIndex];
			movementLeft = MAX_MOVEMENT_PER_TURN;
		}

		public void AddPhysicsObject(PhysicsObject physicsObject)
		{
			physicsManager.Add(physicsObject);
			AddChild(physicsObject);
		}
	}
}
