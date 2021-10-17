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

			return TryExecuteMove(move).CanExecute && !MoveIsBlocked(move);
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


		public ActionResult TryPlaceWall(Wall wall, Direction direction, List<Wall> otherWalls)
		{
			if (wall.X > 6 || wall.Y > 7)
			{
				return new ActionResult(false);
			}
			bool canPlace = true;
			Wall wallObject = direction == Direction.Horizontal
				? new HorizontalWall { X = wall.X, Y = wall.Y }
				: new VerticalWall { X = wall.X, Y  = wall.Y };

			foreach (var walls in otherWalls)
			{
				canPlace &= !wallObject.Intersects(walls);
			}
			_wallService.AddWall(wallObject);


			if (canPlace && PlayerCanReachOppositeSide())
			{
				return new ActionResult(true);
			}

			_wallService.RemoveWall(wallObject);
			return new ActionResult(false);
		}

		private bool ValidateMoveThroughPlayer(Cell sourceCell, Cell targetCell)
		{
			var opponentFigureCell = _cellService.GetCellWithPlayerFigure(_gameSessionService.GetOpponentPlayer());

			if (sourceCell.GetDistanceToAnotherCell(targetCell) > 2 ||
			    sourceCell.GetDistanceToAnotherCell(opponentFigureCell) > 1)
				return false;

			var expectedTargetCell = _cellService.GetNonDiagonalAdjacentCells(opponentFigureCell)
				.FirstOrDefault(cell => cell.StandingPlayer == Player.None && (
				               cell.IsSameRow(sourceCell) || cell.IsSameColumn(sourceCell)));
			if (expectedTargetCell == null) return false;
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
				if (!TryExecuteMove(new PlayerFigureMove(cell, neighbourCell)).CanExecute ||
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
			_cellService.IsDiagonalCellNeighbour(sourceCell, targetCell) &&
            _cellService.GetNonDiagonalCellNeighbours(targetCell)
	            .Any(cell => cell.StandingPlayer == _gameSessionService.GetOpponentPlayer());


		public ActionResult TryExecuteMove(PlayerFigureMove move)
		{
			var canMove =
				!MoveIsBlocked(move) &&
				(
					ValidateLinearMove(move.Source, move.Target) ||
					ValidateDiagonalMove(move.Source, move.Target) ||
					ValidateMoveThroughPlayer(move.Source, move.Target));

			var result = new ActionResult(canMove);

			return result;

		}
	}
}
