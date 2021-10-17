using System.Collections.Generic;
using Core.Game.Objects.Actions;

namespace Core.Game.Objects
{
	public class VerticalWall : Wall
	{
		public override Cell BeginCell => new Cell(Y,X);
		public override Cell EndCell => new Cell(Y, X + 1);
		public override List<Cell> GetAdjacentCells() =>
			new List<Cell>
			{
				BeginCell,
				new Cell(BeginCell.Coordinates.X + 1, BeginCell.Coordinates.Y),
				EndCell,
				new Cell(BeginCell.Coordinates.X + 1, BeginCell.Coordinates.Y + 1)
			};
	}
}
