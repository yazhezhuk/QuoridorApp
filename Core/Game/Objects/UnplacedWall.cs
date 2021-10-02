using System.Collections.Generic;

namespace Core.Game.Objects
{
	public class UnplacedWall : Wall
	{
		public UnplacedWall()
		{
			Line = -1;
			OffsetFromEdge = -1;
		}

		public override Coordinates BeginCoordinates { get; } = new Coordinates();
		public override Coordinates EndCoordinates { get; } = new Coordinates();
		public override List<Coordinates> GetAdjacentCoordinates()
		{
			return new List<Coordinates>();
		}
	}
}