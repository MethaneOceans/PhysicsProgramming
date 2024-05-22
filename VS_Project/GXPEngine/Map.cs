using System;
using static GXPEngine.Assets.Assets;

namespace GXPEngine
{
	internal class Map : GameObject
	{
		AnimationSprite mapSprite;
		Tiles[,] mapTiles;

		public Map(int width, int height, int desiredTileSize = 64)
		{
			string tilesheetPath = GetTexturePath("Dungeon_Tileset.png");
			mapTiles = new Tiles[width, height];
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					if ((x + y) % 2 == 0) mapTiles[x, y] = Tiles.BasicVoid;
					else mapTiles[x, y] = Tiles.BasicFloor;

					// Create and place animationsprites with frames set tile type
					AnimationSprite tile = new AnimationSprite(tilesheetPath, 10, 10);
					tile.SetFrame((int)mapTiles[x, y]);
					float scaleFactor = desiredTileSize / (float)tile.width;
					tile.scale = scaleFactor;
					
					tile.x = x * tile.width;
					tile.y = y * tile.height;

					AddChild(tile);
				}
			}
		}

		enum Tiles
		{
			BasicVoid = 78,
			BasicFloor = 79,
		}
	}
}
