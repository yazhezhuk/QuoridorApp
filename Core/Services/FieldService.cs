using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Core.Game.Objects;
using Core.Game.Objects.Moves;
using Core.Game.Types;
using Core.Interfaces.Game.Objects;
using Core.Interfaces.GameServices;
using PlayerFigureDisplacement = Core.Game.Objects.Actions.PlayerFigureDisplacement;

namespace Core.Services
{
	public class FieldService : IFieldService //TODO refactor this shite
	{
		private readonly Field _gameField;
		
		public FieldService(Field gameField)
		{
			_gameField = gameField;
		}

		public IEnumerable<Cell> GetAllCells() => _gameField.Cells.AsReadOnly();

		#region Displacements
		public IReadOnlySet<PlayerFigureDisplacement> GetAllBlockedDisplacements()
		{
			var blockedDisplacements = new SortedSet<PlayerFigureDisplacement>();

			foreach (Wall wall in _gameField.Walls.Where(wall => wall.WallDirection != WallDirection.None))
			{
				foreach (PlayerFigureDisplacement displacement in DisplacementsBlockedByWall(wall))
				{
					blockedDisplacements.Add(displacement);
				}
			}
			return blockedDisplacements;
		}

		public bool IsDisplacementBlocked(PlayerFigureDisplacement displacement) =>
			GetAllBlockedDisplacements().Contains(displacement);

		private PlayerFigureDisplacement GetBlockedDisplacementByCells(Tuple<Cell, Cell> cells)
			=> new PlayerFigureDisplacement(cells.Item1.Coordinates, cells.Item2.Coordinates);
		#endregion


		#region CellRelatedStuff

		public List<Cell> GetAllCellNeighbours(Cell cell) =>
			_gameField.Cells.FindAll(neighbourCell =>
				cell.Coordinates.IsNeighbourInDiagonalWith(neighbourCell.Coordinates) ||
				cell.Coordinates.IsNeighbourInColumnWith(neighbourCell.Coordinates) ||
				cell.Coordinates.IsNeighbourInRowWith(neighbourCell.Coordinates));

		public List<Cell> GetNonDiagonalCellNeighbours(Cell cell) =>
			_gameField.Cells.FindAll(neighbourCell =>
				neighbourCell.Coordinates.IsNeighbourInColumnWith(cell.Coordinates)
				|| neighbourCell.Coordinates.IsNeighbourInRowWith(cell.Coordinates));

		public List<Cell> GetCellNeighbours(Cell cell) =>
			_gameField.Cells.FindAll(neighbourCell =>
				neighbourCell.Coordinates.IsNeighbourInColumnWith(cell.Coordinates)
				|| neighbourCell.Coordinates.IsNeighbourInRowWith(cell.Coordinates)
				|| neighbourCell.Coordinates.IsNeighbourInDiagonalWith(cell.Coordinates));

		public Cell GetCellByPosition(Coordinates position) =>
			_gameField.Cells.First(cell => cell.Coordinates.Equals(position));

		public Cell GetNextCellInDirection(Cell cell, MoveDirection direction) =>
			GetNonDiagonalCellNeighbours(cell)
				.Where(cells => cells.Coordinates != cell.Coordinates)
				.ToArray()[(int)direction];

		#endregion


		


		private List<Cell> GetCellsAdjacentToWall(Wall wall)
		{
			var cells = new List<Cell>();
			switch (wall.WallDirection)
			{
				case WallDirection.Horizontal:
					cells.AddRange(_gameField.Cells
						.FindAll(cell =>
							(cell.Coordinates.IsNeighbourInColumnWith(wall.BeginCoordinates) ||
							 cell.Coordinates.IsNeighbourInColumnWith(wall.EndCoordinates)) &&
							cell.Coordinates.Y - wall.BeginCoordinates.Y >= 0));
					break;
				case WallDirection.Vertical:
					cells.AddRange(_gameField.Cells
						.FindAll(cell =>
							(cell.Coordinates.IsNeighbourInRowWith(wall.BeginCoordinates) ||
							cell.Coordinates.IsNeighbourInRowWith(wall.EndCoordinates)) &&
							cell.Coordinates.X - wall.BeginCoordinates.X <= 0));
					break;
				case WallDirection.None:
					break;
			}
			return cells;
		}



		public bool ValidateDiagonalMove(Cell sourceCell, Cell targetCell) {
			var opponentPlayerFigure = GetCellNeighbours(sourceCell)
				.Find(cell => cell.HasPlayerFigure() && cell.StandingPlayer != sourceCell.StandingPlayer);
			if (opponentPlayerFigure == null)
				return false;
			var moveDirection = PlayerFigureDisplacement.GetMoveDirection(sourceCell.Coordinates,
					opponentPlayerFigure.Coordinates);

			var s1 = !GetAllBlockedDisplacements().Contains(new PlayerFigureDisplacement(sourceCell, targetCell));
			var s2 = GetAllBlockedDisplacements().Contains(
				new PlayerFigureDisplacement(opponentPlayerFigure, GetNextCellInDirection(opponentPlayerFigure,moveDirection)));


			return GetCellNeighbours(targetCell).Count(cell => cell.HasPlayerFigure()) == 2 &&
				       !GetAllBlockedDisplacements().Contains(new PlayerFigureDisplacement(sourceCell, targetCell)) &&
				       GetAllBlockedDisplacements().Contains(
					       new PlayerFigureDisplacement(opponentPlayerFigure, GetNextCellInDirection(opponentPlayerFigure,moveDirection)));

		}

		public bool ValidateLinearMove(Cell sourceCell, Cell targetCell)
		{

			if (GetNonDiagonalCellNeighbours(sourceCell).Contains(targetCell)) // casual move
				return true;
			var opponentPlayerFigure = GetNonDiagonalCellNeighbours(sourceCell)
				.Find(cell => cell.HasPlayerFigure() && cell.StandingPlayer != sourceCell.StandingPlayer);
			if (opponentPlayerFigure != null && !GetNonDiagonalCellNeighbours(targetCell).Contains(opponentPlayerFigure))
				return false;

			if (opponentPlayerFigure != null)
			{
				var nextCell = GetNextCellInDirection(opponentPlayerFigure,
					PlayerFigureDisplacement.GetMoveDirection(sourceCell.Coordinates,
						opponentPlayerFigure.Coordinates));

				var s1 = !GetAllBlockedDisplacements().Contains(new
					PlayerFigureDisplacement(opponentPlayerFigure.Coordinates, targetCell.Coordinates));

				return	nextCell.Equals(targetCell) && !GetAllBlockedDisplacements().Contains(new
				PlayerFigureDisplacement(opponentPlayerFigure.Coordinates,targetCell.Coordinates)) ;
			}
			return false;


		}


		public bool ValidateMoveToCell(Cell sourceCell,Cell targetCell)
		{
			return sourceCell.Coordinates.IsNeighbourInDiagonalWith(targetCell.Coordinates)
				? ValidateDiagonalMove(sourceCell, targetCell)
				: ValidateLinearMove(sourceCell, targetCell);

		}


		public IList<PlayerFigureDisplacement> DisplacementsBlockedByWall(Wall wall)
		{
			IList<PlayerFigureDisplacement> blockedDisplacements =
				new Collection<PlayerFigureDisplacement>();

			var adjacentCells = GetCellsAdjacentToWall(wall).ToList();

			//vertical/horizontal
			if (wall.WallDirection == WallDirection.Vertical)
			{
				blockedDisplacements.Add(
					GetBlockedDisplacementByCells(new Tuple<Cell, Cell>(adjacentCells[0], adjacentCells[1])));
				blockedDisplacements.Add(
					GetBlockedDisplacementByCells(new Tuple<Cell, Cell>(adjacentCells[1], adjacentCells[0])));

				blockedDisplacements.Add(
					GetBlockedDisplacementByCells(new Tuple<Cell, Cell>(adjacentCells[2], adjacentCells[3])));
				blockedDisplacements.Add(
					GetBlockedDisplacementByCells(new Tuple<Cell, Cell>(adjacentCells[3], adjacentCells[2])));
				//diagonal
				blockedDisplacements.Add(
					GetBlockedDisplacementByCells(new Tuple<Cell, Cell>(adjacentCells[0], adjacentCells[3])));
				blockedDisplacements.Add(
					GetBlockedDisplacementByCells(new Tuple<Cell, Cell>(adjacentCells[1], adjacentCells[2])));
				blockedDisplacements.Add(
					GetBlockedDisplacementByCells(new Tuple<Cell, Cell>(adjacentCells[3], adjacentCells[0])));
				blockedDisplacements.Add(
					GetBlockedDisplacementByCells(new Tuple<Cell, Cell>(adjacentCells[2], adjacentCells[1])));
			}
			else
			{
				blockedDisplacements.Add(
					GetBlockedDisplacementByCells(new Tuple<Cell, Cell>(adjacentCells[0], adjacentCells[2])));
				blockedDisplacements.Add(
					GetBlockedDisplacementByCells(new Tuple<Cell, Cell>(adjacentCells[2], adjacentCells[0])));

				blockedDisplacements.Add(
					GetBlockedDisplacementByCells(new Tuple<Cell, Cell>(adjacentCells[1], adjacentCells[3])));
				blockedDisplacements.Add(
					GetBlockedDisplacementByCells(new Tuple<Cell, Cell>(adjacentCells[3], adjacentCells[1])));
				//diagonal
				blockedDisplacements.Add(
					GetBlockedDisplacementByCells(new Tuple<Cell, Cell>(adjacentCells[0], adjacentCells[3])));
				blockedDisplacements.Add(
					GetBlockedDisplacementByCells(new Tuple<Cell, Cell>(adjacentCells[1], adjacentCells[2])));
				blockedDisplacements.Add(
					GetBlockedDisplacementByCells(new Tuple<Cell, Cell>(adjacentCells[3], adjacentCells[0])));
				blockedDisplacements.Add(
					GetBlockedDisplacementByCells(new Tuple<Cell, Cell>(adjacentCells[2], adjacentCells[1])));
			}

			return blockedDisplacements;
		}
	}
}