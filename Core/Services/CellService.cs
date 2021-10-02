using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Core.Game.Objects;
using Core.Game.Objects.Moves;
using Core.Game.Types;
using Core.GameStates;
using Core.Interfaces.Game.Objects;
using Core.Interfaces.GameServices;
using PlayerFigureDisplacement = Core.Game.Objects.Actions.PlayerFigureDisplacement;

namespace Core.Services
{
	public class CellService
	{
		private readonly GameSession _gameSession;
		
		public CellService(GameSession gameSession)
		{
			_gameSession = gameSession;
		}

		public List<Cell> GetAllCells() => _gameSession.GameField.Cells;
		public Cell[,] GetAllCellsAsArray() => _gameSession.GameField.CellsAsArray;

		public bool CellIsNonDiagonalNeighbour(Cell sourceCell, Cell neighbour) =>
			GetNonDiagonalCellNeighbours(sourceCell).Contains(neighbour);

		public List<Cell> GetNonDiagonalCellNeighbours(Cell cell) =>
			_gameSession.GameField.Cells.FindAll(neighbourCell =>
				neighbourCell.Coordinates.IsNeighbourInColumnWith(cell.Coordinates)
				|| neighbourCell.Coordinates.IsNeighbourInRowWith(cell.Coordinates));

		public List<Cell> GetCellNeighbours(Cell cell) =>
			_gameSession.GameField.Cells.FindAll(neighbourCell =>
				neighbourCell.Coordinates.IsNeighbourInColumnWith(cell.Coordinates)
				|| neighbourCell.Coordinates.IsNeighbourInRowWith(cell.Coordinates)
				|| neighbourCell.Coordinates.IsNeighbourInDiagonalWith(cell.Coordinates));

		public Cell GetCellWithPlayerFigure(int playerId) =>
			_gameSession.GameField.Cells.Find(cell => cell.StandingPlayer?.Id == playerId);

		public List<Cell> GetCellsAdjacentToWall(Wall wall) =>
			wall.GetAdjacentCoordinates()
				.Select(GetCellByCoordinates)
				.ToList();

		public Cell GetCellByCoordinates(Coordinates coordinates) =>
			_gameSession.GameField.Cells.First(cell => cell.Coordinates.Equals(coordinates));

		public Cell GetNextCellInDirection(Cell cell, MoveDirection direction) =>
			GetNonDiagonalCellNeighbours(cell)
				.Where(cells => cells.Coordinates != cell.Coordinates)
				.ToArray()[(int)direction];

		public bool EnemyFigureIsNear(Cell sourceCell)
		{
			return GetCellNeighbours(sourceCell)
				.Find(cell => cell.HasPlayerFigure() && cell.Coordinates != sourceCell.Coordinates) != null;
		}




	}
}
