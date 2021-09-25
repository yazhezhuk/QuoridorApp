using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Core.Extensions;
using Core.Game.Objects;
using Core.Game.Objects.Actions;
using Core.Game.Types;
using Core.Interfaces.Game;
using Core.Interfaces.Game.Objects;
using Core.Interfaces.Game.Shared;
using Core.Interfaces.GameServices;

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

		public bool IsCellIsBlocked(Cell cell)
		{
			throw new NotImplementedException();
		}

		public bool CanPlaceWall()
		{
			throw new System.NotImplementedException();
		}

		public IReadOnlySet<IPlayerFigureDisplacement> GetAllBlockedDisplacements()
		{
			var blockedDisplacements = new HashSet<IPlayerFigureDisplacement>();
			
			foreach (Wall wall in _gameField.Walls)
			{
				foreach (IPlayerFigureDisplacement displacement in DisplacementsBlockedByWall(wall))
				{
					blockedDisplacements.Add(displacement);
				}
			}
			return blockedDisplacements;
		}
		

		public List<Cell> GetCellNeighbours(Cell cell) => 
			_gameField.Cells.FindAll(neighbourCell =>
				cell.Coordinates.IsNeighbourInDiagonalWith(neighbourCell.Coordinates) ||
				cell.Coordinates.IsNeighbourInColumnWith(neighbourCell.Coordinates) ||
				cell.Coordinates.IsNeighbourInRowWith(neighbourCell.Coordinates));
		
		private IPlayerFigureDisplacement GetBlockedDisplacementByCells(Tuple<Cell, Cell> cells)
			=> new PlayerFigureDisplacement(cells.Item1.Coordinates, cells.Item2.Coordinates);

		private List<Cell> GetCellsAdjacentToWall(IWall wall)
		{
			var cells = new List<Cell>();
			switch (wall.WallDirection)
			{
				case WallDirection.Horizontal:
					cells.AddRange(_gameField.Cells
						.FindAll(cell =>
							Math.Abs(cell.Coordinates.Y - wall.BeginCoordinates.Y) <= 1 &&
							(cell.Coordinates.X == wall.BeginCoordinates.X || cell.Coordinates.X == wall.EndCoordinates.X-1))
					);
					break;
				case WallDirection.Vertical:
					cells.AddRange(_gameField.Cells
						.FindAll(cell =>
							Math.Abs(cell.Coordinates.X - wall.BeginCoordinates.X) <= 1 &&
							(cell.Coordinates.Y == wall.BeginCoordinates.Y || cell.Coordinates.Y == wall.EndCoordinates.Y-1)));
					break;
				case WallDirection.None:
					break;
			}
			
			return cells;
		}

		public IList<IPlayerFigureDisplacement> DisplacementsBlockedByWall(Wall wall)
		{
			IList<IPlayerFigureDisplacement> blockedDisplacements =
				
				new Collection<IPlayerFigureDisplacement>();
			var adjacentCells = GetCellsAdjacentToWall(wall)
				.Tupelize()
				.ToList();
			
			foreach (var cell in adjacentCells)
			{
				blockedDisplacements.Add(GetBlockedDisplacementByCells(cell));
			}

			return blockedDisplacements;
		}
	}
}