using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Core.Game.Objects;
using Core.Game.Types;
using Core.Interfaces.Game.Objects;
using Core.Interfaces.GameServices;
using PlayerFigureDisplacement = Core.Game.Objects.Actions.PlayerFigureDisplacement;

namespace Core.Services
{
	public class FieldService : IFieldService //TODO somehow separate cell logic
	{
		private readonly Field _gameField;
		
		public FieldService(Field gameField)
		{
			_gameField = gameField;
		}

		public IEnumerable<Cell> GetAllCells() => _gameField.Cells.AsReadOnly();


		public IReadOnlySet<PlayerFigureDisplacement> GetAllBlockedDisplacements()
		{
			var blockedDisplacements = new SortedSet<PlayerFigureDisplacement>();
			
			foreach (Wall wall in _gameField.Walls.Where(wall => wall.WallDirection != WallDirection.None))
			{
				foreach (PlayerFigureDisplacement displacement in DisplacementsBlockedByWall(wall))
				{
					blockedDisplacements.Add(displacement);
					blockedDisplacements.Add(new PlayerFigureDisplacement(displacement.NewCoordinates,displacement.FromCoordinates));
				}
			}
			return blockedDisplacements;
		}

		public List<Cell> GetAllCellNeighbours(Cell cell) =>
			_gameField.Cells.FindAll(neighbourCell =>
				cell.Coordinates.IsNeighbourInDiagonalWith(neighbourCell.Coordinates) ||
				cell.Coordinates.IsNeighbourInColumnWith(neighbourCell.Coordinates) ||
				cell.Coordinates.IsNeighbourInRowWith(neighbourCell.Coordinates));

		public List<Cell> GetNonDiagonalCellNeighbours(Cell cell) =>
			_gameField.Cells.FindAll(neighbourCell =>
				neighbourCell.Coordinates.IsNeighbourInColumnWith(cell.Coordinates)
				|| neighbourCell.Coordinates.IsNeighbourInRowWith(cell.Coordinates));
		
		private PlayerFigureDisplacement GetBlockedDisplacementByCells(Tuple<Cell, Cell> cells)
			=> new PlayerFigureDisplacement(cells.Item1.Coordinates, cells.Item2.Coordinates);

		private List<Cell> GetCellsAdjacentToWall(IWall wall)
		{
			var cells = new List<Cell>();
			switch (wall.WallDirection)
			{
				case WallDirection.Horizontal:
					cells.AddRange(_gameField.Cells
						.FindAll(cell =>
								 (cell.Coordinates.Y - wall.BeginCoordinates.Y == 1 || (cell.Coordinates.Y - wall.BeginCoordinates.Y == 0))&&
							(cell.Coordinates.X == wall.BeginCoordinates.X || cell.Coordinates.X == wall.EndCoordinates.X-1))
					);
					break;
				case WallDirection.Vertical:
					cells.AddRange(_gameField.Cells
						.FindAll(cell =>
							(cell.Coordinates.X - wall.BeginCoordinates.X == 1 || (cell.Coordinates.X - wall.BeginCoordinates.X == 0)) &&
							(cell.Coordinates.Y == wall.BeginCoordinates.Y || cell.Coordinates.Y == wall.EndCoordinates.Y-1)));
					break;
				case WallDirection.None:
					break;
			}
			
			return cells;
		}

		public Cell GetCellByPosition(Coordinates position) =>
			_gameField.Cells.First(cell => cell.Coordinates.Equals(position));


		public bool CanMoveToCell(Cell sourceCell,Cell targetCell)
		{

			var cellNeighbours = GetNonDiagonalCellNeighbours(sourceCell);


			cellNeighbours.Select(cll => cll.Coordinates.ToString()).ToList().ForEach(Console.WriteLine);

			return cellNeighbours.Contains(targetCell);
		}


		public IList<PlayerFigureDisplacement> DisplacementsBlockedByWall(Wall wall)
		{
			IList<PlayerFigureDisplacement> blockedDisplacements =
				new Collection<PlayerFigureDisplacement>();
			
			
			var adjacentCells = GetCellsAdjacentToWall(wall)
				.ToList();

			if (wall.WallDirection == WallDirection.Horizontal)
			{
				blockedDisplacements.Add(GetBlockedDisplacementByCells(new Tuple<Cell, Cell>(adjacentCells[0],adjacentCells[2])));
				blockedDisplacements.Add(GetBlockedDisplacementByCells(new Tuple<Cell, Cell>(adjacentCells[1],adjacentCells[3])));
			}
			else
			{
				blockedDisplacements.Add(GetBlockedDisplacementByCells(new Tuple<Cell, Cell>(adjacentCells[0],adjacentCells[1])));
				blockedDisplacements.Add(GetBlockedDisplacementByCells(new Tuple<Cell, Cell>(adjacentCells[2],adjacentCells[3])));
			}



			return blockedDisplacements;
		}
	}
}