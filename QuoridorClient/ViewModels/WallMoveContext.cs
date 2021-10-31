using System.Collections.Generic;
using Core.Game.Objects;
using QuoridorClient.Model;

namespace QuoridorClient
{
	public class WallMoveContext
	{
		public Coordinates current { get; set; }
		public Coordinates opponent { get; set; }

		public int turn { get; set; }

		public WallViewModel toSetup { get; set; }
		public List<WallViewModel> otherWalls { get; set; }
	}
}
