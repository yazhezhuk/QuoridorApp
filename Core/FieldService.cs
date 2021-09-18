using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Security;
using Core.Game.Objects;
using Core.Game.Objects.Actions;
using Core.Game.Types;
using Core.Interfaces.Game;
using Core.Interfaces.Game.Shared;
using Core.Interfaces.GameServices;

namespace Core
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

		public List<Cell> GetCellNeighbours(Cell cell) => 
			_gameField.Cells.FindAll(neighbourCell =>
				cell.Coordinates.IsNeighbourInDiagonalWith(neighbourCell.Coordinates) ||
				cell.Coordinates.IsNeighbourInColumnWith(neighbourCell.Coordinates) ||
				cell.Coordinates.IsNeighbourInRowWith(neighbourCell.Coordinates));

		public Cell GetRightHorizontalNeighbour(Cell cell) =>
			GetCellNeighbours(cell).Find(neighbour =>
				cell.Coordinates.IsNeighbourInRowWith(neighbour.Coordinates) &&
				neighbour.Coordinates.X - cell.Coordinates.X > 0);
		
		public Cell GetLowerVerticalNeighbour(Cell cell) => 
			GetCellNeighbours(cell).Find(neighbour =>
				cell.Coordinates.IsNeighbourInColumnWith(neighbour.Coordinates) &&
				neighbour.Coordinates.Y - cell.Coordinates.Y < 0);
		
		private bool InSameColumnOrNeighbour(Cell cell, Wall wall) =>
			cell.Coordinates.X == wall.Coordinates.X ||
			(cell.Coordinates.X == wall.Coordinates.X + 1 &&
			 wall.WallDirection == WallDirection.Horizontal);
		private bool InSameRowOrNeighbour(Cell cell, Wall wall) =>
			cell.Coordinates.IsNeighbourInRowWith(wall.Coordinates) ||
			//TODO write adequate neighbour search
		
		public IList<IPlayerFigureDisplacement> DisplacementsBlockedByWall(Wall wall)
		{
			IList<IPlayerFigureDisplacement> blockedDisplacements =
				new Collection<IPlayerFigureDisplacement>();
			foreach (Cell cell in _gameField.Cells)
			{
				//TODO find easy way to define wall placement
				if (InSameColumnOrNeighbour(cell, wall)) 
					blockedDisplacements.Add(new PlayerFigureDisplacement());
				else if (InSameRowOrNeighbour(cell, wall))
					blockedDisplacements.Add(new PlayerFigureDisplacement());
			}

			return blockedDisplacements;
		}
	}
}