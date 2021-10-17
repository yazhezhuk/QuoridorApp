using System;
using System.Collections.Generic;
using System.Linq;
using Core.Game.GameStates;
using Core.Game.Objects;
using Core.Game.Objects.Actions;
using Core.Game.Types;
using System.Text.Json;
using Core.Interfaces.GameServices;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuoridorClient.Model;
using SQLitePCL;

namespace QuoridorClient.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class MoveController : ControllerBase
	{
		private readonly GameSessionService _gameSessionService;
		private readonly GameSession _gameSession;
		private readonly CellService _cellService;
		private readonly WallService _wallService;
		private readonly MoveService _moveService;
		private readonly ILogger<string> _logger;

		public MoveController(ILogger<string> logger)
		{
			var gameField = new Field(new List<Wall>());
			_gameSession = new GameSession(gameField);
			_gameSessionService = new GameSessionService(_gameSession);
			_cellService = new CellService(_gameSession,_gameSessionService);
			_wallService = new WallService(_gameSession);
			_moveService = new MoveService(_gameSessionService, _cellService,_wallService);
			_logger = logger;
		}

		[HttpPost]
		[Route("")]
		public string TryFigure([FromBody]FigureMoveContext context)
		{
			_logger.Log(LogLevel.Critical,"Im alive");
			_gameSession.GameField.Walls = context
				.walls
				.Select<WallViewModel,Wall>(wall =>
					wall.direction == 0
						? new HorizontalWall { X = wall.position.Y, Y = wall.position.X }
						: new VerticalWall{X = wall.position.X,Y = wall.position.Y})
				.ToList();
			_gameSession.Turn = context.turn;
			_gameSession.CurrentPlayer = _gameSessionService.GetCurrentPlayer();

			_gameSession.GameField.SetFigure(_gameSessionService.GetCurrentPlayer(), context.cellFrom);
			_gameSession.GameField.SetFigure(_gameSessionService.GetOpponentPlayer(), context.opponent);

			var playerCellFrom = _cellService.GetCellWithPlayerFigure(_gameSession.CurrentPlayer);
			var cellTo = _cellService.GetByCoordinates(context.cellTo);

			var move = new PlayerFigureMove(playerCellFrom, cellTo);
			var result = _moveService.TryExecuteMove(move);
			return JsonSerializer.Serialize(result);
		}

		[HttpPost]
		[Route("wall")]
		public string TryWall([FromBody] WallMoveContext context)
		{
			_logger.Log(LogLevel.Critical, "Im wall alive");
			_gameSession.GameField.Walls = _gameSession.GameField.Walls = context
				.otherWalls
				.Select<WallViewModel,Wall>(wall =>
					wall.direction == 1
						? new HorizontalWall { X = wall.position.Y, Y = wall.position.X }
						: new VerticalWall{X = wall.position.Y,Y = wall.position.X})
				.ToList();

		_gameSession.Turn = context.turn;
			_gameSession.CurrentPlayer = _gameSessionService.GetCurrentPlayer();

			_gameSession.GameField.SetFigure(_gameSessionService.GetCurrentPlayer(), context.current);
			_gameSession.GameField.SetFigure(_gameSessionService.GetOpponentPlayer(), context.opponent);

			var result = _moveService.TryPlaceWall(new Wall{ X =context.toSetup.position.X, Y = context.toSetup.position.Y },
			(Direction)context.toSetup.direction,_gameSession.GameField.Walls);
			return JsonSerializer.Serialize(result);
		}
	}
}
