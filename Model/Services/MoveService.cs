using System;
using System.Collections.Generic;
using System.Linq;
using Core.Game.Objects;
using Core.Game.Objects.Actions;
using Core.Game.Types;
using Core.Interfaces.GameServices;

namespace Core.Services
{
	public class MoveService : IMoveService
	{
		private readonly GameSessionService _gameSessionService;
		private readonly CellService _cellService;
		private readonly WallService _wallService;

		public MoveService(GameSessionService gameSessionService,
			CellService fieldService,
			WallService wallService)
		{
			_gameSessionService = gameSessionService;
			_cellService = fieldService;
			_wallService = wallService;
		}

		public bool ValidatePlayerFigureMove(PlayerFigureMove move)
		{
			GetFiguresBlockedDisplacements()
				.Select(disp => disp.ToString())
				.ToList()
				.ForEach(Console.WriteLine);
			Console.WriteLine(move);

			return CanExecuteMove(move) && !MoveIsBlocked(move);
		}

		public ISet<PlayerFigureMove> GetFiguresBlockedDisplacements()
		{
			var displacements = new HashSet<PlayerFigureMove>();
			foreach (var wall in _wallService.GetAllWalls()) // moves blocked by walls
			{
				foreach (var playerFigureDisplacement in wall.GetBlockedDisplacements())
					displacements.Add(playerFigureDisplacement);
			}

			return displacements;
		}

		public List<Cell> GetNotBlockedCellNeighbours(Cell cell) =>
			_cellService.GetNonDiagonalCellNeighbours(cell)
				.Where(neighbourCell => !MoveIsBlocked(new PlayerFigureMove(cell,neighbourCell)))
				.ToList();


		public bool MoveIsBlocked(PlayerFigureMove move) =>
			GetFiguresBlockedDisplacements().Contains(move);


		public bool TryPlaceWall(int wallId, int line, int offset, Direction direction)
		{
			if (line > 6 || offset > 7)
			{
				return false;
			}
			bool canPlace = true;
			Wall wallObject = direction == Direction.Horizontal
				? new HorizontalWall { Id = wallId, Line = line, OffsetFromEdge = offset }
				: new VerticalWall { Id = wallId, Line = line, OffsetFromEdge = offset };

			_wallService.AddWall(wallObject);

			foreach (var wall in _wallService.GetAllWalls().Where(wall => wall.Id != wallId))
			{
				canPlace &= !wallObject.Intersects(wall);
			}

			if (canPlace && PlayerCanReachOppositeSide())
			{
				return true;
			}

			_wallService.RemoveWall(wallObject);
			return false;
		}

		private bool ValidateMoveThroughPlayer(Cell sourceCell, Cell targetCell)
		{
			var opponentFigureCell = _cellService.GetCellWithPlayerFigure(_gameSessionService.GetOpponentPlayer());

			if (sourceCell.GetDistanceToAnotherCell(targetCell) > 2)
				return false;

			var expectedTargetCell = _cellService.GetNonDiagonalAdjacentCells(opponentFigureCell)
				.First(cell => cell.StandingPlayer == Player.None && (
				               cell.IsSameRow(sourceCell) || cell.IsSameRow(sourceCell)));

			if (targetCell != expectedTargetCell && targetCell.GetDistanceToAnotherCell(expectedTargetCell) < 2)
			{
				return ValidateMoveThroughPlayer(sourceCell, expectedTargetCell);
			}

			var moveFromOpponentFigureToAdjacentCell = new PlayerFigureMove(opponentFigureCell, targetCell);
			var moveFromSourceToOpponentCell = new PlayerFigureMove(sourceCell, opponentFigureCell);

			return !MoveIsBlocked(moveFromOpponentFigureToAdjacentCell) &&
			       !MoveIsBlocked(moveFromSourceToOpponentCell);
		}

		public bool PlayerCanReachOppositeSide()
		{
			var playerFigure = _cellService.GetCellWithPlayerFigure(_gameSessionService.GetCurrentPlayer());
			var visitedCells = new HashSet<Cell>{ playerFigure };

			DepthFirstSearch(playerFigure, visitedCells);

			return visitedCells.Aggregate(false, (canMove, visitedCell) =>
				canMove | _gameSessionService.ValidateVictory(visitedCell));
		}

		private void DepthFirstSearch(Cell cell, HashSet<Cell> visitedCells)
		{
			var validUnvisitedNeighbours = GetNotBlockedCellNeighbours(cell);

			foreach (var neighbourCell in validUnvisitedNeighbours)
			{
				if (!CanExecuteMove(new PlayerFigureMove(cell, neighbourCell)) ||
				    visitedCells.Contains(neighbourCell))
					continue;

				visitedCells.Add(neighbourCell);
				DepthFirstSearch(neighbourCell, visitedCells);
			}
		}

		private bool ValidateLinearMove(Cell sourceCell, Cell targetCell)
		{
			var test= _cellService.CellIsNonDiagonalNeighbour(sourceCell, targetCell);
			return _cellService.CellIsNonDiagonalNeighbour(sourceCell, targetCell);
		}

		private bool ValidateDiagonalMove(Cell sourceCell, Cell targetCell) =>
			!ValidateMoveThroughPlayer(sourceCell, targetCell) &&
			_cellService.CellIsNonDiagonalNeighbour(sourceCell, targetCell) &&
            _cellService.GetNonDiagonalCellNeighbours(targetCell)
	            .Any(cell => cell.StandingPlayer == _gameSessionService.GetOpponentPlayer());


		public bool CanExecuteMove(PlayerFigureMove move) =>
		    !MoveIsBlocked(move) && (
			ValidateLinearMove(move.Source, move.Target) ||
			ValidateDiagonalMove(move.Source, move.Target) ||
			ValidateMoveThroughPlayer(move.Source, move.Target));


	}
}
