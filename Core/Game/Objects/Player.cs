using System.Collections.Generic;
using Core.GameObjects;
using Core.Interfaces.Game;

namespace Core.Game.Objects
{
	public class Player : AbstractGameObject, IWithCoordinates
	{
		public string Name { get; set; }
		public int WallRemains { get; } = 10;

		public Cell CurrentCell { get; set; } 
		public List<Wall> PlacedWalls { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
	}
}