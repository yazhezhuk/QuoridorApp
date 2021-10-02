using System.Collections.Generic;
using Core.Game.Objects.Actions;

namespace Core.Game.Objects
{
	public class VerticalWall : Wall
	{
		public override Coordinates BeginCoordinates => new Coordinates(Line,OffsetFromEdge);
		public override Coordinates EndCoordinates => new Coordinates(Line, OffsetFromEdge + 1);
		public override List<Coordinates> GetAdjacentCoordinates() =>
			new List<Coordinates>
			{
				BeginCoordinates,
				new Coordinates(BeginCoordinates.X + 1, BeginCoordinates.Y),
				EndCoordinates,
				new Coordinates(BeginCoordinates.X + 1, BeginCoordinates.Y + 1),
			};
	}
}
