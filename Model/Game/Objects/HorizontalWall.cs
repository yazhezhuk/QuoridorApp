using System.Collections.Generic;
using Core.Game.Objects.Actions;

namespace Core.Game.Objects
{
	public class HorizontalWall : Wall
	{
		public override Cell BeginCell => new Cell(X,Y);
		public override Cell EndCell => new Cell(X + 1, Y);
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
