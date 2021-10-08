using System.Collections.Generic;
using Core.Game.Objects;
using Core.Game.Types;

namespace Core.Interfaces.GameServices
{
	public interface ICellService
	{
		List<Cell> GetNonDiagonalCellNeighbours(Cell cell);

		List<Cell> GetCellNeighbours(Cell cell);

		Cell GetCellWithPlayerFigure(Player player);

		List<Cell> GetCellsAdjacentToWall(Wall wall);

		List<Cell> GetNonDiagonalAdjacentCells(Cell cell);

		bool EnemyFigureIsNear(Cell sourceCell);
	}
}
