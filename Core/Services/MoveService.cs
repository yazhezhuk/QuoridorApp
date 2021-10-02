using System;
using System.Collections.Generic;
using System.Linq;
using Core.Game.Objects;
using Core.Game.Objects.Actions;
using Core.Game.Objects.Moves;
using Core.Game.Types;
using Core.Interfaces.GameServices;

namespace Core.Services
{
	public class MoveService : IMoveService
	{
		private readonly DisplacementService _displacementService;
		private readonly GameSessionService _gameSessionService;
		private readonly CellService _cellService;
		private readonly WallService _wallService;



		public MoveService(GameSessionService gameSessionService,
			CellService fieldService,
			DisplacementService displacementService,
			WallService wallService)
		{
			_displacementService = displacementService;
			_gameSessionService = gameSessionService;
			_cellService = fieldService;
			_wallService = wallService;
		}

		public bool ValidatePlayerFigureMove(PlayerFigureMove move)
		{
			_displacementService.GetFiguresBlockedDisplacements()
				.Select(disp => disp.ToString())
				.ToList()
				.ForEach(Console.WriteLine);
			Console.WriteLine(move.PositionDisplacement);

			return ValidateMoveToCell(_cellService.GetCellByCoordinates(move.PositionDisplacement.FromCoordinates),
				_cellService.GetCellByCoordinates(move.PositionDisplacement.NewCoordinates));
		}



		public bool TryPlaceWall(int wallId, int line, int offset, WallDirection direction)
		{
			if (line > 7 || offset > 7)
			{
				return false;
			}
			bool canPlace = true;
			Wall wallObject = direction == WallDirection.Horizontal
				? new HorizontalWall { Id = wallId, Line = line, OffsetFromEdge = offset }
				: new VerticalWall { Id = wallId, Line = line, OffsetFromEdge = offset };

			_wallService.AddWall(wallObject);

			foreach (var wall in _wallService.GetAllWalls().Where(wall => wall.Id != wallId))
			{
				canPlace &= !wallObject.Intersects(wall);
			}

			if (canPlace && ValidatePlayerCanReachEnd())
			{
				return true;
			}

			_wallService.RemoveWall(wallObject);
			return false;
		}




		public bool ValidateNonTrivialMove(Cell sourceCell, Cell targetCell) {
			var opponentFigureCell = _cellService
				.GetCellNeighbours(sourceCell)
				.Find(cell => cell.HasPlayerFigure() && cell.StandingPlayer != sourceCell.StandingPlayer);

			var expectedMoveDirection = PlayerFigureDisplacement.GetMoveDirection
				(sourceCell.Coordinates, opponentFigureCell.Coordinates);

			var cellNextToOpponentFigure =  _cellService.GetNextCellInDirection(opponentFigureCell,expectedMoveDirection);

			var displacementFromSourceToTarget = new PlayerFigureDisplacement(sourceCell, targetCell);
			var displacementFromOpponentFigureToAdjacentCell =
				new PlayerFigureDisplacement(opponentFigureCell, cellNextToOpponentFigure);


			if (_displacementService.IsDisplacementBlocked(displacementFromSourceToTarget)) //if we even unable move to figure
				return false;

			return  (sourceCell.GetDistanceToCell(targetCell) == 1 || opponentFigureCell.GetDistanceToCell
			(targetCell) == 1) &&
				!_displacementService.IsDisplacementBlocked(displacementFromOpponentFigureToAdjacentCell) ||
			       _displacementService.IsDisplacementBlocked(displacementFromSourceToTarget);


		}

		public bool ValidatePlayerCanReachEnd()
		{
			var playerFigure = _cellService.GetCellWithPlayerFigure(
				_gameSessionService.GetCurrentPlayer().Id);

			var visitedCells = new HashSet<Cell>{playerFigure};
			DepthFirstSearch(playerFigure, visitedCells);
			Console.WriteLine("Visited cells:");
			foreach (Cell visitedCell in visitedCells)
			{
				Console.WriteLine(visitedCell.Coordinates);
			}
			var moveIsValid = false;
			foreach (var visitedCell in visitedCells)
			{
				moveIsValid |= _gameSessionService.ValidateVictory(visitedCell); // if we can validate win for one visited cell then path exists
			}

			if (moveIsValid)
			{
				Console.WriteLine("This move is Valid");
			}
			return moveIsValid;
		}

		private void DepthFirstSearch(Cell cell, HashSet<Cell> visitedCells)
		{
			var validUnvisitedNeighbours =
				_displacementService.GetNotBlockedCellNeighbours(cell)
					.ToList();
			if (validUnvisitedNeighbours.Count == 0)
			{
				return;
			}
			foreach (var neighbourCell in validUnvisitedNeighbours)
			{
				if (visitedCells.Contains(neighbourCell))
					continue;
				visitedCells.Add(neighbourCell);
				DepthFirstSearch(neighbourCell, visitedCells);
			}
		}

		public bool ValidateLinearMove(Cell sourceCell, Cell targetCell) =>
			!targetCell.HasPlayerFigure() &&
			_cellService.CellIsNonDiagonalNeighbour(sourceCell,targetCell) &&
			!_displacementService.IsDisplacementBlocked(new PlayerFigureDisplacement(sourceCell,targetCell));

		public bool ValidateMoveToCell(Cell sourceCell,Cell targetCell)
		{
			return _cellService.EnemyFigureIsNear(sourceCell)
			       && ((PlayerFigureDisplacement.GetMoveDirection(sourceCell.Coordinates,targetCell.Coordinates) == MoveDirection.Diagonal
			            && _cellService.GetCellNeighbours(sourceCell).Contains(targetCell))
			           || (PlayerFigureDisplacement.GetMoveDirection(sourceCell.Coordinates,targetCell.Coordinates) != MoveDirection.Diagonal
			               && !_cellService.GetCellNeighbours(sourceCell).Contains(targetCell)))
				? ValidateNonTrivialMove(sourceCell, targetCell)
				: ValidateLinearMove(sourceCell, targetCell);
		}

	}
}
