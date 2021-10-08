using System.Collections.Generic;
using Core.Game.Objects.Actions;

namespace Core.Game.Objects
{
	public class HorizontalWall : Wall
	{
		public override Cell BeginCell => new Cell(OffsetFromEdge,Line);
		public override Cell EndCell => new Cell(OffsetFromEdge + 1, Line);
		public override List<Cell> GetAdjacentCells() =>
			new List<Cell>
			{
				BeginCell,
				new Cell(BeginCell.Coordinates.X, BeginCell.Coordinates.Y + 1),
				EndCell,
				new Cell(BeginCell.Coordinates.X + 1, BeginCell.Coordinates.Y + 1)
			};
	}
}
