using System.Collections.Generic;
using Core.GameObjects;
using Core.Interfaces.Game;

namespace Core.Game.Objects
{
	public class PlayerFigure : AbstractGameObject, IWithCoordinates
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