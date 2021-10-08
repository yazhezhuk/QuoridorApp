using System.Collections.Generic;
using System.Linq;
using Core.Game.GameStates;
using Core.Game.Objects;
using Core.Game.Types;
using Core.Interfaces.GameServices;

namespace Core.Services
{
	public class CellService : ICellService
	{
		private readonly GameSession _gameSession;
		private readonly GameSessionService _gameSessionService;
		
		public CellService(GameSession gameSession,GameSessionService gameSessionService)
		{
			_gameSession = gameSession;
			_gameSessionService = gameSessionService;
		}

		public List<Cell> GetAllCells() => _gameSession.GameField.Cells;
		public Cell[,] GetAllCellsAsArray() => _gameSession.GameField.CellsAsArray;

		public bool CellIsNonDiagonalNeighbour(Cell sourceCell, Cell neighbour)
		{
			var rez = GetNonDiagonalCellNeighbours(sourceCell).Contains(neighbour);
			return rez;
		}

		public List<Cell> GetNonDiagonalCellNeighbours(Cell cell) =>
			_gameSession.GameField.Cells.FindAll(neighbourCell =>
				neighbourCell.IsNeighbourInColumnWith(cell)
				|| neighbourCell.IsNeighbourInRowWith(cell));

		public List<Cell> GetCellNeighbours(Cell cell) =>
			_gameSession.GameField.Cells.FindAll(neighbourCell =>
				neighbourCell.IsNeighbourInColumnWith(cell)
				|| neighbourCell.IsNeighbourInRowWith(cell)
				|| neighbourCell.IsNeighbourInDiagonalWith(cell));

		public Cell GetCellWithPlayerFigure(Player player) =>
			_gameSession.GameField.Cells.Find(cell => cell.StandingPlayer == player);

		public List<Cell> GetCellsAdjacentToWall(Wall wall) =>
			wall.GetAdjacentCells()
				.ToList();

		public List<Cell> GetNonDiagonalAdjacentCells(Cell cell) =>
			_gameSession.GameField.Cells
				.Where(c => c.IsNeighbourInColumnWith(cell) || c.IsNeighbourInRowWith(cell))
				.ToList();

		public bool EnemyFigureIsNear(Cell sourceCell)
		{
			return GetCellNeighbours(sourceCell)
				.Count(cell => cell.HasPlayerFigure() && cell != sourceCell) != 0;
		}




	}
}
