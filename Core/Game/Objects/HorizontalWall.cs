using System.Collections.Generic;
using Core.Game.Objects.Actions;

namespace Core.Game.Objects
{
	public class HorizontalWall : Wall
	{
		public override Coordinates BeginCoordinates => new Coordinates(OffsetFromEdge,Line);
		public override Coordinates EndCoordinates => new Coordinates(OffsetFromEdge + 1, Line);
		public override List<Coordinates> GetAdjacentCoordinates() =>
			new List<Coordinates>
			{
				BeginCoordinates,
				new Coordinates(BeginCoordinates.X, BeginCoordinates.Y + 1),
				EndCoordinates,
				new Coordinates(BeginCoordinates.X + 1, BeginCoordinates.Y + 1)
			};
	}
}