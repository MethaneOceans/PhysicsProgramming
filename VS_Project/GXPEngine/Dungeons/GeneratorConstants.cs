using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.Dungeons
{
	internal static class GeneratorConstants
	{
		public const int MIN_ROOM_SIZE = 7;
		public const int MIN_AREA_SIZE = MIN_ROOM_SIZE + 2;
		public const int MIN_AREA_SIZE_TO_SPLIT = MIN_AREA_SIZE * 2 + 1; 
	}
}
