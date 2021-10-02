using System.Collections.Generic;
using Core.Interfaces.Game;
using Core.Interfaces.Game.Shared;

namespace Core.Game.Objects
{
	public class PlayerFigure : GameObject, IWithCoordinates
	{
		public PlayerFigure(Coordinates coordinates)
		{
			Coordinates = coordinates;
		}
		
		public string Name { get; set; }
		public int WallRemains { get; } = 10;
		public Coordinates Coordinates { get; set; }
	}
}
